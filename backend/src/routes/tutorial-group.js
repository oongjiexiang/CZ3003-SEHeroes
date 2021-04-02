const express = require("express");
const router = express.Router();
const TutorialGroupController = require("../controllers/tutorial-group-controller");
const UserController = require("../controllers/user-controller");

router.post("/", (req, res) => {
    const {tutorialGroupId, student} = req.body;

    TutorialGroupController.createGroup(
        tutorialGroupId,
        student,
        (err, info) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(info);
            }
        }
    );
});

router.put("/:tutorialGroupId/add", (req, res) => {
    const {tutorialGroupId} = req.params
    const { matricNo } = req.body;

    TutorialGroupController.updateGroup(
        {
            tutorialGroupId: tutorialGroupId,
            matricNo: matricNo,
        },
        (err, docid) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                UserController.updateUser(
                    matricNo,
                    {"tutorialGroup": tutorialGroupId},
                    (err, info) => {
                        if (err) {
                            return res.status(500).send({ message: `${err}` });
                        } else {
                            return res.status(200).send("Student added to tutorial group");
                        }
                    }
                );
            }
        }
    );
});


router.put("/:tutorialGroupId/remove", (req, res) => {
    const {tutorialGroupId} = req.params
    const { matricNo } = req.body;

    TutorialGroupController.removeStudentFromGroup(
        {
            tutorialGroupId: tutorialGroupId,
            matricNo: matricNo
        },

        (err, docid) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                UserController.updateUser(
                    matricNo,
                    {"tutorialGroup": ""},
                    (err, info) => {
                        if (err) {
                            return res.status(500).send({ message: `${err}` });
                        } else {
                            return res.status(200).send("Student removed from tutorial group");
                        }
                    }
                );
            }
        }
    );
});


router.delete("/:tutorialGroupId", (req, res) => {
    const { tutorialGroupId } = req.params;
    TutorialGroupController.deleteTutorialGroup(tutorialGroupId, (err, msg) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(msg);
        }
    });
});

router.get("/", (req, res) => {
    TutorialGroupController.getAllGroups((err, groups) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(groups);
        }
    });
});

router.get("/:tutorialGroupId", (req, res) => {
    const {tutorialGroupId} = req.params;

    TutorialGroupController.getTutorialGroup(
        tutorialGroupId,
        (err, groups) => {
        if (err) {
            return res.status(500).send({ message: `${err}` });
        } else {
            return res.status(200).send(groups);
        }
    });
});

module.exports = router;
