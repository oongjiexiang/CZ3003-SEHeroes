using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfileController : MonoBehaviour
{
    TextMeshProUGUI userInformation;
    // Start is called before the first frame update
    void Start()
    {
        userInformation = GameObject.Find("User Information").GetComponent<TextMeshProUGUI>();
        userInformation.text = "Username: " + ProgramStateController.username + "\n" +
                                    "Email: " + ProgramStateController.email + "\n" +
                                        "Matriculation No.: " + ProgramStateController.matricNo + "\n" +
                                            "Character: " + ProgramStateController.characterType + "\n";
        
    }

    // Update is called once per frame

    public void linkToFacebook() {

    }

    public void backToMainMenu() {
        SceneManager.LoadScene(sceneName:"MainMenu");
    }
}
