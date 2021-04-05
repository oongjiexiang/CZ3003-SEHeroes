using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class API_Assignment : MonoBehaviour{
    public static Boolean asgRequestDone;
    public static Boolean asgQRequestDone;
    public static Boolean asgQListRequestDone;
    public static Boolean asgQAddDone;
    public static List<JSONNode> jsonNodeAsgQ;

    public API_Assignment(){
        jsonNodeAsgQ = new List<JSONNode>();
        asgRequestDone = true;
        asgQRequestDone = true;
        asgQAddDone = true;
    }

    // public Assignment getAssignment(string assignmentId){
    //     API_Connection conn = new API_Connection();
    //     JSONNode jsonNode = null;
    //     // print("in main: " + json_receivedData.Count);
    //     yield return StartCoroutine(conn.GetData("assignment/", null, s => {
    //         jsonNode = JSON.Parse(s);
    //     }));
    //     return new Assignment(jsonNode);
    // }
    public IEnumerator getAssignmentQuestion(string assignmentQuestionId){
        JSONNode jsonNode = null;
        API_Connection conn = new API_Connection();
        asgQRequestDone = false;
        yield return StartCoroutine(conn.GetData("assignmentQuestion/" + assignmentQuestionId, null, s => {    // change link
            jsonNode = JSON.Parse(s);
            jsonNodeAsgQ.Add(jsonNode);
        }));
        asgQRequestDone = true;
    }
    public IEnumerator getAssignmentQuestionList(Assignment asg){
        JSONNode jsonNode = null;
        API_Connection conn = new API_Connection();
        asgQListRequestDone = false;
        Dictionary<string, string> queryParams = new Dictionary<string, string>();
        queryParams.Add("assignmentId", asg.assignmentId);
        yield return StartCoroutine(conn.GetData("assignmentQuestion/", queryParams, s => {    // change link
            jsonNode = JSON.Parse(s);
            jsonNodeAsgQ.Add(jsonNode);
        }));
        asgQListRequestDone = true;
    }
    public IEnumerator saveBack(Assignment asg){
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(asg);
        Debug.Log(jsonString + " for assignment");
        yield return StartCoroutine(conn.PostData("assignment", jsonString, s => {
            Debug.Log(s);
            Debug.Log(JSON.Parse(s));
            asg.assignmentId = JSON.Parse(s);
        }));
        Debug.Log(asg.assignmentId + " is this assignment's id ");
        // yield return null;
    }
    // public IEnumerator createAssignment(Assignment asg, List<AssignmentQuestion> asgQuestion){
    //     Debug.Log("");
    // }
    public IEnumerator addQuestion(Assignment asg, AssignmentQuestion asgQuestion){
        asgQAddDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(asgQuestion);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId + "/addQuestion", jsonString, s => {
            Debug.Log(s);
            Debug.Log(JSON.Parse(s));
            asgQuestion.assignmentQuestionId = JSON.Parse(s);
        }));
        Debug.Log(asgQuestion.assignmentQuestionId + " is this question's id ");
        asgQAddDone = true;
        // yield return null;
    }
}
