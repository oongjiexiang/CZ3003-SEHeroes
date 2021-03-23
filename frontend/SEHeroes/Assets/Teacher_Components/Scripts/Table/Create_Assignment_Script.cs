using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class Choice
{
    public int question_num;
    public string question { get; set; }
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string D { get; set; }
    public string correct_ans_string;
    public int correct_ans;
}

public class Create_Assignment_Script : MonoBehaviour
{
    // variables
    private string assignmentName = "None";
    private List<Choice> asgQuestionList;
    private Choice current_question;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;

    // Start is called before the first frame update
    void Awake()
    {
        // display
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        entryContainer = panelObject.transform.Find("Panel_Question_Creation");
        popUp.SetActive(false);

        // buttons
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;
        entryContainer.Find("Button_A").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(0));
        entryContainer.Find("Button_B").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(1));
        entryContainer.Find("Button_C").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(2));
        entryContainer.Find("Button_D").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(3));

        // variables: questions
        asgQuestionList = new List<Choice>();
        current_question = createNewQuestion();
    }
    public void ClickCreate()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Cancel").gameObject.SetActive(false);
        popUp.transform.Find("Popup_Create").gameObject.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
    }
    public void ClickCancel()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Cancel").gameObject.SetActive(true);
        popUp.transform.Find("Popup_Create").gameObject.SetActive(false);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
    }
    public void popupQuestionIncomplete()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Cancel").gameObject.SetActive(false);
        popUp.transform.Find("Popup_Create").gameObject.SetActive(false);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
    public void ClickPreviousQuestion()
    {
        retrieveFields(current_question);
        if(current_question.question_num == asgQuestionList.Count || validateFields())
        {
            current_question = asgQuestionList[current_question.question_num - 1];
            if (current_question.question_num == 0) populateFields(current_question, false);
            else populateFields(current_question, true);
        }
        else popupQuestionIncomplete();
    }
    void retrieveFields(Choice current_question)
    {
        assignmentName = panelObject.transform.Find("InputField_Name").GetComponent<InputField>().text;
        current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;
        current_question.A = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
        current_question.B = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
        current_question.C = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
        current_question.D = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
    }
    Choice createNewQuestion()
    {
        Choice current_question = new Choice();
        current_question.question_num = asgQuestionList.Count;
        current_question.A = current_question.B = current_question.C = current_question.D = "";
        current_question.correct_ans = -1;
        current_question.correct_ans_string = "";
        return current_question;
    }
    public void ClickNextQuestion()
    {
        if (current_question.question_num == asgQuestionList.Count)
        {
            retrieveFields(current_question);
            if (!validateFields()) popupQuestionIncomplete();
            else
            {
                selectCorrectAnswer(current_question.correct_ans);
                asgQuestionList.Add(current_question);
                current_question = createNewQuestion();
                populateFields(current_question, true);
            }
        }
        else if (current_question.question_num == asgQuestionList.Count - 1)
        {
            retrieveFields(current_question);
            if (!validateFields()) popupQuestionIncomplete();
            else
            {
                current_question = createNewQuestion();
                populateFields(current_question, true);
            }
            
            
        }
        else
        {
            retrieveFields(current_question);
            if (!validateFields()) popupQuestionIncomplete();
            else
            {
                current_question = asgQuestionList[current_question.question_num + 1];
                populateFields(current_question, true);
            }
        }
    }
    private bool validateFields()
    {
        if (assignmentName == "" || current_question.A == "" || current_question.B == "" || current_question.question == "") return false;
        selectCorrectAnswer(current_question.correct_ans);
        if (current_question.correct_ans_string == "" || current_question.correct_ans == -1) return false;
        return true;
    }
    private void populateFields(Choice current_question, bool buttonInteractable)
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.question;
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.A;
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.B;
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.C;
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.D;
        entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + (current_question.question_num + 1).ToString();
        selectCorrectAnswer(current_question.correct_ans);
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = buttonInteractable;
    }
    private void selectCorrectAnswer(int answerIndex)
    {
        entryContainer.Find("Button_A").GetComponent<Button>().GetComponent<Image>().color = Color.red;
        entryContainer.Find("Button_B").GetComponent<Button>().GetComponent<Image>().color = Color.red;
        entryContainer.Find("Button_C").GetComponent<Button>().GetComponent<Image>().color = Color.red;
        entryContainer.Find("Button_D").GetComponent<Button>().GetComponent<Image>().color = Color.red;
        current_question.correct_ans = answerIndex;
        switch (answerIndex)
        {
            case 0:
                {
                    entryContainer.Find("Button_A").GetComponent<Button>().GetComponent<Image>().color = Color.green;
                    current_question.correct_ans_string = entryContainer.Find("InputField_A").Find("Text").GetComponent<Text>().text;
                    break;
                }
            case 1:
                {
                    entryContainer.Find("Button_B").GetComponent<Button>().GetComponent<Image>().color = Color.green;
                    current_question.correct_ans_string = entryContainer.Find("InputField_B").Find("Text").GetComponent<Text>().text;
                    break;
                }
            case 2:
                {
                    entryContainer.Find("Button_C").GetComponent<Button>().GetComponent<Image>().color = Color.green;
                    current_question.correct_ans_string = entryContainer.Find("InputField_C").Find("Text").GetComponent<Text>().text;
                    break;
                }
            case 3:
                {
                    entryContainer.Find("Button_D").GetComponent<Button>().GetComponent<Image>().color = Color.green;
                    current_question.correct_ans_string = entryContainer.Find("InputField_D").Find("Text").GetComponent<Text>().text;
                    break;
                }
            default:
                break;
        }
    }
    public void confirmCancel()
    {
        SceneManager.LoadScene("Assignments");
    }
    public void exitCancel()
    {
        popUp.gameObject.SetActive(false);
    }
    public void confirmCreate()
    {
        SceneManager.LoadScene("Assignments");
    }
    public void exitCreate()
    {
        popUp.gameObject.SetActive(false);
    }
    public void popupQuestionIncompleteAcknowledge()
    {
        popUp.gameObject.SetActive(false);
    }
}

