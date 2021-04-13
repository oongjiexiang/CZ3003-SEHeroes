using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tutorial_Navigation_Controller : MonoBehaviour
{
    const string tutorialGroupScene = "Tutorial_Group";
    const string tutorialGroupManagementScene = "Tutorial_Group_Management";
    const string tutorialGroupEditorScene = "Tutorial_Group_Editor";
    //const string studentManagementScene = "Student_Management";
    public Transform dropdownMenu;

    
    public void LoadTutorialGroupScene()
    {
    	SceneManager.LoadScene(tutorialGroupScene);
    }

    public void LoadTutorialGroupManagementScene()
    {
    	//int menuIndex = dropdownMenu.GetComponent<Dropdown>().value;
        //List<Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<Dropdown> ().options;
        //string value = menuOptions[menuIndex].text;
        SceneManager.LoadScene(tutorialGroupManagementScene);
        
    }

    public void LoadTutorialGroupEditorScene()
    {
    	SceneManager.LoadScene(tutorialGroupEditorScene);
    }

    /*
    public void LoadStudentManagementScene()
    {
    	SceneManager.LoadScene(studentManagementScene);
    }
    */


}
