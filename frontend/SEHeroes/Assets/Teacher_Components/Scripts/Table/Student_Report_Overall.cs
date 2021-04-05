using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Student_Report_Overall : MonoBehaviour
{
    
    List<string> assignmentScore;
    GameObject tutorialPopUp;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> assignmentScoreTextArray = new List<string>();
    List<string> allScoreArray = new List<string>();
    List<string> allTriesArray = new List<string>();
    public static string currentStudentMatric= Student_List_Script.matricNumber;
    

    private static string baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentResult?matricNo=" + currentStudentMatric;
    
    //use this for initialization
    void Start () 
    { 
       currentStudentMatric= Student_List_Script.matricNumber;
       baseAssignmentInfoURL = "https://seheroes.herokuapp.com/assignmentResult?matricNo=" + currentStudentMatric;
       StartCoroutine(GetAssignmentInfo());
       
    }

    IEnumerator GetAssignmentInfo()
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
            assignmentScoreTextArray.Add(assignmentInfo[i]["score"]);
            //Debug.Log();
            
        }

        assignmentScore = assignmentScoreTextArray;
        

        
        if (assignmentScore.Count > 0)
        {
 
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            int counter = 0;
            int minScore = 500;
            int maxScore = 0;
            for (int i = 0; i < assignmentScore.Count; i++)
            { 
                counter = counter + System.Convert.ToInt16(assignmentScore[i]);
                if (System.Convert.ToInt16(assignmentScore[i])<minScore)
                {
                    minScore = System.Convert.ToInt16(assignmentScore[i]);
                }

                if (System.Convert.ToInt16(assignmentScore[i])>maxScore)
                {
                    maxScore = System.Convert.ToInt16(assignmentScore[i]);
                }
            }
            float averageFloat = counter/assignmentScore.Count;
            string averageString = averageFloat.ToString();


            GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
            playerTextPanel.transform.SetParent(mainScrollContentView.transform);
            playerTextPanel.transform.localScale = new Vector3(1,1,1);
            playerTextPanel.transform.localPosition = new Vector3(0,0,0);
            playerTextPanel.transform.Find("Text_Average_Score").GetComponent<Text>().text = "Average Assignment Score: " + averageString;
            playerTextPanel.transform.Find("Text_Min_Score").GetComponent<Text>().text = "Minimum Assignment Score: " + minScore;
            playerTextPanel.transform.Find("Text_Max_Score").GetComponent<Text>().text = "Maximum Assignment Score: " + maxScore;
        }

        

    }

    public void viewReport()
    {
        SceneManager.LoadScene("Tutorial_Group_Management");
    }

    public void hideAssignmentScorePopUp() 
    {
        tutorialPopUp.gameObject.SetActive (false);
    }

    public void showAssignmentScorePopUp() 
    {
        tutorialPopUp.gameObject.SetActive (true);
    }



}

