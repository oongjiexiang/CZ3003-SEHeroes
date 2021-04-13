/**
 * Controller for assignment result related logic.
 * @module assignment-result-controller
 * @category controller
 */

const admin = require('firebase-admin');
const db = admin.firestore();
const AssignmentResultCollection = db.collection("assignmentResult")
const AssignmentCollection = db.collection("assignment")

/**
 * Create assignment result and store into the database. Result identify by (matricNo, assignmentId). 
 * Result will be updated if exists in database. Number of tried will auto increase by 1. 
 * Score will take the max of new result and previous result.
 * @param {Object} record - New assignment result details, including assignmentId, matricNo and score.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createOrUpdateAssignmentResult = async function(record, callback) {

    if (record['assignmentId'] == null  || record['matricNo'] == null || record['score']== null ) {
        callback('Missing fields', null)
        return
    }
    try{
        const matricNo = record['matricNo']
        const assignmentId = record['assignmentId']
        const assignmentResult = await AssignmentResultCollection.where('matricNo', '==', matricNo)
                                                            .where('assignmentId', '==', assignmentId).get();
        
        if(assignmentResult.empty){
            record['tried'] = 1
            await AssignmentResultCollection.add(record)
            callback(null, "Result added");
            return;
        }
        
        let id = assignmentResult.docs[0].id;
        await AssignmentResultCollection.doc(id).update({ 
            'score': Math.max(record['score'], assignmentResult.docs[0].data()['score']),
            'tried': assignmentResult.docs[0].data()['tried']+1
        });
        callback(null,"Result updated");
        
        
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Delete an assignment result from database by assignmentResultId.
 * @param {String} assignmentResultId - AssignmentResultId of assignment result to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteAssignmentResult = async function(assignmentResultId, callback){
    try{
        const res = await AssignmentResultCollection.doc(assignmentResultId).delete();
        callback(null, "Delete successfully")       
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Get all assignment results based on filter.
 * @param {Object} queryMap - Object of filter, field can include matricNo and tutorialGroupId.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAllAssignmentResults = async function(queryMap, callback){
    try{
        let results = AssignmentResultCollection
        for (const key in queryMap) {
            results = results.where(key, "==", queryMap[key])
        }
        const snapshot = await results.get()
        if(snapshot.empty){
            callback('Asssignment result does not exist', null)
            return;
        }
        
        const result = await AssignmentCollection.get();
        const assignmentNameMap = {}
        result.forEach((doc) => assignmentNameMap[doc.id] = doc.data().assignmentName);

        const assignmentResults = []
        snapshot.forEach(doc =>{
            const data = doc.data();
            data['assignmentResultId'] = doc.id;
            if(assignmentNameMap[data.assignmentId]) data['assignmentName'] = assignmentNameMap[data.assignmentId];
            assignmentResults.push(data);
        });
        callback(null, assignmentResults);
    
    } catch(err) {
        callback(err, null)
    }
}
