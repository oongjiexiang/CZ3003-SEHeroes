using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Input_Script : MonoBehaviour
{
    string stringToEdit = "Hello";
    void OnGUI()
    {
        //stringToEdit = GUILayout.TextField(stringToEdit, 25);
        GUILayout.BeginArea(new Rect(10, 10, 100, 100));
        GUILayout.Button("Click me");
        GUILayout.Button("Or me");
        GUILayout.EndArea();
    }
}
