using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Questions_View_Script : MonoBehaviour
{
    
    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;

    // Start is called before the first frame update
    void Awake()
    {
        // display
        panelObject.transform.Find("World_Section_Subheader_Text").GetComponent<Text>().text = "World/Section: " + Tutorial_List_Script.indexNumber + "/" + Tutorial_List_Script.indexNumber;

    }
}

