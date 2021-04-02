using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectionConfirmation : MonoBehaviour
{
    private static GameObject confirmationMessage;
    private static TextMeshProUGUI textMesh;

    void Start() {
            confirmationMessage = GameObject.Find("ConfirmationMessageCanvas");
            textMesh = GameObject.Find("ConfirmationMessage").GetComponent<TextMeshProUGUI>();
            confirmationMessage.SetActive(false);
        }

    public static void showMessage() {
        switch(ProgramStateController.level){
            case "Easy":
                textMesh.text = "- ENTERING EASY MODE -\nPLEASE CONFIRM";
                break;
            case "Medium":
                textMesh.text = "- ENTERING MEDIUM MODE -\nPLEASE CONFIRM";
                break;
            case "Hard":
                textMesh.text = "- ENTERING HARD MODE -\nPLEASE CONFIRM";
                break;
        }
        confirmationMessage.SetActive(true);
    }

    public static void closeMessage() {
        confirmationMessage.SetActive(false);
    }
}
