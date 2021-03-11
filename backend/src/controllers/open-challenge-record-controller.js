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