const express = require("express");
const router = express.Router();
const AssignmentReportController = require("../controllers/assignment-report-controller");

router.get('/', (req, res) => {
    const { tutorialGroupId, assignmentId } = req.query

    let queryMap = {}
    queryMap['tutorialGroupId'] = tutorialGroupId
    if (assignmentId != null) queryMap['assignmentId'] = assignmentId

    AssignmentReportController.getAssignmentReportByTutorialGroup(
        queryMap,
        (err, results) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(results)
            }
        }
    )
})

module.exports = router