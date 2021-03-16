const admin = require('firebase-admin');
const db = admin.firestore();

module.exports['createOpenChallengeRecord'] = async function(record, callback) {
    try{
        if (!record['questions'] || !record['team1'] || !record['team2'] || !record['type']) {
            return res.status(400).send({ message: 'Missing fields' })
        }
        const reply = await db.collection("openChallengeRecord").add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllOpenChallengeRecords'] = async function(record, callback) {
    try{ 
        const snapshot = await db.collection('openChallengeRecord').get()
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

module.exports['getOpenChallengeRecord'] = async function(recordId, callback) {
    try{ 
        const record = await db.collection('openChallengeRecord').doc(recordId).get()
        if (!record.exists) {
            callback('No such record found', null)
        }
        else {
            callback(null, record.data())
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateOpenChallengeRecord'] = async function(recordId, updateFields, callback) {
    try{
        const record = await db.collection("openChallengeRecord").doc(recordId).get()
        if (!record.exists) {
            callback('No such record found', null)
        }
        else{
            const res = await db.collection("openChallengeRecord").doc(recordId).update(updateFields)
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteOpenChallengeRecord'] = async function(recordId, callback) {
    try{
        const res = await db.collection("openChallengeRecord").doc(recordId).delete()
        callback(null, res)
        
    } catch(err) {
        callback(err, null)
    }
}