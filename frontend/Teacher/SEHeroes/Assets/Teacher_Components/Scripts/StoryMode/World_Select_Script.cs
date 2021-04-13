using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class World_Select_Script : MonoBehaviour
{

    public static string worldChoice;

    // Start is called before the first frame update
    public void onPlanningSelect()
    {
        worldChoice = "Planning";
        SceneManager.LoadScene("Question_Bank_Section");
    }

    public void onDesignSelect()
    {
        worldChoice = "Design";
        SceneManager.LoadScene("Question_Bank_Section");
    }

    public void onImplementationSelect()
    {
        worldChoice = "Implementation";
        SceneManager.LoadScene("Question_Bank_Section");
    }

    public void onTestingSelect()
    {
        worldChoice = "Testing";
        SceneManager.LoadScene("Question_Bank_Section");
    }

    public void onMaintenanceSelect()
    {
        worldChoice = "Maintenance";
        SceneManager.LoadScene("Question_Bank_Section");
    }

 

}
