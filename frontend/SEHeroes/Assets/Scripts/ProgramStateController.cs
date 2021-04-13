using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class ProgramStateController : MonoBehaviour
{
    // Login
    public static bool loggedIn = false;

    // Character Selection Registration
    public static string characterSelected;

    // User Information
    public static string username;
    public static string email;
    public static string matricNo;
    public static string characterType;

    // Lock State
    public static List<JSONNode> allUnlockedState = new List<JSONNode>();

    // Game State
    public static string world;
    public static string section;
    public static string level;
    
    // Assignment
    public static string assName;
    public static string assID;

    public static Vector3 player1Pos = new Vector3(-6.87f,-0.6f,302f);
    public static Vector3 player2Pos = new Vector3(6.87f,-0.6f,302f);
    public static string sceneToLoad;
    
    public static void viewState() {
        // Debug.Log(world);
        // Debug.Log(section);
        // Debug.Log(level);
        // Debug.Log(username);
        // Debug.Log(matricNo);
        // Debug.Log(characterType);
        // Debug.Log(characterSelected);
    }
}