using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class World_Schedule_List : MonoBehaviour
{
    
    List<string> worldList;
    List<string> sectionList;
    List<string> tutorialGroupList;
    
    List<JSONNode> unlockDateList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> worldTextArray = new List<string>();
    List<string> allSectionArray = new List<string>();
    List<string> allTutorialGroupArray = new List<string>();
    List<JSONNode> allUnlockDateArray = new List<JSONNode>();

    

    private static string baseWorldScheduleInfoURL = "https://seheroes.herokuapp.com/world";
    
    //use this for initialization
    void Start () 
    { 
       StartCoroutine(GetWorldSchedule());
       
    }

    IEnumerator GetWorldSchedule()
    {
        UnityWebRequest worldScheduleInfoRequest = UnityWebRequest.Get(baseWorldScheduleInfoURL);

        yield return worldScheduleInfoRequest.SendWebRequest();

        if (worldScheduleInfoRequest.isNetworkError || worldScheduleInfoRequest.isHttpError)
        {
            //Debug.LogError(worldScheduleInfoRequest.error);
            yield break;
        }

        JSONNode worldScheduleInfo = JSON.Parse(worldScheduleInfoRequest.downloadHandler.text);
        
        for (int i = 0; i < worldScheduleInfo.Count; i++)
        {
            worldTextArray.Add(worldScheduleInfo[i]["world"]);
            allSectionArray.Add(worldScheduleInfo[i]["section"]);
            allTutorialGroupArray.Add(worldScheduleInfo[i]["tutorialGroupId"]);
            allUnlockDateArray.Add(worldScheduleInfo[i]["unlockDate"]);
            //Debug.Log();
            
        }

        worldList = worldTextArray;
        

        sectionList = new List<string>();
        sectionList = allSectionArray;
        //Debug.Log(sectionList);

        tutorialGroupList = new List<string>();
        tutorialGroupList = allTutorialGroupArray;

        unlockDateList = new List<JSONNode>();
        unlockDateList = allUnlockDateArray;
        
        if (worldList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < worldList.Count; i++)
            { 
                string value = worldList[i];
                string currentSection = sectionList[i];
                string currentTutorialGroup = tutorialGroupList[i];
                int currentUnlockYear = unlockDateList[i]["year"];
                int currentUnlockMonth = unlockDateList[i]["month"];
                int currentUnlockDay = unlockDateList[i]["day"];
                int currentUnlockHour = unlockDateList[i]["hour"];
                int currentUnlockMinute = unlockDateList[i]["minute"];
                //Debug.Log(sectionList[i].Count);
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_World").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Section").GetComponent<Text>().text = currentSection;
                playerTextPanel.transform.Find("Text_Tutorial_Group").GetComponent<Text>().text = currentTutorialGroup;
                playerTextPanel.transform.Find("Text_Date_Time").GetComponent<Text>().text = currentUnlockYear.ToString() + '/' + currentUnlockMonth.ToString() + '/' + currentUnlockDay.ToString() + ' ' + currentUnlockHour.ToString() + ':' + currentUnlockMinute.ToString();
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

    public void hideworldListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (false);
    }

    public void showworldListPopUp() 
    {
        tutorialPopUp.gameObject.SetActive (true);
    }



}


/*
public class Student_Assignment_List_Script : MonoBehaviour
{
    List<string> worldList;
    List<string> assignmentGradeList;
    GameObject assignmentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;

    //use this for initialization
    void Start () 
    {
       setworldListData ();
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
    void setworldListData() 
    {
        worldList = new List<string> ();
        int index;
        for (int i = 0; i < 15; i++)
        {
            index = Random.Range(1, 20);
            worldList.Add (index.ToString());
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
        if (worldList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < worldList.Count; i++)
            {
                string value = worldList [i];
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

    public void hideworldListPopUp() 
    {
        assignmentPopUp.gameObject.SetActive (false);
    }

    public void showworldListPopUp() 
    {
        assignmentPopUp.gameObject.SetActive (true);
    }



}


*/