using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

// Handles GET, POST, PUT and DELETE requests for Story Mode Questions
public class API_Storymode : MonoBehaviour{
    public static Boolean getStoryQDone;
    public static Boolean updateStoryQDone;
    public static Boolean addStoryQDone;
    public static Boolean deleteStoryQDone;
    public static JSONNode jsonNodeStoryQ;

    public API_Storymode(){
        getStoryQDone = false;
        updateStoryQDone = false;
        addStoryQDone = false;
        deleteStoryQDone = false;
    }

    // Gets story mode question by its ID
    public IEnumerator getStoryQ(string storyModeQuestionId){
        getStoryQDone = false;
        API_Connection conn = new API_Connection();
        yield return StartCoroutine(conn.GetData("StoryModeQuestion/" + storyModeQuestionId, null, s => {
            jsonNodeStoryQ = JSON.Parse(s);
        }));
        getStoryQDone = true;
    }
    
    // Updates an existing story mode question
    public IEnumerator updateStoryQ(StoryModeQuestion storyModeQ){
        updateStoryQDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(storyModeQ);
        yield return StartCoroutine(conn.PutData("StoryModeQuestion/" + storyModeQ.storyModeQuestionId, jsonString, s => {
            Debug.Log(JSON.Parse(s));
        }));
        updateStoryQDone = true;
    }

    // Adds a story mode question
    public IEnumerator addStoryQ(StoryModeQuestion storyModeQ){
        addStoryQDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(storyModeQ);
        yield return StartCoroutine(conn.PostData("StoryModeQuestion/", jsonString, s => {
            storyModeQ.storyModeQuestionId = JSON.Parse(s);
        }));
        addStoryQDone = true;
    }

    // Deletes a story mode question
    public IEnumerator deleteStoryQ(StoryModeQuestion storyModeQ){
        deleteStoryQDone = false;
        API_Connection conn = new API_Connection();
        yield return StartCoroutine(conn.DeleteData("StoryModeQuestion/" + storyModeQ.storyModeQuestionId, s => {
            Debug.Log(JSON.Parse(s));
        }));
        deleteStoryQDone = true;
    }
}
