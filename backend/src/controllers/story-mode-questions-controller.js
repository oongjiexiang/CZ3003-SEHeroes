/**
 * Controller for story mode question related logic.
 * @module story-mode-question-controller
 * @category controller
 */

const admin = require('firebase-admin');
const db = admin.firestore();
const storyModeQuestionCollection = db.collection("storyModeQuestion")

/**
 * Create story mode question and store into the database. Story mode question must have necessary field.
 * @param {Object} record - New story mode question details, including question, answer, correctAnswer, world, level and section.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.createStoryModeQuestion= async function(record, callback) {

    if (record['answer'] == null || record['correctAnswer'] == null|| record['level'] == null
                || record['question']== null || record['section']== null || record['world'] == null) {
        callback('Missing fields', null)
        return
    }
    if(!record['image']) delete record['image']

    try{
        const reply = await storyModeQuestionCollection.add(record)
        callback(null, reply.id)
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Get all story mode question data that satisfy filter.
 * @param {Object} queryMap - Filter to query, can include world, section and level.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getAllStoryModeQuestions= async function(queryMap, callback) {
    try{ 
        let questions = storyModeQuestionCollection
        for (const key in queryMap) {
            questions = questions.where(key, "==", queryMap[key])
        }
        const snapshot = await questions.get()
        if (snapshot.empty) {
            callback('No data', null)
        }
        else {
            const res = []
            snapshot.forEach(doc => {
                const data = doc.data();
                data['storyModeQuestionId'] = doc.id;
                res.push(data);
            })
            callback(null, res)
        }
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Get story mode question data by storyModeQuestionId.
 * @param {String} storyModeQuestionId - StoryModeQuestionId of story mode question.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.getStoryModeQuestion= async function(storyModeQuestionId, callback) {
    try{ 
        const question = await storyModeQuestionCollection.doc(storyModeQuestionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else {
            const data = question.data();
            data['storyModeQuestionId'] = storyModeQuestionId;
            callback(null, data)
        }
    } catch(err) {
        callback(err, null)
    }
}

/**
 * Update story mode question by storyModeQuestionId, updated field name must be valid.
 * @param {String} storyModeQuestionId - StoryModeQuestionId of story mode question to be updated.
 * @param {Object} updateFields - Object include new data to update.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.updateStoryModeQuestion= async function(storyModeQuestionId, updateFields, callback) {
    try{
        const question = await storyModeQuestionCollection.doc(storyModeQuestionId).get()
        if (!question.exists) {
            callback('No such question found', null)
        }
        else{
            const res = await storyModeQuestionCollection.doc(storyModeQuestionId).update(updateFields)
            callback(null, "Update successfully")
        }
    } catch(err) {
        callback(err, null)
    }
}


/**
 * Delete story mode question by storyModeQuestionId.
 * @param {String} storyModeQuestionId - StoryModeQuestionId of story mode question to be deleted.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports.deleteStoryModeQuestion= async function(storyModeQuestionId, callback) {
    try{
        const res = await storyModeQuestionCollection.doc(storyModeQuestionId).delete()
        callback(null, "Delete successfully") 
    } catch(err) {
        callback(err, null)
    }
}