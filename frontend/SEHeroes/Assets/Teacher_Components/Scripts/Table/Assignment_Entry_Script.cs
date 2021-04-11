using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

// Controls Assignments Scene
public class Assignment_Entry_Script : MonoBehaviour
{
    private Transform entryTemplate;
    private Transform entryContainer;
    private GameObject popUp;
    List<Assignment> assignmentList;

    public Text noRecordLabel;
    public GameObject mainContentPanel;
    public GameObject content;

    public static Assignment chosenAsg;
    private API_Assignment conn;

    IEnumerator Start()
    {
        conn = (API_Assignment)transform.GetComponent(typeof(API_Assignment));
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        popUp.SetActive(false);
        yield return StartCoroutine(setAssignmentList());
        tableInitialize();
        popUp.transform.Find("Popup_Delete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(confirmDelete);
        popUp.transform.Find("Popup_Delete").Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(exitDelete);
    }
    // Obtains assignment list from backend
    IEnumerator setAssignmentList()
    {
        API_Connection conn = new API_Connection();
        JSONNode jsonNode = null;
        yield return StartCoroutine(conn.GetData("Assignment", null, s => {
            jsonNode = JSON.Parse(s);
        }));
        assignmentList = new List<Assignment>();
        for(int i = 0; i < jsonNode.Count; i++){
            assignmentList.Add(new Assignment(jsonNode[i]));
        }
    }
    // Share to Telegram
    IEnumerator shareTele(){
        WWWForm form = new WWWForm();
        form.AddField("assignmentId", chosenAsg.assignmentId);

        using (UnityWebRequest www = UnityWebRequest.Post("https://seheroes.herokuapp.com/tele", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError){
                Debug.Log(www.error);
            }
            else{
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    // Share to Twitter
    IEnumerator shareTweet(){
        WWWForm form = new WWWForm();
        form.AddField("assignmentId", chosenAsg.assignmentId);

        using (UnityWebRequest www = UnityWebRequest.Post("https://seheroes.herokuapp.com/tweet", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError){
                Debug.Log(www.error);
            }
            else{
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    // UI Display for the list
    void tableInitialize()
    {
        entryContainer = content.transform.Find("Assignment_Entry_Container");
        entryContainer.gameObject.SetActive(true);
        entryTemplate = entryContainer.Find("Assignment_Entry_Template").transform;
        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 50f;
        for (int i = 0; i < assignmentList.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryTransform.gameObject.SetActive(true);
            entryTransform.Find("Text_No").GetComponent<Text>().text = (i+1).ToString();
            entryTransform.Find("Text_Name").GetComponent<Text>().text = assignmentList[i].assignmentName;
            if(assignmentList[i].dueDate.time() <= DateTime.Now){
                entryTransform.Find("Text_Status").GetComponent<Text>().text = "Passed";
                entryTransform.Find("Text_Status").GetComponent<Text>().color = Color.red;    
            }
            else if(assignmentList[i].startDate.time() <= DateTime.Now){
                entryTransform.Find("Text_Status").GetComponent<Text>().text = "Ongoing till " + assignmentList[i].dueDate.printTime();
                entryTransform.Find("Text_Status").GetComponent<Text>().color = new Color(0.11f, 0.51f, 0.04f, 1.0f);
            }
            else{
                entryTransform.Find("Text_Status").GetComponent<Text>().text = "From " + assignmentList[i].startDate.printTime();
            }
            entryTransform.Find("Text_Name").Find("Button_Edit").Find("Text").GetComponent<Text>().text = "Edit";
            entryTransform.Find("Text_Name").Find("Button_Edit").GetComponent<Button>().onClick.AddListener(() => {
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[chosenAsgIndex-1];
                editAssignment();
            });
            entryTransform.Find("Text_Name").Find("Button_Delete").Find("Text").GetComponent<Text>().text = "Delete";
            entryTransform.Find("Text_Name").Find("Button_Delete").GetComponent<Button>().onClick.AddListener(() => {
                // This block should be delete
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[chosenAsgIndex-1];
                deleteAssignment();
            });
            entryTransform.Find("Text_Name").Find("Button_Share_Tele").GetComponent<Button>().onClick.AddListener(() => {
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[chosenAsgIndex-1];
                shareAssignmentTele();
            });
            entryTransform.Find("Text_Name").Find("Button_Share_Tweet").GetComponent<Button>().onClick.AddListener(() => {
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[chosenAsgIndex-1];
                shareAssignmentTweet();
            });
            entryTransform.localScale = new Vector2(1, 1);
            entryTransform.localPosition = new Vector2(0, -templateHeight * i);
        }
        if (assignmentList.Count == 0)
        {
            noRecordLabel.gameObject.SetActive(true);
        }
        else
        {
            noRecordLabel.gameObject.SetActive(false);
        }
    }
    // Event called when Telegram icon is clicked
    void shareAssignmentTele(){
        StartCoroutine(shareTele());
    }
    // Event called when Telegram icon is clicked
    void shareAssignmentTweet(){
        StartCoroutine(shareTweet());
    }
    public void editAssignment()
    {
        SceneManager.LoadScene("Assignment_Edit_Meta");
    }
    public void scheduleAssignment()
    {
        SceneManager.LoadScene("Assignments_Schedule");
    }
    public void deleteAssignment()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(true);   
    }
    public void confirmDelete(){
        // delete request to backend here
        StartCoroutine(conn.deleteAssignment(chosenAsg));
        SceneManager.LoadScene("Assignments");
    }
    public void exitDelete(){
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(false);   
        popUp.SetActive(false);
    }
}
