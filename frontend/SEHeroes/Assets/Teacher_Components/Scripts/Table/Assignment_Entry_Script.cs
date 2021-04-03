using System.Collections;
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
    private assignment dummyCreateAssign(){
        assignment asg = new assignment();
        asg.assignmentName = "assg1a";
        asg.tries = 2;
        asg.startDate = 1124;      // will change
        asg.dueDate = 21351325;        // will change
        // asg.questions = new List<assignmentQuestion>();
        asg.questions = new List<string>();
        asg.questions.Add("What is my course?");
        asg.questions.Add("What is your year?");
        asg.questions.Add("What is his school?");
        // assignmentQuestion q1 = new assignmentQuestion();
        // q1.question = "What is your name?";
        // q1.score = 100;
        // q1.answer = new List<string>();
        // string[] temp = { "Ash", "Bob", "chakra", "Dan" };
        // q1.answer.AddRange(temp);
        // q1.correctAnswer = q1.answer[2];
        // q1.image = "";
        // asg.questions.Add(q1);

        // assignmentQuestion q2 = new assignmentQuestion();
        // q2.question = "What is your age?";
        // q2.score = 100;
        // q2.answer = new List<string>();
        // string[] temp2 = { "19", "29", "39", "49" };
        // q2.answer.AddRange(temp2);
        // q2.correctAnswer = q2.answer[1];
        // q2.image = "";
        // asg.questions.Add(q2);

        // assignmentQuestion q3 = new assignmentQuestion();
        // q3.question = "What is your age?";
        // q3.score = 100;
        // q3.answer = new List<string>();
        // string[] temp3 = {"19", "29", "39", "49"};
        // q3.answer.AddRange(temp3);
        // q3.correctAnswer = q3.answer[3];
        // q3.image = "";
        // asg.questions.Add(q3);
        return asg; 
    }
    IEnumerator dummyPostAssignment(assignment asg){
        API_Connection conn = new API_Connection();
        string jsonToPost = JsonUtility.ToJson(asg);
        print(jsonToPost + " is post url for assignment");
        yield return conn.PostData("assignment", jsonToPost);
    }
    IEnumerator dummyPutAssignment(assignment asg){
        API_Connection conn = new API_Connection();
        string jsonToPut = JsonUtility.ToJson(asg);
        print(jsonToPut + " is PUT url for assignment");
        yield return conn.PutData("assignment/1rO3ShtqbePhkjoQ2Jcd", jsonToPut);
    }
    IEnumerator dummyDeleteAssignment(assignment asg){
        API_Connection conn = new API_Connection();
        yield return conn.DeleteData("assignment/1rO3ShtqbePhkjoQ2Jcd");
    }
    IEnumerator setAssignmentList()
    {
        
        // yield return conn.GetData("user");
        // yield return conn.DeleteData("assignment/B4AF3RCBqaxTzbx9rDtx");
        // yield return dummyPostAssignment(dummyCreateAssign());
        // yield return dummyPutAssignment(dummyCreateAssign());
        yield return dummyDeleteAssignment(dummyCreateAssign());

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
