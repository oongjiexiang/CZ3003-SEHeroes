using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    const string homeScene = "Home";
    const string tutorialGroupScene = "Tutorial_Group";
    const string storyModeScene = "Story_Mode";
    const string questionBankScene = "Question_Bank";
    const string leaderboardScene = "Leaderboard";
    const string assignmentScene = "Assignments";
    const string settingsScene = "Settings";
    const string tutorialGroupManagementScene = "Tutorial_Group_Management";
    const string tutorialGroupEditorScene = "Tutorial_Group_Editor";
    const string studentManagementScene = "Student_Management";
    const string createAssignmentScene = "Assignments_Creation";
    const string addAssignmentScene = "Assignments_Creation_Add";
    public void LoadNextScene() 
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    	SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadHomeScene()
    {
    	SceneManager.LoadScene(homeScene);
    }

    public void LoadTutorialGroupScene()
    {
    	SceneManager.LoadScene(tutorialGroupScene);
    }

    public void LoadStoryModeScene()
    {
    	SceneManager.LoadScene("Story_Mode");
    }

    public void LoadQuestionBankScene()
    {
    	SceneManager.LoadScene(questionBankScene);
    }

    public void LoadLeaderboardScene()
    {
    	SceneManager.LoadScene(leaderboardScene);
    }

    public void LoadAssignmentsScene()
    {
    	SceneManager.LoadScene(assignmentScene);
    }

    public void LoadSettingsScene()
    {
    	SceneManager.LoadScene(settingsScene);
    }

    public void LoadTutorialGroupManagementScene()
    {
    	SceneManager.LoadScene(tutorialGroupManagementScene);
    }

    public void LoadTutorialGroupEditorScene()
    {
    	SceneManager.LoadScene(tutorialGroupEditorScene);
    }

    public void LoadStudentManagementScene()
    {
    	SceneManager.LoadScene(studentManagementScene);
    }

    public void LoadCreateAssignmentScene()
    {
        SceneManager.LoadScene(createAssignmentScene);
    }
    public void LoadAddAssignmentScene()
    {
        SceneManager.LoadScene(addAssignmentScene);
    }
}
