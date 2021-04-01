const admin = require('firebase-admin');
const db = admin.firestore();
const assignmentResultCollection = db.collection("assignmentResult")
const tutorialGroupCollection = db.collection("tutorialGroup")

module.exports['getAssignmentReportByTutorialGroup'] = async function(queryMap, callback) {
    if (!queryMap['tutorialGroupId']) {
        callback('Missing tutorial group ID', null)
        return
    }
    try{
        const report = []
        const record = await tutorialGroupCollection.doc(queryMap['tutorialGroupId']).get()
        var users = []
        if (record.empty) {
            callback('No students in tutorial group', null)
        }
        else {
            for (const user of record.data()['student']) {
                users.push(user)
            }
        }
        const all_data = []
        const availableAssignments = new Set()
        for (const matricNo of users) {
            let selection = null
            if (queryMap['assignmentId'] == null) {
                selection = assignmentResultCollection.where("matricNo", "==", matricNo)
            }
            else {
                selection = assignmentResultCollection.where("matricNo", "==", matricNo).where("assignmentId", "==", queryMap['assignmentId'])
            }
            snapshot = await selection.get()
            if (!snapshot.empty) {
                snapshot.forEach(doc => {
                    all_data.push(doc.data())
                    availableAssignments.add(doc.data()['assignmentId'])
                })
            }
        }
        availableAssignments.forEach((assignmentId, notUsed, set) => {
            const assignmentResult = {}
            const raw_data = []
            const scores = []
            for (const entry of all_data) {
                if (entry['assignmentId'] == assignmentId) {
                    raw_data.push(entry)
                    scores.push(entry['score'])
                }
            }
            scores.sort((a,b) => a-b)
            const med = (scores[(scores.length - 1) >> 1] + scores[scores.length >> 1]) / 2
            assignmentResult['assignmentId'] = assignmentId
            assignmentResult['data'] = {
                min: Math.min(...scores),
                max: Math.max(...scores),
                mean: scores.reduce((a,b) => a+b, 0) / scores.length,
                median: med,
                raw_data: raw_data
            }
            report.push(assignmentResult)          
        })
        callback(null, report)
    } catch(err) {
        callback(err, null)
    }
}
