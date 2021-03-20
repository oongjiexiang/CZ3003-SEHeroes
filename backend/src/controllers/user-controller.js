const admin = require("firebase-admin");
const { getUser } = require("./account-controller");
const db = admin.firestore();
const userRef = db.collection('users');

module.exports['createUser'] = async function(record, callback){
    try {
        console.log(record['character']);
        if (!record["character"] ||!record["matricNo"] ||!record["openChallengeRating"] ||!record["tutorialGroup"]||!record["username"]) {
        console.log("Missing Field");
        return res.status(400).send({ message: "Missing Field" });
        }
        matricNumber = record["matricNo"];
        const result = await userRef.where("matricNo", "==", matricNumber).get();
        if (result.empty) {
            // just create a new item with random id //
            userRef.doc().set(record);
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

module.exports['updateUser'] = async function (record, callback) {
    try {
        if (!record["character"] ||!record["matricNo"] ||!record["openChallengeRating"] ||!record["tutorialGroup"] ||!record["username"]) {
          console.log("Missing Field");
          return res.status(400).send({ message: "Missing Field" });
        }
        matricNumber = record["matricNo"];
        const result = await userRef.where("matricNo", "==", matricNumber).get();
        if (result.empty) {
            console.log("User does not exists!");
            return
        }
        else {
            result.forEach((doc) => {
                userRef.doc(doc.id).set(record);
                callback(null,doc.id)
         });
        }
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['getUser'] = async function (matricNo, callback) {
    try {
        const result = await userRef.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            console.log("Use does not exist!");
        }
        else {
            result.forEach((doc) => {
                user = doc.data();
                console.log(user);
                callback(null,user)
            })
        }
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['deleteUser'] = async function (matricNo, callback) {
    try {
        console.log(matricNo);
        const result = await userRef.where("matricNo", "==", matricNo).get();
        console.log(result);
        if (result.empty) {
            console.log("User does not exist!");
        } else {
            result.forEach((doc) => {
                userRef.doc(doc.id).delete();
                callback(null, "deleted");
            });
        }
    }
    catch (err) {
        callback(err, null);
    }
}

module.exports['getAllUsers'] = async function (callback) {
    try {
        const result = await userRef.get();
        if (result.empty) {
            consile.log("User data is empty")
        }
        else {
            result.forEach((doc) => {
                callback(null, doc.data);
            })
        }
    }
    catch (err) {
        callback(err, null);
    }
}