using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RegistrationController : MonoBehaviour
{
    private string username;
    private string email;
    private string password;
    private string confirmPassword;
    private string matriculationNo;

    void Start() {
    }

    void Update() {
        
    }

    void getForm() {
        username = InputFieldController.username;
        email = InputFieldController.email;
        password = InputFieldController.password;
        confirmPassword = InputFieldController.confirmPassword;
        matriculationNo = InputFieldController.matriculationNo;
    }

    void validateForm() {
        // validation script not yet fully implemented
        if(string.IsNullOrEmpty(username) ||
            string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(confirmPassword) ||
                        string.IsNullOrEmpty(matriculationNo)) {
                            Debug.Log("One of the fields is empty");
                            MissingInputField.promptMissingField();
                        }
        else
            MissingInputField.clearPrompt();
    }
    public void register() {
        Debug.Log("Register Button Clicked!");
        getForm();
        validateForm();
        // Call backend register flow

        Debug.Log(username);
        Debug.Log(email);
        Debug.Log(password);
        Debug.Log(confirmPassword);
        Debug.Log(matriculationNo);
    }
    public void backButton() {
        SceneManager.LoadScene(sceneName:"Login");
    }
    public void exitGame() {
        Application.Quit();
        Debug.Log("EXITING GAME!");
    }

}
