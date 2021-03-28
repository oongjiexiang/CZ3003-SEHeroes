using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Tutorial_View_Script : MonoBehaviour
{
    
    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;

    // Start is called before the first frame update
    void Awake()
    {
        // display
        panelObject.transform.Find("Tutorial_Subheader_Text").GetComponent<Text>().text = "Tutorial Index: " + Tutorial_List_Script.indexNumber;

    }
}

