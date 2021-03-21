const admin = require("firebase-admin");
const db = admin.firestore();
const tutorialGroupCollection = db.collection('tutorialGroup')

module.exports['createGroup'] = async function (tutorialGroupId, students, callback) {
    // group id refers to the tutorial group that you want to create
    // students are the students inside that tutorial group
    try {
        // check whether the tutorial group with such group id exists, if yes, ignore else create new group
        const record = await tutorialGroupCollection.doc(tutorialGroupId).get();
        if (record.exists) {
            console.log("Tutorial group already exist");
            callback(null, "Tutorial group already exist");
        }
        else {
            const data = {
                "students": students
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
        const groupNumber = req['tutorialGroupId'];
        const matricNo = req['studematricNont'];
        const record = tutorialGroupCollection.doc(groupNumber);
        const unionRes = await record.update({
            students: admin.firestore.FieldValue.arrayUnion(matricNo)
        })
        callback(null, "student " + matricNo + " has been added to group " + groupNumber + "!");
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
            students: admin.firestore.FieldValue.arrayRemove(matricNo)
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
            callback(null,"Group" + tutorialGroupId + "deleted");
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
            console.log("No tutorial group at this moment");
        }
        else {
            var dict = []
            record.forEach(doc => {
                const data = doc.data();
                data['tutorialGroupId'] = doc.id;
                dict.push(data);
            })
            // returns a dictionary where the key is the tutorial group id and the value is the students
            console.log(dict);
            callback(null, dict);
        }
    }
    catch (err) {
        callback(err, null);
    }
}