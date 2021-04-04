using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;

public static class APIController
{
    // Login API
    private static readonly string baseLoginAPIURL = "https://seheroes.herokuapp.com/login?";

    // Register API
    private static readonly string baseRegisterAPIURL = "https://seheroes.herokuapp.com/register?";

    // Story Mode API
    public static List<JSONNode> allQA = new List<JSONNode>();
    private static readonly string baseStoryModeQuesAPIURL = "https://seheroes.herokuapp.com/storyModeQuestion?";
    public static bool APIdone = false;

    // Assignment API
    private static readonly string baseAssQuesAPIURL = "https://seheroes.herokuapp.com/assignmentQuestion?assignmentId=";

    public static IEnumerator RequestLogin(string matricNo, string password) {
        WWWForm form = new WWWForm();
        form.AddField("matricNo", matricNo);
        form.AddField("password", password);

        string RequestLoginURL = baseLoginAPIURL;
                                    
        UnityWebRequest loginRequest = UnityWebRequest.Post(RequestLoginURL, form);
        yield return loginRequest.SendWebRequest();

        if (loginRequest.isNetworkError || loginRequest.isHttpError)
        {
            Debug.LogError(loginRequest.error);
            // yield break;
        }

        JSONNode loginInfo = JSON.Parse(loginRequest.downloadHandler.text);
        Debug.Log(loginInfo);
        if(loginInfo["message"].Equals("Login successfully")){
            ProgramStateController.loggedIn = true;
            ProgramStateController.username = loginInfo["username"];
            ProgramStateController.email = loginInfo["email"];
            ProgramStateController.characterType = loginInfo["character"];
            ProgramStateController.matricNo = loginInfo["matricNo"];
            LoginController.loginSuccessful();
        }
        else{
            MissingInputField.setText(loginInfo["message"]);
            MissingInputField.promptMissingField();
        }
    }

    public static IEnumerator RequestRegister(string username, string password, string email, string character, string matricNo, string confirmPassword) {
        
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("password2", confirmPassword);
        form.AddField("email", email);
        form.AddField("character", character);
        form.AddField("matricNo", matricNo);

        string RequestRegisterURL = baseRegisterAPIURL;
                                    
        UnityWebRequest registerRequest = UnityWebRequest.Post(RequestRegisterURL, form);
        yield return registerRequest.SendWebRequest();

        if (registerRequest.isNetworkError || registerRequest.isHttpError)
        {
            Debug.LogError(registerRequest.error);
            // yield break;
        }

        JSONNode registerInfo = JSON.Parse(registerRequest.downloadHandler.text);
        Debug.Log(registerInfo);
        if(registerInfo["message"].Equals("Register successfully")){
            RegistrationController.registerSuccessful();
        }
        else{
            MissingInputField.setText(registerInfo["message"]);
            MissingInputField.promptMissingField();
        }
    }
    
    // Get Story Mode Questions and Answers
    public static IEnumerator GetStoryModeQuesAPI() {
        APIdone = false;
        string QuesURL = baseStoryModeQuesAPIURL +
                                "world=" +ProgramStateController.world +
                                    "&section=" + ProgramStateController.section +
                                        "&level=" + ProgramStateController.level;
        UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
        yield return quesRequest.SendWebRequest();

        if (quesRequest.isNetworkError || quesRequest.isHttpError)
        {
            Debug.LogError(quesRequest.error);
            yield break;
        }

        JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
        for (int i = 0; i < quesInfo.Count; i++)
            BattleSceneController.allQA.Add(quesInfo[i]);

        APIdone = true;
    }
    public static IEnumerator GetAssignmentQuesAPI() {
        APIdone = false;
        string QuesURL = baseAssQuesAPIURL + ProgramStateController.assID;
        UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
        yield return quesRequest.SendWebRequest();

        if (quesRequest.isNetworkError || quesRequest.isHttpError)
        {
            Debug.LogError(quesRequest.error);
            yield break;
        }

        JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
        for (int i = 0; i < quesInfo.Count; i++)
            AssignmentBattleSceneController.allQA.Add(quesInfo[i]);

        APIdone = true;
    }
}