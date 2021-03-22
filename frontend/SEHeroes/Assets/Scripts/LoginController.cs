using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login() {
        // Login logic to be done later -- BRYSON
        SceneManager.LoadScene(sceneName:"MainMenu");
    }
}
