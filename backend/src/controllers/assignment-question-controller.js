const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentQuestionCollection = db.collection("assignmentQuestion")

module.exports['createAssignmentQuestion'] = async function(record, callback) {
    if (record['answer'] == null || record['correctAnswer'] == null  || record['question'] == null || record['score'] == null ) {
        callback('Missing fields', null)
        return
    }
    if(!record['image']){
        delete record['image']
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
            callback(null, "Update successfully");
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignmentQuestion'] = async function(assignmentQuestionId, callback){
    try{
        const res = await assignmentQuestionCollection.doc(assignmentQuestionId).delete();
        callback(null, "Delete successfully")
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
            const data = assignmentQuestion.data();
            data['assignmentQuestionId'] = assignmentQuestionId;
            callback(null, data);
        }
    } catch(err) {
        callback(err, null)
    }
}

//get assignment questions by a list of assignmentQuestionId
module.exports['getAssignmentQuestions'] = async function(assignmentQuestionIds, callback){
    try{
        if(assignmentQuestionIds.length == 0){
            callback('Asssignment question is empty', null)
            return
        }
        
        const assignmentQuestions = []
        for(let i = 0; i < assignmentQuestionIds.length; i++){
            let assignmentQuestion = await assignmentQuestionCollection.doc(assignmentQuestionIds[i]).get();
            let data = assignmentQuestion.data();
            if(data != null){
                data['assignmentQuestionId'] = assignmentQuestionIds[i];
                assignmentQuestions.push(data)
            }
        }
        callback(null, assignmentQuestions);
        
    } catch(err) {
        callback(err, null)
    }
}