  
const admin = require('firebase-admin');

const db = admin.firestore();
db.settings({ ignoreUndefinedProperties: true });
const worldRef = db.collection('world');

module.exports['update_add_restrictions'] = async function(record,callback) {
    // Are we assuming that all the data we obtained using the 3 parameter must be unique
    try {
        if (!record['sectionNo'] || !record['worldNo'] || !record['tutorialGroupID'] || !record['unlockDate']) {
            console.log('Missing Field');
            return res.status(400).send({message:'Missing Field'})
        }
        sectNo = record['sectionNo'];
        worldnumber = record['worldNo'];
        tutorial_group = record['tutorialGroupID'];
        unlockdate = record['unlockDate'];
        console.log(unlockdate);
        const result = await worldRef.where('sectionNo', '==', sectNo).where('worldNo', '==', worldnumber).where('tutorialGroupID', '==', tutorial_group).get();
        if (result.empty) {
            // just create a new item with random id //
            worldRef.doc().set(record);
            callback(null, "new restriction added for section number " + String(sectNo) + " world number " + String(worldnumber) + " tutorial group " + tutorial_group);
            return;
        }
        // assuming that we already have the object with same sectNo, world-number and tutorial group, we just update the object's unlockdate
        result.forEach(doc => {
            console.log("changing unlock_date");
            worldRef.doc(doc.id).update({ 'unlockDate': unlockdate });
            callback(null,"restriction modified for section number " +String(sectNo) +" world number " +String(worldnumber) +" tutorial group " +tutorial_group);
        });
    }
    catch (err) {
        callback(err, null);
    }
}
 

module.exports['remove_restriction'] = async function (record, callback) {
  //sectNo, worldnumber, tutorial_group
    try {
        console.log("Running Now");
        if (!record['sectionNo'] || !record['worldNo'] || !record['tutorialGroupID']) {
            console.log('Missing Field');
            return res.status(400).send({message:'Missing Field'})
        }
        sectNo = record["sectionNo"];
        worldnumber = record["worldNo"];
        tutorial_group = record["tutorialGroupID"];
        unlockdate = record["unlockDate"];
        const result = await worldRef.where("sectionNo", "==", sectNo).where("worldNo", "==", worldnumber).where("tutorialGroupID", "==", tutorial_group).get();
        if (result.empty) {
            console.log("No existing document.");
            callback(null, "No exisiting document found");
            return;
        }
        result.forEach((doc) => {
            worldRef.doc(doc.id).delete();
            callback(null, "restriction removed for section number " + String(sectNo) + " world number " + worldnumber + " tutorial group " + tutorial_group);
         });
    }
    catch (err) {
        callback(err, null);
    }
}