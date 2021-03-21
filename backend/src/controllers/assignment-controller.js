const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentCollection = db.collection("assignment")

module.exports['createAssignment'] = async function(record, callback) {
    if (record['assignmentName'] == null || record['startDate'] == null || record['dueDate'] == null 
                || record['questions'] == null || record['tries'] == null) {
        callback('Missing fields', null)
        return
    }
    try{
        const reply = await assignmentCollection.add(record)
        callback(null, reply.id)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateAssignment'] = async function(assignmentId, updateMap, callback){
    try{
        const assignment = await assignmentCollection.doc(assignmentId).get();
        if(!assignment.exists){
            callback('Asssignment does not exist', null)
        }
        else{
            const res = await assignmentCollection.doc(assignmentId).update(updateMap)
            callback(null, "Update successfully");
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignment'] = async function(assignmentId, callback){
    try{
        const res = await assignmentCollection.doc(assignmentId).delete();
        callback(null, "Delete successfully")
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAssignment'] = async function(assignmentId, callback){
    try{
        const assignment = await assignmentCollection.doc(assignmentId).get();
        if(!assignment.exists){
            callback('Asssignment does not exist', null)
        }
        else{
            const data = assignment.data();
            data['assignmentId'] = assignmentId;
            callback(null, data);
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignments'] = async function(callback){
    try{
        const snapshot = await assignmentCollection.get();
        if(snapshot.empty){
            callback('Asssignment is empty', null)
        }
        else{
            const assignments = []
            snapshot.forEach(doc =>{
                const data = doc.data();
                data['assignmentId'] = doc.id;
                assignments.push(data);
            });
            callback(null, assignments);
        }
    } catch(err) {
        callback(err, null)
    }
}