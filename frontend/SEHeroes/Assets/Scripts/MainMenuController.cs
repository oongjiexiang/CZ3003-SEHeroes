using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    void Start() {

    }

    void Update() {

    }

    public void loadStoryMode() {
        SceneManager.LoadScene(sceneName:"WorldSelection");
    }

    public void loadOpenChallenge() {

    }

    public void loadBossBattle() {

    }

    public void loadProfile() {
        SceneManager.LoadScene(sceneName:"Profile");
    }
}

