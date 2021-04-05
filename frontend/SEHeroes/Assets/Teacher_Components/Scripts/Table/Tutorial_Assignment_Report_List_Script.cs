using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Tutorial_Assignment_Report_List_Script : MonoBehaviour
{
    
    List<string> assignmentList;
    List<string> assignmentMinList;
    List<string> assignmentMaxList;
    List<string> assignmentMeanList;
    List<string> assignmentMedianList;
    //List<string> assignmentMinList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> assignmentIdTextArray = new List<string>();
    List<string> allMinArray = new List<string>();
    List<string> allMaxArray = new List<string>();
    List<string> allMeanArray = new List<string>();
     List<string> allMedianArray = new List<string>();
     public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;
    

    private static string baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentReport?tutorialGroupId=" + currentTutorialIndex;
    
    //use this for initialization
    void Start () 
    { 
       currentTutorialIndex= Tutorial_List_Script.indexNumber;
        baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentReport?tutorialGroupId=" + currentTutorialIndex;
       StartCoroutine(GetAssignmentResults());
       
    }

    IEnumerator GetAssignmentResults()
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
            allMinArray.Add(assignmentInfo[i]["data"]["min"]);
            allMaxArray.Add(assignmentInfo[i]["data"]["max"]);
            allMeanArray.Add(assignmentInfo[i]["data"]["mean"]);
            allMedianArray.Add(assignmentInfo[i]["data"]["median"]);
            //Debug.Log();
            
        }

        assignmentList = assignmentIdTextArray;
        

        assignmentMinList = new List<string>();
        assignmentMinList = allMinArray;
        //Debug.Log(assignmentMinList);

        assignmentMaxList = new List<string>();
        assignmentMaxList = allMaxArray;

        assignmentMeanList = new List<string>();
        assignmentMeanList = allMeanArray;

        assignmentMedianList = new List<string>();
        assignmentMedianList = allMedianArray;
        
        if (assignmentList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < assignmentList.Count; i++)
            { 
                string value = assignmentList[i];
                string assignmentMin = assignmentMinList[i];
                string assignmentMax = assignmentMaxList[i];
                string assignmentMean = assignmentMeanList[i];
                string assignmentMedian = assignmentMedianList[i];
                //Debug.Log(assignmentMinList[i].Count);
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_No").GetComponent<Text>().text = i.ToString();
                playerTextPanel.transform.Find("Text_Assignment").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Min").GetComponent<Text>().text = assignmentMin;
                playerTextPanel.transform.Find("Text_Max").GetComponent<Text>().text = assignmentMax;
                playerTextPanel.transform.Find("Text_Mean").GetComponent<Text>().text = assignmentMean;
                playerTextPanel.transform.Find("Text_Median").GetComponent<Text>().text = assignmentMedian;
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