using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class Tutorial_Editor_Script : MonoBehaviour
{

    public static string matricNumber;
    public static string studentUsername;
    public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;

    public string matricNum;
    public GameObject inputField;
    public GameObject AddedPanel;
   
    

    private readonly string baseAddStudentURL = "https://seheroes.herokuapp.com/tutorialGroup/" + currentTutorialIndex + "/add";
    

    void Start()
    {
        AddedPanel.gameObject.SetActive (false);
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
        public string matricNo;
    }

    IEnumerator AddNewStudent()
    {
        
        matricNum = inputField.GetComponent<Text>().text;
        inputField.GetComponent<Text>().text = " ";
        

        //creating instance of class
        TutorialClass studentAdd = new TutorialClass();
        studentAdd.matricNo = matricNum;

        //use the JsonUtility.ToJson method to serialize it (convert it) to the JSON format
        string json_remove_student = JsonUtility.ToJson(studentAdd);
        // json now contains: '{"matricNo":<student_matric_number>}'

        //To convert the JSON back into an object, use JsonUtility.FromJson:
        // myObject = JsonUtility.FromJson<MyClass>(json);

        //Convert the json format to byte form to prepare to pass to put api
        byte[] bytes_add_student = System.Text.Encoding.UTF8.GetBytes(json_remove_student);

        //Using UnityWebRequest to do a put request to the database
        using (UnityWebRequest studentAddRequest = UnityWebRequest.Put(baseAddStudentURL, bytes_add_student))
        {
            studentAddRequest.SetRequestHeader("Content-Type", "application/json");
 
            yield return studentAddRequest.SendWebRequest();
 
            if (studentAddRequest.isNetworkError)
            {
                Debug.Log(studentAddRequest.error);
            }
            else
            {
                Debug.Log(studentAddRequest.downloadHandler.text);
            }
        }

        AddedPanel.gameObject.SetActive (true);


    }

    public void viewStudent()
    {
        SceneManager.LoadScene("Student_Management");
    }

    public void dismissAdd()
    {
        SceneManager.LoadScene("Tutorial_Group_Management");
    }

  


}

