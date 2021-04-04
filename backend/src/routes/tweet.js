const express = require("express");
const router = express.Router();
const Twit = require('twit')
const AssignmentController  = require("../controllers/assignment-controller");

const T = new Twit({
    consumer_key: 'uo9KkRVlJKlJKfqwSWNq263IE',
    consumer_secret: 'IJngNoGQv99ylLeEITi6vodA821Qj2q5CsemRaQUgseo29lGjf',
    access_token: '1299178188531142662-D60xY0P1LaD4IyPzOZsoBRYVhWvC1x',
    access_token_secret: 'G6PseNkH7Kr50b2QKBo0SqeYRSYApj2guvsQwx6Lxha8M'
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
                    { status: `${assignmentName} is out now! Take on the challenge and complete it by ${dueDate['day']}/${dueDate['month']}/${dueDate['year']}.` }, 
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

// module.exports['postTweet'] = async function (assignment, deadline, callback) {
//     T.post('statuses/update', { status: 'hello world!' }, function(err, data, response) {
//         console.log(data)
//     })
// }
module.exports = router;