const express = require ("express");
const passport = require("passport");
const facebookAuthController = require('./facebook-auth-controller.js');

const admin = require('firebase-admin');
const db = admin.firestore();
const facebookAuthCollection = db.collection('facebookAuth');

const router = express.Router();

router.get("/facebook", 
  (req, res, next) => {
    
    passport.authenticate("facebook", {scope: 'email'})(req, res, next)
    res.status(200).send();
  }
)

router.get(
  "/facebook/callback",
  (req, res, next) => {
    console.log("...", req.query)
    passport.authenticate("facebook", {
      successRedirect: "/auth",
      failureRedirect: "/auth/fail"
    })(req, res, next)
  }
);

router.get("/fail", (req, res) => {
  res.send("Failed attempt");
});

router.get("/", (req, res) => {
  res.send("Success");
});

module.exports = router;