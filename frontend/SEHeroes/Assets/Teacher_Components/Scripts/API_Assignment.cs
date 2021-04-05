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
    public static Boolean asgQDeleteDone;
    public static Boolean asgQUpdateDone;
    public static List<JSONNode> jsonNodeAsgQ;

    public API_Assignment(){
        jsonNodeAsgQ = new List<JSONNode>();
        asgRequestDone = true;
        asgQRequestDone = true;
        asgQAddDone = true;
        asgQDeleteDone = true;
        asgQUpdateDone = true;
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
    public IEnumerator addQuestion(Assignment asg, AssignmentQuestion asgQuestion){
        asgQAddDone = false;
        API_Connection conn = new API_Connection();
        // AssignmentQuestionForAPI asgQAPI = new AssignmentQuestionForAPI(asgQuestion);
        string jsonString = JsonUtility.ToJson(asgQuestion);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId + "/addQuestion", jsonString, s => {
            asgQuestion.assignmentQuestionId = JSON.Parse(s);
        }));
        asgQAddDone = true;
    }
    public IEnumerator deleteQuestion(Assignment asg, AssignmentQuestion asgQuestion){
        asgQDeleteDone = false;
        API_Connection conn = new API_Connection();
        // AssignmentQuestionForAPI asgQAPI = new AssignmentQuestionForAPI(asgQuestion);
        string jsonString = "{\"assignmentQuestionId: \"" + asgQuestion.assignmentQuestionId + "\"}";
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId + "/removeQuestion", jsonString, s => {
            asgQuestion.assignmentQuestionId = JSON.Parse(s);
        }));
        asgQDeleteDone = true;
    }
    public IEnumerator updateQuestion(AssignmentQuestion asgQuestion){
        asgQUpdateDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(asgQuestion);
        yield return StartCoroutine(conn.PutData("assignmentQUestion/" + asgQuestion.assignmentQuestionId, jsonString, s => {
            print(JSON.Parse(s) == asgQuestion.assignmentQuestionId);
        }));
        asgQUpdateDone = true;
        yield return null;
    }
}
