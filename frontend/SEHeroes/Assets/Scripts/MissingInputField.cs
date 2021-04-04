using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MissingInputField : MonoBehaviour
{
    private static TextMeshProUGUI missingFieldText;
    void Start() {
            missingFieldText = gameObject.GetComponent<TextMeshProUGUI>();
            missingFieldText.enabled = false;
        }

    public static void promptMissingField() {
        missingFieldText.enabled = true;
    }

    public static void clearPrompt() {
        missingFieldText.enabled = false;
    }

    public static void setText(string message) {
        missingFieldText.text = message;
    }
}

