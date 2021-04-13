using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;

public class Tutorial_Index_Dropdown_Handler : MonoBehaviour
{

    public Text TextBox;
    List<string> indexNumberTextArray = new List<string>();

    private readonly string baseTutorialIndexURL = "https://seheroes.herokuapp.com/tutorialGroup";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTutorialIndex());

    }

    IEnumerator GetTutorialIndex()
    {
        UnityWebRequest tutorialIndexRequest = UnityWebRequest.Get(baseTutorialIndexURL);

        yield return tutorialIndexRequest.SendWebRequest();

        if (tutorialIndexRequest.isNetworkError || tutorialIndexRequest.isHttpError)
        {
            //Debug.LogError(tutorialIndexRequest.error);
            yield break;
        }

        JSONNode tutorialInfo = JSON.Parse(tutorialIndexRequest.downloadHandler.text);

        for (int i = 0; i < tutorialInfo.Count; i++)
        {
            indexNumberTextArray.Add(tutorialInfo[i]["tutorialGroupId"]);
            
        }

        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> tutorialIndex = new List<string>();

        tutorialIndex = indexNumberTextArray;

        foreach(var index in tutorialIndex)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = index });
        }

        DropdownItemSelected(dropdown);

       //dropdown.onValueChanged.AddListener(delegate DropdownItemSelected(dropdown));


    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        TextBox.text = dropdown.options[index].text;

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
