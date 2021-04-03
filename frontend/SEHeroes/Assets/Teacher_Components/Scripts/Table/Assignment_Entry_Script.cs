using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Assignment_Entry_Script : MonoBehaviour
{
    private Transform entryTemplate;
    private Transform entryContainer;
    private GameObject popUp;
    List<string> assignmentNames;
    List<string> assignmentStatus;

    public Text noRecordLabel;
    public GameObject mainContentPanel;
    public GameObject content;

    public static string assignmentName;

    IEnumerator Start()
    {
        yield return StartCoroutine(setAssignmentList());
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        popUp.SetActive(false);
        tableInitialize();
    }

    IEnumerator setAssignmentList()
    {
        API_Connection conn = new API_Connection();
        yield return conn.GetData("user");
        yield return conn.DeleteData("assignment/B4AF3RCBqaxTzbx9rDtx");
        assignmentNames = new List<string>();
        assignmentStatus = new List<string>();
        var list = new List<string> { "Ongoing", "Closed", "Scheduled" };

        int week = 0;
        var rd = new System.Random();
        for (int i = 0; i < 20; i++)
        {
            week++;
            assignmentNames.Add("Week " + week.ToString());
            int index = rd.Next(list.Count);
            assignmentStatus.Add(list[index]);
        }
    }

    void tableInitialize()
    {
        entryContainer = content.transform.Find("Assignment_Entry_Container");
        entryTemplate = entryContainer.Find("Assignment_Entry_Template").transform;
        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 50f;

        for (int i = 0; i < assignmentNames.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryTransform.gameObject.SetActive(true);
            entryTransform.Find("Text_No").GetComponent<Text>().text = i.ToString();
            entryTransform.Find("Text_Name").GetComponent<Text>().text = assignmentNames[i];
            entryTransform.Find("Text_Status").GetComponent<Text>().text = assignmentStatus[i];
            entryTransform.Find("Text_Name").Find("Button_Edit").GetComponent<Button>().onClick.AddListener(() => {
                assignmentName = entryTransform.Find("Text_Name").GetComponent<Text>().text;
                viewAssignment();
            });
            entryTransform.Find("Text_Name").Find("Button_Delete").GetComponent<Button>().onClick.AddListener(() => {
                assignmentName = entryTransform.Find("Text_Name").GetComponent<Text>().text;
                viewAssignment();
            });
            entryTransform.localScale = new Vector2(1, 1);
            entryTransform.localPosition = new Vector2(0, -templateHeight * i);
        }
        if (assignmentNames.Count == 0)
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
        SceneManager.LoadScene("Assignments_View");
    }
    public void scheduleAssignment()
    {
        SceneManager.LoadScene("Assignments_Schedule");
    }
    public void deleteAssignment()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(true);   
        SceneManager.LoadScene("Assignments");
    }
    public void confirmDelete()
    {
        //
        SceneManager.LoadScene("Assignments");
    }
    public void exitDelete()
    {
        popUp.gameObject.SetActive(false);
    }
}
