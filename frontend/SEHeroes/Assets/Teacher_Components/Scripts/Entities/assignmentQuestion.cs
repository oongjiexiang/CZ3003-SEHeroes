using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;
using UnityEngine;

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
}