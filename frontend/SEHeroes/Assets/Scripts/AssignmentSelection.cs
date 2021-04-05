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
        SceneManager.LoadScene(sceneName:"AssignmentBattle");
    }

    public void cancelButton() {
        DialogMessageController.closeMessage();
    }

    public void backButton() {
        destroyDuplicates();
        SceneManager.LoadScene(sceneName:"MainMenu");
    }
    void destroyDuplicates() {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clones");
        foreach(GameObject clone in clones){
            Destroy(clone);
        }
    }
}
