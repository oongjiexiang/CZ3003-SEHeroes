/**
 * Controller for story mode result related logic.
 * @module story-mode-result-controller
 * @category controller
 */

const admin = require('firebase-admin');
const db = admin.firestore();
const storyModeResultCollection = db.collection("storyModeResult")


/**
 * Create story mode result and store into the database. Result identify by (matricNo, world, section, level). 
 * Result will be updated if exists in database. 
 * Star will take the max of new result and previous result.
 * @param {Object} record - New story mode result details, including matricNo, world, section, level and star.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createOrUpdateStoryModeResult= async function(record, callback) {
    if (record['level'] == null|| record['section']== null || record['star']== null || record['matricNo']== null || record['world']== null) {
        callback('Missing fields', null)
        return
    }
    try{
        const world = record['world']
        const section = record['section']
        const level = record['level']
        const matricNo = record['matricNo']

        const result = await storyModeResultCollection.where('world', '==', world).where('section', '==', section)
                            .where('level', '==', level).where('matricNo', '==', matricNo).get();

        if (result.empty) {
            await storyModeResultCollection.add(record);
            callback(null, "Result added");
            return;
        }

        let id = result.docs[0].id;
        if(result.docs[0].data().star < record['star']) await storyModeResultCollection.doc(id).update({ 'star': record['star'] });
        callback(null, "Result updated");
        
    } catch(err) {
        callback(err, null)
    }
}


/**
 * Get all story mode results.
 * @param {Object} queryMap - Object of filter, field can include matricNo, world, section and level.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAllStoryModeResults= async function(queryMap, callback) {
    try{ 
        let results = storyModeResultCollection
        for (const key in queryMap) {
            results = results.where(key, "==", queryMap[key])
        }
        const snapshot = await results.get()
        if (snapshot.empty) {
            callback('No data', null)
        }
        else {
            var res = []
            const worldMap = {
                "Planning": 0,
                "Design": 1,
                "Implementation": 2,
                "Testing": 3,
                "Maintainance": 4
            }
            snapshot.forEach(doc => {
                const data = doc.data();
                data['storyModeResultId'] = doc.id;
                if(worldMap[data.world] != null) res.push(data)
            })

            res.sort((a, b) => worldMap[a.world] < worldMap[b.world] ? -1 : (worldMap[a.world] > worldMap[b.world] ? 1 : 0));

            callback(null, res)
            }
    } catch(err) {
        callback(err, null)
    }
}

module.exports.getStoryModeResult= async function(resultId, callback) {
    try{ 
        const result = await storyModeResultCollection.doc(resultId).get()
        if (!result.exists) {
            callback('No such result found', null)
        }
        else {
            const data = result.data();
            data['storyModeResultId'] = resultId;
            callback(null, data);
        }
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Delete an story mode result from database by resultId.
 * @param {String} resultId - ResultId of story mode result to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteStoryModeResult= async function(resultId, callback) {
    try{
        const res = await storyModeResultCollection.doc(resultId).delete()
        callback(null, "Delete successfully")
        
    } catch(err) {
        callback(err, null)
    }
}