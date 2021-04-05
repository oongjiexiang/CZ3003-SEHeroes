using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class Section_Select_Script : MonoBehaviour
{

    public string sectionChoice;
    public GameObject inputField;

    // Start is called before the first frame update
    public void onSectionSelect()
    {
        sectionChoice = inputField.GetComponent<Text>().text;
        SceneManager.LoadScene("Question_Bank_Management");
    }
 

}
