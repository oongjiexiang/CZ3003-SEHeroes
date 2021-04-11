using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class World_Select_Editor_Script : MonoBehaviour
{
    public static string worldChoice;

    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();

        
        items.Add("Planning");
        items.Add("Design");
        items.Add("Implementation");
        items.Add("Testing");
        items.Add("Maintenance");
        

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);


        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        try{
            worldChoice = dropdown.options[index].text;
        }
        catch(ArgumentOutOfRangeException){
            worldChoice = "";
        }
    }

}
