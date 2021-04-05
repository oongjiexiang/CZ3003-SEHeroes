const passport = require("passport");
const dotenv = require("dotenv");
const strategy = require("passport-facebook");

const admin = require('firebase-admin');
const { firestore } = require("firebase-admin");
const db = admin.firestore();
const accountCollection = db.collection('account');

const FacebookStrategy = strategy.Strategy;

dotenv.config();
passport.serializeUser(function(user, done) {
  done(null, user);
});

passport.deserializeUser(function(obj, done) {
  done(null, obj);
});

passport.use(
  new FacebookStrategy(
    {
      clientID: process.env.FACEBOOK_CLIENT_ID,
      clientSecret: process.env.FACEBOOK_CLIENT_SECRET,
      callbackURL: process.env.FACEBOOK_CALLBACK_URL,
      profileFields: ["email", "name", "id"],
      passReqToCallback : true 
    },
    async function(req, accessToken, refreshToken, profile, done) {
      const { email, first_name, last_name } = profile._json;
      console.log(profile._json)
      const userData = {
        email,
        firstName: first_name,
        lastName: last_name
      };
      //await accountCollection.doc().set(userData);
      done(null, profile);
    }
  )
);