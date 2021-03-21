using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour
{
    public string level;
    SpriteRenderer renderer;
    Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>(); 
        initialColor = renderer.material.color;
        // initialColor.a = 0.7f;
        // renderer.material.color = initialColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        switch(level) {
            case "Easy":
                SceneManager.LoadScene(sceneName:"Forest");
                break;
            case "Medium":
                SceneManager.LoadScene(sceneName:"Village");
                break;
            case "Hard":
                SceneManager.LoadScene(sceneName:"Snowland");
                break;
        }
    }

    void OnMouseOver() {
        initialColor.a = 0.7f;
        renderer.material.color = initialColor;

    }

    void OnMouseExit() {
        initialColor.a = 1.0f;
        renderer.material.color = initialColor;
    }
}
