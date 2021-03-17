using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Student_Assignment_List_Script : MonoBehaviour
{
    List<string> assignmentList;
    List<string> assignmentGradeList;
    GameObject assignmentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;

    //use this for initialization
    void Start () 
    {
       setAssignmentListData ();
       //setAssignmentsNameListData();
       assignmentPopupInitialized ();
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


    //Assignment popup methods
    void setAssignmentListData() 
    {
        assignmentList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(1, 20);
            assignmentList.Add (index.ToString());
        }
    }
    

    /*void setAssignmentsGradeListData() 
    {
        assignmentGradeList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 100);
            assignmentGradeList.Add (index.ToString());
        }
    }
    */

    void assignmentPopupInitialized () 
    {
        if (assignmentList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < assignmentList.Count; i++)
            {
                string value = assignmentList [i];
                //string assignmentGrade = assignmentGradeList [i];
                string assignmentGrade = "A";
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find ("Cell_Text").GetComponent<Text> ().text=i + ".                     " + "Assignment " + value + "                     " + assignmentGrade + "                     " + "See Report";
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
    }

    public void hideAssignmentListPopUp() 
    {
        assignmentPopUp.gameObject.SetActive (false);
    }

    public void showAssignmentListPopUp() 
    {
        assignmentPopUp.gameObject.SetActive (true);
    }



}
