using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
public class Tutorial_List_Script : MonoBehaviour
{
    List<string> tutorialList;
    List<string> studentNumberList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;

    //use this for initialization
    void Start () 
    {
       setTutorialListData ();
       setStudentsNumberListData();
       tutorialPopupInitialized ();
    }

    //Update is called once per frame
    void Update ()
    {

    }

    //public void backButtonAction
    //{

    //}

    //public void computerButtonAction
    //{
        //manager.Instance.isComputerVsPlayer = true;
        //SceneManager.LoadScene (2);
    //}


    //Tutorial popup methods
    void setTutorialListData() 
    {
        tutorialList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 10000);
            tutorialList.Add (index.ToString());
        }
    }

    void setStudentsNumberListData() 
    {
        studentNumberList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 100);
            studentNumberList.Add (index.ToString());
        }
    }

    void tutorialPopupInitialized () 
    {
        if (tutorialList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < tutorialList.Count; i++)
            {
                string value = tutorialList [i];
                string studentNumber = studentNumberList [i];
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find ("Cell_Text").GetComponent<Text> ().text=i + ".                  " + value + "                  " + studentNumber + "                  " + "Manage";
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
    }

    public void hideTutorialListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (false);
    }

    public void showTutorialListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (true);
    }



}
*/


public class Tutorial_List_Script : MonoBehaviour
{
    List<string> tutorialList;
    List<string> studentNumberList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;

    //use this for initialization
    void Start () 
    {
       setTutorialListData ();
       setStudentsNumberListData();
       tutorialPopupInitialized ();
    }

    //Update is called once per frame
    void Update ()
    {

    }

    //public void backButtonAction
    //{

    //}

    //public void computerButtonAction
    //{
        //manager.Instance.isComputerVsPlayer = true;
        //SceneManager.LoadScene (2);
    //}


    //Tutorial popup methods
    void setTutorialListData() 
    {
        tutorialList = new List<string> ();
        //tutorialList = tutorialAPI.GetTutorialIndexArray();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 10000);
            tutorialList.Add (index.ToString());
        }
    }

    void setStudentsNumberListData() 
    {
        studentNumberList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 100);
            studentNumberList.Add (index.ToString());
        }
    }

    void tutorialPopupInitialized () 
    {
        if (tutorialList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < tutorialList.Count; i++)
            {
                string value = tutorialList [i];
                string studentNumber = studentNumberList [i];
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find ("Cell_Text").GetComponent<Text> ().text=i + ".                  " + value + "                  " + studentNumber + "                  " + "Manage";
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
    }

    public void hideTutorialListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (false);
    }

    public void showTutorialListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (true);
    }



}
