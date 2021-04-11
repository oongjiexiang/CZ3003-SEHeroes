using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Assignment_List : MonoBehaviour
{
    List<string> assignmentNames;
    List<string> assignmentStatus;
    GameObject assignmentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;

    // Start is called before the first frame update
    void Start()
    {
        setAssignmentList();
        assignmentMainPopupInitialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setAssignmentList()
    {
        assignmentNames = new List<string>();
        assignmentStatus = new List<string>();
        var list = new List<string> { "Ongoing", "Closed", "Scheduled" };

        int week = 0;
        var rd = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            week++;
            assignmentNames.Add("Week " + week.ToString());
            int index = rd.Next(list.Count);
            assignmentStatus.Add(list[index]);
        }
    }

    void assignmentMainPopupInitialized()
    {
        if(assignmentNames.Count > 0)
        {
            noRecordLabel.gameObject.SetActive (false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < assignmentNames.Count; i++)
            {
                GameObject assignmentTextPanel = (GameObject)Instantiate(ContentDataPanel);
                assignmentTextPanel.transform.SetParent(mainScrollContentView.transform);
                assignmentTextPanel.transform.localScale = new Vector3(1, 1, 1);
                assignmentTextPanel.transform.localPosition = new Vector3(0, 0, 0);
                assignmentTextPanel.transform.Find("Cell_Text").GetComponent<Text> ().text = i + ".                     " + assignmentNames[i] + "                     " + assignmentStatus[i];
            }   
        }
        else
        {
            noRecordLabel.gameObject.SetActive(true);
        }
    }
    public void hideAssignmentTeacherListPopUp()
    {
        assignmentPopUp.gameObject.SetActive(false);
    }
    public void showAssignmentTeacherListPopUp()
    {
        assignmentPopUp.gameObject.SetActive(true);
    }
}
