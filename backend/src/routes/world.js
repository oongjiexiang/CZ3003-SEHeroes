const express = require("express");
const router = express.Router();
const WorldController = require("../controllers/world-controller");

//Add or update
router.post("/", (req, res) => {
    const { section, world, tutorialGroupId, unlockDate } = req.body;
    WorldController.update_add_restrictions(
        {
                section: section,
                world: world,
                tutorialGroupId: tutorialGroupId,
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
    const { section, world, tutorialGroupId } = req.body;

    WorldController.remove_restriction(
        {
            section: section,
            world: world,
            tutorialGroupId: tutorialGroupId,
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
    const { section, world, tutorialGroupId } = req.query;

    let queryMap = {}
    if(section != null) queryMap['section'] = section;
    if(world != null) queryMap['world'] = world;
    if(tutorialGroupId != null) queryMap['tutorialGroupId'] = tutorialGroupId;

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

router.get("/unlocked",  (req, res) => {
    const { matricNo } = req.query;

    if(!matricNo) return res.status(500).send({ message: "Please provide matricNo"})
    
    WorldController.getUnlockedByMatricNo(
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