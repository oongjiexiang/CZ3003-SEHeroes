const e = require('express');
const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentResultCollection = db.collection("assignmentResult")

module.exports['createAssignmentResult'] = async function(record, callback) {

    if (!record['assignmentId'] || !record['studentId'] || !record['score'] || !record['tried']) {
        callback('Missing fields', null)
        return
    }
    try{
        const studentId = record['studentId']
        const assignmentId = record['assignmentId']
        const assignmentResult = await assignmentResultCollection.where('studentId', '==', studentId)
                                                            .where('assignmentId', '==', assignmentId).get();
        
        if(assignmentResult.empty){
            const reply = await assignmentResultCollection.add(record)
            callback(null, reply.id)
        }
        else{
            callback('Asssignment result of the student already exist', null)
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['updateAssignmentResult'] = async function(studentId, assignmentId, updateMap, callback){
    console.log(studentId, assignmentId)
    try{
        const assignmentResult = await assignmentResultCollection.where('studentId', '==', studentId)
                                                            .where('assignmentId', '==', assignmentId).get();
                                          
        if(assignmentResult.empty){
            callback('Asssignment result does not exist', null)
        }
        else{
            let assignmentResultId = assignmentResult.docs[0].id;
            const res = await assignmentResultCollection.doc(assignmentResultId).update(updateMap);
            callback(null, res)       
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignmentResult'] = async function(studentId, assignmentId, callback){
    try{
        const assignmentResult = await assignmentResultCollection.where('studentId', '==', studentId)
                            .where('assignmentId', '==', assignmentId).get();

        if(assignmentResult.empty){
            callback('Asssignment result does not exist', null)
            
        }
        else{
            let assignmentResultId = assignmentResult.docs[0].id;
            const res = await assignmentResultCollection.doc(assignmentResultId).delete();
            callback(null, res)       
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAssignmentResult'] = async function(studentId, assignmentId, callback){
    try{
        const assignmentResult = await assignmentResultCollection.where('studentId', '==', studentId)
                                        .where('assignmentId', '==', assignmentId).get();
        if(assignmentResult.empty){
            callback('Asssignment result does not exist', null)
        }
        else{
            callback(null, assignmentResult.docs[0].data());
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignmentResults'] = async function(callback){
    try{
        const snapshot = await assignmentResultCollection.get();
        if(snapshot.empty){
            callback('Asssignment does not exist', null)
        }
        else{
            const assignmentResults = []
            snapshot.forEach(doc =>
                assignmentResults.push(doc.data())
            );
            callback(null, assignmentResults);
        }
    } catch(err) {
        callback(err, null)
    }
}