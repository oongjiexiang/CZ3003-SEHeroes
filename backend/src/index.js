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
app.use(bodyParser.urlencoded({
	extended: true
}));
app.use(cors({ origin: true }));

const Account = require("./routes/account");
app.use("/account", Account);

db = admin.firestore();

//For testing
const {deleteAssignmentResult}  = require('./controllers/assignment-result-controller');
const testingMiddleWareFunction = (req, res) => {
	//Just change this part
	deleteAssignmentResult(
		'naruto', 'rasengan', 
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

const Assignment = require("./routes/assignment");
app.use('/assignment', Assignment)

const PORT = process.env.PORT || 8000;
app.listen(PORT, () => {
	console.log(`App running on port ${PORT}...`);
	console.log(`http://localhost:${PORT}/`)
});
