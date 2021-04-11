using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    const string homeScene = "Home";
    const string tutorialGroupScene = "Tutorial_Group";
    const string storyModeScene = "Story_Mode";
    const string questionBankScene = "Question_Bank_World";
    const string leaderboardScene = "Leaderboard";
    const string assignmentScene = "Assignments";
    const string settingsScene = "Settings";
    const string tutorialGroupManagementScene = "Tutorial_Group_Management";
    const string tutorialGroupEditorScene = "Tutorial_Group_Editor";
    const string tutorialGroupCreatorScene = "Tutorial_Group_Creator";
    const string studentManagementScene = "Student_Management";
    const string createAssignmentScene = "Assignments_Creation";
    const string addAssignmentScene = "Assignments_Creation_Add";
    const string tutorialReport = "Tutorial_Group_Report";
    const string addAssignmentMetaScene = "Assignment_Creation_Meta";
    const string studentReport = "Student_Report";
    const string worldSchedule = "World_Scheduling";   
    const string worldScheduleEditor = "World_Schedule_Editor";
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

    public void LoadTutorialGroupCreatorScene()
    {
    	SceneManager.LoadScene(tutorialGroupCreatorScene);
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

    public void LoadTutorialReportScene()
    {
        SceneManager.LoadScene(tutorialReport);
    }
    public void LoadCreateAssignmentMetaScene(){
        SceneManager.LoadScene(addAssignmentMetaScene);
    }
    public void LoadStudentReportScene()
    {
        SceneManager.LoadScene(studentReport);
    }
    public void LoadWorldSchedulingScene()
    {
        SceneManager.LoadScene(worldSchedule);
    }
    public void LoadWorldScheduleEditorScene()
    {
        SceneManager.LoadScene(worldScheduleEditor);
    }
}
