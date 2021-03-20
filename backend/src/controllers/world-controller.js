const admin = require('firebase-admin');
const db = admin.firestore();
const worldCollection = db.collection('world');

module.exports['update_add_restrictions'] = async function(record,callback) {
    
    // Are we assuming that all the data we obtained using the 3 parameter must be unique
    if (record['section'] == null|| record['world']== null || record['tutorialGroupID']== null || record['unlockDate']== null) {
        callback('Missing fields', null)
        return
    }
    try {
        const section = record['section'];
        const world = record['world'];
        const tutorialGroupID = record['tutorialGroupID'];
        const unlockDate = record['unlockDate'];

        const result = await worldCollection.where('section', '==', section).where('world', '==', world)
                            .where('tutorialGroupID', '==', tutorialGroupID).get();
        if (result.empty) {
            // just create a new item with random id //
            worldCollection.doc().set(record);
            callback(null, "new restriction added for section number " + String(section) + 
                        " world number " + String(world) + " tutorial group " + tutorialGroupID);
            return;
        }
        // assuming that we already have the object with same section, 
        // world-number and tutorial group, we just update the object's unlockDate
        result.forEach(doc => {
            worldCollection.doc(doc.id).update({ 'unlockDate': unlockDate });
            callback(null,"restriction modified for section number " +String(section) +" world number " +String(world) +" tutorial group " +tutorialGroupID);
        });
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
        result.forEach((doc) => {
            worldCollection.doc(doc.id).delete();
            callback(null, "restriction removed for section number " + String(section) + " world number " + world 
                            + " tutorial group " + tutorialGroupID);
         });
    }
    catch (err) {
        callback(err, null);
    }
}