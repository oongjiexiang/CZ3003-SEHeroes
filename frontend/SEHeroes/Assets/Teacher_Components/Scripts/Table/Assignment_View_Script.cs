using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Assignment_View_Script : MonoBehaviour
{
    // variables
    // private List<Choice> asgQuestionList;
    // private Choice current_question;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;

    // Start is called before the first frame update
    // void Awake()
    // {
    //     // display
    //     popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
    //     popUp.SetActive(false);
    //     entryContainer = panelObject.transform.Find("Panel_Question_List");
    //     panelObject.transform.Find("Header_Text").GetComponent<Text>().text = Assignment_Entry_Script.assignmentName;

    //     // button
    //     panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;

    //     // variables: questions
    //     asgQuestionList = LoadQuestions();
    //     current_question = asgQuestionList[0];
    //     if (asgQuestionList.Count == 1) populateFields(current_question, false, false);
    //     else populateFields(current_question, false, true);
    // }
    // public void ClickEdit()
    // {
    //     SceneManager.LoadScene("Assignments_Edit");
    // }
    // public void ClickDelete()
    // {
    //     popUp.SetActive(true);
    //     popUp.transform.Find("Popup_Delete").gameObject.SetActive(true);
    // }
    // public void ClickPreviousQuestion()
    // {
    //     current_question = asgQuestionList[current_question.question_num - 1];
    //     if (current_question.question_num == 0) populateFields(current_question, false, true);
    //     else populateFields(current_question, true, true);
    // }
    // private List<Choice> LoadQuestions()
    // {
    //     List<Choice> list_dummy = new List<Choice>();
    //     for(int i = 0; i < 4; i++)
    //     {
    //         list_dummy.Add(createNewQuestion(i.ToString(), i));
    //     }
    //     return list_dummy;
    // }
    // Choice createNewQuestion(string text, int count)
    // {
    //     Choice current_question = new Choice();
    //     current_question.question_num = count;
    //     current_question.A = current_question.B = current_question.C = current_question.D = text + "test";
    //     current_question.correct_ans = 0;
    //     current_question.correct_ans_string = text + " test";
    //     return current_question;
    // }
    // public void ClickNextQuestion()
    // {
    //     current_question = asgQuestionList[current_question.question_num + 1];
    //     if (current_question.question_num == asgQuestionList.Count - 1) populateFields(current_question, true, false);
    //     else populateFields(current_question, true, true);
    // }
    // private void populateFields(Choice current_question, bool prevButtonInteractable, bool nextButtonInteractable)
    // {
    //     entryContainer.Find("Text_Asg_Question").GetComponent<Text>().text = current_question.question;
    //     entryContainer.Find("Text_Choice_A").GetComponent<Text>().text = current_question.A;
    //     entryContainer.Find("Text_Choice_B").GetComponent<Text>().text = current_question.B;
    //     entryContainer.Find("Text_Choice_C").GetComponent<Text>().text = current_question.C;
    //     entryContainer.Find("Text_Choice_D").GetComponent<Text>().text = current_question.D;
    //     entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + (current_question.question_num + 1).ToString();
    //     panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = prevButtonInteractable;
    //     panelObject.transform.Find("Button_NextQ").GetComponent<Button>().interactable = nextButtonInteractable;

    //     entryContainer.Find("Choice_A").GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Choice_B").GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Choice_C").GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Choice_D").GetComponent<Image>().color = Color.red;
    //     switch (current_question.correct_ans)
    //     {
    //         case 0:
    //             entryContainer.Find("Choice_A").GetComponent<Image>().color = Color.green;
    //             break;
    //         case 1:
    //             entryContainer.Find("Choice_B").GetComponent<Image>().color = Color.green;
    //             break;
    //         case 2:
    //             entryContainer.Find("Choice_C").GetComponent<Image>().color = Color.green;
    //             break;
    //         case 3:
    //             entryContainer.Find("Choice_D").GetComponent<Image>().color = Color.green;
    //             break;
    //         default:
    //             break;
    //     }
    // }
    // public void confirmDelete()
    // {
    //     //
    //     SceneManager.LoadScene("Assignments");
    // }
    // public void exitDelete()
    // {
    //     popUp.gameObject.SetActive(false);
    // }
}

