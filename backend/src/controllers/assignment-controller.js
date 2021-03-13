const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentCollection = db.collection("assignment")

module.exports['createAssignment'] = async function(record, callback) {
    try{
        if (!record['startDate'] || !record['dueDate'] || !record['questions'] || !record['tries']) {
            callback('Missing fields', null)
        }
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
            callback(null, res);
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignment'] = async function(assignmentId, callback){
    try{
        const res = await assignmentCollection.doc(assignmentId).delete();
        console.log(res)
        callback(null, res)
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
            callback(null, assignment.data());
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignment'] = async function(callback){
    try{
        const snapshot = await assignmentCollection.get();
        if(snapshot.empty){
            callback('Asssignment is empty', null)
        }
        else{
            const assignments = []
            snapshot.forEach(doc =>
                assignments.push(doc.data())
            );
            callback(null, assignments);
        }
    } catch(err) {
        callback(err, null)
    }
}