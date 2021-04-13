using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssignmentSelection : MonoBehaviour
{
    public string currentAssignmentName;
    public string currentAssignmentID;

    public void selectAssignment() {
        ProgramStateController.assName = currentAssignmentName;
        ProgramStateController.assID = currentAssignmentID;
        DialogMessageController.showMessage("Assignment Selection");
    }

    public void enterAssignmentBattle() {
        ProgramStateController.sceneToLoad = "AssignmentBattle";
        SceneManager.LoadScene(sceneName:"Loading");
    }

    public void cancelButton() {
        DialogMessageController.closeMessage();
    }

    public void backButton() {
        SceneManager.LoadScene(sceneName:"MainMenu");
    }

    public void backToOpenChallenge() {
        SceneManager.LoadScene(sceneName:"OpenChallengeMainMenu");
    }

    public void backToAssignmentSelection() {
        SceneManager.LoadScene(sceneName:"AssignmentSelection");
    }
}
