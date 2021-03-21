using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Object that entered the trigger : " + other);
        CharacterController player = other.gameObject.GetComponent<CharacterController>();
        
        SceneManager.LoadScene(sceneName:"LevelSelection");
    }
}
