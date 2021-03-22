using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleUpdater : MonoBehaviour
{
    public bool isTitle;

    private static string world;
    private static string section;
    private static string username;

    private TextMeshProUGUI textMesh;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        world = WorldSelectionController.world;
        section = SectionController.section;
        username = CharacterController.username;
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
