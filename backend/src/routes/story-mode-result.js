const express = require("express");
const router = express.Router();
const StoryModeResultController = require("../controllers/story-mode-result-controller");

router.post('/', (req, res) => {
    const { level, section, star, matricNo, world } = req.body;

    StoryModeResultController.createOrUpdateStoryModeResult(
        {
            level: level,
            section: section,
            star: star,
            matricNo: matricNo,
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

    const { level, section, world, matricNo} = req.query;

    let queryMap = {}
    if(world != null) queryMap['world'] = world;
    if(section != null) queryMap['section'] = section;
    if(level != null) queryMap['level'] = level;
    if(matricNo != null) queryMap['matricNo'] = matricNo;

    StoryModeResultController.getAllStoryModeResults(
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
});

router.delete('/:resultId', (req, res) => {
    const { resultId } = req.params;
    StoryModeResultController.deleteStoryModeResult(
        resultId,
        (err, info) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(info)
            }
        }
    )
});

module.exports = router