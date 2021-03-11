const admin = require('firebase-admin');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const credentials = require("../credentials.json");

admin.initializeApp({
  credential: admin.credential.cert(credentials)
});

const app = express();
app.use(bodyParser.json());
app.use(cors({ origin: true }));

const User = require("./routes/user");
app.use("/user", User);

db = admin.firestore();

//For testing
const {createOpenChallengeRecord}  = require('./controllers/open-challenge-record-controller');
const testingMiddleWareFunction = (req, res) => {
	//Just change this part
	createOpenChallengeRecord(
		{
			team1: ['A', 'B'],
			team2: ['C', 'D'],
			type: "single",
			questions: ['savd6a', 'asgrebr']
		},
		(err, uid) => {
			if(err){
				return res.status(500).send({ message: `${err}`})
			}
			else{
				return res.status(200).send(uid)
			}
		}
	)
}
app.use('/test', 
	testingMiddleWareFunction
)

const PORT = process.env.PORT || 8000;
app.listen(PORT, () => {
	console.log(`App running on port ${PORT}...`);
	console.log(`http://localhost:${PORT}/`)
});
