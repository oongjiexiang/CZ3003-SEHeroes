using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;


public class Tutorial_List_Script : MonoBehaviour
{
    
    List<string> tutorialList;
    List<JSONNode> studentNumberList;
    //List<string> studentNumberList;
    GameObject tutorialPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<string> indexNumberTextArray = new List<string>();
    List<JSONNode> allStudentsArray = new List<JSONNode>();
    public static string indexNumber;
    

    private readonly string baseTutorialIndexURL = "https://seheroes.herokuapp.com/tutorialGroup";
    
    //use this for initialization
    void Start () 
    { 
       StartCoroutine(GetTutorialIndex());
       
    }

    IEnumerator GetTutorialIndex()
    {
        UnityWebRequest tutorialIndexRequest = UnityWebRequest.Get(baseTutorialIndexURL);

        yield return tutorialIndexRequest.SendWebRequest();

        if (tutorialIndexRequest.isNetworkError || tutorialIndexRequest.isHttpError)
        {
            //Debug.LogError(tutorialIndexRequest.error);
            yield break;
        }

        JSONNode tutorialInfo = JSON.Parse(tutorialIndexRequest.downloadHandler.text);
        
        for (int i = 0; i < tutorialInfo.Count; i++)
        {
            indexNumberTextArray.Add(tutorialInfo[i]["tutorialGroupId"]);
            allStudentsArray.Add(tutorialInfo[i]["student"]);
            //Debug.Log();
            
        }

        tutorialList = indexNumberTextArray;
        

        studentNumberList = new List<JSONNode>();
        studentNumberList = allStudentsArray;
        //Debug.Log(studentNumberList);
        
        if (tutorialList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < tutorialList.Count; i++)
            { 
                string value = tutorialList[i];
                string studentNumber = studentNumberList[i].Count.ToString();
                //Debug.Log(studentNumberList[i].Count);
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_No").GetComponent<Text>().text = i.ToString();
                playerTextPanel.transform.Find("Text_Index").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Students").GetComponent<Text>().text = studentNumber;
                
                playerTextPanel.transform.Find("Text_Students").transform.Find("Button_View").GetComponent<Button>().onClick.AddListener(() => {
                indexNumber = playerTextPanel.transform.Find("Text_Index").GetComponent<Text>().text;
                Debug.Log(indexNumber);
                viewTutorial();
            });
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
        

    }

    public void viewTutorial()
    {
        SceneManager.LoadScene("Tutorial_Group_Management");
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
