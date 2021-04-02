using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionController : MonoBehaviour
{

    public string currentSection;

    private string world;
    GameObject guideMessageBox;

    // Start is called before the first frame update
    void Start()
    {
        world = ProgramStateController.world;
        ProgramStateController.viewState();
        
        if(string.IsNullOrEmpty(currentSection)){
            guideMessageBox = GameObject.Find("GuideMessage");
        }

        if(world!=null) {
            if(world.Contains("Forest"))
                GameObject.FindGameObjectWithTag("ForestMusic").GetComponent<MusicController>().PlayMusic();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        ProgramStateController.section = currentSection;
        
        CharacterController player = other.gameObject.GetComponent<CharacterController>();
        SceneManager.LoadScene(sceneName:"LevelSelection");
    }

    public void BackButtonOnClick() {
        GameObject.FindGameObjectWithTag("ForestMusic").GetComponent<MusicController>().StopMusic();
        SceneManager.LoadScene(sceneName:"WorldSelection");
    }

    public void closeGuideMessage() {
        guideMessageBox.SetActive(false);
    }
}
