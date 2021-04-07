using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

public class API_Assignment : MonoBehaviour{
    public static Boolean asgRequestDone;
    public static Boolean asgQRequestDone;
    public static Boolean asgQListRequestDone;
    public static Boolean asgQAddDone;
    public static Boolean asgQDeleteDone;
    public static Boolean asgQUpdateDone;
    public static Boolean asgAddDone;
    public static Boolean asgDeleteDone;
    public static Boolean asgUpdateDone;
    public static List<JSONNode> jsonNodeAsgQ;

    public API_Assignment(){
        jsonNodeAsgQ = new List<JSONNode>();
        asgRequestDone = true;
        asgQRequestDone = true;
        asgQAddDone = true;
        asgQDeleteDone = true;
        asgQUpdateDone = true;
        asgQAddDone = true;
        asgDeleteDone = true;
        asgUpdateDone = true;
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
    
    public IEnumerator addAssignment(Assignment asg, List<AssignmentQuestion> asgQuestions){
        asgAddDone = false;
        API_Connection conn = new API_Connection();
        AssignmentForAPI asgAPI = new AssignmentForAPI(asg, asgQuestions);
        string jsonString = JsonUtility.ToJson(asgAPI);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PostData("assignment/", jsonString, s => {
            // asgQuestion.assignmentQuestionId = 
            print(JSON.Parse(s));
        }));
        // yield return null;
        asgQAddDone = true;
    }
    public IEnumerator updateAssignment(Assignment asg){
        asgUpdateDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(asg);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId, jsonString, s => {
            print(JSON.Parse(s));
        }));
        asgUpdateDone = true;
    }
    public IEnumerator deleteAssignment(Assignment asg){
        print(asg.assignmentId);
        asgDeleteDone = false;
        API_Connection conn = new API_Connection();
        yield return StartCoroutine(conn.DeleteData("assignment/" + asg.assignmentId, s => {
            print(JSON.Parse(s));
        }));
        yield return null;
        asgQDeleteDone = true;
    }
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
        string jsonString = JsonUtility.ToJson(asgQuestion);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId + "/addQuestion", jsonString, s => {
            asgQuestion.assignmentQuestionId = JSON.Parse(s);
        }));
        asgQAddDone = true;
    }
    public IEnumerator deleteQuestion(Assignment asg, AssignmentQuestion asgQuestion){
        // asgQDeleteDone = false;
        // API_Connection conn = new API_Connection();
        string jsonString = "{\"assignmentQuestionId: \"" + asgQuestion.assignmentQuestionId + "\"}";
        // Debug.Log(jsonString + " for assignment question");
        // yield return StartCoroutine(conn.PutData("assignment/" + asg.assignmentId + "/removeQuestion", jsonString, s => {
        //     asgQuestion.assignmentQuestionId = JSON.Parse(s);
        // }));
        // asgQDeleteDone = true;
        // byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);
        // byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json_add_tutorial);
        //     tutorialAddRequest.SetRequestHeader("Content-Type", "application/json");
        // using (UnityWebRequest www = UnityWebRequest.Put("https://seheroes.herokuapp.com/assignment/" + asg.assignmentId + "/removeQuestion", myData))
        // {
        //     yield return www.SendWebRequest();

        //     if (www.isNetworkError){
        //         Debug.Log(www.error);
        //     }
        //     else{
        //         Debug.Log(www.downloadHandler.text);
        //     }
        // }
        AssignmentQuestionIdForAPI asgId = new AssignmentQuestionIdForAPI(asgQuestion);
        string json = JsonUtility.ToJson(asgId);
        var assignmentQDeleteRequest =new  UnityWebRequest("https://seheroes.herokuapp.com/assignment/" + asg.assignmentId + "/removeQuestion", "PUT");
    
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        assignmentQDeleteRequest.SetRequestHeader("Content-Type", "application/json");
        assignmentQDeleteRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        assignmentQDeleteRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return assignmentQDeleteRequest.SendWebRequest();
        Debug.Log("Status Code: " + assignmentQDeleteRequest.responseCode);

        // yield return assignmentQDeleteRequest.SendWebRequest();

        if (assignmentQDeleteRequest.isNetworkError)
        {
            Debug.Log(assignmentQDeleteRequest.error);
        }
        else
        {
            Debug.Log(assignmentQDeleteRequest.downloadHandler.text);
        }
    }
    public IEnumerator updateQuestion(AssignmentQuestion asgQuestion){
        asgQUpdateDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(asgQuestion);
        yield return StartCoroutine(conn.PutData("assignmentQUestion/" + asgQuestion.assignmentQuestionId, jsonString, s => {
            print(JSON.Parse(s) == asgQuestion.assignmentQuestionId);
        }));
        asgQUpdateDone = true;
    }
}
