/**
 * Controller for user related logic.
 * @module user-controller
 * @category controller
 */

const admin = require("firebase-admin");
const db = admin.firestore();
const usersCollection = db.collection('users');


/**
 * Create user and store into the database. User must have necessary field.
 * It will check duplication of user by checking the matricNo.
 * @param {Object} record - New user details, including matricNo, username and character.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createUser= async function(record, callback){
    if (record["matricNo"] == null|| record["username"]== null || record['character']== null) {
        callback('Missing fields', null)
        return
    }

    try {
        const matricNumber = record["matricNo"];
        const result = await usersCollection.where("matricNo", "==", matricNumber).get();
        if (result.empty) {
            // just create a new item with random id 
            record['openChallengeRating'] = 0;
            await usersCollection.doc().set(record);
            callback(null, "User created");
        }
        // assuming that we already have the user with such user name and matric number dont do anything
        else {
            callback(null, "User already exist");
        }

    } catch (err) {
      callback(err, null);
    }
}

/**
 * Update user by matricNo, updated field name must be valid.
 * @param {String} matricNo - MatricNo of assignment to be updated.
 * @param {Object} updateMap - Object include new data to update.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.updateUser= async function (matricNo, updateMap, callback) {
    try {
        const result = await usersCollection.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            callback("User does not exists",null)
        }
        else {
            
            result.forEach((doc) => {
                usersCollection.doc(doc.id).update(updateMap);
                callback(null,"Update sucessfully")
         });
        }
    }
    catch (err) {
        callback(err, null);
    }
}

/**
 * Get user data by matricNo.
 * @param {String} matricNo - MatricNo of assignment.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getUser= async function (matricNo, callback) {
    try {
        const result = await usersCollection.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            callback("User does not exists",null)
        }
        else {
            result.forEach((doc) => {
                const user = doc.data();
                callback(null, user)
            })
        }
    }
    catch (err) {
        callback(err, null);
    }
}

/**
 * Delete an user from database by matricNo.
 * @param {String} matricNo - MatricNo of user to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteUser= async function (matricNo, callback) {
    try {
        console.log(matricNo);
        const result = await usersCollection.where("matricNo", "==", matricNo).get();
        console.log(result);
        if (result.empty) {
            callback("User does not exists",null)
        } else {
            result.forEach((doc) => {
                usersCollection.doc(doc.id).delete();
                callback(null, "Delete successfully");
            });
        }
    }
    catch (err) {
        callback(err, null);
    }
}

/**
 * Get all user data in the database by filter.
 * @param {Object} queryMap - Object of filter, field can include tutorialGroupId and character.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAllUsers= async function (queryMap, callback) {
    try {

        let results = usersCollection
        for (const key in queryMap) {
            results = results.where(key, "==", queryMap[key])
        }
        const snapshot = await results.get()
        if (snapshot.empty) {
            callback("User does not exists!",null)
        }
        else {
            const users = []
            snapshot.forEach((doc) => {
                users.push(doc.data());
            })
            callback(null, users);
        }
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports.getLeaderboard= async function (callback) {
    try {
        const snapshot = await usersCollection.get()
        if (snapshot.empty) {
            callback("User does not exists!",null)
            return
        }
        let users = []
        snapshot.forEach((doc) => {users.push(doc.data());})
        users = users.filter(data => !isNaN(data.openChallengeRating))
        users.forEach(data => data.openChallengeRating = parseInt(data.openChallengeRating))
        users.sort((a, b) => b.openChallengeRating - a.openChallengeRating);
        callback(null, users);
    }
    catch (err) {
        callback(err, null);
    }
}

