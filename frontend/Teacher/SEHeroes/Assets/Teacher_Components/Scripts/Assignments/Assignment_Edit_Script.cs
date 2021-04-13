using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls Assignment_Edit Scene
public class Assignment_Edit_Script : MonoBehaviour
{
    // variables
    private Assignment asg;
    private AssignmentQuestion current_question;
    private List<AssignmentQuestion> asgQuestionList;
    private int cur;
    private int originLength;
    private bool newQuestion;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;
    private Dropdown dropdownAnswer;
    private API_Assignment conn;
    

    // Start is called before the first frame update
    void Start()
    {
        // display
        asg = Assignment_Edit_Meta_Script.editAsg;
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        entryContainer = panelObject.transform.Find("Panel_Question_Creation");
        dropdownAnswer = entryContainer.Find("Dropdown_Answer").GetComponent<Dropdown>();
        panelObject.transform.Find("Button_Delete").GetComponent<Button>().interactable = false;    // need to change
        popUp.SetActive(false);
        conn = (API_Assignment)transform.GetComponent(typeof(API_Assignment));
        panelObject.gameObject.SetActive(false);

        // button events
        panelObject.transform.Find("Button_NextQ").GetComponent<Button>().onClick.AddListener(ClickNextOrAdd);
        panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().onClick.AddListener(ClickPrevious);
        panelObject.transform.Find("Button_Return").GetComponent<Button>().onClick.AddListener(ClickReturn);
        panelObject.transform.Find("Button_Save").GetComponent<Button>().onClick.AddListener(ClickSave);
        panelObject.transform.Find("Button_Clear").GetComponent<Button>().onClick.AddListener(ClickClear);
        panelObject.transform.Find("Button_Delete").GetComponent<Button>().onClick.AddListener(ClickDelete);
        popUp.transform.Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionIncompleteAcknowledge);
        popUp.transform.Find("Popup_Info").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionInfoAcknowledge);
        popUp.transform.Find("Popup_Delete").Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(exitDelete);
        popUp.transform.Find("Popup_Delete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(confirmDelete);
        
        // variables: questions
        fetchQuestions(asg); 
        newQuestion = false;
        current_question = new AssignmentQuestion();
        asgQuestionList = new List<AssignmentQuestion>();
        cur = 0;
    }
    void Update(){
        if(API_Assignment.asgQListRequestDone){
            for(int i = 0; i < API_Assignment.jsonNodeAsgQ.Count; i++){
                for(int j = 0; j < API_Assignment.jsonNodeAsgQ[i].Count; j++){
                    asgQuestionList.Add(new AssignmentQuestion(API_Assignment.jsonNodeAsgQ[i][j]));
                }
            }
            API_Assignment.jsonNodeAsgQ.Clear();
            API_Assignment.asgQListRequestDone = false;
            current_question = asgQuestionList[0];
            populateFields(current_question);
            originLength = asgQuestionList.Count;
            panelObject.gameObject.SetActive(true);
        }
        if(API_Assignment.asgQAddDone){
            API_Assignment.jsonNodeAsgQ.Clear();
            API_Assignment.asgQAddDone = false;
        }
    }
    // Obtains assignment details from backend
    private void fetchQuestions(Assignment asg){ 
        API_Assignment.asgQListRequestDone = false;
        StartCoroutine(conn.getAssignmentQuestionList(asg));        
    }
    public void ClickReturn(){
        SceneManager.LoadScene("Assignments");
    }

    // Logic when saving. The question might be a new or existing question
    public void ClickSave()
    {
        if(validateFields()){
            if(newQuestion && cur == asgQuestionList.Count - 1){
                newQuestion = false;
                StartCoroutine(conn.addQuestion(asg, current_question));
            }
            else{
                StartCoroutine(conn.updateQuestion(current_question));
            }
            populateFields(current_question);
        }
        else{
            popupQuestionIncomplete();
        }
    }
    public void ClickPrevious(){
        current_question = asgQuestionList[--cur];
        populateFields(current_question);
    }
    // The button could be 'Next' or 'Add'. Two different logics
    public void ClickNextOrAdd(){
        try{
            current_question = asgQuestionList[cur+1];
            cur++;
            populateFields(current_question);
        }
        catch(ArgumentOutOfRangeException){
            if(!newQuestion){
                current_question = new AssignmentQuestion();
                asgQuestionList.Add(current_question);
                cur++;
                newQuestion = true;
                populateFields(current_question);
            }
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
        exitDelete();
        if(cur == asgQuestionList.Count - 1 && newQuestion){
            newQuestion = false;
        }
        else{
            StartCoroutine(conn.deleteQuestion(asg, current_question));
        }
        if(cur < asgQuestionList.Count - 1){
            asgQuestionList.RemoveAt(cur);
            current_question = asgQuestionList[cur];
            populateFields(current_question);
        }
        else{
            asgQuestionList.RemoveAt(cur);
            current_question = asgQuestionList[--cur];
            populateFields(current_question);
        }
        
    }
    public void exitDelete(){
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    private void setPopupInfoMessage(string message){   // helper function to set popup's message easily
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    private bool validateFields(){
        string ans;
        current_question.question = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;

        current_question.answer = new List<string>();
        ans = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
        if(ans != "") current_question.answer.Add(ans);
        ans = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
        if(ans != "") current_question.answer.Add(ans);
        ans = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
        if(ans != "") current_question.answer.Add(ans);
        ans = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
        if(ans != "") current_question.answer.Add(ans);

        // There must be a question string
        if(current_question.question == ""){
            setPopupInfoMessage("A question must be given");
            return false;
        }

        if(current_question.answer.Count < 2){
            setPopupInfoMessage("There must be at least two answers");
            return false;
        }
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
        try{
            current_question.score = int.Parse(entryContainer.Find("InputField_Score").GetComponent<InputField>().text);
            if(current_question.score < 0){
                setPopupInfoMessage("Score must not be negative");
                return false;
            }
        }
        catch(FormatException){
            setPopupInfoMessage("Score must be a whole number");
            return false;
        }
        populateFields(current_question);
        switch(current_question.correctAnswer){
            case 2: {
                if(current_question.answer.Count <= 2){
                    setPopupInfoMessage("With two answers, C and D cannot be the correct answer");
                    return false;
                }
                break;
            }
            case 3: {
                if(current_question.answer.Count == 2){
                    setPopupInfoMessage("With two answers, C and D cannot be the correct answer");
                    return false;
                }
                else if(current_question.answer.Count == 3){
                    setPopupInfoMessage("With three answers, D cannot be the correct answer");
                    return false;
                }
                break;
            }
        }
        return true;
    }
    // Populate UI fields
    private void populateFields(AssignmentQuestion current_question)
    {
        ClickClear();

        entryContainer.Find("Text_Question").GetComponent<Text>().text = "Question " + (cur + 1).ToString();
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.question;
        
        try{
            entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.answer[0];
            entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.answer[1];
            entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.answer[2];
            entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.answer[3];
        }
        catch(ArgumentOutOfRangeException){}
        
        entryContainer.Find("InputField_Score").GetComponent<InputField>().text = current_question.score.ToString();
        if(current_question.correctAnswer == -1) dropdownAnswer.value = 0;
        else dropdownAnswer.value = current_question.correctAnswer + 1;

        if(asgQuestionList.Count > 1) panelObject.transform.Find("Button_Delete").GetComponent<Button>().interactable = true;
        else panelObject.transform.Find("Button_Delete").GetComponent<Button>().interactable = false;
        
        if(cur == 0) panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = false;
        else panelObject.transform.Find("Button_PrevQ").GetComponent<Button>().interactable = true;

        panelObject.transform.Find("Button_NextQ").Find("Text").GetComponent<Text>().text = "Next";
        panelObject.transform.Find("Button_NextQ").GetComponent<Button>().interactable = true;
        if(cur == asgQuestionList.Count - 1){
            panelObject.transform.Find("Button_NextQ").Find("Text").GetComponent<Text>().text = "Add Question";
            if(!newQuestion) panelObject.transform.Find("Button_NextQ").GetComponent<Button>().interactable = true;
            else panelObject.transform.Find("Button_NextQ").GetComponent<Button>().interactable = false;
        }
    }
    private void popupQuestionIncomplete(){
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
}
