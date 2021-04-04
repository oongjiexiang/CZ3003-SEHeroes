const admin = require("firebase-admin");
const db = admin.firestore();
const usersCollection = db.collection('users');

module.exports['createUser'] = async function(record, callback){
    if (record["matricNo"] == null|| record["username"]== null || record['character']== null) {
        callback('Missing fields', null)
        return
    }

    try {
        const matricNumber = record["matricNo"];
        const result = await usersCollection.where("matricNo", "==", matricNumber).get();
        if (result.empty) {
            // just create a new item with random id 
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

module.exports['updateUser'] = async function (matricNo, updateMap, callback) {
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

module.exports['getUser'] = async function (matricNo, callback) {
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

module.exports['deleteUser'] = async function (matricNo, callback) {
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

module.exports['getAllUsers'] = async function (queryMap, callback) {
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