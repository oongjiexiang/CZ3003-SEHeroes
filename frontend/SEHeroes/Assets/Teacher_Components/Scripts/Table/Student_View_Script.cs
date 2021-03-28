using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Student_View_Script : MonoBehaviour
{
    
    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;

    // Start is called before the first frame update
    void Awake()
    {
        // display
        panelObject.transform.Find("Student_Subheader_Text").GetComponent<Text>().text = Student_List_Script.studentUsername + " (" + Student_List_Script.matricNumber + ")";

    }
}

