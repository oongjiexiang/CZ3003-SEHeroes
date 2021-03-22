const admin = require("firebase-admin");
const db = admin.firestore();
const storyMode = db.collection("storyModeResult");

module.exports["getStoryModeReport"] = async function (callback) {
  try {
    const result = await storyMode.get();
    const KeySet = new Set();
    result.forEach((doc) => {
      const array = [];
      data = doc.data();
      array.push(data["world"]);
      array.push(data["section"]);
      array.push(data["level"]);
      sKey = "";
      for (i = 0; i < array.length; i++) {
        sKey = sKey + array[i] + " ";
      }

      const res = sKey.split(" ");
      KeySet.add(sKey);
    });
    output = [];
    KeySet.forEach((item) => {
      tmp = item.split(" ");
      world = tmp[0];
      section = tmp[1];
      level = tmp[2];
      const temp = {};
      temp["world"] = world;
      temp["section"] = section;
      temp["level"] = level;
      temp["data"] = { 0: 0, 1: 0, 2: 0, 3: 0 };
      console.log(temp);
      result.forEach((doc) => {
        dat = doc.data();
        if (
          world == dat["world"] &&
          section == dat["section"] &&
          level == dat["level"]
        ) {
          star = String(dat["star"]);
          temp["data"][star] = temp["data"][star] + 1;
        }
      });
      output.push(temp);
    });
    // print out the result
    console.log(output);
    callback(null, output);
    return
  } catch (err) {
    callback(err, null);
  }
};
