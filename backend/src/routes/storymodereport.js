
const express = require("express");
const router = express.Router();
const storyModeController = require("../controllers/story-mode-report-controller");

router.get("/", (req,res) => {
    storyModeController.getStoryModeReport((err, reports) => {
    if (err) {
      return res.status(500).send({ message: `${err}` });
    } else {
      return res.status(200).send(reports);
    }
  });
});
module.exports = router;