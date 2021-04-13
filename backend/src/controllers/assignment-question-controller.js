/**
 * Controller for assignment question related logic.
 * @module assignment-question-controller
 * @category controller
 */

const admin = require('firebase-admin');
const db = admin.firestore();
const AssignmentQuestionCollection = db.collection("assignmentQuestion")


/**
 * Create assignment question and store into the database. Assignment question must have necessary field.
 * @param {Object} record - New assignment question details, including question, answer, correctAnswer and score.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createAssignmentQuestion = async function(record, callback) {
    if (record['answer'] == null || record['correctAnswer'] == null  || record['question'] == null || record['score'] == null ) {
        callback('Missing fields', null)
        return
    }
    try{
        const reply = await AssignmentQuestionCollection.add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Update assignment question by assignmentQuestionId, updated field name must be valid.
 * @param {String} assignmentQuestionId - AssignmentQuestionId of assignment question to be updated.
 * @param {Object} updateMap - Object include new data to update.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.updateAssignmentQuestion = async function(assignmentQuestionId, updateMap, callback){
    try{
        const assignmentQuestion = await AssignmentQuestionCollection.doc(assignmentQuestionId).get();
        if(!assignmentQuestion.exists){
            callback('Asssignment question does not exist', null)
        }
        else{
            const res = await AssignmentQuestionCollection.doc(assignmentQuestionId).update(updateMap)
            callback(null, "Update successfully");
        }
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Delete an assignment question from database by assignmentQuestionId.
 * @param {String} assignmentQuestionId - AssignmentQuestionId of assignment question to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteAssignmentQuestion = async function(assignmentQuestionId, callback){
    try{
        const res = await AssignmentQuestionCollection.doc(assignmentQuestionId).delete();
        callback(null, "Delete successfully")
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Get assignment question data by assignmentQuestionId.
 * @param {String} assignmentQuestionId - AssignmentQuestionId of assignment question.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAssignmentQuestion = async function(assignmentQuestionId, callback){
    try{
        const assignmentQuestion = await AssignmentQuestionCollection.doc(assignmentQuestionId).get();
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

/**
 * Get assignment questions by a list of assignmentQuestionId.
 * @param {Object} assignmentQuestionIds - List of assignmentQuestionId of assignment.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAssignmentQuestions = async function(assignmentQuestionIds, callback){
    try{
        if(assignmentQuestionIds.length == 0){
            callback('Asssignment question is empty', null)
            return
        }
        
        const assignmentQuestions = []
        for(let i = 0; i < assignmentQuestionIds.length; i++){
            let assignmentQuestion = await AssignmentQuestionCollection.doc(assignmentQuestionIds[i]).get();
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