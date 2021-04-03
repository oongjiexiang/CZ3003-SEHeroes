using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;

public static class APIController
{
    public static List<JSONNode> allQA = new List<JSONNode>();
    private static readonly string baseStoryModeQuesAPIURL = "https://seheroes.herokuapp.com/storyModeQuestion?";
    public static bool APIdone = false;

    // Get Story Mode Questions and Answers
    public static IEnumerator GetStoryModeQuesAPI() {
        string QuesURL = baseStoryModeQuesAPIURL +
                                "world=" +ProgramStateController.world.Split(' ')[1] +
                                    "&section=" + ProgramStateController.section +
                                        "&level=" + ProgramStateController.level;
        UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
        yield return quesRequest.SendWebRequest();

        if (quesRequest.isNetworkError || quesRequest.isHttpError)
        {
            Debug.LogError(quesRequest.error);
            yield break;
        }

        JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
        for (int i = 0; i < quesInfo.Count; i++)
            allQA.Add(quesInfo[i]);

        APIdone = true;
    }
}
