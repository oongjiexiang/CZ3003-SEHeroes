const admin = require("firebase-admin");
const db = admin.firestore();
const tutorialGroupCollection = db.collection('tutorialGroup');
const usersCollection = db.collection('users');

module.exports['createGroup'] = async function (tutorialGroupId, student, callback) {
    // group id refers to the tutorial group that you want to create
    // students are the students inside that tutorial group
    try {
        // check whether the tutorial group with such group id exists, if yes, ignore else create new group
        const record = await tutorialGroupCollection.doc(tutorialGroupId).get();
        if (record.exists) {
            callback(null, "Tutorial group already exist");
        }
        else {
            let data = {}
            if(student == null) data["student"] = []
            else{
                let existing_student = []
                for(let i = 0; i < student.length; i++){
                    let matricNo = student[i]
                    const result = await usersCollection.where("matricNo", "==", matricNo).get();
                    if (result.empty) {
                        continue
                    }
                    else {

                        
                        result.forEach((doc) => {
                            usersCollection.doc(doc.id).update({"tutorialGroup": tutorialGroupId});
                            existing_student.push(matricNo)
                    })}
                }
                data["student"] = existing_student
            }
            const res = await tutorialGroupCollection.doc(tutorialGroupId).set(data);
            callback(null, "Tutorial group " + tutorialGroupId + " created");
        }
    }
    catch (err) {
        callback(err, null);
    }
}


module.exports['updateGroup'] = async function (req, callback) {
    // Atomically add a new student id to the "studentID" array field.
    if (!req['tutorialGroupId'] || !req['matricNo']) {
        callback('Missing fields', null)
        return
    }
    try {
        const tutorialGroupId = req['tutorialGroupId'];
        const matricNo = req['matricNo'];
        const record = tutorialGroupCollection.doc(tutorialGroupId);
        const unionRes = await record.update({
            student: admin.firestore.FieldValue.arrayUnion(matricNo)
        })
        callback(null, "student " + matricNo + " has been added to group " + tutorialGroupId + "!");
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['removeStudentFromGroup']= async function(req,callback) {
    //Atomically remove a new student id to the "studentID" array field.
    if (!req['tutorialGroupId'] || !req['matricNo']) {
        callback('Missing fields', null)
        return
    }   
    try {
        const groupNumber = req['tutorialGroupId'];
        const matricNo = req['matricNo'];
        const record = tutorialGroupCollection.doc(groupNumber);
        const removeRes = await record.update({
            student: admin.firestore.FieldValue.arrayRemove(matricNo)
        });
        callback(null, matricNo+" is removed from "+"group "+groupNumber+"!");
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['deleteTutorialGroup'] = async function (tutorialGroupId, callback) {
    // delete a whole tutorial group
    try {
        const record = await tutorialGroupCollection.doc(tutorialGroupId).get();
        if (record.exists) {
            const res = tutorialGroupCollection.doc(tutorialGroupId).delete();
            callback(null,"Group " + tutorialGroupId + " deleted");
        }
        else {
            callback(null, "tutorial group does not exist!");
        }
    }
    catch (err) {
        callback(err, null);
    }   
}

module.exports['getAllGroups'] = async function (callback) {
    // get all tutorial groups
    try {
        // check if tutorial groups collection is empty or not
        const record = await tutorialGroupCollection.get();
        if (record.empty) {
            callback("No tutorial group at this moment", null);
        }
        else {
            var dict = []
            record.forEach(doc => {
                const data = doc.data();
                data['tutorialGroupId'] = doc.id;
                dict.push(data);
            })
            // returns a dictionary where the key is the tutorial group id and the value is the students
            callback(null, dict);
        }
    }
    catch (err) {
        callback(err, null);
    }
}


module.exports['getTutorialGroup'] = async function (tutorialGroupId, callback) {
    try {
        const record = await tutorialGroupCollection.doc(tutorialGroupId).get();
        if (!record.exists) {
            callback("Not exist", null);
        }
        else {
            const data = record.data();
            data['tutorialGroupId'] =tutorialGroupId;
            callback(null, data);
        }
    }
    catch (err) {
        callback(err, null);
    }
}
