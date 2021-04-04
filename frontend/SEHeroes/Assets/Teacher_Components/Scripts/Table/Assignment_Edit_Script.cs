using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Assignment_Edit_Script : MonoBehaviour
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
    //     entryContainer = panelObject.transform.Find("Panel_Question_Creation");
    //     panelObject.transform.Find("Header_Text").GetComponent<Text>().text = Assignment_Entry_Script.assignmentName;

    //     // buttons
    //     panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;
    //     entryContainer.Find("Button_A").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(0));
    //     entryContainer.Find("Button_B").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(1));
    //     entryContainer.Find("Button_C").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(2));
    //     entryContainer.Find("Button_D").GetComponent<Button>().onClick.AddListener(() => selectCorrectAnswer(3));

    //     // variables: questions
    //     asgQuestionList = LoadQuestions();
    //     current_question = asgQuestionList[0];
    //     populateFields(current_question, false);
    // }
    // private List<Choice> LoadQuestions()
    // {
    //     List<Choice> list_dummy = new List<Choice>();
    //     for(int i = 0; i < 4; i++)
    //     {
    //         list_dummy.Add(createDummyQuestion(i.ToString(), i));
    //     }
    //     return list_dummy;
    // }
    // public void ClickSave()
    // {
    //     popUp.SetActive(true);
    //     popUp.transform.Find("Popup_Cancel").gameObject.SetActive(false);
    //     popUp.transform.Find("Popup_Save").gameObject.SetActive(true);
    //     popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
    // }
    // public void ClickCancel()
    // {
    //     popUp.SetActive(true);
    //     popUp.transform.Find("Popup_Cancel").gameObject.SetActive(true);
    //     popUp.transform.Find("Popup_Save").gameObject.SetActive(false);
    //     popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
    // }
    // public void popupQuestionIncomplete()
    // {
    //     popUp.SetActive(true);
    //     popUp.transform.Find("Popup_Cancel").gameObject.SetActive(false);
    //     popUp.transform.Find("Popup_Save").gameObject.SetActive(false);
    //     popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    // }
    // public void ClickPreviousQuestion()
    // {
    //     retrieveFields(current_question);
    //     if(current_question.question_num == asgQuestionList.Count || validateFields())
    //     {
    //         current_question = asgQuestionList[current_question.question_num - 1];
    //         if (current_question.question_num == 0) populateFields(current_question, false);
    //         else populateFields(current_question, true);
    //     }
    //     else popupQuestionIncomplete();
    // }
    // void retrieveFields(Choice current_question)
    // {
    //     current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;
    //     current_question.A = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
    //     current_question.B = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
    //     current_question.C = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
    //     current_question.D = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
    // }
    // Choice createNewQuestion()
    // {
    //     Choice current_question = new Choice();
    //     current_question.question_num = asgQuestionList.Count;
    //     current_question.A = current_question.B = current_question.C = current_question.D = "";
    //     current_question.correct_ans = -1;
    //     current_question.correct_ans_string = "";
    //     return current_question;
    // }
    // Choice createDummyQuestion(string text, int count)
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
    //     if (current_question.question_num == asgQuestionList.Count)
    //     {
    //         retrieveFields(current_question);
    //         if (!validateFields()) popupQuestionIncomplete();
    //         else
    //         {
    //             selectCorrectAnswer(current_question.correct_ans);
    //             asgQuestionList.Add(current_question);
    //             current_question = createNewQuestion();
    //             populateFields(current_question, true);
    //         }
    //     }
    //     else if (current_question.question_num == asgQuestionList.Count - 1)
    //     {
    //         retrieveFields(current_question);
    //         if (!validateFields()) popupQuestionIncomplete();
    //         else
    //         {
    //             current_question = createNewQuestion();
    //             populateFields(current_question, true);
    //         }
    //     }
    //     else
    //     {
    //         retrieveFields(current_question);
    //         if (!validateFields()) popupQuestionIncomplete();
    //         else
    //         {
    //             current_question = asgQuestionList[current_question.question_num + 1];
    //             populateFields(current_question, true);
    //         }
    //     }
    // }
    // private bool validateFields()
    // {
    //     if (current_question.A == "" || current_question.B == "" || current_question.question == "") return false;
    //     selectCorrectAnswer(current_question.correct_ans);
    //     if (current_question.correct_ans_string == "" || current_question.correct_ans == -1) return false;
    //     return true;
    // }
    // private void populateFields(Choice current_question, bool buttonInteractable)
    // {
    //     entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.question;
    //     entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.A;
    //     entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.B;
    //     entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.C;
    //     entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.D;
    //     entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + (current_question.question_num + 1).ToString();
    //     selectCorrectAnswer(current_question.correct_ans);
    //     panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = buttonInteractable;
    // }
    // private void selectCorrectAnswer(int answerIndex)
    // {
    //     entryContainer.Find("Button_A").GetComponent<Button>().GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Button_B").GetComponent<Button>().GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Button_C").GetComponent<Button>().GetComponent<Image>().color = Color.red;
    //     entryContainer.Find("Button_D").GetComponent<Button>().GetComponent<Image>().color = Color.red;
    //     current_question.correct_ans = answerIndex;
    //     switch (answerIndex)
    //     {
    //         case 0:
    //             {
    //                 entryContainer.Find("Button_A").GetComponent<Button>().GetComponent<Image>().color = Color.green;
    //                 current_question.correct_ans_string = entryContainer.Find("InputField_A").Find("Text").GetComponent<Text>().text;
    //                 break;
    //             }
    //         case 1:
    //             {
    //                 entryContainer.Find("Button_B").GetComponent<Button>().GetComponent<Image>().color = Color.green;
    //                 current_question.correct_ans_string = entryContainer.Find("InputField_B").Find("Text").GetComponent<Text>().text;
    //                 break;
    //             }
    //         case 2:
    //             {
    //                 entryContainer.Find("Button_C").GetComponent<Button>().GetComponent<Image>().color = Color.green;
    //                 current_question.correct_ans_string = entryContainer.Find("InputField_C").Find("Text").GetComponent<Text>().text;
    //                 break;
    //             }
    //         case 3:
    //             {
    //                 entryContainer.Find("Button_D").GetComponent<Button>().GetComponent<Image>().color = Color.green;
    //                 current_question.correct_ans_string = entryContainer.Find("InputField_D").Find("Text").GetComponent<Text>().text;
    //                 break;
    //             }
    //         default:
    //             break;
    //     }
    // }
    // public void confirmCancel()
    // {
    //     SceneManager.LoadScene("Assignments");
    // }
    // public void exitCancel()
    // {
    //     popUp.gameObject.SetActive(false);
    // }
    // public void confirmSave()
    // {
    //     SceneManager.LoadScene("Assignments");
    // }
    // public void exitSave()
    // {
    //     popUp.gameObject.SetActive(false);
    // }
    // public void popupQuestionIncompleteAcknowledge()
    // {
    //     popUp.gameObject.SetActive(false);
    // }
}

