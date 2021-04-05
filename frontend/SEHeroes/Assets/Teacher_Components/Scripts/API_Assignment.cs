using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class API_Assignment : MonoBehaviour{
    // API_Connection conn;
    // public API_Assignment(){
    //     conn = new API_Connection();
    // }
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
    public IEnumerator saveBack(AssignmentQuestion asgQuestion){
        API_Connection conn = new API_Connection();
        Debug.Log(new API_Connection());
        string jsonString = JsonUtility.ToJson(asgQuestion);
        Debug.Log(conn);
        Debug.Log(jsonString + " for assignment question");
        yield return StartCoroutine(conn.PostData("assignmentQuestion", jsonString, s => {
            Debug.Log(s);
            Debug.Log(JSON.Parse(s));
            asgQuestion.assignmentQuestionId = JSON.Parse(s);
        }));
        Debug.Log(asgQuestion.assignmentQuestionId + " is this question's id ");
        // yield return null;
    }
}
