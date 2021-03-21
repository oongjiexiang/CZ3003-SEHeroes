using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice
{
    public int question_id;
    public string question {get; set;}
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string D { get; set; }
}
public class Create_Assignment_Script : MonoBehaviour
{
    private string assignmentName = "dummy";
    private List<Choice> asgQuestionList;
    private Choice current = null;
    private Transform entryContainer;
    // Start is called before the first frame update
    void Awake()
    {
        asgQuestionList = new List<Choice>();
    }
    void ClickCreate()
    {

    }
    void ClickCancel()
    {

    }
    void ClickPreviousQuestion()
    {

    }
    public void ClickNextQuestion()
    {
        entryContainer = transform.Find("Panel_Question_Creation");
        current = new Choice();

        assignmentName = entryContainer.Find("InputField_Name").GetComponent<Text>().text;
        current.question = entryContainer.Find("InputField_Question").GetComponent<Text>().text;
        current.A = entryContainer.Find("InputField_A").GetComponent<Text>().text;
        current.B = entryContainer.Find("InputField_B").GetComponent<Text>().text;
        current.C = entryContainer.Find("InputField_C").GetComponent<Text>().text;
        current.D = entryContainer.Find("InputField_D").GetComponent<Text>().text;

        if (assignmentName == "" || current.A == "" || current.B == "" || current.question == "")
        {
            Debug.Log("cannot load scene");
        }
        else
        {
            current.question_id = asgQuestionList.Count;
            asgQuestionList.Add(current);
            entryContainer.Find("InputField_Question").GetComponent<Text>().text = "";
            entryContainer.Find("InputField_A").GetComponent<Text>().text = "";
            entryContainer.Find("InputField_B").GetComponent<Text>().text = "";
            entryContainer.Find("InputField_C").GetComponent<Text>().text = "";
            entryContainer.Find("InputField_D").GetComponent<Text>().text = "";
            entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + asgQuestionList.Count.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
