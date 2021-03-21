const admin = require('firebase-admin');
const db = admin.firestore();
const storyModeResultCollection = db.collection("storyModeResult")


module.exports['createOrUpdateStoryModeResult'] = async function(record, callback) {
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

        let id = assignmentResult.docs[0].id;
        await storyModeResultCollection.doc(id).update({ 'star': record['star'] });
        callback(null, "Result updated");
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllStoryModeResults'] = async function(queryMap, callback) {
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
            snapshot.forEach(doc => {
                const data = doc.data();
                doc['storyModeResultId'] = doc.id;
                res.push(data)
            })
            callback(null, res)
            }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getStoryModeResult'] = async function(resultId, callback) {
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

module.exports['deleteStoryModeResult'] = async function(resultId, callback) {
    try{
        const res = await storyModeResultCollection.doc(resultId).delete()
        callback(null, "Delete successfully")
        
    } catch(err) {
        callback(err, null)
    }
}