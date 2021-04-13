/**
 * Index file for whole application. Handle config of firebase, initialize express router and start the whole application.
 * @module index
 */


const admin = require('firebase-admin');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const config = require("./config");
const path = require("path");

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
app.set("views", path.join(__dirname, "views"));
app.set("view engine", "pug");
app.use(express.static(path.join(__dirname, "public")));
//app.use(favicon(path.join(__dirname,'public','favicon.ico')));

db = admin.firestore();

//For testing
// const {deleteAssignmentResult}  = require('./controllers/assignment-result-controller');
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
app.use("/", Account);

const User = require("./src/routes/user")
const TutorialGroup = require("./src/routes/tutorial-group")
const World = require("./src/routes/world")
const Assignment = require("./src/routes/assignment");
const AssignmentQuestion = require("./src/routes/assignment-question");
const AssignmentResult = require("./src/routes/assignment-result");
const StoryModeQuestion = require("./src/routes/story-mode-question");
const StoryModeResult = require("./src/routes/story-mode-result");
const OpenChallengeRecord = require("./src/routes/open-challenge-record");
const AssignmentReport = require("./src/routes/assignment-report");
const StoryModeReport = require("./src/routes/story-mode-report");
const Learderboard = require("./src/routes/leaderboard");
const Tweet = require("./src/routes/tweet");
const Tele = require("./src/routes/telegram");
const AssignmentViewer = require("./src/routes/assignment-viewer");

app.use('/user', User)
app.use('/tutorialGroup', TutorialGroup)
app.use('/world', World)
app.use('/storyModeQuestion', StoryModeQuestion)
app.use('/storyModeResult', StoryModeResult)
app.use('/storyModeReport', StoryModeReport)
app.use('/openChallengeRecord', OpenChallengeRecord)
app.use('/assignment', Assignment)
app.use('/assignmentQuestion', AssignmentQuestion)
app.use('/assignmentResult', AssignmentResult)
app.use('/assignmentReport', AssignmentReport)
app.use('/leaderboard', Learderboard)
app.use('/tweet', Tweet)
app.use("/tele", Tele);
app.use("/assignmentViewer", AssignmentViewer);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
	console.log(`App running on port ${PORT}...`);
	console.log(`http://localhost:${PORT}/`)
});
