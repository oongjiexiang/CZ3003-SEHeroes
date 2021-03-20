const express = require("express");
const router = express.Router();
const AssignmentQuestionController  = require("../controllers/assignment-question-controller");
const AssignmentController  = require("../controllers/assignment-controller");

router.post('/', (req, res) => {
    const { answer, correctAnswer, question, score, image } = req.body;

    AssignmentQuestionController.createAssignmentQuestion(
        {
            answer: answer,
            correctAnswer: correctAnswer,
            question: question,
            score: score,
            image: image
        }, 
        (err, assignmentQuestionId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignmentQuestionId)
            }
        }
    )
});

//Get list of assignment question by assignment id
router.get('/', (req, res) => {
    const {assignmentId} = req.query;
    if(assignmentId == null) return res.status(500).send({ message: "missing assignmentId"})

    AssignmentController.getAssignment(
        assignmentId,
        (err, assignment) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                AssignmentQuestionController.getAssignmentQuestions(
                    assignment.questions,
                    (err, assignmentQuestions) => {
                        if(err){
                            return res.status(500).send({ message: `${err}`})
                        }
                        else{
                            return res.status(200).send(assignmentQuestions)
                        }
                    }
                )
            }
        }
    )
});

router.get('/:assignmentQuestionId', (req, res) => {
    const { assignmentQuestionId } = req.params;
    AssignmentQuestionController.getAssignmentQuestion(
        assignmentQuestionId,
        (err, assignmentQuestion) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignmentQuestion)
            }
        }
    )
});

router.patch('/:assignmentQuestionId', (req, res) => {
    const { assignmentQuestionId } = req.params;
    const { answer, correctAnswer, question, score, image } = req.body;

    const updateMap = {}
    if(answer != null) updateMap['answer'] = answer;
    if(correctAnswer != null) updateMap['correctAnswer'] = correctAnswer;
    if(question != null) updateMap['question'] = question;
    if(score != null) updateMap['score'] = score;
    if(image != null) updateMap['image'] = image;
    
    AssignmentQuestionController.updateAssignmentQuestion(
        assignmentQuestionId,
        updateMap, 
        (err, assignmentId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignmentQuestionId)
            }
        }
    )
});

router.delete('/:assignmentQuestionId', (req, res) => {
    const { assignmentQuestionId } = req.params;
    AssignmentQuestionController.deleteAssignmentQuestion(
        assignmentQuestionId,
        (err, message) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(message)
            }
        }
    )
});

module.exports = router