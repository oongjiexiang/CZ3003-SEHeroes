const express = require("express");
const router = express.Router();
const AssignmentController  = require("../controllers/assignment-controller");

router.post('/', (req, res) => {
    const { assignmentName, startDate, dueDate, questions, tries } = req.body;
    
    AssignmentController.createAssignment(
        {
            assignmentName: assignmentName,
            startDate: objectToDate(startDate),
            dueDate: objectToDate(dueDate),
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
    const{ matricNo } = req.query;

    if(matricNo){
        AssignmentController.getAssignmentsByMatricNo(
            matricNo,
            (err, assignemnts) => {
                if(err){
                    return res.status(500).send({ message: `${err}`})
                }
                else{
                    return res.status(200).send(assignemnts)
                }
            }
        )
    }
    else{
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
    }
   
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

router.put('/:assignmentId', (req, res) => {
    const { assignmentId } = req.params;
    const { assignmentName, startDate, dueDate, questions, tries } = req.body;

    const updateMap = {}
    if(assignmentName != null) updateMap['assignmentName'] = assignmentName;
    if(startDate != null) updateMap['startDate'] = objectToDate(startDate);
    if(dueDate != null) updateMap['dueDate'] = objectToDate(dueDate);
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


function objectToDate(obj){
    try{
        const {year, month, day, hour, minute} = obj;
        var date = new Date(year, month-1, day, hour, minute,0,0);
        if(date instanceof Date && !isNaN(date))
            return date;
        else
            return null;
    }
    catch{
        //console.log("date format error");
        return null;
    }
}

module.exports = router