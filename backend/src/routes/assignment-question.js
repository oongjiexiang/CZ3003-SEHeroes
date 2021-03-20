const express = require("express");
const router = express.Router();
const AssignmentQuestionController  = require("../controllers/assignment-question-controller");

router.post('/', (req, res) => {
    const { answer, correctAnswer, question, score } = req.body;

    AssignmentQuestionController.createAssignmentQuestion(
        {
            answer: answer,
            correctAnswer: correctAnswer,
            question: question,
            score: score
        }, 
        (err, assignemntQuestionId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntQuestionId)
            }
        }
    )
});

router.get('/', (req, res) => {
    AssignmentQuestionController.getAllAssignmentQuestions(
        (err, assignemntQuestions) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntQuestions)
            }
        }
    )
});

router.get('/:assignmentQuestionId', (req, res) => {
    const { assignmentQuestionId } = req.params;
    AssignmentQuestionController.getAssignmentQuestion(
        assignmentQuestionId,
        (err, assignemntQuestion) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntQuestion)
            }
        }
    )
});

router.patch('/:assignmentQuestionId', (req, res) => {
    const { assignmentQuestionId } = req.params;
    const { answer, correctAnswer, question, score } = req.body;

    const updateMap = {}
    if(answer) updateMap['answer'] = answer;
    if(correctAnswer) updateMap['correctAnswer'] = correctAnswer;
    if(question) updateMap['question'] = question;
    if(score) updateMap['score'] = score;
    
    AssignmentQuestionController.updateAssignmentQuestion(
        assignmentQuestionId,
        updateMap, 
        (err, assignemntId) => {
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