using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;


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

    IEnumerator Start()
    {
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        popUp.SetActive(false);
        yield return StartCoroutine(setAssignmentList());
        tableInitialize();
        popUp.transform.Find("Popup_Delete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(confirmDelete);
        popUp.transform.Find("Popup_Delete").Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(exitDelete);
    }
    IEnumerator setAssignmentList()
    {
        API_Connection conn = new API_Connection();
        print(conn);
        JSONNode jsonNode = null;
        // print("in main: " + json_receivedData.Count);
        yield return StartCoroutine(conn.GetData("Assignment", null, s => {
            jsonNode = JSON.Parse(s);
        }));
        assignmentList = new List<Assignment>();
        for(int i = 0; i < jsonNode.Count; i++){
            assignmentList.Add(new Assignment(jsonNode[i]));
        }
    }
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
            entryTransform.Find("Text_Name").Find("Button_Edit").Find("Text").GetComponent<Text>().text = "View";
            entryTransform.Find("Text_Name").Find("Button_Edit").GetComponent<Button>().onClick.AddListener(() => {
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[chosenAsgIndex-1];
                viewAssignment();
            });
            entryTransform.Find("Text_Name").Find("Button_Delete").Find("Text").GetComponent<Text>().text = "Delete";
            entryTransform.Find("Text_Name").Find("Button_Delete").GetComponent<Button>().onClick.AddListener(() => {
                // This block should be delete
                int chosenAsgIndex = int.Parse(entryTransform.Find("Text_No").GetComponent<Text>().text);
                chosenAsg = assignmentList[i-1];
                deleteAssignment();
            });
            entryTransform.Find("Text_Name").Find("Button_Share").GetComponent<Button>().onClick.AddListener(shareAssignment);
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
    public void viewAssignment()
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
        SceneManager.LoadScene("Assignments");
    }
    public void exitDelete(){
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(false);   
        popUp.SetActive(false);
    }
    public void shareAssignment(){
        print("Still need to connect to API");
    }
}
