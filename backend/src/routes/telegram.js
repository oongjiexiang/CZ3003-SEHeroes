const express = require("express");
const router = express.Router();
const teleController = require("../controllers/telegram-controller");
const AssignmentController  = require("../controllers/assignment-controller");

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
                const dueDate = assignment['dueDate']['day'] + '/' + assignment['dueDate']['month'] 
                                            + '/' + assignment['dueDate']['year'];
                const tries = assignment['tries']
                teleController.makeAnnouncement(
                    {
                        assignmentName: assignmentName,
                        dueDate: dueDate,
                        tries:tries,
                        assignmentId: assignmentId
                    },
                    (err, info) => {
                        if (err) {
                            return res.status(500).send({ message: `${err}` });
                        } else {
                            return res.status(200).send(info);
                        }
                });
            }
        }
    )
});

module.exports = router;