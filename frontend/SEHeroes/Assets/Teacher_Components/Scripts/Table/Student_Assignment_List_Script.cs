using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Student_Assignment_List_Script : MonoBehaviour
{
    
    List<string> assignmentList;
    List<string> assignmentScoreList;
    List<string> assignmentTriesList;
    //List<string> assignmentScoreList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> assignmentIdTextArray = new List<string>();
    List<string> allScoreArray = new List<string>();
    List<string> allTriesArray = new List<string>();
    public static string currentStudentMatric= Student_List_Script.matricNumber;
    

    private static string baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentResult?matricNo=" + currentStudentMatric;
    
    //use this for initialization
    void Start () 
    { 
       currentStudentMatric= Student_List_Script.matricNumber;
        baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentResult?matricNo=" + currentStudentMatric; 
       StartCoroutine(GetAssignmentId());
       
    }

    IEnumerator GetAssignmentId()
    {
        UnityWebRequest assignmentInfoRequest = UnityWebRequest.Get(baseAssignmentInfoURL);

        yield return assignmentInfoRequest.SendWebRequest();

        if (assignmentInfoRequest.isNetworkError || assignmentInfoRequest.isHttpError)
        {
            //Debug.LogError(assignmentInfoRequest.error);
            yield break;
        }

        JSONNode assignmentInfo = JSON.Parse(assignmentInfoRequest.downloadHandler.text);
        
        for (int i = 0; i < assignmentInfo.Count; i++)
        {
            assignmentIdTextArray.Add(assignmentInfo[i]["assignmentName"]);
            allScoreArray.Add(assignmentInfo[i]["score"]);
            allTriesArray.Add(assignmentInfo[i]["tried"]);
            //Debug.Log();
            
        }

        assignmentList = assignmentIdTextArray;
        

        assignmentScoreList = new List<string>();
        assignmentScoreList = allScoreArray;
        //Debug.Log(assignmentScoreList);

        assignmentTriesList = new List<string>();
        assignmentTriesList = allTriesArray;
        
        if (assignmentList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < assignmentList.Count; i++)
            { 
                string value = assignmentList[i];
                string assignmentScore = assignmentScoreList[i];
                string assignmentTry = assignmentTriesList[i];
                //Debug.Log(assignmentScoreList[i].Count);
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_No").GetComponent<Text>().text = i.ToString();
                playerTextPanel.transform.Find("Text_Assignment").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Score").GetComponent<Text>().text = assignmentScore;
                playerTextPanel.transform.Find("Text_Tries").GetComponent<Text>().text = assignmentTry;
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
        

    }

    public void viewReport()
    {
        SceneManager.LoadScene("Tutorial_Group_Management");
    }

    public void hideAssignmentListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (false);
    }

    public void showAssignmentListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (true);
    }



}


/*
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
    

    void setAssignmentsGradeListData() 
    {
        assignmentGradeList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(0, 100);
            assignmentGradeList.Add (index.ToString());
        }
    }
    

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


*/