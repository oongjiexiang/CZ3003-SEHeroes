using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene() 
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    	SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadHomeScene()
    {
    	SceneManager.LoadScene(0);
    }

    public void LoadTutorialGroupScene()
    {
    	SceneManager.LoadScene(1);
    }

    public void LoadStoryModeScene()
    {
    	SceneManager.LoadScene(2);
    }

    public void LoadQuestionBankScene()
    {
    	SceneManager.LoadScene(3);
    }

    public void LoadLeaderboardScene()
    {
    	SceneManager.LoadScene(4);
    }

    public void LoadAssignmentsScene()
    {
    	SceneManager.LoadScene(5);
    }

    public void LoadSettingsScene()
    {
    	SceneManager.LoadScene(6);
    }


}
