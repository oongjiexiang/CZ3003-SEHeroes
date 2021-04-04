using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgramStateController
{
    public static string sceneToLoad;

    // User-Information
    public static string username;
    public static string email;
    public static string characterType;
    public static string matricNo;

    // Login
    public static bool loggedIn = false;

    // Character Selection during Sign-Up
    public static string characterSelected;

    
    // Story Mode State
    public static string world;
    public static string section;
    public static string level;
    

    public static void viewState() {
        // Debug.Log(world);
        // Debug.Log(section);
        // Debug.Log(level);
        // Debug.Log(username);
        // Debug.Log(characterType);
        Debug.Log(characterSelected);
    }
}
