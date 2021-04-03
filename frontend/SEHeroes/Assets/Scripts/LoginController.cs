using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    private string username;
    private string password;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getForm() {
        username = InputFieldController.username;
        password = InputFieldController.password;
    }

    void validateForm() {
        // validation script not yet fully implemented
        if(string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password)) {
                            Debug.Log("One of the fields is empty");
                            MissingInputField.promptMissingField();
                        }
        else
            MissingInputField.clearPrompt();
    }

    public void register() {
        Debug.Log("Register Account Button Clicked!");
        SceneManager.LoadScene(sceneName:"CharacterSelection");
    }

    public void login() {
        // Login logic to be done later before going into Main Menu -- BRYSON
        // Fetch user details
        Debug.Log("Login Button Clicked!");
        getForm();
        validateForm();

        Debug.Log(username);
        Debug.Log(password);

        SceneManager.LoadScene(sceneName:"MainMenu");
    }

    public void socialMediaLogin() {
        Debug.Log("Social Media Login Button Clicked!");
        // JIAQING: This is where I neeeeed you :()
    }

    public void forgotPassword() {
        Debug.Log("Forgot Password Button Clicked!");
    }

    public void exitGame() {
        Application.Quit();
    }

}
