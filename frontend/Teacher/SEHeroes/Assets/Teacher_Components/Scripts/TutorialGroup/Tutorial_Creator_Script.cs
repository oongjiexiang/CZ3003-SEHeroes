using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Tutorial_Creator_Script : MonoBehaviour
{

    public static string matricNumber;
    public static string studentUsername;
    public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;

    public string matricNum;
    public GameObject inputField;
    public GameObject SubmitPanel;
   
    

    private readonly string baseAddTutorialURL = "https://seheroes.herokuapp.com/tutorialGroup";
    
    void Start()
    {
        SubmitPanel.gameObject.SetActive (false);
    }

    //use this for initialization
    public void onSubmit () 
    { 
       StartCoroutine(AddNewStudent());
       
    }


    //class to describe what variables you want to store in your JSON data.
    
    [System.Serializable]
    public class TutorialClass
    {
        public string tutorialGroupId;
        public List<string> student;
    }
    

    IEnumerator AddNewStudent()
    {
        
        string tutorialId = inputField.GetComponent<Text>().text;

        List<string> studentsList = new List<string>();
        studentsList = Student_Creator_List_Script.matricNumberTextArray;
        Debug.Log(studentsList.Count);
        

        //string json_add_tutorial = JsonUtility.ToJson(studentsList);
        //Debug.Log(json_add_tutorial);
        

        //creating instance of class
        TutorialClass tutorialAdd = new TutorialClass();
        tutorialAdd.tutorialGroupId = tutorialId;
        tutorialAdd.student = studentsList;

        //use the JsonUtility.ToJson method to serialize it (convert it) to the JSON format
        string json_add_tutorial = JsonUtility.ToJson(tutorialAdd);
        Debug.Log(json_add_tutorial);
        // json now contains: '{"matricNo":<student_matric_number>}'

        //To convert the JSON back into an object, use JsonUtility.FromJson:
        // myObject = JsonUtility.FromJson<MyClass>(json);

        //Convert the json format to byte form to prepare to pass to put api
        //byte[] bytes_add_tutorial = System.Text.Encoding.UTF8.GetBytes(json_add_tutorial);

        //WWWForm form = new WWWForm();
        //form.AddField("tutorialGroupId", tutorialId);
        //form.AddField("student", json_add_tutorial);

        //Using UnityWebRequest to do a put request to the database
        var tutorialAddRequest =new  UnityWebRequest(baseAddTutorialURL, "POST");
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json_add_tutorial);
        tutorialAddRequest.SetRequestHeader("Content-Type", "application/json");
        tutorialAddRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        tutorialAddRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

        Debug.Log("Status Code: " + tutorialAddRequest.responseCode);

        yield return tutorialAddRequest.SendWebRequest();

        if (tutorialAddRequest.isNetworkError)
        {
            Debug.Log(tutorialAddRequest.error);
        }
        else
        {
            Debug.Log(tutorialAddRequest.downloadHandler.text);
        }
        
/*
        using (UnityWebRequest tutorialAddRequest = UnityWebRequest.Put(baseAddTutorialURL, json_add_tutorial))
        {
            tutorialAddRequest.SetRequestHeader("Content-Type", "application/json");
 
            yield return tutorialAddRequest.SendWebRequest();
 
            if (tutorialAddRequest.isNetworkError)
            {
                Debug.Log(tutorialAddRequest.error);
            }
            else
            {
                Debug.Log(tutorialAddRequest.downloadHandler.text);
            }
        }
*/
        SubmitPanel.gameObject.SetActive (true);


    }

    public void viewStudent()
    {
        SceneManager.LoadScene("Student_Management");
    }


    public void dismissSubmit()
    {
        SceneManager.LoadScene("Tutorial_Group");
    }

  


}

