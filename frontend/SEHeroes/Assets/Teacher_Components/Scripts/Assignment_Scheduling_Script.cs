using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assignment_Scheduling_Script : MonoBehaviour
{
    private Transform entryTemplate;
    private Transform entryContainer;
    List<string> assignmentNames;
    List<string> assignmentStatus;
    public Text noRecordLabel;
    // Start is called before the first frame update
    private void Awake()
    {
        setAssignmentList();
        tableInitialize();

    }

    void setAssignmentList()
    {
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
        entryContainer = transform.Find("Assignment_Entry_Container");
        entryTemplate = entryContainer.Find("Assignment_Entry_Template");
        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 50f;

        for (int i = 0; i < assignmentNames.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryTransform.gameObject.SetActive(true);
            entryTransform.Find("Text_No").GetComponent<Text>().text = i.ToString();
            entryTransform.Find("Text_Assignment").GetComponent<Text>().text = assignmentNames[i];
            //entryTransform.Find("Text_Status").GetComponent<Text>().text = assignmentStatus[i];
            entryTransform.localScale = new Vector2(1, 1);
            entryTransform.localPosition = new Vector2(0, -templateHeight * i);
        }
        //Debug.Log(assignmentNames.Count);
        if (assignmentNames.Count == 0)
        {
            noRecordLabel.gameObject.SetActive(true);
        }
        else
        {
            noRecordLabel.gameObject.SetActive(false);
        }
    }
}
