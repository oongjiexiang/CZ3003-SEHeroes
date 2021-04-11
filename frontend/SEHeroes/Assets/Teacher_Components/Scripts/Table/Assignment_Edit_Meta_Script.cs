using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

// Controls Assignments_Edit_Meta Scene
public class Assignment_Edit_Meta_Script : MonoBehaviour
{
    const int NUM_TRY = 10;
    private GameObject popUp;
    public GameObject panelObject;
    public GameObject mainContentPanel;
    public static Assignment editAsg;
    private API_Assignment conn;
    void Start()
    {
        conn = (API_Assignment)transform.GetComponent(typeof(API_Assignment));
        editAsg = Assignment_Entry_Script.chosenAsg;
        populateRetries();
        populateFields();
        panelObject.transform.Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(() => clickCancel());
        panelObject.transform.Find("Button_Edit").GetComponent<Button>().onClick.AddListener(() => clickEdit());
        mainContentPanel.transform.Find("Panel_Messages").Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(() => incompleteAcknowledge());
    }
    // Populate UI fields 
    void populateFields(){
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        popUp.SetActive(false);
        panelObject.transform.Find("InputField_Name").GetComponent<InputField>().text = editAsg.assignmentName;
        panelObject.transform.Find("Dropdown_Try").GetComponent<Dropdown>().value = editAsg.tries;
        if(editAsg.startDate.time() <= DateTime.Now){
            getDropdown("Handle_Start_Date", "Dropdown_Year").gameObject.SetActive(false);
            getDropdown("Handle_Start_Date", "Dropdown_Month").gameObject.SetActive(false);
            getDropdown("Handle_Start_Date", "Dropdown_Day").gameObject.SetActive(false);
            getDropdown("Handle_Start_Time", "Dropdown_Hour").gameObject.SetActive(false);
            getDropdown("Handle_Start_Time", "Dropdown_Minute").gameObject.SetActive(false);
            panelObject.transform.Find("Text_Date").gameObject.SetActive(true);
            panelObject.transform.Find("Text_Time").gameObject.SetActive(true);
            panelObject.transform.Find("Text_Date").GetComponent<Text>().text = editAsg.startDate.printOnlyDate();
            panelObject.transform.Find("Text_Time").GetComponent<Text>().text = editAsg.startDate.printOnlyTime();
            Datetime_Controller_Script handleEnd = (Datetime_Controller_Script)panelObject.transform.Find("Handle_End_Date").GetComponent(typeof(Datetime_Controller_Script));
            handleEnd.FocusValues(editAsg.dueDate);
        }
        else{
            Datetime_Controller_Script handleStart = (Datetime_Controller_Script)panelObject.transform.Find("Handle_Start_Date").GetComponent(typeof(Datetime_Controller_Script));
            handleStart.FocusValues(editAsg.startDate);
            Datetime_Controller_Script handleEnd = (Datetime_Controller_Script)panelObject.transform.Find("Handle_End_Date").GetComponent(typeof(Datetime_Controller_Script));
            handleEnd.FocusValues(editAsg.dueDate);
        }
        print(editAsg.startDate.time());
        print(editAsg.dueDate.time());
    }
    // Populate try dropdown options
    void populateRetries(){
        List<string> numTry = new List<string>();
        for(int i = 1; i < NUM_TRY; i++){
            numTry.Add(i.ToString());
        }
        panelObject.transform.Find("Dropdown_Try").GetComponent<Dropdown>().AddOptions(numTry);
    }
    // event handlers
    public void clickCancel(){
        SceneManager.LoadScene("Assignments");
    }
    public void clickEdit(){
        if(verifyFields()){
            StartCoroutine(conn.updateAssignment(editAsg));
            SceneManager.LoadScene("Assignments_Edit");
        }
        else{
            popUp.SetActive(true);
            popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
        }
    }
    public void incompleteAcknowledge(){
        popUp.SetActive(false);
    }
    private void incompletePopup(string message){
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    // Helper function: Get the handle of the dropdown
    private Dropdown getDropdown(string dropdownSection, string dropdownName){
        return panelObject.transform.Find(dropdownSection).Find(dropdownName).GetComponent<Dropdown>();
    }
    // Verify if all data are valid
    public bool verifyFields(){
        List<Dropdown> dropdowns = new List<Dropdown>();
        List<string> time = new List<string>();

        Dropdown dropdown = panelObject.transform.Find("Dropdown_Try").GetComponent<Dropdown>();
        editAsg.assignmentName = panelObject.transform.Find("InputField_Name").Find("Text").GetComponent<Text>().text;
        editAsg.tries = int.Parse(dropdown.options[dropdown.value].text);

        dropdowns.Add(getDropdown("Handle_Start_Date", "Dropdown_Year"));
        dropdowns.Add(getDropdown("Handle_Start_Date", "Dropdown_Month"));
        dropdowns.Add(getDropdown("Handle_Start_Date", "Dropdown_Day"));
        dropdowns.Add(getDropdown("Handle_Start_Time", "Dropdown_Hour"));
        dropdowns.Add(getDropdown("Handle_Start_Time", "Dropdown_Minute"));
        dropdowns.Add(getDropdown("Handle_End_Date", "Dropdown_Year"));
        dropdowns.Add(getDropdown("Handle_End_Date", "Dropdown_Month"));
        dropdowns.Add(getDropdown("Handle_End_Date", "Dropdown_Day"));
        dropdowns.Add(getDropdown("Handle_End_Time", "Dropdown_Hour"));
        dropdowns.Add(getDropdown("Handle_End_Time", "Dropdown_Minute"));
        foreach(var dp in dropdowns){
            time.Add(dp.options[dp.value].text);
        }
        
        editAsg.startDate = new AsgDate(DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[0], time[1], time[2], time[3], time[4], 0)));
        editAsg.dueDate = new AsgDate(DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[5], time[6], time[7], time[8], time[9], 0)));
        if(editAsg.assignmentName == ""){
            incompletePopup("Please give the assignment a name.");
            return false;
        }
        if(editAsg.startDate.time() <= DateTime.Now && !panelObject.transform.Find("Text_Date").gameObject.active){
            incompletePopup("Start date has passed. Please choose another date");
            return false;
        }
        if(editAsg.startDate.time() >= editAsg.dueDate.time()){
            incompletePopup("Start date cannot be later than the due date");
            return false;
        }
        return true;
    }
}
