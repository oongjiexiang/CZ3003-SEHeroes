const functions = require('firebase-functions');
const admin = require('firebase-admin');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const credentials = require("../../credentials.json");

admin.initializeApp({
  credential: admin.credential.cert(credentials)
});

const app = express();
app.use(bodyParser.json());
app.use(cors({ origin: true }));

const User = require("./routes/user");
app.use("/user", User);

exports.api = functions.https.onRequest(app);