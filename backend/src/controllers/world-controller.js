const admin = require('firebase-admin');
const db = admin.firestore();
const worldCollection = db.collection('world');

module.exports['update_add_restrictions'] = async function(record,callback) {
    
    // Are we assuming that all the data we obtained using the 3 parameter must be unique
    if (record['section'] == null|| record['world']== null || record['tutorialGroupID']== null || record['unlockDate']== null) {
        callback('Missing fields', null)
        return
    }
    console.log(record)
    try {
        const section = record['section'];
        const world = record['world'];
        const tutorialGroupID = record['tutorialGroupID'];
        const unlockDate = record['unlockDate'];

        const result = await worldCollection.where('section', '==', section).where('world', '==', world)
                            .where('tutorialGroupID', '==', tutorialGroupID).get();
        if (result.empty) {
            // just create a new item with random id //
            await worldCollection.doc().set(record);
            callback(null, "new restriction added for section number " + String(section) + 
                        " world number " + String(world) + " tutorial group " + tutorialGroupID);
            return;
        }
        // assuming that we already have the object with same section, 
        // world-number and tutorial group, we just update the object's unlockDate
        const id = result.docs[0].id
        
        await worldCollection.doc(id).update({ 'unlockDate': unlockDate });
        callback(null,"restriction modified for section number " +String(section) +" world number " +String(world) +" tutorial group " +tutorialGroupID);
        
    }
    catch (err) {
        callback(err, null);
    }
}
 

module.exports['remove_restriction'] = async function (record, callback) {
    if (record['section'] == null|| record['world']== null || record['tutorialGroupID']== null) {
        callback('Missing fields', null)
        return
    }
    //section, world, tutorialGroupID
    try {
        const section = record['section'];
        const world = record['world'];
        const tutorialGroupID = record['tutorialGroupID'];
        
        const result = await worldCollection.where('section', '==', section).where('world', '==', world)
                            .where('tutorialGroupID', '==', tutorialGroupID).get();
        if (result.empty) {
            callback(null, "No exisiting document found");
            return;
        }

        const id = result.docs[0].id
        await worldCollection.doc(id).delete();
        callback(null, "restriction removed for section number " + String(section) + " world number " + world 
                        + " tutorial group " + tutorialGroupID);
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['getRestriction'] = async function(queryMap, callback) {
    try{ 
        let result = worldCollection
        for (const key in queryMap) {
            result = result.where(key, "==", queryMap[key])
        }
        const snapshot = await result.get()
        
        const res = []
        snapshot.forEach(doc => {
            const data = doc.data();
            data['unlockDate'] = dateToObject(data['unlockDate'].toDate())
            res.push(data);
        })
        callback(null, res)
        
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