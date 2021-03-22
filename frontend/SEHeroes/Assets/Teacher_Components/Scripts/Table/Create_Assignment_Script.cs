using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choice
{
    public int question_num;
    public string question {get; set;}
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string D { get; set; }
}
public class Create_Assignment_Script : MonoBehaviour
{
    private string assignmentName = "None";
    private List<Choice> asgQuestionList;
    private Choice current_question;
    private int current_question_num;
    public GameObject panelObject;
    private Transform entryContainer;
    // Start is called before the first frame update
    void Awake()
    {
        entryContainer = panelObject.transform.Find("Panel_Question_Creation");
        //current_question_num = 1;
        asgQuestionList = new List<Choice>();
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;
        current_question = new Choice();
        current_question.question_num = 0;
    }
    public void ClickCreate()
    {

    }
    public void ClickPreviousQuestion()
    {
        current_question = asgQuestionList[current_question.question_num - 1];
        if (current_question.question_num == 0)
        {
            populateFields(current_question, false); 
            //panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;
        }
        else
        {
            populateFields(current_question, true);
        }
        
    }
    public void ClickNextQuestion()
    {
        if (current_question.question_num >= asgQuestionList.Count)
        {
            assignmentName = panelObject.transform.Find("InputField_Name").GetComponent<InputField>().text;
            current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;
            current_question.A = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
            current_question.B = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
            current_question.C = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
            current_question.D = entryContainer.Find("InputField_D").GetComponent<InputField>().text;

            if (assignmentName == "" || current_question.A == "" || current_question.B == "" || current_question.question == "")
            {
                Debug.Log("cannot load scene");
            }
            else
            {
                asgQuestionList.Add(current_question);
                current_question = new Choice();
                current_question.question_num = asgQuestionList.Count;
                cleanFields(current_question);
            }
        }
        else if(current_question.question_num == asgQuestionList.Count - 1)
        {
            current_question = new Choice();
            current_question.question_num = asgQuestionList.Count;
            cleanFields(current_question);
        }
        else
        {
            current_question = asgQuestionList[current_question.question_num + 1];
            populateFields(current_question, true);
        }
    }
    private void populateFields(Choice current_question, bool buttonInteractable)
    {
        fillFields(current_question.question, current_question.A, current_question.B, current_question.C, 
            current_question.D, current_question.question_num + 1, buttonInteractable);
       
    }
    private void cleanFields(Choice current_question)
    {
        fillFields("", "", "", "", "", current_question.question_num + 1, true);
    }
    private void fillFields(string question, string A, string B, string C, string D, int question_num, bool buttonInteractable)
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = question;
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = A;
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = B;
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = C;
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = D;
        entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + question_num.ToString();
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = buttonInteractable;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
