using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

// Story Mode Questions are stored in the database
[Serializable]
public class StoryModeQuestion{
    public string section;
    public List<string> answer;
    public string question;
    public string level;
    public string world;
    public int correctAnswer;
    [System.NonSerialized]
    public string storyModeQuestionId;  
    public StoryModeQuestion(JSONNode jsonStoryQ){
        answer = new List<string>();
        section = jsonStoryQ["section"];
        question = jsonStoryQ["question"];
        level = jsonStoryQ["level"];
        world = jsonStoryQ["world"];
        storyModeQuestionId = jsonStoryQ["storyModeQuestionId"];
        correctAnswer = int.Parse(jsonStoryQ["correctAnswer"]);
        for(int i = 0; i < jsonStoryQ["answer"].Count; i++){
            answer.Add(jsonStoryQ["answer"][i]);
        }
    }  
    public StoryModeQuestion(string questionId){
        this.storyModeQuestionId = questionId;
    }
    public StoryModeQuestion(string world, string section){
        this.world = world;
        this.section = section;
        answer = new List<string>();
        correctAnswer = 0;
        storyModeQuestionId = "";
        question = "";
        level = "Easy";
    }
}