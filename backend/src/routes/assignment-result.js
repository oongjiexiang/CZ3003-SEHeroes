const express = require("express");
const router = express.Router();
const AssignmentResultController  = require("../controllers/assignment-result-controller");

router.post('/', (req, res) => {
    const { assignmentId, studentId, tried, score } = req.body;
    console.log(req.body)
    AssignmentResultController.createAssignmentResult(
        {
            assignmentId: assignmentId,
            studentId: studentId,
            tried: tried,
            score: score
        }, 
        (err, assignemntResultId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntResultId)
            }
        }
    )
});

router.get('/', (req, res) => {
    AssignmentResultController.getAllAssignmentResults(
        (err, assignemntResults) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntResults)
            }
        }
    )
});

router.get('/:assignmentId/:studentId', (req, res) => {
    const { assignmentId, studentId } = req.params;
    AssignmentResultController.getAssignmentResult(
        studentId, assignmentId,
        (err, assignemntResult) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntResult)
            }
        }
    )
});

router.patch('/:assignmentId/:studentId', (req, res) => {
    const { assignmentId, studentId } = req.params;
    const { tried, score } = req.body;

    const updateMap = {}
    if(req.body.assignmentId) updateMap['assignmentId'] = req.body.assignmentId;
    if(req.body.studentId) updateMap['studentId'] = req.body.studentId;
    if(tried) updateMap['tried'] = tried;
    if(score) updateMap['score'] = score;
    
    AssignmentResultController.updateAssignmentResult(
        studentId,
        assignmentId,
        updateMap, 
        (err, assignemntResultId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntResultId)
            }
        }
    )
});

router.delete('/:assignmentId/:studentId', (req, res) => {
    const { assignmentId, studentId } = req.params;
    AssignmentResultController.deleteAssignmentResult(
        studentId,
        assignmentId,
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