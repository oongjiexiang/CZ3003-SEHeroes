using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogMessageController : MonoBehaviour
{
    private static Canvas dialogCanvas;
    private static TextMeshProUGUI textMesh;

    void Start() {
            dialogCanvas = gameObject.GetComponent<Canvas>();
            textMesh = dialogCanvas.GetComponentInChildren<TextMeshProUGUI>();
            dialogCanvas.enabled = false;
        }

    public static void showMessage(string currentInterface) {
        switch(currentInterface){
            case "Registration":
                textMesh.text = "Registration Succesful!";
                break;

            case "Level Selection":
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
                break;
        }
        dialogCanvas.enabled = true;
    }

    public static void closeMessage() {
        dialogCanvas.enabled = false;
    }
}
