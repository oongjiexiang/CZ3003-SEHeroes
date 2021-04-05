const express = require("express");
const router = express.Router();
const UserController = require("../controllers/user-controller");

router.get("/", (req, res) => {
    UserController.getLeaderboard(
        (err, result) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(result);
        }
    });
});

module.exports = router;