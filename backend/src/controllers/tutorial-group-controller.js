/**
 * Controller for tutorial group related logic.
 * @module tutorial-group-controller
 * @category controller
 */


const admin = require("firebase-admin");
const db = admin.firestore();
const tutorialGroupCollection = db.collection('tutorialGroup');
const usersCollection = db.collection('users');



/**
 * Create tutorial group and store into the database.
 * @param {String} tutorialGroupId - TutorialGroupId of the new tutorial group.
 * @param {Object} student - A list of matricNo of students that allocated into this tutorial group. 
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createGroup= async function (tutorialGroupId, student, callback) {
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


/**
 * Add a student into tutorial group.
 * @param {Object} req - Object include tutorialGroupId and matricNo.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.updateGroup= async function (req, callback) {
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

/**
 * Remove a student from tutorial group.
 * @param {Object} req - Object include tutorialGroupId and matricNo.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.removeStudentFromGroup = async function(req,callback) {
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


/**
 * Detele a tutorial group by tutorialGroupId.
 * @param {String} tutorialGroupId - tutorialGroupId of tutorial group to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteTutorialGroup= async function (tutorialGroupId, callback) {
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

/**
 * Get all tutorial groups data.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAllGroups= async function (callback) {
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

/**
 * Get tutorial group data by tutorialGroupId.
 * @param {String} tutorialGroupId - tutorialGroupId of tutorial group.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getTutorialGroup= async function (tutorialGroupId, callback) {
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
