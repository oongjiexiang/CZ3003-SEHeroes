using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Create_Assignment_Meta_Script : MonoBehaviour
{
    const int NUM_TRY = 10;
    private Assignment newAsg;
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private GameObject popUp;
    void Start()
    {
        newAsg = new Assignment();
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        popUp.SetActive(false);
        populateRetries();
        panelObject.transform.Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(() => clickCancel());
        panelObject.transform.Find("Button_Create").GetComponent<Button>().onClick.AddListener(() => clickCreate());
        mainContentPanel.transform.Find("Panel_Messages").Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(() => incompleteAcknowledge());
    }
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
    public void clickCreate(){
        if(verifyFields()) SceneManager.LoadScene("Assignments_Creation");
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
    // logic
    private Dropdown getDropdown(string dropdownSection, string dropdownName){
        return panelObject.transform.Find(dropdownSection).Find(dropdownName).GetComponent<Dropdown>();
    }
    public bool verifyFields(){
        List<Dropdown> dropdowns = new List<Dropdown>();
        List<int> time = new List<int>();

        Dropdown dropdown = panelObject.transform.Find("Dropdown_Try").GetComponent<Dropdown>();
        Dropdown startMonthD = getDropdown("Handle_Start_Date", "Dropdown_Month");
        Dropdown endMonthD = getDropdown("Handle_End_Date", "Dropdown_Month");
        newAsg.assignmentName = panelObject.transform.Find("InputField_Name").Find("Text").GetComponent<Text>().text;
        newAsg.tries = int.Parse(dropdown.options[dropdown.value].text);
        string startMonth = startMonthD.options[startMonthD.value].text;
        string endMonth = endMonthD.options[endMonthD.value].text;

        dropdowns.Add(getDropdown("Handle_Start_Date", "Dropdown_Year"));
        dropdowns.Add(getDropdown("Handle_Start_Date", "Dropdown_Day"));
        dropdowns.Add(getDropdown("Handle_Start_Time", "Dropdown_Hour"));
        dropdowns.Add(getDropdown("Handle_Start_Time", "Dropdown_Minute"));
        dropdowns.Add(getDropdown("Handle_End_Date", "Dropdown_Year"));
        dropdowns.Add(getDropdown("Handle_End_Date", "Dropdown_Day"));
        dropdowns.Add(getDropdown("Handle_End_Time", "Dropdown_Hour"));
        dropdowns.Add(getDropdown("Handle_End_Time", "Dropdown_Minute"));
        foreach(var dp in dropdowns){
            time.Add(int.Parse(dp.options[dp.value].text));
        }
        
        newAsg.startDate = DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[0], startMonth, time[1], time[2], time[3], 0));
        newAsg.dueDate = DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[4], endMonth, time[5], time[6], time[7], 0));
        if(newAsg.assignmentName == ""){
            incompletePopup("Please give the assignment a name.");
            return false;
        }
        if(newAsg.startDate <= DateTime.Now){
            incompletePopup("Start date has passed. Please choose another date");
            return false;
        }
        if(newAsg.startDate >= newAsg.dueDate){
            incompletePopup("Start date cannot be later than the due date");
            return false;
        }
        return true;
    }
}
