using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story_Mode_Edit_Script : MonoBehaviour
{
    // variables
    private StoryModeQuestion current_question;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;
    private Dropdown dropdownAnswer;
    private API_Storymode conn; 
    
    void Start()
    {
        // display   
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        entryContainer = panelObject.transform.Find("Panel_Question_Creation_Story");
        dropdownAnswer = entryContainer.Find("Dropdown_Answer").GetComponent<Dropdown>();
        popUp.SetActive(false);
        conn = (API_Storymode)transform.GetComponent(typeof(API_Storymode));
        panelObject.gameObject.SetActive(false);

        // button events
        panelObject.transform.Find("Button_Return").GetComponent<Button>().onClick.AddListener(ClickReturn);
        panelObject.transform.Find("Button_Save").GetComponent<Button>().onClick.AddListener(ClickSave);
        panelObject.transform.Find("Button_Clear").GetComponent<Button>().onClick.AddListener(ClickClear);
        panelObject.transform.Find("Button_Delete").GetComponent<Button>().onClick.AddListener(ClickDelete);
        popUp.transform.Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionIncompleteAcknowledge);
        popUp.transform.Find("Popup_Info").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionInfoAcknowledge);
        popUp.transform.Find("Popup_Delete").Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(exitDelete);
        popUp.transform.Find("Popup_Delete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(confirmDelete);

        fetchQuestions();
    }
    void Update(){
        if(API_Storymode.getStoryQDone){
            API_Storymode.getStoryQDone = false;
            current_question = new StoryModeQuestion(API_Storymode.jsonNodeStoryQ);
            populateFields();
            panelObject.gameObject.SetActive(true);
        }
    }
    private void fetchQuestions(){   
        API_Storymode.getStoryQDone = false;
        print(Questions_List_Script.editStoryModeQ.storyModeQuestionId);
        print(conn);
        StartCoroutine(conn.getStoryQ(Questions_List_Script.editStoryModeQ.storyModeQuestionId));        
    }
    public void ClickReturn(){
        SceneManager.LoadScene("Question_Bank_Management");
    }
    public void ClickSave()
    {
        if(validateFields()){
            StartCoroutine(conn.updateStoryQ(current_question));
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
    public void ClickDelete(){
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(true);
    }
    public void popupQuestionIncompleteAcknowledge(){
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    public void popupQuestionInfoAcknowledge(){
        popUp.transform.Find("Popup_Info").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    public void confirmDelete(){
        print("confirm delete"); // to delete later
        StartCoroutine(conn.deleteStoryQ(current_question));
        SceneManager.LoadScene("Questions_List_Script");
    }
    public void exitDelete(){
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    private void setPopupInfoMessage(string message){   // helper function to set popup's message easily
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    private bool validateFields(){
        try{
            current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;
            current_question.answer[0] = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
            current_question.answer[1] = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
            current_question.answer[2] = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
            current_question.answer[3] = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
        }
        catch(ArgumentOutOfRangeException){}
        // There must be a question string
        if(current_question.question == ""){
            setPopupInfoMessage("A question must be given");
            return false;
        }

        // All four answers must be given
        try{
            for(int i = 0; i < current_question.answer.Count; i++){
                if(current_question.answer[i] == ""){
                    setPopupInfoMessage("All four answers must be given");
                    return false;
                }
            }
        }
        catch(ArgumentOutOfRangeException){}

        try{
            for(int i = 0; i < current_question.answer.Count-1; i++){
                for(int j = i+1; j < current_question.answer.Count; j++){
                    if(current_question.answer[i].Equals(current_question.answer[j])){
                        setPopupInfoMessage("All answers must be unique");
                        return false;
                    }
                }
            }
        }
        catch(ArgumentOutOfRangeException){}
        
        if(dropdownAnswer.value == 0){
            setPopupInfoMessage("Please choose an answer");
            return false;
        }
        current_question.correctAnswer = dropdownAnswer.value-1;
        return true;
    }
    private void populateFields()
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.question;
        print(current_question.question);
        print(entryContainer.Find("InputField_Question").GetComponent<InputField>().text);
        try{
            entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.answer[0];
            entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.answer[1];
            entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.answer[2];
            entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.answer[3];
        }
        catch(ArgumentOutOfRangeException){

        }
        dropdownAnswer.value = current_question.correctAnswer + 1;
    }
    private void popupQuestionIncomplete(){
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
}
