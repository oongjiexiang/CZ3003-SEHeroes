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
    private string character;

    void getForm() {
        username = InputFieldController.username;
        email = InputFieldController.email;
        password = InputFieldController.password;
        confirmPassword = InputFieldController.confirmPassword;
        matriculationNo = InputFieldController.matriculationNo;
        character = ProgramStateController.characterSelected;
    }

    public void register() {
        Debug.Log("Register Button Clicked!");
        getForm();

        StartCoroutine(APIController.RequestRegister(username, password, email, character, matriculationNo, confirmPassword));   
    }

    public static void registerSuccessful() {
        DialogMessageController.showMessage("Registration");
        SceneManager.LoadScene(sceneName:"Login");
    }

    public void backButton() {
        SceneManager.LoadScene(sceneName:"Login");
    }
    public void exitGame() {
        Application.Quit();
        Debug.Log("EXITING GAME!");
    }
}
