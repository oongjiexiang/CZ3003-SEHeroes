const express = require("express");
const router = express.Router();
const TutorialGroupController = require("../controllers/tutorial-group-controller");

router.post("/", (req, res) => {
    const {tutorialGroupId, students} = req.body;

    TutorialGroupController.createGroup(
        tutorialGroupId,
        students,
        (err, info) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send(info);
            }
        }
    );
});

router.patch("/add", (req, res) => {
    const { tutorialGroupId, students } = req.body;

    TutorialGroupController.updateGroup(
        {
            tutorialGroupId: tutorialGroupId,
            students: students,
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


router.patch("/remove", (req, res) => {
    const {tutorialGroupId,students} = req.body;

    TutorialGroupController.removeStudentFromGroup(
        {
            tutorialGroupId: tutorialGroupId,
            students: students
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

module.exports = router;