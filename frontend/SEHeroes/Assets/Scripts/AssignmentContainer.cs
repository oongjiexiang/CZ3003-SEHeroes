using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;
using TMPro;

public class AssignmentContainer : MonoBehaviour
{
    public static List<JSONNode> allAssignments = new List<JSONNode>();
    private GameObject[] AssignmentContainerPrefab;
    private GameObject[] clones;
    private int height_offset = -200;
    private int margin = 340;
    public static bool APIdone = false;
    public static bool noAssignment = false;

    void Start() {
        StartCoroutine(APIController.GetAssignments(ProgramStateController.matricNo));
    }

    void Update() {
        if(APIdone){
            if(noAssignment){
                NoAssignment();
            }
            else{
                // debugPrint();
                AssignmentContainerPrefab = new GameObject[allAssignments.Count];
                clones = new GameObject[allAssignments.Count];
                // Loading prefab & assignment details fetched from API
                for(int i = 0; i < AssignmentContainerPrefab.Length; i++){
                    AssignmentContainerPrefab[i] = Instantiate(Resources.Load<GameObject>("Prefabs/AssignmentContainer"));
                    TextMeshProUGUI textMesh = AssignmentContainerPrefab[i].GetComponentInChildren<TextMeshProUGUI>();
                    textMesh.text = "Assignment Name: " + allAssignments[i]["assignmentName"] + "\n" + 
                                        "Due Date: " + allAssignments[i]["dueDate"] + "\n" + 
                                            "Tries Left: " + allAssignments[i]["tries"] + "\n" + 
                                                "Highest Score: " + allAssignments[i]["score"] + "\n";

                    AssignmentSelection script = AssignmentContainerPrefab[i].GetComponentInChildren<Button>().GetComponent<AssignmentSelection>();
                    script.currentAssignmentName = allAssignments[i]["assignmentName"];
                    script.currentAssignmentID = allAssignments[i]["assignmentId"];

                    // Place assignment containers in a list of scrollable manner
                    clones[i] = Instantiate(AssignmentContainerPrefab[i], new Vector3(0, height_offset, 0), Quaternion.identity);
                    Destroy(AssignmentContainerPrefab[i]);
                    clones[i].transform.SetParent(gameObject.transform.parent, false);
                    height_offset = height_offset - margin;
                }
            }
        }
        APIdone = false;
    }
    void debugPrint() {
        for(int i = 0; i < allAssignments.Count; i++){
            Debug.Log(allAssignments[i]["assignmentName"]);
            Debug.Log(allAssignments[i]["dueDate"]);
            Debug.Log(allAssignments[i]["tries"]);
            Debug.Log(allAssignments[i]["score"]);
        }
    }
    void NoAssignment() {
        GameObject noAssignment = Resources.Load<GameObject>("Prefabs/NoAssignmentText");
        var newAssignmentContainer = Instantiate(noAssignment, new Vector3(0, 560, 0), Quaternion.identity);
        newAssignmentContainer.transform.SetParent(gameObject.transform.parent, false);    
    }
}
