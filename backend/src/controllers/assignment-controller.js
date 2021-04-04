const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentCollection = db.collection("assignment")
const usersCollection = db.collection('users');
const assignmentResultCollection = db.collection("assignmentResult")


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
            data['startDate'] = dateToObject(data['startDate'].toDate())
            data['dueDate'] = dateToObject(data['dueDate'].toDate())
            callback(null, data);
        }
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAssignmentsByMatricNo'] = async function(matricNo, callback){
    try{
        let result = await usersCollection.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            callback("User does not exists", null)
            return
        }

        const now = new Date();
        result = await assignmentCollection.where('dueDate', '>=', now).get();
        

        let assignmentsData = {}
        result.forEach((doc) => {
            if(doc.data()['startDate'].toDate() <= now) assignmentsData[doc.id] = {"assignmentId":doc.id, "score": 0, ...doc.data()}
        });

        result = await assignmentResultCollection.where("matricNo","==", matricNo).get();
        result.forEach((doc) => {
            let assignmentResult = doc.data();
            assignmentsData[assignmentResult['assignmentId']]['tries'] -= assignmentResult['tried'];
            assignmentsData[assignmentResult['assignmentId']]['score'] = assignmentResult['score'];
        })
        
        const assignments = []
        for (const key in assignmentsData) {
            let data = {assignmentId: key, ...assignmentsData[key]};
            data['startDate'] = dateToObject(data['startDate'].toDate())
            data['dueDate'] = dateToObject(data['dueDate'].toDate())
            assignments.push(data);
        }

        callback(null, assignments)
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignments'] = async function(callback){
    try{
        const snapshot = await assignmentCollection.get();
        
        const assignments = []
        snapshot.forEach(doc =>{
            const data = doc.data();
            data['assignmentId'] = doc.id;
            data['startDate'] = dateToObject(data['startDate'].toDate())
            data['dueDate'] = dateToObject(data['dueDate'].toDate())
            assignments.push(data);
        });
        callback(null, assignments);
        
    } catch(err) {
        callback(err, null)
    }
}

function dateToObject(date){
    return{
        year: date.getFullYear(),
        month: date.getMonth(),
        day: date.getDay(),
        hour: date.getHours(),
        minute: date.getMinutes(),
        second: date.getSeconds()
    }
}