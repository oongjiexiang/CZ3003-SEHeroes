const express = require("express");
const router = express.Router();
const OpenChallengeRecordController = require("../controllers/open-challenge-record-controller");

router.post('/', (req, res) => {
    const { questions, team1, team2, type } = req.body;

    OpenChallengeRecordController.createOpenChallengeRecord(
        {
            questions: questions,
            team1: team1,
            team2: team2,
            type: type
        }, 
        (err, recordId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(recordId)
            }
        }
    )
});

router.get('/', (req, res) => {
    OpenChallengeRecordController.getAllOpenChallengeRecords(
        (err, records) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(records)
            }
        }
    )
});

router.get('/:recordId', (req, res) => {
    const { recordId } = req.params;
    OpenChallengeRecordController.getOpenChallengeRecord(
        recordId,
        (err, record) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(record)
            }
        }
    )
});

router.patch('/:recordId', (req, res) => {
    const { recordId } = req.params;
    const { questions, team1, team2, type } = req.body;

    const updateFields = {}
    if(questions != null) updateFields['questions'] = questions;
    if(team1 != null) updateFields['team1'] = team1;
    if(team2 != null) updateFields['team2'] = team2;
    if(type != null) updateFields['type'] = type;
    
    OpenChallengeRecordController.updateOpenChallengeRecord(
        recordId,
        updateFields, 
        (err, recordId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(recordId)
            }
        }
    )
});

router.delete('/:recordId', (req, res) => {
    const { recordId } = req.params;
    OpenChallengeRecordController.deleteOpenChallengeRecord(
        recordId,
        (err, record) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(record)
            }
        }
    )
});

module.exports = router