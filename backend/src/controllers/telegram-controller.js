/**
 * Controller for telegram related logic.
 * @module telegram-controller
 * @category controller
 */

process.env.NTBA_FIX_319 = 1;
const TelegramBot = require("node-telegram-bot-api");
const { telegramToken } = require("../../config");

// replace the value below with the Telegram token you receive from @BotFather
const token = telegramToken;
const chatId = -1001403838304;
// Create a bot that uses 'polling' to fetch new updates

/**
 * Post announcement in telegram group. The announcement will include a URL link that shows all of the assignment question.
 * Example: https://seheroes.herokuapp.com/assignmentViewer/NxJOfby4fzYteZTSbWe7
 * @param {Object} req - Assignment detail include assignmentName, dueDate, tries and assignmentId.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports["makeAnnouncement"] = async function (req, callback) {
    try {
        // check whether the tutorial group with such group id exists, if yes, ignore else create new group
        let assignmentName = req['assignmentName'];
        let dueDate = req['dueDate'].toString();
        let tries = req['tries']
        let assignmentId = req['assignmentId']
        //console.log("Running now");
        let sentence = assignmentName + " " + "is due on " + dueDate + 
                                    ". You have up to " + tries + " attempt. Go ahead and complete it now!\n\n"
                                    +`You can view the questions at https://seheroes.herokuapp.com/assignmentViewer/${assignmentId}`;
        //console.log(sentence);
        const bot = new TelegramBot(token);
        bot.sendMessage(chatId, sentence);

        callback(null, "Message sent");
    } catch (err) {
        callback(err, null);
    }
};