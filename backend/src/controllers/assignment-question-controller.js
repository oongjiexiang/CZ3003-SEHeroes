const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentQuestionCollection = db.collection("assignmentQuestion")

module.exports['createAssignmentQuestion'] = async function(record, callback) {
    if (!record['answer'] || !record['correctAnswer'] || !record['question'] || !record['score']) {
        callback('Missing fields', null)
        return
    }
    try{
        const reply = await assignmentQuestionCollection.add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateAssignmentQuestion'] = async function(assignmentQuestionId, updateMap, callback){
    try{
        const assignmentQuestion = await assignmentQuestionCollection.doc(assignmentQuestionId).get();
        if(!assignmentQuestion.exists){
            callback('Asssignment question does not exist', null)
        }
        else{
            const res = await assignmentQuestionCollection.doc(assignmentQuestionId).update(updateMap)
            callback(null, res);
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignmentQuestion'] = async function(assignmentQuestionId, callback){
    try{
        const res = await assignmentQuestionCollection.doc(assignmentQuestionId).delete();
        callback(null, res)
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAssignmentQuestion'] = async function(assignmentQuestionId, callback){
    try{
        const assignmentQuestion = await assignmentQuestionCollection.doc(assignmentQuestionId).get();
        if(!assignmentQuestion.exists){
            callback('Asssignment question does not exist', null)
        }
        else{
            callback(null, assignmentQuestion.data());
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignmentQuestions'] = async function(callback){
    try{
        const snapshot = await assignmentQuestionCollection.get();
        if(snapshot.empty){
            callback('Asssignment question is empty', null)
        }
        else{
            const assignmentQuestions = []
            snapshot.forEach(doc =>
                assignmentQuestions.push(doc.data())
            );
            callback(null, assignmentQuestions);
        }
    } catch(err) {
        callback(err, null)
    }
}