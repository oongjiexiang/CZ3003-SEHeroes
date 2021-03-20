const express = require("express");
const router = express.Router();
const WorldController = require("../controllers/world-controller");

//Add or update
router.patch("/", (req, res) => {
    const { sectionNo, worldNo, tutorialGroupID, unlockDate } = req.body;

    WorldController.update_add_restrictions(
        {
                sectionNo: sectionNo,
                worldNo: worldNo,
                tutorialGroupID: tutorialGroupID,
                unlockDate: new Date(unlockDate)
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
    const { sectionNo, worldNo, tutorialGroupID } = req.body;

    WorldController.remove_restriction(
        {
            sectionNo: sectionNo,
            worldNo: worldNo,
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

module.exports = router;