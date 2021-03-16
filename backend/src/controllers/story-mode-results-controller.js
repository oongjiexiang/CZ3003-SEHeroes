const admin = require('firebase-admin');
const db = admin.firestore();

module.exports['createStoryModeResult'] = async function(record, callback) {
    try{
        if (!record['level'] || !record['section'] || !record['star'] || !record['userID'] || !record['world']) {
            return res.status(400).send({ message: 'Missing fields' })
        }
        const reply = await db.collection("storyModeResults").add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllStoryModeResults'] = async function(callback) {
    try{ 
        const snapshot = await db.collection('storyModeResults').get()
        if (snapshot.empty) {
            callback('No data', null)
        }
        else {
            var res = []
            snapshot.forEach(doc => {
                res.push(doc.data())
            })
            callback(null, res)
            }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getStoryModeResult'] = async function(resultId, callback) {
    try{ 
        const result = await db.collection('storyModeResults').doc(resultId).get()
        if (!result.exists) {
            callback('No such result found', null)
        }
        else {
            callback(null, result.data())
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateStoryModeResult'] = async function(resultId, updateFields, callback) {
    try{
        const result = await db.collection("storyModeResults").doc(resultId).get()
        if (!result.exists) {
            callback('No such result found', null)
        }
        else{
            const res = await db.collection("storyModeResults").doc(resultId).update(updateFields)
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteStoryModeResult'] = async function(resultId, callback) {
    try{
        const res = await db.collection("storyModeResults").doc(resultId).delete()
        callback(null, res)
        
    } catch(err) {
        callback(err, null)
    }
}