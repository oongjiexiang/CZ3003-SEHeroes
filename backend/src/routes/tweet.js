const express = require("express");
const router = express.Router();
const Twit = require('twit')
const AssignmentController  = require("../controllers/assignment-controller");
const config = require("../../config");


const T = new Twit({
    consumer_key: config.twitterConsumerKey,
    consumer_secret: config.twitterConsumerSecret,
    access_token: config.twitterAccessToken,
    access_token_secret: config.twitterAccessTokenSecret
})

router.post("/", (req, res) => {
    const { assignmentId } = req.body;
    AssignmentController.getAssignment(
        assignmentId,
        (err, assignment) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                const assignmentName = assignment['assignmentName']
                const dueDate = assignment['dueDate']
                T.post(
                    'statuses/update', 
                    { status: `${assignmentName} is out now! Take on the challenge and complete it by ${dueDate['day']}/${dueDate['month']}/${dueDate['year']}. 
                    \n\nYou can view the questions at https://seheroes.herokuapp.com/assignmentViewer/${assignmentId}` }, 
                    function(err, data, response) {
                        if (err) {
                            return res.status(500).send({ message: `${err}` });
                        }
                        return res.status(200).send(data);
                    }
                );
            }
        }
    )
});

// module.exports.postTweet= async function (assignment, deadline, callback) {
//     T.post('statuses/update', { status: 'hello world!' }, function(err, data, response) {
//         console.log(data)
//     })
// }
module.exports = router;