using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

public class API_StoryMode : MonoBehaviour{
    public static Boolean getStoryQDone;
    public static Boolean updateStoryQDone;
    public static Boolean addStoryQDone;
    public static Boolean deleteStoryQDone;
    public static JSONNode jsonNodeStoryQ;

    public API_StoryMode(){
        getStoryQDone = false;
        updateStoryQDone = false;
        addStoryQDone = false;
        deleteStoryQDone = false;
    }

    public IEnumerator getStoryQ(string storyModeQuestionId){
        getStoryQDone = false;
        API_Connection conn = new API_Connection();
        yield return StartCoroutine(conn.GetData("StoryModeQuestion/" + storyModeQuestionId, null, s => {
            jsonNodeStoryQ = JSON.Parse(s);
        }));
        getStoryQDone = true;
    }
    
    public IEnumerator updateStoryQ(StoryModeQuestion storyModeQ){
        updateStoryQDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(storyModeQ);
        Debug.Log(jsonString + " for StoryModeQuestion question");
        yield return StartCoroutine(conn.PutData("StoryModeQuestion/" + storyModeQ.storyModeQuestionId, jsonString, s => {
            print(JSON.Parse(s));
        }));
        updateStoryQDone = true;
    }
    public IEnumerator addStoryQ(StoryModeQuestion storyModeQ){
        addStoryQDone = false;
        API_Connection conn = new API_Connection();
        string jsonString = JsonUtility.ToJson(storyModeQ);
        Debug.Log(jsonString + " for StoryModeQuestion question");
        yield return StartCoroutine(conn.PostData("StoryModeQuestion/", jsonString, s => {
            storyModeQ.storyModeQuestionId = JSON.Parse(s);
        }));
        addStoryQDone = true;
    }
    public IEnumerator deleteStoryQ(StoryModeQuestion storyModeQ){
        deleteStoryQDone = false;
        API_Connection conn = new API_Connection();
        yield return StartCoroutine(conn.DeleteData("StoryModeQuestion/" + storyModeQ.storyModeQuestionId, s => {
            print(JSON.Parse(s));
        }));
        deleteStoryQDone = true;
    }
}
