const express = require("express");
const router = express.Router();
const StoryModeQuestionsController = require("../controllers/story-mode-questions-controller");

router.post('/', (req, res) => {
    const { answer, correctAns, image, level, question, section, world } = req.body;

    StoryModeQuestionsController.createStoryModeQuestion(
        {
            answer: answer,
            correctAns: correctAns,
            image: image,
            level: level,
            question: question,
            section: section,
            world: world
        }, 
        (err, questionId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(questionId)
            }
        }
    )
});

router.get('/', (req, res) => {
    StoryModeQuestionsController.getAllStoryModeQuestions(
        (err, questions) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(questions)
            }
        }
    )
});

router.get('/:questionId', (req, res) => {
    const { questionId } = req.params;
    StoryModeQuestionsController.getStoryModeQuestion(
        questionId,
        (err, question) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(question)
            }
        }
    )
});

router.patch('/:questionId', (req, res) => {
    const { questionId } = req.params;
    const { answer, correctAns, image, level, question, section, world } = req.body;

    const updateFields = {}
    if(answer) updateFields['answer'] = answer;
    if(correctAns) updateFields['correctAns'] = correctAns;
    if(image) updateFields['image'] = image;
    if(level) updateFields['level'] = level;
    if(question) updateFields['question'] = question;
    if(section) updateFields['section'] = section;
    if(world) updateFields['world'] = world;
    
    StoryModeQuestionsController.updateStoryModeQuestion(
        questionId,
        updateFields, 
        (err, questionId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(questionId)
            }
        }
    )
});

router.delete('/:questionId', (req, res) => {
    const { questionId } = req.params;
    StoryModeQuestionsController.deleteStoryModeQuestion(
        questionId,
        (err, question) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(question)
            }
        }
    )
});

module.exports = router