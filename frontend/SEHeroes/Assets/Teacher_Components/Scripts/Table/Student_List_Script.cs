using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Student_List_Script : MonoBehaviour
{
    
    List<JSONNode> studentList;
    List<JSONNode> studentNameList;
    //List<JSONNode> studentNameList;
    GameObject studentPopUp;
    public Text noRecordLabel;
    public GameObject mainScrollContentView;
    public GameObject ContentDataPanel;
    //public Tutorial_Group_API_Controller tutorialAPI;
    List<JSONNode> matricNumberTextArray = new List<JSONNode>();
    List<JSONNode> studentNameArray = new List<JSONNode>();
    public static string matricNumber;
    public static string studentUsername;
    public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;
    

    private static string baseStudentInfoURL = "https://seheroes.herokuapp.com/tutorialGroup/" + currentTutorialIndex;
    private static string baseUsersURL = "https://seheroes.herokuapp.com/user";
    
    //use this for initialization
    void Start () 
    { 
        currentTutorialIndex= Tutorial_List_Script.indexNumber;
        baseStudentInfoURL = "https://seheroes.herokuapp.com/tutorialGroup/" + currentTutorialIndex;
       StartCoroutine(GetStudentInfo());
       
    }

    IEnumerator GetStudentInfo()
    {
        //GET students' matriculation number in a tutorial group
        UnityWebRequest studentInfoRequest = UnityWebRequest.Get(baseStudentInfoURL);

        yield return studentInfoRequest.SendWebRequest();

        if (studentInfoRequest.isNetworkError || studentInfoRequest.isHttpError)
        {
            //Debug.LogError(studentInfoRequest.error);
            yield break;
        }

        JSONNode studentInfo = JSON.Parse(studentInfoRequest.downloadHandler.text);

        
        //GET students' name from users
        UnityWebRequest usersRequest = UnityWebRequest.Get(baseUsersURL);

        yield return usersRequest.SendWebRequest();

        if (usersRequest.isNetworkError || usersRequest.isHttpError)
        {
            yield break;
        }

        JSONNode userInfo = JSON.Parse(usersRequest.downloadHandler.text);
        
        
        //loop to get a list of student matriculation number in tutorial group
        for (int i = 0; i < studentInfo["student"].Count; i++)
        {
            matricNumberTextArray.Add(studentInfo["student"][i]);
            //Debug.Log();
            for (int j = 0; j < userInfo.Count; j++)
            {
                if (studentInfo["student"][i] == userInfo[j]["matricNo"])
                {
                    studentNameArray.Add(userInfo[j]["username"]);
                }
            }
            
        }
        

        studentList = matricNumberTextArray;
        

        studentNameList = new List<JSONNode>();
        studentNameList = studentNameArray;
        
        if (studentList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainScrollContentView.transform;
            for (int i = 0; i < studentList.Count; i++)
            { 
                string value = studentList[i];
                string studentName = studentNameList[i];
                GameObject playerTextPanel = (GameObject)Instantiate(ContentDataPanel);
                playerTextPanel.transform.SetParent(mainScrollContentView.transform);
                playerTextPanel.transform.localScale = new Vector3(1,1,1);
                playerTextPanel.transform.localPosition = new Vector3(0,0,0);
                playerTextPanel.transform.Find("Text_No").GetComponent<Text>().text = i.ToString();
                playerTextPanel.transform.Find("Text_Matric").GetComponent<Text>().text = value;
                playerTextPanel.transform.Find("Text_Name").GetComponent<Text>().text = studentName;
                
                playerTextPanel.transform.Find("Text_Name").transform.Find("Button_View").GetComponent<Button>().onClick.AddListener(() => {
                matricNumber = playerTextPanel.transform.Find("Text_Matric").GetComponent<Text>().text;
                studentUsername = playerTextPanel.transform.Find("Text_Name").GetComponent<Text>().text;
                viewStudent();
            });
            }
        }
        else 
        {
            noRecordLabel.gameObject.SetActive (true);
        }
        

    }

    public void viewStudent()
    {
        SceneManager.LoadScene("Student_Management");
    }

    public void hidestudentListPopUp() 
    {
        studentPopUp.gameObject.SetActive (false);
    }

    public void showstudentListPopUp() 
    {
        studentPopUp.gameObject.SetActive (true);
    }



}

