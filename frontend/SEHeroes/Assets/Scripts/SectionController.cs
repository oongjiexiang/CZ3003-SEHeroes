using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class SectionController : MonoBehaviour
{

    public string currentSection;

    public static bool APIdone = false;

    private string world;
    
    private List<string> unlockedSections = new List<string>();
    GameObject guideMessageBox;

    // Start is called before the first frame update
    void Start()
    {
        world = ProgramStateController.world;

        if(APIdone){
            for(int i = 0; i < ProgramStateController.allUnlockedState.Count; i++) {
                unlockedSections.Add(ProgramStateController.allUnlockedState[i]["section"]);
                Debug.Log("Unlocked Sections: " + unlockedSections[i]);
            }
        }

        if(string.IsNullOrEmpty(currentSection)){
            guideMessageBox = GameObject.Find("GuideMessage");
        }

        if(world!=null) {
            if(world.Contains("Planning"))
                GameObject.FindGameObjectWithTag("PlanningMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Village"))
            //     GameObject.FindGameObjectWithTag("VillageMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Snowland"))
            //     GameObject.FindGameObjectWithTag("SnowlandMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Desert"))                
            //     GameObject.FindGameObjectWithTag("DesertMusic").GetComponent<MusicController>().PlayMusic();
            // else if(world.Contains("Ashland"))    
            //     GameObject.FindGameObjectWithTag("AshlandMusic").GetComponent<MusicController>().PlayMusic();
        }
            
    }

    void OnCollisionEnter2D(Collision2D other) {
        ProgramStateController.section = currentSection;

        if(unlockedSections.Contains(currentSection))
            SceneManager.LoadScene(sceneName:"LevelSelection");
        else
            DialogMessageController.showMessage("Section Selection");
    }

    public void closeConfirmation() {
        DialogMessageController.closeMessage();
    }

    public void BackButtonOnClick() {
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
        SceneManager.LoadScene(sceneName:"WorldSelection");
    }

    public void closeGuideMessage() {
        guideMessageBox.SetActive(false);
    }
}
