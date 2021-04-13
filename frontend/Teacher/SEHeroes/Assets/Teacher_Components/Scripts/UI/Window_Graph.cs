using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;

public class Window_Graph : MonoBehaviour
{
    List<string> worldList;
    List<string> sectionList;
    List<string> levelList;

    List<JSONNode> scoreList;
    List<int> avgScoreList;
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateXWorld;
    private RectTransform labelTemplateXSection;
    private RectTransform labelTemplateXLevel;
    private RectTransform labelTemplateY;

    List<string> worldTextArray = new List<string>();

    List<string> allSectionArray = new List<string>();
    List<string> allLevelArray = new List<string>();
    List<int> allAvgScoreArray = new List<int>();
    List<JSONNode> allScoreArray = new List<JSONNode>();

    private readonly string baseStoryModeReportURL = "https://seheroes.herokuapp.com/storyModeReport";
  
    

    private void Awake () 
    {
        StartCoroutine(GetStoryModeReport());
    }


    IEnumerator GetStoryModeReport()
    {
        UnityWebRequest StoryModeReportRequest = UnityWebRequest.Get(baseStoryModeReportURL);

        yield return StoryModeReportRequest.SendWebRequest();

        if (StoryModeReportRequest.isNetworkError || StoryModeReportRequest.isHttpError)
        {
            //Debug.LogError(StoryModeReportRequest.error);
            yield break;
        }

        JSONNode storyReportInfo = JSON.Parse(StoryModeReportRequest.downloadHandler.text);

        int sum = 0;
        int noStudents = 0;
        int avgScore = 0;
        
        for (int i = 0; i < 9; i++)
        {
           worldTextArray.Add(storyReportInfo[i]["world"]);
           allSectionArray.Add(storyReportInfo[i]["section"]);
           allLevelArray.Add(storyReportInfo[i]["level"]);
           allScoreArray.Add(storyReportInfo[i]["data"]);
           //Debug.log(storyReportInfo[i]);
            
            //Calculate average score in a level
            sum = sum + allScoreArray[i]["1"]*1 + allScoreArray[i]["2"]*2 + allScoreArray[i]["3"]*3;
            noStudents = noStudents + allScoreArray[i]["0"] + allScoreArray[i]["1"] + allScoreArray[i]["2"] + allScoreArray[i]["3"];
            if (noStudents > 0)
            {
                 avgScore = sum/noStudents;
            }
           

            allAvgScoreArray.Add(avgScore);
            sum=0;
            noStudents=0;
            
        }

        worldList = worldTextArray;
        

        sectionList = new List<string>();
        sectionList = allSectionArray;
        
        levelList = new List<string>();
        levelList = allLevelArray; 

        avgScoreList = new List<int>();
        avgScoreList = allAvgScoreArray; 

        /*
        if (worldList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < worldList.Count; i++)
            { 
                string value = worldList[i];
                string studentNumber = sectionList[i].Count.ToString();
                //Debug.Log(sectionList[i].Count);
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_No").GetComponent<Text>().text = i.ToString();
                playerTextPanel.transform.Find("Text_Index").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Students").GetComponent<Text>().text = studentNumber;
                
                playerTextPanel.transform.Find("Text_Students").transform.Find("Button_View").GetComponent<Button>().onClick.AddListener(() => {
                indexNumber = playerTextPanel.transform.Find("Text_Index").GetComponent<Text>().text;
                viewTutorial();
            });
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
        */

        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateXWorld = graphContainer.Find("labelTemplateX_World").GetComponent<RectTransform>();
        labelTemplateXSection = graphContainer.Find("labelTemplateX_Section").GetComponent<RectTransform>();
        labelTemplateXLevel = graphContainer.Find("labelTemplateX_Level").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
 

        List<int> valueList = new List<int>();
        valueList = avgScoreList;
        
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 10f;
        float xSize = 100f;

        GameObject lastCircleGameObject = null;
        for (int i=0; i<valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject = circleGameObject;

            RectTransform labelXWorld = Instantiate(labelTemplateXWorld);
            labelXWorld.SetParent(graphContainer, false);
            labelXWorld.gameObject.SetActive(true);
            labelXWorld.anchoredPosition = new Vector2(xPosition, -7f);
            labelXWorld.GetComponent<Text>().text = worldList[i];

            RectTransform labelXSection = Instantiate(labelTemplateXSection);
            labelXSection.SetParent(graphContainer, false);
            labelXSection.gameObject.SetActive(true);
            labelXSection.anchoredPosition = new Vector2(xPosition, -27f);
            labelXSection.GetComponent<Text>().text = sectionList[i];

            RectTransform labelXLevel = Instantiate(labelTemplateXLevel);
            labelXLevel.SetParent(graphContainer, false);
            labelXLevel.gameObject.SetActive(true);
            labelXLevel.anchoredPosition = new Vector2(xPosition, -47f);
            labelXLevel.GetComponent<Text>().text = levelList[i];


        }

        int separatorCount = 10;
        for (int i=0; i<=separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

        }
        

    }


    private GameObject CreateCircle(Vector2 anchoredPosition) 
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        return gameObject;
    }

    //Come back to this point to try again
/*
    private void ShowGraph(List<int> valueList) 
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastCircleGameObject = null;
        for (int i=0; i<valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject = circleGameObject;

            RectTransform labelXWorld = Instantiate(labelTemplateXWorld);
            labelXWorld.SetParent(graphContainer, false);
            labelXWorld.gameObject.SetActive(true);
            labelXWorld.anchoredPosition = new Vector2(xPosition, -7f);
            labelXWorld.GetComponent<Text>().text = i.ToString();

            RectTransform labelXSection = Instantiate(labelTemplateXSection);
            labelXSection.SetParent(graphContainer, false);
            labelXSection.gameObject.SetActive(true);
            labelXSection.anchoredPosition = new Vector2(xPosition, -27f);
            labelXSection.GetComponent<Text>().text = i.ToString();

            RectTransform labelXLevel = Instantiate(labelTemplateXLevel);
            labelXLevel.SetParent(graphContainer, false);
            labelXLevel.gameObject.SetActive(true);
            labelXLevel.anchoredPosition = new Vector2(xPosition, -47f);
            labelXLevel.GetComponent<Text>().text = i.ToString();


        }

        int separatorCount = 10;
        for (int i=0; i<=separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

        }
    }
    */

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        rectTransform.sizeDelta = new Vector2(distance,3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0,0, UtilsClass.GetAngleFromVectorFloat(dir));
    }


}
