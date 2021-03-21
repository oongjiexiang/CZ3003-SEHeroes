const admin = require('firebase-admin');
const db = admin.firestore();
const storyModeQuestionCollection = db.collection("storyModeQuestion")

module.exports['createStoryModeQuestion'] = async function(record, callback) {

    if (record['answer'] == null || record['correctAnswer'] == null|| record['level'] == null
                || record['question']== null || record['section']== null || record['world'] == null) {
        callback('Missing fields', null)
        return
    }
    if(!record['image']) delete record['image']

    try{
        const reply = await storyModeQuestionCollection.add(record)
        callback(null, reply.id)
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllStoryModeQuestions'] = async function(queryMap, callback) {
    try{ 
        let questions = storyModeQuestionCollection
        for (const key in queryMap) {
            questions = questions.where(key, "==", queryMap[key])
        }
        const snapshot = await questions.get()
        if (snapshot.empty) {
            callback('No data', null)
        }
        else {
            const res = {}
            snapshot.forEach(doc => {
                const data = doc.data();
                data['storyModeQuestionId'] = doc.id;
                res.push(data);
            })
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getStoryModeQuestion'] = async function(storyModeQuestionId, callback) {
    try{ 
        const question = await storyModeQuestionCollection.doc(storyModeQuestionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else {
            const data = question.data();
            data['storyModeQuestionId'] = storyModeQuestionId;
            callback(null, data)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateStoryModeQuestion'] = async function(storyModeQuestionId, updateFields, callback) {
    try{
        const question = await storyModeQuestionCollection.doc(storyModeQuestionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else{
            const res = await storyModeQuestionCollection.doc(storyModeQuestionId).update(updateFields)
            callback(null, "Update successfully")
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteStoryModeQuestion'] = async function(storyModeQuestionId, callback) {
    try{
        const res = await storyModeQuestionCollection.doc(storyModeQuestionId).delete()
        callback(null, "Delete successfully") 
    } catch(err) {
        callback(err, null)
    }
}