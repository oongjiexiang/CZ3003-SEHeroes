const admin = require("firebase-admin");
const db = admin.firestore();

const trGroup = db.collection('tutorialGroup')
module.exports['createGroup'] = async function (groupId, students, callback) {
    // group id refers to the tutorial group that you want to create
    // students are the students inside that tutorial group
    try {
        // check whether the tutorial group with such group id exists, if yes, ignore else create new group
        console.log("Running now");
        students={"studentID":students}
        const record = await trGroup.doc(groupId).get();
        if (record.exists) {
            console.log("Tutorial group already exist");
        }
        else {
            console.log("Creating tutorial group")
            const res = await trGroup.doc(groupId).set(students);
            console.log("Tutorial group successfully created");
        }
    }
    catch (err) {
        callback(err, null);
    }
}


module.exports['updateGroup'] = async function (req, callback) {
    

    // Atomically add a new student id to the "studentID" array field.
    try {
        console.log('Running now');
        if (!req['group_id'] || !req['student_id']) {
            console.log('Missing Field');
            return res.status(400).send({ message: 'Missing Field' })
        }
        groupNumber = req['group_id'];
        studentid = req['student_id'];
        const record = trGroup.doc(groupNumber);
        const unionRes = await record.update({
            studentID: admin.firestore.FieldValue.arrayUnion(studentid)
        })
        return
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['removeStudentFromGroup']= async function(req,callback) {
//Atomically remove a new student id to the "studentID" array field.
    try {
        console.log('Running now');
        if (!req["group_id"] || !req["student_id"]) {
            console.log("Missing Field");
            return res.status(400).send({ message: "Missing Field" });
        }
        groupNumber = req["group_id"];
        studentid = req["student_id"];
        const record = trGroup.doc(groupNumber);
        const removeRes = await record.update({
          studentID: admin.firestore.FieldValue.arrayRemove(studentid),
        });
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['deleteTutorialGroup'] = async function (groupId, callback) {
// delete a whole tutorial group
    try {
        console.log("Running now");
        const record = await trGroup.doc(groupId).get();
        if (record.exists) {
            const res = trGroup.doc(groupId).delete();
            console.log("Tutorial group deleted");
        }
        else {
            console.log("Tutorial group does not exists");
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
        const record = await trGroup.get();
        if (record.empty) {
            console.log("No tutorial group at this moment");
        }
        else { }
        var dict = {}
        record.forEach(doc => {
            dict[doc.id] = doc.data();
        })
        // returns a dictionary where the key is the tutorial group id and the value is the students
        console.log(dict);
        return dict;
    }
    catch (err) {
        callback(err, null);
    }
}