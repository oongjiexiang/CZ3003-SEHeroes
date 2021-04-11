using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;
using UnityEngine;
using System;

// Assignment Questions are stored in the database
[Serializable]
public class AssignmentQuestion{
    public string question;
    [System.NonSerialized]
    public string assignmentQuestionId;
    public int score;
    public List<string> answer;
    public int correctAnswer;
    public AssignmentQuestion(){
        question = "";
        assignmentQuestionId = "";
        score = 0;
        correctAnswer = -1;
        answer = new List<string>();
        for(int i = 0; i < 4; i++){
            answer.Add("");
        }
    }
    public AssignmentQuestion(JSONNode jsonAsgQ){
        question = jsonAsgQ["question"];
        assignmentQuestionId = jsonAsgQ["assignmentQuestionId"];
        score = jsonAsgQ["score"];
        correctAnswer = int.Parse(jsonAsgQ["correctAnswer"]);
        answer = new List<string>();
        for(int i = 0; i < jsonAsgQ["answer"].Count; i++){
            answer.Add(jsonAsgQ["answer"][i]);
        }
    }
}

// This class is created to cater to the correct serialized Assignment Question JSON format that the backend expects
public class AssignmentQuestionForAPI{
    public AssignmentQuestion question;
    public AssignmentQuestionForAPI(AssignmentQuestion asgQ){
        question = asgQ;
    }
}