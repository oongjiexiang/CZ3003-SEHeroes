/**
 * Controller for story mode report related logic.
 * @module story-mode-report-controller
 * @category controller
 */


const admin = require("firebase-admin");
const db = admin.firestore();
const storyModeResultCollection = db.collection("storyModeResult");
const tutorialGroupCollection = db.collection('tutorialGroup');



/**
 * Get story mode report.
 * @param {Object} queryMap - Object of filter, field can include tutorialGroupId and matricNo.
 * @param {requestCallback} callback - A callback to return http response.
 */
module.exports["getStoryModeReport"] = async function (queryMap, callback) {
    try {
        const result = await storyModeResultCollection.get();
        let resultData = []
        result.forEach((doc) => resultData.push(doc.data()));

        if(queryMap['tutorialGroupId'] != null){
            const record = await tutorialGroupCollection.doc(queryMap['tutorialGroupId']).get();
            if (!record.exists) {
                callback("Tutorial group not exist", null);
                return;
            }
            else {
                const student = record.data()['student'];

                resultData = resultData.filter(res => ((res['matricNo'] != null) && student.includes(res['matricNo'])));
            }
        }

        const KeySet = new Set();
        resultData.forEach((data) => {
            const array = [];
            array.push(data["world"]);
            array.push(data["section"]);
            array.push(data["level"]);
            let sKey = "";
            for (i = 0; i < array.length; i++) {
                sKey = sKey + array[i] + " ";
            }

            const res = sKey.split(" ");
            KeySet.add(sKey);
        });
        let output = [];
        KeySet.forEach((item) => {
            let tmp = item.split(" ");
            let world = tmp[0];
            let section = tmp[1];
            let level = tmp[2];
            const temp = {};
            temp["world"] = world;
            temp["section"] = section;
            temp["level"] = level;
            temp["data"] = { 0: 0, 1: 0, 2: 0, 3: 0 };
            //console.log(temp);
            resultData.forEach((dat) => {
                if (
                    world == dat["world"] &&
                    section == dat["section"] &&
                    level == dat["level"]
                ) {
                    let star = String(dat["star"]);
                    temp["data"][star] = temp["data"][star] + 1;
                }
            });
            output.push(temp);
        });
        // print out the result
        //console.log(output);
        callback(null, output);
        return
    } catch (err) {
        callback(err, null);
    }
};
