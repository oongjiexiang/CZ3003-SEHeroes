using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelectionController : MonoBehaviour
{
    public string currentWorld;
    public static string world;

    SpriteRenderer renderer;
    Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        
        renderer = gameObject.GetComponent<SpriteRenderer>(); 
        initialColor = renderer.material.color;
        initialColor.a = 0.8f;
        renderer.material.color = initialColor;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown() {
        world = currentWorld;

        if(currentWorld.Contains("Forest"))
                SceneManager.LoadScene(sceneName:"Forest");
        else if(currentWorld.Contains("Village"))
                SceneManager.LoadScene(sceneName:"Village");
        else if(currentWorld.Contains("Snowland"))
                SceneManager.LoadScene(sceneName:"Snowland");
        else if(currentWorld.Contains("Desert"))                
                SceneManager.LoadScene(sceneName:"Desert");
        else if(currentWorld.Contains("Ashland"))    
                SceneManager.LoadScene(sceneName:"Ashland");
    }

    void OnMouseOver() {
        initialColor.a = 1.0f;
        renderer.material.color = initialColor;

    }

    void OnMouseExit() {
        initialColor.a = 0.8f;
        renderer.material.color = initialColor;
    }
}
