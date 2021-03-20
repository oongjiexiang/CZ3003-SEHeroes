const express = require("express");
const router = express.Router();
const TutorialGroupController = require("../controllers/tutorial-group-controller");


router.post("/", (req, res) => {
    const {groupid,students} = req.body;

    TutorialGroupController.createGroup(
        groupid,
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
    const { group_id, student_id } = req.body;

    TutorialGroupController.updateGroup(
        {
            group_id: group_id,
            student_id: student_id,
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
    const {group_id,student_id} = req.body;

    TutorialGroupController.removeStudentFromGroup(
        {
            group_id: group_id,
            student_id:student_id
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


router.delete("/:groupID", (req, res) => {
    const { groupID } = req.params;
    TutorialGroupController.deleteTutorialGroup(groupID, (err, msg) => {
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