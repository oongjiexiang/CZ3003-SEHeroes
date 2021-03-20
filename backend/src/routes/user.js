const express = require("express");
const router = express.Router();
const UserController = require("../controllers/user-controller");

router.post("/", (req, res) => {
    const {character,matricNo,openChallengeRating,tutorialGroup,username} = req.body;

    UserController.createUser(
        {
            character:character,
            matricNo:matricNo,
            openChallengeRating: openChallengeRating,
            tutorialGroup: tutorialGroup,
            username:username
        },
        (err, info) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(info);
            }
        }
    );
});



router.patch("/", (req, res) => {
    const { character, matricNo, openChallengeRating, tutorialGroup,username } = req.body;

    UserController.updateUser(
        {
            character: character,
            matricNo: matricNo,
            openChallengeRating: openChallengeRating,
            tutorialGroup: tutorialGroup,
            username:username
        },
        
        (err, docid) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(docid);
            }
        }
    );
});

router.delete("/:matricNo", (req, res) => {
    const { matricNo } = req.params;
    UserController.deleteUser(
        matricNo,
        (err, msg) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(msg);
        }
    });
});


router.get("/:matricNo", (req, res) => {
    const { matricNo } = req.params;
    UserController.getUser(matricNo, (err, assignemnt) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(assignemnt);
        }
    });
});

router.get("/", (req, res) => {
    UserController.getAllUsers((err, users) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(users);
        }
    });
});


module.exports = router;