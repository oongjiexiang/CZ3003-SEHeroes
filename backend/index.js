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

const Account = require("./routes/account");
app.use("/", Account);

const User = require("./routes/user")
const TutorialGroup = require("./routes/tutorial-group")
const World = require("./routes/world")
const Assignment = require("./routes/assignment");
const AssignmentQuestion = require("./routes/assignment-question");
const AssignmentResult = require("./routes/assignment-result");
const StoryModeQuestion = require("./routes/story-mode-question");
const StoryModeResult = require("./routes/story-mode-result");
const OpenChallengeRecord = require("./routes/open-challenge-record");
const AssignmentReport = require("./routes/assignment-report");
const StoryModeReport = require("./routes/story-mode-report");
const Learderboard = require("./routes/leaderboard");
const Tweet = require("./routes/tweet");
const Tele = require("./routes/telegram");
const AssignmentViewer = require("./routes/assignment-viewer");

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

data = [
    {
        "answer": [
            "Assumptions may affect all aspects of the project and pose a certain degree of risk.",
            "Assumptions and constraints are generally documented with associated attributes.",
            "Assumptions and constraints are generally documented to be generic, like business rules.",
            "Constraints are defined as restrictions or limitations on possible solutions."
        ],
        "correctAnswer": "2",
        "score": "7",
        "question": "Which of the following statements does NOT describe assumptions or constraints?",
        "assignmentQuestionId": "Bbop7Z8gmg4d6JD7B9J8"
    },
    {
        "correctAnswer": "0",
        "question": "The design model should be traceable to the requirements model?",
        "score": "6",
        "answer": [
            "True",
            "False"
        ],
        "assignmentQuestionId": "YKTfeWnWIcCe4TOSQg6X"
    },
    {
        "correctAnswer": "0",
        "answer": [
            "communication, planning, modeling, construction, deployment",
            "communication, risk management, measurement, production, reviewing",
            "analysis, designing, programming, debugging, maintenance",
            "analysis, planning, designing, programming, testing"
        ],
        "score": "3",
        "question": "Which of these are the 5 generic software engineering framework activities?",
        "assignmentQuestionId": "7CjjgtjnRt1bJMKFaPNW"
    },
    {
        "answer": [
            "A known deliverable.",
            "A documented representation of a condition or capability.",
            "Whatever the business analyst deems it to be.",
            "A list of items presented to the business analyst on a napkin."
        ],
        "score": "5",
        "question": "A requirement is best described by which of the following:",
        "correctAnswer": "1",
        "assignmentQuestionId": "BX8LB6SLlBpcvQJJi1OI"
    },
    {
        "question": "Larger programming teams are always more productive than smaller teams",
        "correctAnswer": "1",
        "answer": [
            "True",
            "False"
        ],
        "score": "2",
        "assignmentQuestionId": "jkOwq2ijxay79e7YodWm"
    },
    {
        "question": "The so called \"new economy\" that gripped commerce and finance during the 1990s died and no longer influences decisions made by businesses and software engineers.",
        "correctAnswer": "1",
        "score": "5",
        "answer": [
            "True",
            "False"
        ],
        "assignmentQuestionId": "hIxvF06JLA9d3gY2p09p"
    },
    {
        "question": "Every communication activity should have a facilitator to make sure that the customer is not allowed to dominate the proceedings",
        "answer": [
            "True",
            "False"
        ],
        "correctAnswer": "1",
        "score": "6",
        "assignmentQuestionId": "D1YZwMvKDO3Dkl2m0JjT"
    },
    {
        "answer": [
            "Dialog map.",
            "Dialog hierarchy.",
            "Navigation flow.",
            "Interface map."
        ],
        "score": "5",
        "correctAnswer": "3",
        "question": "Storyboards depict interfaces and related elements and have several synonyms. Which of the following is the LEAST applicable term for a storyboard?",
        "assignmentQuestionId": "CAzYk3sPsFh2z6nUHI3L"
    },
    {
        "answer": [
            "Document the data characteristics which will become the columns or fields in a database.",
            "Document the business concepts that will be the basis for Use Cases that involve data.",
            "Document the business objects that will contain data characteristics.",
            "Be used mainly for database design, because entities or classes do not belong in requirements documentation."
        ],
        "score": "3",
        "question": "The purpose of finding and modeling entities/classes in requirements is to:",
        "correctAnswer": "2",
        "assignmentQuestionId": "vkI7yhg9EVuWMCHuhi4g"
    },
    {
        "correctAnswer": "3",
        "question": "What are the types of requirement in Quality Function Deployment(QFD) ?",
        "score": "5",
        "answer": [
            "Known, Unknown, Undreamed",
            "Functional, Non-Functional",
            "User, Developer",
            "Normal, Expected, Exciting"
        ],
        "assignmentQuestionId": "OIprZcg9NV3PHudnadrJ"
    },
    {
        "correctAnswer": "1",
        "question": "In general software only succeeds if its behavior is consistent with the objectives of its designers.",
        "answer": [
            "True",
            "False"
        ],
        "score": "5",
        "assignmentQuestionId": "9isafHanZUrgQoZo97sh"
    },
    {
        "score": "2",
        "question": "There are no real differences between creating WebApps and MobileApps",
        "correctAnswer": "1",
        "answer": [
            "True",
            "False"
        ],
        "assignmentQuestionId": "aCz0maBtEdKRH4y1IuiX"
    },
    {
        "score": "7",
        "correctAnswer": "0",
        "question": "Many of the tasks from the generic task sets for analysis modeling and design can be conducted in parallel with one another.",
        "answer": [
            "True",
            "False"
        ],
        "assignmentQuestionId": "f3sYi3nk5jbCeJXSoIwY"
    },
    {
        "answer": [
            "Define useful software features and functions delivered to end-users",
            "Determine a schedule used to deliver each software increment",
            "Provide a substitute to performing detailed scheduling of activities",
            "Used to estimate the effort required build the current increment"
        ],
        "question": "What role(s) do user stories play in agile planning?",
        "correctAnswer": "3",
        "score": "5",
        "assignmentQuestionId": "9vkCXEM6n4vOTQzPBkYa"
    },
    {
        "answer": [
            "Plan Business Analysis Approach.",
            "Conduct Stakeholder Analysis.",
            "Plan Business Analysis Communication.",
            "Communicate Requirements."
        ],
        "correctAnswer": "2",
        "score": "7",
        "question": "One group of stakeholders is located in Austin, Texas, and another located in Russia. During what activity would this information be considered critical?",
        "assignmentQuestionId": "51YFYNh41w5XRaGU6HYZ"
    }
]

app.get("/test", (req, res) => {
	res.render("index", { title: "Home", data: data, assignmentName: "Assignment 3" });
  });

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
	console.log(`App running on port ${PORT}...`);
	console.log(`http://localhost:${PORT}/`)
});
