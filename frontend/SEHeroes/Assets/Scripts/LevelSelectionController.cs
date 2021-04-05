using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectionController : MonoBehaviour
{
    public string currentLevel;
    public static bool APIdone = false;

    private string world;
    private string section;
    private List<string> unlockedLevels = new List<string>();
    private Image image;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {   
        world = ProgramStateController.world;
        section = ProgramStateController.section;
        image = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();

        if(APIdone){
            for(int i = 0; i < ProgramStateController.allUnlockedState.Count; i++) {
                if(ProgramStateController.allUnlockedState[i]["section"].Equals(section)){
                    for(int j = 0; j < ProgramStateController.allUnlockedState[i]["level"].Count; j++)
                        unlockedLevels.Add(ProgramStateController.allUnlockedState[i]["level"][j]);
                }
            }
        }

        lockSections();
    }

    // Update is called once per frame
    void lockSections() {
        if(!unlockedLevels.Contains(currentLevel) && !string.IsNullOrEmpty(currentLevel)){
            image.color = new Color(255,255,255,0.3f);
            button.interactable = false;
        }
    }

    public void OnClick() {
        ProgramStateController.level = currentLevel;
        DialogMessageController.showMessage("Level Selection");
    }

    public void EnterBattle() {
        ProgramStateController.sceneToLoad = "StoryModeBattle";
        if(world.Contains("Planning"))
                GameObject.FindGameObjectWithTag("PlanningMusic").GetComponent<MusicController>().StopMusic();
            // else if(world.Contains("Village"))
            //     GameObject.FindGameObjectWithTag("VillageMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Snowland"))
            //     GameObject.FindGameObjectWithTag("SnowlandMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Desert"))                
            //     GameObject.FindGameObjectWithTag("DesertMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Ashland"))    
            //     GameObject.FindGameObjectWithTag("AshlandMusic").GetComponent<MusicController>().PlayMusic();
        SceneManager.LoadScene(sceneName:"Loading");
    }
    public void closeConfirmation() {
        DialogMessageController.closeMessage();
    }

    public void BackButtonOnClick() {
        if(world.Contains("Planning"))
                SceneManager.LoadScene(sceneName:"Forest");
        else if(world.Contains("Design"))
                SceneManager.LoadScene(sceneName:"Village");
        else if(world.Contains("Implementation"))
                SceneManager.LoadScene(sceneName:"Snowland");
        else if(world.Contains("Testing"))                
                SceneManager.LoadScene(sceneName:"Desert");
        else if(world.Contains("Maintenance"))    
                SceneManager.LoadScene(sceneName:"Ashland");
    }

}
