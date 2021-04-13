using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Student_Creator_List_Script : MonoBehaviour
{
    
    List<JSONNode> studentList;
    List<JSONNode> studentNameList;
    //List<JSONNode> studentNameList;
    GameObject studentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    public GameObject ErrorPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;

    List<JSONNode> studentNameArray = new List<JSONNode>();
    public static string matricNumber;
    public static string studentUsername;
    public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;


    public string matricNum;
    public GameObject inputField;

    public static List<string> matricNumberTextArray = new List<string>();
    List<string> tempMatricNumberTextArray = new List<string>();

     List<string> allUsersMatric = new List<string>();

    private static string baseUsersURL = "https://seheroes.herokuapp.com/user";

    void Start () 
    { 
        ErrorPanel.gameObject.SetActive (false);
       StartCoroutine(GetStudentInfo());
    }


    IEnumerator GetStudentInfo()
    {

        
        //GET students' name from users
        UnityWebRequest usersRequest = UnityWebRequest.Get(baseUsersURL);

        yield return usersRequest.SendWebRequest();

        if (usersRequest.isNetworkError || usersRequest.isHttpError)
        {
            yield break;
        }

        JSONNode userInfo = JSON.Parse(usersRequest.downloadHandler.text);
        
        for (int i = 0; i < userInfo.Count; i++)
            {
                allUsersMatric.Add(userInfo[i]["matricNo"]);
            }

    }
    

    public void onAdd()
    {
        matricNum = inputField.GetComponent<Text>().text;

        

        bool valid = false;
        for (int j = 0; j < allUsersMatric.Count; j++)
        {
                if (matricNum == allUsersMatric[j])
                {

                    bool duplicated = false;
                    for(int k = 0; k < matricNumberTextArray.Count; k++){
                        if(matricNum == matricNumberTextArray[k]) duplicated = true;
                    }

                    

                    if(!duplicated){
                        valid = true;
                        RectTransform rt = (RectTransform)mainScrollContentView.transform;
                        
                        string value = matricNum;
                        GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                        playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                        playerTextPanel.transform.localScale = new Vector3(1,1,1);
                        playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                        playerTextPanel.transform.Find("Text_Matric").GetComponent<Text>().text = value;

                        matricNumberTextArray.Add(matricNum);
                    }

                }
        }
        if(!valid) ErrorPanel.gameObject.SetActive (true);

    }


    public void dismissError()
    {
        ErrorPanel.gameObject.SetActive (false);
    }




}

