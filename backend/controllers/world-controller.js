const admin = require('firebase-admin');
const db = admin.firestore();
const WorldCollection = db.collection('world');
const UsersCollection = db.collection('users');
const storyModeResultCollection = db.collection("storyModeResult") 

module.exports['update_add_restrictions'] = async function(record,callback) {
    
    // Are we assuming that all the data we obtained using the 3 parameter must be unique
    if (record['section'] == null|| record['world']== null || record['tutorialGroupId']== null || record['unlockDate']== null) {
        callback('Missing fields', null)
        return
    }
    console.log(record)
    try {
        const section = record['section'];
        const world = record['world'];
        const tutorialGroupId = record['tutorialGroupId'];
        const unlockDate = record['unlockDate'];

        const result = await WorldCollection.where('section', '==', section).where('world', '==', world)
                            .where('tutorialGroupId', '==', tutorialGroupId).get();
        if (result.empty) {
            // just create a new item with random id //
            await WorldCollection.doc().set(record);
            callback(null, "new restriction added for section number " + String(section) + 
                        " world number " + String(world) + " tutorial group " + tutorialGroupId);
            return;
        }
        // assuming that we already have the object with same section, 
        // world-number and tutorial group, we just update the object's unlockDate
        const id = result.docs[0].id
        
        await WorldCollection.doc(id).update({ 'unlockDate': unlockDate });
        callback(null,"restriction modified for section number " +String(section) +" world number " +String(world) +" tutorial group " +tutorialGroupId);
        
    }
    catch (err) {
        callback(err, null);
    }
}
 

module.exports['remove_restriction'] = async function (record, callback) {
    if (record['section'] == null|| record['world']== null || record['tutorialGroupId']== null) {
        callback('Missing fields', null)
        return
    }
    //section, world, tutorialGroupId
    try {
        const section = record['section'];
        const world = record['world'];
        const tutorialGroupId = record['tutorialGroupId'];
        
        const result = await WorldCollection.where('section', '==', section).where('world', '==', world)
                            .where('tutorialGroupId', '==', tutorialGroupId).get();
        if (result.empty) {
            callback(null, "No exisiting document found");
            return;
        }

        const id = result.docs[0].id
        await WorldCollection.doc(id).delete();
        callback(null, "restriction removed for section number " + String(section) + " world number " + world 
                        + " tutorial group " + tutorialGroupId);
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['getRestriction'] = async function(queryMap, callback) {
    try{ 
        let result = WorldCollection
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

module.exports['getUnlockedByMatricNo'] = async function(matricNo, callback) {
    try{
        let result = await UsersCollection.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            callback("User does not exists", null)
            return
        }
        
        let tutorialGroup = result.docs[0].data()['tutorialGroup']

        if(!tutorialGroup){
            callback(null, []) //All lock
            return
        }

        const now = new Date();
        result = await WorldCollection.where('unlockDate', '<=', now).get();

        let unlocked = []
        result.forEach((doc) => {
            let data = doc.data();
            if(data.tutorialGroupId == tutorialGroup) unlocked.push({world: data.world, section: data.section, level: ["Easy"]});
        });

        result = await storyModeResultCollection.where('matricNo', '==', matricNo).get();
        const passResult = []
        result.forEach((doc) => passResult.push(doc.data()));

        unlocked.forEach((data) => {
            passResult.forEach((passData) =>{
                if(passData.world == data.world && passData.section == data.section){
                    if(parseInt(passData.star) >= 2){
                        if(passData.level == "Easy") data.level.push("Medium");
                        if(passData.level == "Medium") data.level.push("Hard");
                    }
                }
            })
        })
        console.log(unlocked);
        callback(null, unlocked);
        
    } catch(err) {
        callback(err, null)
    }
}

function dateToObject(date){
    return{
        year: date.getFullYear(),
        month: date.getMonth()+1,
        day: date.getDate(),
        hour: date.getHours(),
        minute: date.getMinutes()
    }
}