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

module.exports = router;