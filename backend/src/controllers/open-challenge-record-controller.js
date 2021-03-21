const admin = require('firebase-admin');
const db = admin.firestore();
const openChallengeRecordCollection = db.collection("openChallengeRecord")

module.exports['createOpenChallengeRecord'] = async function(record, callback) {
    if (record['questions'] == null || record['team1'] == null || record['team2'] == null || record['type'] == null) {
        callback('Missing fields', null)
        return
    }
    try{
        const reply = await openChallengeRecordCollection.add(record)
        callback(null, reply.id)
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllOpenChallengeRecords'] = async function(callback) {
    try{ 
        const snapshot = await openChallengeRecordCollection.get()
        if (snapshot.empty) {
            callback('No data', null)
        }
        else {
            var res = []
            snapshot.forEach(doc => {
                const record = doc.data();
                record.openChallengeRecordId = doc.id
                res.push(record)
            })
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getOpenChallengeRecord'] = async function(recordId, callback) {
    try{ 
        const record = await openChallengeRecordCollection.doc(recordId).get()
        if (!record.exists) {
            callback('No such record found', null)
        }
        else {
            const data = record.data();
            data.openChallengeRecordId = recordId
            callback(null, data)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateOpenChallengeRecord'] = async function(recordId, updateFields, callback) {
    try{
        const record = await openChallengeRecordCollection.doc(recordId).get()
        if (!record.exists) {
            callback('No such record found', null)
        }
        else{
            const res = await openChallengeRecordCollection.doc(recordId).update(updateFields)
            callback(null, "Update successfully")
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteOpenChallengeRecord'] = async function(recordId, callback) {
    try{
        const res = await openChallengeRecordCollection.doc(recordId).delete()
        callback(null, "Delete successfully")
        
    } catch(err) {
        callback(err, null)
    }
}