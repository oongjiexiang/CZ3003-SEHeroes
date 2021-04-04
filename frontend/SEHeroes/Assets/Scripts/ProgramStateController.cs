using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramStateController : MonoBehaviour
{
    // Login
    public static bool loggedIn = false;

    // Character Selection Registration
    public static string characterSelected;

    // User Information
    public static string username = "brys0001";
    public static string email;
    public static string matricNo = "U1920410B";
    public static string characterType = "RedWarrior";

    // Game State
    public static string world="Planning";
    public static string section="Introduction";
    public static string level="Hard";
    
    // Assignment
    public static string assID="FdT7HsqGsSfL4Dus5xNq";

    public static string sceneToLoad;
    public static void viewState() {
        // Debug.Log(world);
        // Debug.Log(section);
        // Debug.Log(level);
        // Debug.Log(username);
        // Debug.Log(characterType);
        Debug.Log(characterSelected);
    }
}