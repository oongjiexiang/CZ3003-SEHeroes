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
    public static string username = "brys0001";
    public static string email;
    public static string matricNo = "U13572468";
    public static string characterType = "Magician";

    // Lock State
    public static List<JSONNode> allUnlockedState = new List<JSONNode>();

    // Game State
    public static string world="Planning";
    public static string section="Introduction";
    public static string level="Easy";
    
    // Assignment
    public static string assName;
    public static string assID="FdT7HsqGsSfL4Dus5xNq";

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