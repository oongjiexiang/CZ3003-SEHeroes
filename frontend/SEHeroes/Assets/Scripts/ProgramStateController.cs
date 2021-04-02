using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgramStateController
{
    public static string sceneToLoad;
    
    public static string world;
    public static string section;
    public static string level;
    public static string username = "brys0001";
    public static string characterType = "Magician";

    public static void viewState() {
        Debug.Log(world);
        Debug.Log(section);
        Debug.Log(level);
        Debug.Log(username);
        Debug.Log(characterType);
    }
}
