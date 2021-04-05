const admin = require('firebase-admin');
const db = admin.firestore();
const accountCollection = db.collection('account');
const usersCollection = db.collection('users');
const bcrypt = require('bcrypt');

module.exports['register'] = async function(record, callback){
    try {
        const {username, email, password, matricNo, character} = record;

        const result = await accountCollection.where("matricNo", "==", matricNo).get();
        if (!result.empty) {
            callback("User with this matricNo already exist!", null);
            return;
        }
        
        const newAccount = {email, password, matricNo};
        const newUser = {matricNo, character, username, openChallengeRating: 0};
        const salt = await bcrypt.genSalt(10);
        newAccount.password = await bcrypt.hash(password, salt);

        await accountCollection.doc().set(newAccount);
        await usersCollection.doc().set(newUser);
        
        callback(null, record)
    } catch (err) {
      callback(err, null);
    }
}

module.exports['login'] = async function(record, callback){
    try {
        const {password, matricNo} = record;
        const result = await accountCollection.where("matricNo", "==", matricNo).get();
        if (result.empty) {
            callback("Invalid credentials!", null);
            return;
        }
        
        const userResult = await usersCollection.where("matricNo", "==", matricNo).get()
        const user = userResult.docs[0].data()
        const account = result.docs[0].data()


        const valid = await bcrypt.compare(password, account.password)

        if(!valid){
            callback("Invalid credentials!", null);
            return;
        }
        
        callback(null, {email: account.email, ...user})
    } catch (err) {
      callback(err, null);
    }
}
