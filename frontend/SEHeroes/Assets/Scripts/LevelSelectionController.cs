using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectionController : MonoBehaviour
{
    public string currentLevel;

    private string world;
    private string section;

    // Start is called before the first frame update
    void Start()
    {   
        world = ProgramStateController.world;
        section = ProgramStateController.section;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick() {
        ProgramStateController.level = currentLevel;
        DialogMessageController.showMessage("Level Selection");
    }

    public void EnterBattle() {
        ProgramStateController.sceneToLoad = "StoryModeBattle";
        SceneManager.LoadScene(sceneName:"Loading");
    }

    public void BackButtonOnClick() {
        if(world.Contains("Forest"))
                SceneManager.LoadScene(sceneName:"Forest");
        else if(world.Contains("Village"))
                SceneManager.LoadScene(sceneName:"Village");
        else if(world.Contains("Snowland"))
                SceneManager.LoadScene(sceneName:"Snowland");
        else if(world.Contains("Desert"))                
                SceneManager.LoadScene(sceneName:"Desert");
        else if(world.Contains("Ashland"))    
                SceneManager.LoadScene(sceneName:"Ashland");
    }

    public void closeConfirmation() {
        DialogMessageController.closeMessage();
    }
}
