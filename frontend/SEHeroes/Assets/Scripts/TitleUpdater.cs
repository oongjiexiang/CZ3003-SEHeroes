using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleUpdater : MonoBehaviour
{
    public bool isTitle;

    private string world;
    private string section;
    private string username;

    private TextMeshProUGUI textMesh;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        world = ProgramStateController.world;
        section = ProgramStateController.section;
        username = ProgramStateController.username;
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTitle){
            text = "World: " + world + "\n" + "Section: " + section;
        }
        else{
            text = username;
        }
        textMesh.text = text;
    }
}
