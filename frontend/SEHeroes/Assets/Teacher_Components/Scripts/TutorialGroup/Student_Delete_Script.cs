using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Student_Delete_Script : MonoBehaviour
{
    List<string> studentList;
    List<string> studentNameList;
    GameObject studentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;

    //use this for initialization
    void Start () 
    {
       setStudentListData ();
       //setStudentsNameListData();
       studentPopupInitialized ();
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


    //Student popup methods
    void setStudentListData() 
    {
        studentList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(10000, 90000);
            studentList.Add (index.ToString());
        }
    }

    /*void setStudentsNameListData() 
    {
        studentNameList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 100);
            studentNameList.Add (index.ToString());
        }
    }
    */

    void studentPopupInitialized () 
    {
        if (studentList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < studentList.Count; i++)
            {
                string value = studentList [i];
                //string studentName = studentNameList [i];
                string studentName = "John Koh Kim Toh";
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find ("Cell_Text").GetComponent<Text> ().text=i + ".                     " + value + "                     " + studentName + "                     " + "Delete";
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
    }

    public void hideStudentListPopUp() 
    {
        studentPopUp.gameObject.SetActive (false);
    }

    public void showStudentListPopUp() 
    {
        studentPopUp.gameObject.SetActive (true);
    }



}
