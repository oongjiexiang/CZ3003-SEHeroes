using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;
using TMPro;

public class LeaderboardContainer : MonoBehaviour
{
    public static List<JSONNode> allLeaderboardStudents = new List<JSONNode>();
    private GameObject[] LeaderboardContainerPrefab;
    private int height_offset = -200;
    private int margin = 340;
    public static bool APIdone = false;

    void Start() {
        StartCoroutine(APIController.GetLeaderboard());
    }

    void Update() {

        if(APIdone){
            LeaderboardContainerPrefab = new GameObject[10];

            for(int i = 0; i < allLeaderboardStudents.Count; i++) {
                Debug.Log(allLeaderboardStudents[i]);
            }

            // Loading prefab & assignment details fetched from API
            for(int i = 0; i < LeaderboardContainerPrefab.Length; i++){
                LeaderboardContainerPrefab[i] = Instantiate(Resources.Load<GameObject>("Prefabs/LeaderboardContainer"));
                TextMeshProUGUI textMesh = LeaderboardContainerPrefab[i].GetComponentInChildren<TextMeshProUGUI>();
                textMesh.text = "Username: " + allLeaderboardStudents[i]["username"] + "\n" + 
                                    "Matriculation No.: " + allLeaderboardStudents[i]["matricNo"] + "\n" + 
                                        "Character: " + allLeaderboardStudents[i]["character"] + "\n" + 
                                            "Rating: " + allLeaderboardStudents[i]["openChallengeRating"] + " (RANK " + (i+1).ToString() + ")";

                if(i<=2)
                    textMesh.color = new Color(0.9686275f,0.7960785f,0.09803922f,1);
            }

            // Place assignment containers in a list of scrollable manner
            for(int i = 0; i < LeaderboardContainerPrefab.Length; i++){
                var newAssignmentContainer = Instantiate(LeaderboardContainerPrefab[i], new Vector3(0, height_offset, 0), Quaternion.identity);
                newAssignmentContainer.transform.SetParent(gameObject.transform.parent, false);
                height_offset = height_offset - margin;
            }
        }
        APIdone = false;
    }
}
