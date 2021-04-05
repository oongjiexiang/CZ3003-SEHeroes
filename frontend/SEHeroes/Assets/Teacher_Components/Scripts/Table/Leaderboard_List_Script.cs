using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Leaderboard_List_Script : MonoBehaviour
{
    
    List<string> matricList;
    List<string> tutorialGroupList;
    List<string> userNameList;
    List<string> ratingList;
 
    GameObject studentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> matricNumberTextArray = new List<string>();
    List<string> allTutorialGroupArray = new List<string>();
    List<string> allUsernameArray = new List<string>();
    List<string> allRatingArray = new List<string>();
    

    private static string baseLeaderboardInfoURL = "https://seheroes.herokuapp.com/leaderboard";
    
    //use this for initialization
    void Start () 
    { 
       StartCoroutine(GetLeaderBoardInfo());
    }

    IEnumerator GetLeaderBoardInfo()
    {
        //GET students' matriculation number in a tutorial group
        UnityWebRequest leaderboardInfoRequest = UnityWebRequest.Get(baseLeaderboardInfoURL);

        yield return leaderboardInfoRequest.SendWebRequest();

        if (leaderboardInfoRequest.isNetworkError || leaderboardInfoRequest.isHttpError)
        {
            //Debug.LogError(leaderboardInfoRequest.error);
            yield break;
        }

        JSONNode leaderboardInfo = JSON.Parse(leaderboardInfoRequest.downloadHandler.text);

        for (int i = 0; i < leaderboardInfo.Count; i++)
        {
            matricNumberTextArray.Add(leaderboardInfo[i]["matricNo"]);
            allTutorialGroupArray.Add(leaderboardInfo[i]["tutorialGroup"]);
            allUsernameArray.Add(leaderboardInfo[i]["username"]);
            allRatingArray.Add(leaderboardInfo[i]["openChallengeRating"]);
            //Debug.Log();
            
        }
        

        matricList = matricNumberTextArray;
        

        tutorialGroupList = new List<string>();
        tutorialGroupList = allTutorialGroupArray;

        userNameList = new List<string>();
        userNameList = allUsernameArray;

        ratingList = new List<string>();
        ratingList = allRatingArray;
        
        if (matricList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < 10; i++)
            { 
                string value = matricList[i];
                string tutorialGroupValue = tutorialGroupList[i];
                string usernameValue = userNameList[i];
                string challengeRatingValue = ratingList[i];
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_Rank").GetComponent<Text>().text = (i+1).ToString();
                playerTextPanel.transform.Find("Text_Matric").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Tutorial_Group").GetComponent<Text>().text = tutorialGroupValue;
                playerTextPanel.transform.Find("Text_Username").GetComponent<Text>().text = usernameValue;
                playerTextPanel.transform.Find("Text_Challenge_Rating").GetComponent<Text>().text = challengeRatingValue;
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
        

    }

    public void viewStudent()
    {
        SceneManager.LoadScene("Student_Management");
    }

    public void hidematricListPopUp() 
    {
        studentPopUp.gameObject.SetActive (false);
    }

    public void showmatricListPopUp() 
    {
        studentPopUp.gameObject.SetActive (true);
    }



}

