using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

// Controls Assignment_Creation_Meta Scene
public class Create_Assignment_Meta_Script : MonoBehaviour
{
    const int NUM_TRY = 10;
    private Assignment newAsg;
    private GameObject popUp;
    public GameObject panelObject;
    public GameObject mainContentPanel;
    public static Assignment tentAssignment;
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
    // Populate the number of tries for an assignment
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
        if(verifyFields()){
            tentAssignment = newAsg;
            SceneManager.LoadScene("Assignments_Creation");
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
    // Helper function to get the handle of dropdowns
    private Dropdown getDropdown(string dropdownSection, string dropdownName){
        return panelObject.transform.Find(dropdownSection).Find(dropdownName).GetComponent<Dropdown>();
    }
    // Verify if all of the data are valid
    public bool verifyFields(){
        List<Dropdown> dropdowns = new List<Dropdown>();
        List<string> time = new List<string>();

        Dropdown dropdown = panelObject.transform.Find("Dropdown_Try").GetComponent<Dropdown>();
        newAsg.assignmentName = panelObject.transform.Find("InputField_Name").Find("Text").GetComponent<Text>().text;
        newAsg.tries = int.Parse(dropdown.options[dropdown.value].text);

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
        DateTime dt_startDate = DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[0], time[1], time[2], time[3], time[4], 0));
        DateTime dt_dueDate = DateTime.Parse(String.Format("{0} {1} {2} {3}:{4}:{5}", time[5], time[6], time[7], time[8], time[9], 0));
        dt_startDate.AddMonths(-1);
        dt_dueDate.AddMonths(-1);
        newAsg.startDate = new AsgDate(dt_startDate);
        newAsg.dueDate = new AsgDate(dt_dueDate);
        
        if(newAsg.assignmentName == ""){
            incompletePopup("Please give the assignment a name.");
            return false;
        }
        if(newAsg.startDate.time() <= DateTime.Now){
            incompletePopup("Start date has passed. Please choose another date");
            return false;
        }
        if(newAsg.startDate.time() >= newAsg.dueDate.time()){
            incompletePopup("Start date cannot be later than the due date");
            return false;
        }
        return true;
    }
}
