using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelectionController : MonoBehaviour
{
    public string containerType;
    public string characterType;
    SpriteRenderer[] characterSprites;
    TextMeshProUGUI[] characterTextMesh;
    static GameObject confirmationContainer;

    // Start is called before the first frame update
    void Start()
    {
        if(containerType.Equals("Character")){
            characterSprites = GetComponentsInChildren<SpriteRenderer>();
            characterTextMesh = GetComponentsInChildren<TextMeshProUGUI>();

            foreach (SpriteRenderer re in characterSprites) {
                re.material.color = new Color32(255, 255, 255, 100);
            }

            foreach (TextMeshProUGUI tm in characterTextMesh) {
                tm.faceColor = new Color32(255, 255, 255, 100);
            }
        }

        if(containerType.Equals("Confirmation")){
            confirmationContainer = gameObject;
            confirmationContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        string header = "Confirm Character selection:\n";
        TextMeshProUGUI textMesh = confirmationContainer.GetComponentInChildren<TextMeshProUGUI>();
        switch(characterType){
            case "Swordman":
                textMesh.text = header + "Swordman";
                break;
            case "Bowman":
                textMesh.text = header + "Bowman";
                break;
            case "Warrior":
                textMesh.text = header + "Warrior";
                break;
            case "Magician":
                textMesh.text = header + "Magician";
                break;
        }
        
        confirmationContainer.SetActive(true);
        CharacterSelectionCollider.disableCollider();
        
    }

    void OnMouseOver() {
        foreach (SpriteRenderer re in characterSprites) {
            re.material.color = new Color32(255, 255, 255, 255);
        }

        foreach (TextMeshProUGUI tm in characterTextMesh) {
            tm.faceColor = new Color32(255, 255, 255, 255);
        }
    }

    void OnMouseExit() {
        foreach (SpriteRenderer re in characterSprites) {
            re.material.color = new Color32(255, 255, 255, 100);
        }

        foreach (TextMeshProUGUI tm in characterTextMesh) {
            tm.faceColor = new Color32(255, 255, 255, 100);
        }
    }

    public void confirmSelection() {
        confirmationContainer.SetActive(false);
        CharacterSelectionCollider.enableCollider();
    }
}
