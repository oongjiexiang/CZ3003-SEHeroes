using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public string type;

    public static string username;
    public static string email;
    public static string password;
    public static string confirmPassword;
    public static string matriculationNo;

    private TMP_InputField textMesh;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(type) {
            case "Username":
            username = textMesh.text;
            break;
            case "Email":
            email = textMesh.text;
            break;
            case "Password":
            password = textMesh.text;
            break;
            case "Confirm Password":
            confirmPassword = textMesh.text;
            break;
            case "Matriculation Number":
            matriculationNo = textMesh.text;
            break;
        }
    }
}
