using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class Create_Assignment_Script : MonoBehaviour
{
    // variables
    private Assignment asg = Create_Assignment_Meta_Script.tentAssignment;
    private AssignmentQuestion current_question;
    private List<AssignmentQuestion> asgQuestionList;
    private int cur;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;
    private Dropdown dropdownAnswer;
    

    // Start is called before the first frame update
    void Awake()
    {
        // display
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        entryContainer = panelObject.transform.Find("Panel_Question_Creation");
        dropdownAnswer = entryContainer.Find("Dropdown_Answer").GetComponent<Dropdown>();
        popUp.SetActive(false);

        // variables: questions
        asgQuestionList = new List<AssignmentQuestion>();
        current_question = new AssignmentQuestion();
        asgQuestionList.Add(current_question);
        cur = 0;
        populateFields(current_question, false);
    }
    public void ClickSaveAndComplete()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Create").gameObject.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
    }
    public void ClickSaveAndPrevious(){
        current_question = asgQuestionList[cur--];
        if(cur > 0) populateFields(current_question, true);
        else populateFields(current_question, false);
    }
    public void ClickSaveAndNext(){
        if(validateFields()){
            try{
                current_question = asgQuestionList[cur++];
                populateFields(current_question, true);
            }
            catch(IndexOutOfRangeException e){
                current_question = new AssignmentQuestion();
                asgQuestionList.Add(current_question);
                cur++;
                populateFields(current_question, true);
            }
        }
        else{
            popupQuestionIncomplete();
        }
    }
    public void ClickClear()
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = "";
        dropdownAnswer.value = 0;
    }
    private void setPopupMessage(string message){
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    private bool validateFields(){
        current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;
        current_question.answer[0] = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
        current_question.answer[1] = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
        current_question.answer[2] = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
        current_question.answer[3] = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
        
        // There must be a question string
        if(current_question.question == ""){
            setPopupMessage("There is no question to be saved");
            return false;
        }

        // All four answers must be given
        for(int i = 0; i < current_question.answer.Count; i++){
            if(current_question.answer[i] == ""){
                setPopupMessage("All four answers must be given");
                return false;
            }
        }

        // Score must be integer
        try{
            current_question.score = int.Parse(entryContainer.Find("InputField_C").GetComponent<InputField>().text);
        }
        catch(FormatException e){
            setPopupMessage("Score must be a whole number");
            return false;
        }

        if(dropdownAnswer.value == 0){
            setPopupMessage("Please choose an answer");
            return false;
        }
        current_question.correctAnswer = dropdownAnswer.options[dropdownAnswer.value].text;
        return true;
    }
    private void populateFields(AssignmentQuestion current_question, bool buttonInteractable)
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.question;
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.answer[0];
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.answer[1];
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.answer[2];
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.answer[3];
        entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + (cur + 1).ToString();
        for(int i = 1; i < 5; i++){
            if(current_question.answer.Equals(dropdownAnswer.options[i].text)){
                dropdownAnswer.value = i;
                break;
            }
            dropdownAnswer.value = 0;
        }
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = buttonInteractable;
    }
    private void popupQuestionIncomplete(){
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Create").gameObject.SetActive(false);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
    public void confirmSaveAndCreate(){
        SceneManager.LoadScene("Assignments");
    }
    public void exitSaveAndCreate(){
        popUp.gameObject.SetActive(false);
    }
    public void popupQuestionIncompleteAcknowledge(){
        popUp.gameObject.SetActive(false);
    }
}

