const express = require("express");
const router = express.Router();
const AssignmentController  = require("../controllers/assignment-controller");

router.post('/', (req, res) => {
    const { assignmentName, startDate, dueDate, questions, tries } = req.body;
    
    AssignmentController.createAssignment(
        {
            assignmentName: assignmentName,
            startDate: new Date(startDate),
            dueDate: new Date(dueDate),
            questions: questions,
            tries: tries
        }, 
        (err, assignemntId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntId)
            }
        }
    )
});

router.get('/', (req, res) => {
    AssignmentController.getAllAssignments(
        (err, assignemnts) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemnts)
            }
        }
    )
});

router.get('/:assignmentId', (req, res) => {
    const { assignmentId } = req.params;
    AssignmentController.getAssignment(
        assignmentId,
        (err, assignemnt) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemnt)
            }
        }
    )
});

router.patch('/:assignmentId', (req, res) => {
    const { assignmentId } = req.params;
    const { assignmentName, startDate, dueDate, questions, tries } = req.body;

    const updateMap = {}
    if(assignmentName != null) updateMap['assignmentName'] = assignmentName;
    if(startDate != null) updateMap['startDate'] = new Date(startDate);
    if(dueDate != null) updateMap['dueDate'] = new Date(dueDate);
    if(questions != null) updateMap['questions'] = questions;
    if(tries != null) updateMap['tries'] = tries;
    
    AssignmentController.updateAssignment(
        assignmentId,
        updateMap, 
        (err, assignemntId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(assignemntId)
            }
        }
    )
});

router.delete('/:assignmentId', (req, res) => {
    const { assignmentId } = req.params;
    AssignmentController.deleteAssignment(
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