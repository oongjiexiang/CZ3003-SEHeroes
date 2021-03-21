const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentResultCollection = db.collection("assignmentResult")

module.exports['createOrUpdateAssignmentResult'] = async function(record, callback) {

    if (record['assignmentId'] == null  || record['matricNo'] == null || record['score']== null ) {
        callback('Missing fields', null)
        return
    }
    try{
        const matricNo = record['matricNo']
        const assignmentId = record['assignmentId']
        const assignmentResult = await assignmentResultCollection.where('matricNo', '==', matricNo)
                                                            .where('assignmentId', '==', assignmentId).get();
        
        if(assignmentResult.empty){
            record['tried'] = 1
            await assignmentResultCollection.add(record)
            callback(null, "Result added");
            return;
        }
        
        let id = assignmentResult.docs[0].id;
        await assignmentResultCollection.doc(id).update({ 
            'score': record['score'],
            'tried': assignmentResult.docs[0].data()['tried']+1
        });
        callback(null,"Result updated");
        
        
    } catch(err) {
        callback(err, null)
    }
}

module.exports['deleteAssignmentResult'] = async function(assignmentResultId, callback){
    try{
        const res = await assignmentResultCollection.doc(assignmentResultId).delete();
        callback(null, "Delete successfully")       
    } catch(err) {
        callback(err, null)
    }
}

module.exports['getAllAssignmentResults'] = async function(queryMap, callback){
    try{
        let results = assignmentResultCollection
        for (const key in queryMap) {
            results = results.where(key, "==", queryMap[key])
        }
        const snapshot = await results.get()
        if(snapshot.empty){
            callback('Asssignment result does not exist', null)
        }
        else{
            const assignmentResults = []
            snapshot.forEach(doc =>{
                const data = doc.data();
                data['assignmentResultId'] = doc.id;
                assignmentResults.push(data);
            });
            callback(null, assignmentResults);
        }
    } catch(err) {
        callback(err, null)
    }
}
