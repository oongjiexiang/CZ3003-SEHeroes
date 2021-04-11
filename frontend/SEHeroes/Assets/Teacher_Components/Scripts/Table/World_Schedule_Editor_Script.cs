using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;



public class World_Schedule_Editor_Script : MonoBehaviour
{

    public static string matricNumber;
    public static string studentUsername;
    public static string currentTutorialIndex= Tutorial_List_Script.indexNumber;

    public string matricNum;
    public GameObject inputFieldWorld;
    public GameObject inputFieldSection;
    public GameObject inputFieldTutorialGroup;
    public GameObject inputFieldYear;
    public GameObject inputFieldMonth;
    public GameObject inputFieldDay;
    public GameObject inputFieldHour;
    public GameObject inputFieldMinute;
    public GameObject SubmitPanel;
   
    

    private readonly string baseAddWorldURL = "https://seheroes.herokuapp.com/world";
    
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
    public class DateTime
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
    
    }

    
    [System.Serializable]
    public class WorldInfo
    {
        public string world;
        public string section;
        public string tutorialGroupId;
        public DateTime unlockDate;
    }
    

    IEnumerator AddNewStudent()
    {
        
        string currentWorld = inputFieldWorld.GetComponent<Text>().text;
        string currentSection = inputFieldSection.GetComponent<Text>().text;
        string currentTutorialGroup = inputFieldTutorialGroup.GetComponent<Text>().text;
        string currentYear = inputFieldYear.GetComponent<Text>().text;
        string currentMonth = inputFieldMonth.GetComponent<Text>().text;
        string currentDay = inputFieldDay.GetComponent<Text>().text;
        string currentHour = inputFieldHour.GetComponent<Text>().text;
        string currentMinute = inputFieldMinute.GetComponent<Text>().text;




        List<JSONNode> timeList = new List<JSONNode>();
       
        
        

        //string json_add_world_schedule = JsonUtility.ToJson(timeList);
        //Debug.Log(json_add_world_schedule);


        DateTime dateInfoAdd = new DateTime();
        dateInfoAdd.year = System.Int32.Parse(currentYear);
        dateInfoAdd.month = System.Int32.Parse(currentMonth);
        dateInfoAdd.day = System.Int32.Parse(currentDay);
        dateInfoAdd.hour = System.Int32.Parse(currentHour);
        dateInfoAdd.minute = System.Int32.Parse(currentMinute);
        

        //creating instance of class
        WorldInfo worldInfoAdd = new WorldInfo();
        worldInfoAdd.world = currentWorld;
        worldInfoAdd.section = currentSection;
        worldInfoAdd.tutorialGroupId = currentTutorialGroup;
        worldInfoAdd.unlockDate = dateInfoAdd;

        //use the JsonUtility.ToJson method to serialize it (convert it) to the JSON format
        string json_add_world_schedule = JsonUtility.ToJson(worldInfoAdd);
        Debug.Log(json_add_world_schedule);
        // json now contains: '{"matricNo":<student_matric_number>}'

        //To convert the JSON back into an object, use JsonUtility.FromJson:
        // myObject = JsonUtility.FromJson<MyClass>(json);

        //Convert the json format to byte form to prepare to pass to put api
        //byte[] bytes_add_tutorial = System.Text.Encoding.UTF8.GetBytes(json_add_world_schedule);

        //WWWForm form = new WWWForm();
        //form.AddField("world", currentWorld);
        //form.AddField("student", json_add_world_schedule);

        //Using UnityWebRequest to do a put request to the database
        var worldInfoAddRequest =new  UnityWebRequest(baseAddWorldURL, "POST");
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json_add_world_schedule);
        worldInfoAddRequest.SetRequestHeader("Content-Type", "application/json");
        worldInfoAddRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        worldInfoAddRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

        Debug.Log("Status Code: " + worldInfoAddRequest.responseCode);

        yield return worldInfoAddRequest.SendWebRequest();

        if (worldInfoAddRequest.isNetworkError)
        {
            Debug.Log(worldInfoAddRequest.error);
        }
        else
        {
            Debug.Log(worldInfoAddRequest.downloadHandler.text);
        }
        
/*
        using (UnityWebRequest worldInfoAddRequest = UnityWebRequest.Put(baseAddWorldURL, json_add_world_schedule))
        {
            worldInfoAddRequest.SetRequestHeader("Content-Type", "application/json");
 
            yield return worldInfoAddRequest.SendWebRequest();
 
            if (worldInfoAddRequest.isNetworkError)
            {
                Debug.Log(worldInfoAddRequest.error);
            }
            else
            {
                Debug.Log(worldInfoAddRequest.downloadHandler.text);
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
        SceneManager.LoadScene("World_Scheduling");
    }

  


}

