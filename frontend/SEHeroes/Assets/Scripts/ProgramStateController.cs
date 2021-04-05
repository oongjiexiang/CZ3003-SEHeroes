using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class ProgramStateController : MonoBehaviour
{
    // Login
    public static bool loggedIn = false;

    // Character Selection during Registration
    public static string characterSelected;

    // User Information -- Fetch during LOGIN
    public static string username;
    public static string email;
    public static string matricNo = "U1920640A";
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

    public static string sceneToLoad;
    public static void viewState() {
        Debug.Log(world);
        Debug.Log(section);
        Debug.Log(level);
        Debug.Log(username);
        Debug.Log(matricNo);
        // Debug.Log(characterType);
        // Debug.Log(characterSelected);
    }
}