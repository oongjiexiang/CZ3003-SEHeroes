const express = require("express");
const router = express.Router();
const WorldController = require("../controllers/world-controller");

//Add or update
router.post("/", (req, res) => {
    const { section, world, tutorialGroupID, unlockDate } = req.body;

    WorldController.update_add_restrictions(
        {
                section: section,
                world: world,
                tutorialGroupID: tutorialGroupID,
                unlockDate: objectToDate(unlockDate)
        },

        (err, msg) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(msg);
            }
        }
    );
});

router.delete("/", (req, res) => {
    const { section, world, tutorialGroupID } = req.body;

    WorldController.remove_restriction(
        {
            section: section,
            world: world,
            tutorialGroupID: tutorialGroupID,
        },

        (err, msg) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(msg);
            }
        }
    );
});


router.get("/", (req, res) => {
    const { section, world, tutorialGroupID } = req.query;

    let queryMap = {}
    if(section != null) queryMap['section'] = section;
    if(world != null) queryMap['world'] = world;
    if(tutorialGroupID != null) queryMap['tutorialGroupID'] = tutorialGroupID;

    WorldController.getRestriction(
        queryMap,
        (err, msg) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(msg);
            }
        }
    );
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

module.exports = router;