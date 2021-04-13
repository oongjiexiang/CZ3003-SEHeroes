const express = require("express");
const router = express.Router();
const storyModeController = require("../controllers/story-mode-report-controller");

router.get("/", (req, res) => {

    const { tutorialGroupId } = req.query;

    const queryMap = {}
    if(tutorialGroupId != null) queryMap['tutorialGroupId'] = tutorialGroupId;

    storyModeController.getStoryModeReport(
        queryMap, 
        (err, reports) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(reports);
            }
        }
    );
});
module.exports = router;
