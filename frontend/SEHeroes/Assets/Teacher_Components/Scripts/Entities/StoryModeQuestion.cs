using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

[Serializable]
public class StoryModeQuestion{
    public string section;
    public List<string> answer;
    public string question;
    public string level;
    public string world;
    public int correctAnswer;
    public string storyModeQuestionId;  
    public StoryModeQuestion(JSONNode jsonStoryQ){
        answer = new List<string>();
        section = jsonStoryQ["section"];
        question = jsonStoryQ["answer"];
        level = jsonStoryQ["level"];
        world = jsonStoryQ["world"];
        storyModeQuestionId = jsonStoryQ["storyModeQuestionId"];
        correctAnswer = int.Parse(jsonStoryQ["correctAnswer"]);
        for(int i = 0; i < jsonStoryQ["answer"].Count; i++){
            answer.Add(jsonStoryQ["answer"][i]);
        }
    }  
}