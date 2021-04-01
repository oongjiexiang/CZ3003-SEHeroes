using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;


public class Tutorial_Group_API_Controller : MonoBehaviour
{
    public Text numberStudentsText;
    public List<string> indexNumberTextArray;

    private readonly string baseTutorialIndexURL = "https://seheroes.herokuapp.com/tutorialGroup";

    
    // Start is called before the first frame update
    void Start()
    {
        numberStudentsText.text = "";
        /*
        foreach (string indexNumberText in indexNumberTextArray)
        {
            indexNumberText.text = "";
        }
        */

        GetTutorialIndex();
    }

    IEnumerator GetTutorialIndex()
    {
        UnityWebRequest tutorialIndexRequest = UnityWebRequest.Get(baseTutorialIndexURL);

        yield return tutorialIndexRequest.SendWebRequest();

        if (tutorialIndexRequest.isNetworkError || tutorialIndexRequest.isHttpError)
        {
            Debug.LogError(tutorialIndexRequest.error);
            yield break;
        }

        JSONNode tutorialInfo = JSON.Parse(tutorialIndexRequest.downloadHandler.text);

        for (int i = 0; i < tutorialInfo.Count; i++)
        {
            indexNumberTextArray.Add(tutorialInfo[0]["tutorialGroupId"]);
        }
        Debug.Log(indexNumberTextArray);

    }

    public List<string> GetTutorialIndexArray()
    {
        return indexNumberTextArray;
    }

    

}
