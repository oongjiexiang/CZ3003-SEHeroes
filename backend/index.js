const admin = require('firebase-admin');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const config = require("./config");

const credentials = {
	"project_id": config.firebaseProjectId,
	"private_key": config.firebasePrivateKey,
	"client_email": config.firebaseClientEmail
}

admin.initializeApp({
  credential: admin.credential.cert(credentials)
});

const app = express();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cors({ origin: true }));

db = admin.firestore();

//For testing
// const {deleteAssignmentResult}  = require('./src/controllers/assignment-result-controller');
// const testingMiddleWareFunction = (req, res) => {
// 	//Just change this part
// 	deleteAssignmentResult(
// 		'naruto', 'rasengan', 
// 		(err, uid) => {
// 			if(err){
// 				return res.status(500).send({ message: `${err}`})
// 			}
// 			else{
// 				return res.status(200).send(uid)
// 			}
// 		}
// 	)
// }
// app.use('/test', 
// 	testingMiddleWareFunction
// )

const Account = require("./src/routes/account");
app.use("/account", Account);

const User = require("./src/routes/user")
const TutorialGroup = require("./src/routes/tutorial-group")
const World = require("./src/routes/world")
const Assignment = require("./src/routes/assignment");
const AssignmentQuestion = require("./src/routes/assignment-question");
const AssignmentResult = require("./src/routes/assignment-result");
const StoryModeQuestion = require("./src/routes/story-mode-question");
const StoryModeResult = require("./src/routes/story-mode-result");
const OpenChallengeRecord = require("./src/routes/open-challenge-record");

app.use('/user', User)
app.use('/tutorialGroup', TutorialGroup)
app.use('/world', World)
app.use('/storymodequestion', StoryModeQuestion)
app.use('/storymoderesult', StoryModeResult)
app.use('/openchallengerecord', OpenChallengeRecord)
app.use('/assignment', Assignment)
app.use('/assignmentQuestion', AssignmentQuestion)
app.use('/assignmentResult', AssignmentResult)

const PORT = process.env.PORT || 8000;
app.listen(PORT, () => {
	console.log(`App running on port ${PORT}...`);
	console.log(`http://localhost:${PORT}/`)
});
