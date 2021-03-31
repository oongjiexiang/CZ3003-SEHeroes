using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramStateController : MonoBehaviour
{
    public static string world;
    public static string section;
    public static string level;
    public static string username = "brys0001";
    public static string characterType = "RedWarrior";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public static void viewState() {
        Debug.Log(world);
        Debug.Log(section);
        Debug.Log(level);
        Debug.Log(username);
        Debug.Log(characterType);
    }
}
