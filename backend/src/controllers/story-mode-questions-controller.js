const admin = require('firebase-admin');
const db = admin.firestore();

module.exports['createStoryModeQuestion'] = async function(record, callback) {
    try{
        if (!record['answer'] || !record['correctAns'] || !record['image'] || !record['level'] || !record['question'] || !record['section'] || !record['world']) {
            return res.status(400).send({ message: 'Missing fields' })
        }
        const reply = await db.collection("storyModeQuestions").add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllStoryModeQuestions'] = async function(callback) {
    try{ 
        const snapshot = await db.collection('storyModeQuestions').get()
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

module.exports['getStoryModeQuestion'] = async function(questionId, callback) {
    try{ 
        const question = await db.collection('storyModeQuestions').doc(questionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else {
            callback(null, question.data())
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateStoryModeQuestion'] = async function(questionId, updateFields, callback) {
    try{
        const question = await db.collection("storyModeQuestions").doc(questionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else{
            const res = await db.collection("storyModeQuestions").doc(questionId).update(updateFields)
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteStoryModeQuestion'] = async function(questionId, callback) {
    try{
        const res = await db.collection("storyModeQuestions").doc(questionId).delete()
        callback(null, res)
        
    } catch(err) {
        callback(err, null)
    }
}