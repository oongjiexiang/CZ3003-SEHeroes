using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelectionController : MonoBehaviour
{
    public string world;
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
        switch(world) {
            case "Forest":
                SceneManager.LoadScene(sceneName:"Forest");
                break;
            case "Village":
                SceneManager.LoadScene(sceneName:"Village");
                break;
            case "Snowland":
                SceneManager.LoadScene(sceneName:"Snowland");
                break;
            case "Desert":
                SceneManager.LoadScene(sceneName:"Desert");
                break;
            case "Ashland":
                SceneManager.LoadScene(sceneName:"Ashland");
                break;
        }
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
