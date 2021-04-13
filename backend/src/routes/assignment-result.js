const express = require("express");
const router = express.Router();
const AssignmentResultController  = require("../controllers/assignment-result-controller");

router.post('/', (req, res) => {
    const { assignmentId, matricNo, tried, score } = req.body;

    AssignmentResultController.createOrUpdateAssignmentResult(
        {
            assignmentId: assignmentId,
            matricNo: matricNo,
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
    const { assignmentId, matricNo} = req.query;

    let queryMap = {}
    if(assignmentId != null) queryMap['assignmentId'] = assignmentId;
    if(matricNo != null) queryMap['matricNo'] = matricNo;

    AssignmentResultController.getAllAssignmentResults(
        queryMap,
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


router.delete('/:assignmentResultId', (req, res) => {
    const { assignmentResultId } = req.params;
    AssignmentResultController.deleteAssignmentResult(
        assignmentResultId,
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