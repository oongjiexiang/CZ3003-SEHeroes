const express = require("express");
const router = express.Router();
const StoryModeQuestionController = require("../controllers/story-mode-questions-controller");

router.post('/', (req, res) => {
    const { answer, correctAnswer, image, level, question, section, world } = req.body;

    StoryModeQuestionController.createStoryModeQuestion(
        {
            answer: answer,
            correctAnswer: correctAnswer,
            image: image,
            level: level,
            question: question,
            section: section,
            world: world
        }, 
        (err, storyModeQuestionId) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(storyModeQuestionId)
            }
        }
    )
});

router.get('/', (req, res) => {
    const { level, section, world } = req.query;

    let queryMap = {}
    if(world != null) queryMap['world'] = world;
    if(section != null) queryMap['section'] = section;
    if(level != null) queryMap['level'] = level;
    
    StoryModeQuestionController.getAllStoryModeQuestions(
        queryMap,
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

router.get('/:storyModeQuestionId', (req, res) => {
    const { storyModeQuestionId } = req.params;
    StoryModeQuestionController.getStoryModeQuestion(
        storyModeQuestionId,
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

router.get('/questionSet', (req, res) => {
    const { world, section, level } = req.query;
    console.log(req.query)
    StoryModeQuestionController.getStoryModeQuestionSet(
        world, section, level,
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

router.patch('/:storyModeQuestionId', (req, res) => {
    const { storyModeQuestionId } = req.params;
    const { answer, correctAnswer, image, level, question, section, world } = req.body;

    const updateFields = {}
    if(answer != null) updateFields['answer'] = answer;
    if(correctAnswer != null) updateFields['correctAnswer'] = correctAnswer;
    if(image != null) updateFields['image'] = image;
    if(level != null) updateFields['level'] = level;
    if(question != null) updateFields['question'] = question;
    if(section != null) updateFields['section'] = section;
    if(world != null) updateFields['world'] = world;
    
    StoryModeQuestionController.updateStoryModeQuestion(
        storyModeQuestionId,
        updateFields, 
        (err, info) => {
            if(err){
                return res.status(500).send({ message: `${err}`})
            }
            else{
                return res.status(200).send(info)
            }
        }
    )
});

router.delete('/:storyModeQuestionId', (req, res) => {
    const { storyModeQuestionId } = req.params;
    StoryModeQuestionController.deleteStoryModeQuestion(
        storyModeQuestionId,
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