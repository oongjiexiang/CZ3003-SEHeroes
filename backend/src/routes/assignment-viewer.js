const express = require("express");
const router = express.Router();
const AssignmentQuestionController  = require("../controllers/assignment-question-controller");
const AssignmentController  = require("../controllers/assignment-controller");

router.get('/:assignmentId', (req, res) => {
    const {assignmentId} = req.params;

    if(assignmentId == null) return res.status(404).send()

    AssignmentController.getAssignment(
        assignmentId,
        (err, assignment) => {
            if(err){
                return res.status(404).send()
            }
            else{
                AssignmentQuestionController.getAssignmentQuestions(
                    assignment.questions,
                    (err, assignmentQuestions) => {
                        if(err){
                            return res.status(404).send()
                        }
                        else{
                            //return res.status(200).send(assignmentQuestions)
                            return res.render("index", {data: assignmentQuestions, assignmentName: assignment.assignmentName });
                        }
                    }
                )
            }
        }
    )
});


module.exports = router