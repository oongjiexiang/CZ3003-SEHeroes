using System.Collections;
using System.Collections.Generic;

public class AssignmentQuestion{
    public string question;
    public string assignmentQuestionId;
    public int score;
    public List<string> answer;
    public string correctAnswer;
    public string image;
    public AssignmentQuestion(){
        question = "";
        assignmentQuestionId = "";
        score = 0;
        correctAnswer = "";
        answer = new List<string>();
        for(int i = 0; i < 4; i++){
            answer.Add("");
        }
        image = "";
    }
}