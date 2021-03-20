const express = require("express");
const router = express.Router();
const StoryModeResultsController = require("../controllers/story-mode-results-controller");

router.post('/', (req, res) => {
    const { level, section, star, userID, world } = req.body;

    StoryModeResultsController.createStoryModeResult(
        {
            level: level,
            section: section,
            star: star,
            userID: userID,
            world: world
        }, 
        (err, resultId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(resultId)
            }
        }
    )
});

router.get('/', (req, res) => {
    StoryModeResultsController.getAllStoryModeResults(
        (err, results) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(results)
            }
        }
    )
});

router.get('/:resultId', (req, res) => {
    const { resultId } = req.params;
    StoryModeResultsController.getStoryModeResult(
        resultId,
        (err, result) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(result)
            }
        }
    )
});

router.patch('/:resultId', (req, res) => {
    const { resultId } = req.params;
    const { level, section, star, userID, world } = req.body;

    const updateFields = {}
    if(level) updateFields['level'] = level;
    if(section) updateFields['section'] = section;
    if(star) updateFields['star'] = star;
    if(userID) updateFields['userID'] = userID;
    if(world) updateFields['world'] = world;
    
    StoryModeResultsController.updateStoryModeResult(
        resultId,
        updateFields, 
        (err, resultId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(resultId)
            }
        }
    )
});

router.delete('/:resultId', (req, res) => {
    const { resultId } = req.params;
    StoryModeResultsController.deleteStoryModeResult(
        resultId,
        (err, result) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(result)
            }
        }
    )
});

module.exports = router