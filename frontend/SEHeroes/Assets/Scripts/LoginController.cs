using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;

public class LoginController : MonoBehaviour
{
    private string matricNo;
    private string password;

    void getForm() {
        matricNo = InputFieldController.matriculationNo;
        password = InputFieldController.password;
    }

    public void register() {
        Debug.Log("Register Account Button Clicked!");
        SceneManager.LoadScene(sceneName:"CharacterSelection");
    }

    public void login() {
        Debug.Log("Login Button Clicked!");
        getForm();

        StartCoroutine(APIController.RequestLogin(matricNo, password));
    }

    public static void loginSuccessful() {
        if(ProgramStateController.loggedIn)
            SceneManager.LoadScene(sceneName:"MainMenu");
    }

    public void socialMediaLogin() {
        Debug.Log("Social Media Login Button Clicked!");
    }

    public void forgotPassword() {
        Debug.Log("Forgot Password Button Clicked!");
    }

    public void exitGame() {
        Application.Quit();
    }

}
