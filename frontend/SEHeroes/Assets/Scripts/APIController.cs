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
    private static readonly string baseGetUnlockedSectAPIURL = "https://seheroes.herokuapp.com/world/unlocked?";
    private static readonly string baseStoryModeResultAPIURL = "https://seheroes.herokuapp.com/storyModeResult";

    // Open Challenge API
    private static readonly string baseGetLeaderboardAPIURL = "https://seheroes.herokuapp.com/leaderboard";

    // Boss Battle API
    private static readonly string baseAssListAPIURL = "https://seheroes.herokuapp.com/assignment?";
    private static readonly string baseAssQuesAPIURL = "https://seheroes.herokuapp.com/assignmentQuestion?assignmentId=";
    private static readonly string baseAssResultAPIURL = "https://seheroes.herokuapp.com/assignmentResult";

    // ------------------------------ AUTHENTICATION ------------------------------ //

    public static IEnumerator RequestLogin(string matricNo, string password) {
        WWWForm form = new WWWForm();
        form.AddField("matricNo", matricNo);
        form.AddField("password", password);

        string RequestLoginURL = baseLoginAPIURL;
                                    
        UnityWebRequest loginRequest = UnityWebRequest.Post(RequestLoginURL, form);
        yield return loginRequest.SendWebRequest();

        JSONNode loginInfo = JSON.Parse(loginRequest.downloadHandler.text);
        Debug.Log(loginInfo);

        if (loginRequest.isNetworkError || loginRequest.isHttpError)
        {
            Debug.LogError(loginRequest.error);
            MissingInputField.setText(loginInfo["message"]);
            MissingInputField.promptMissingField();
            yield break;
        }
        else if(loginInfo["message"].Equals("Login successfully")){
                ProgramStateController.loggedIn = true;
                ProgramStateController.username = loginInfo["username"];
                ProgramStateController.email = loginInfo["email"];
                ProgramStateController.characterType = loginInfo["character"];
                ProgramStateController.matricNo = loginInfo["matricNo"];
                yield return GetUnlockedStates(loginInfo["matricNo"]);
                LoginController.loginSuccessful();
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

        JSONNode registerInfo = JSON.Parse(registerRequest.downloadHandler.text);
        if (registerRequest.isNetworkError || registerRequest.isHttpError)
        {
            Debug.LogError(registerRequest.error);
            MissingInputField.setText(registerInfo["message"]);
            MissingInputField.promptMissingField();
            yield break;
        }
        else if(registerInfo["message"].Equals("Register successfully")){
            RegistrationController.registerSuccessful();
        }
    }

    // ------------------------------ STORY MODE ------------------------------ //

    // Get Unlocked World, Sections, and respective Levels
    public static IEnumerator GetUnlockedStates(string matricNo) {
        string unlockedSectionsURL = baseGetUnlockedSectAPIURL +
                                "matricNo=" + matricNo;
        UnityWebRequest unlockedSectRequest = UnityWebRequest.Get(unlockedSectionsURL);
        yield return unlockedSectRequest.SendWebRequest();

        if (unlockedSectRequest.isNetworkError || unlockedSectRequest.isHttpError)
        {
            Debug.LogError(unlockedSectRequest.error);
            yield break;
        }

        JSONNode unlockedSectionsInfo = JSON.Parse(unlockedSectRequest.downloadHandler.text);
        for (int i = 0; i < unlockedSectionsInfo.Count; i++){
            ProgramStateController.allUnlockedState.Add(unlockedSectionsInfo[i]);
        }

        SectionController.APIdone = true;
        LevelSelectionController.APIdone = true;
    }
    
    // Get Story Mode Questions and Answers
    public static IEnumerator GetStoryModeQuesAPI() {
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

        BattleSceneController.APIdone = true;
    }

    // Post StoryMode Battle Result
    public static IEnumerator PostStoryModeResult(int star) {
        WWWForm form = new WWWForm();
        form.AddField("world", ProgramStateController.world);
        form.AddField("section", ProgramStateController.section);
        form.AddField("level", ProgramStateController.level);
        form.AddField("matricNo", ProgramStateController.matricNo);
        form.AddField("star", star);

        string PostStoryModeResultURL = baseStoryModeResultAPIURL;
                                    
        UnityWebRequest storyModeResultPostRequest = UnityWebRequest.Post(PostStoryModeResultURL, form);
        yield return storyModeResultPostRequest.SendWebRequest();

        if (storyModeResultPostRequest.isNetworkError || storyModeResultPostRequest.isHttpError)
        {
            Debug.LogError(storyModeResultPostRequest.error);
            yield break;
        }

        JSONNode storyModeResultInfo = JSON.Parse(storyModeResultPostRequest.downloadHandler.text);
        Debug.Log(storyModeResultInfo);
    }

    // ------------------------------ OPEN CHALLENGE ------------------------------ //

    public static IEnumerator GetMultipStoryModeQuesAPI() {
        string QuesURL = baseStoryModeQuesAPIURL +
                                "world=" +MultiPlayerBattleSceneController.world +
                                    "&section=" + MultiPlayerBattleSceneController.section +
                                        "&level=" + MultiPlayerBattleSceneController.level;
        UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
        yield return quesRequest.SendWebRequest();

        if (quesRequest.isNetworkError || quesRequest.isHttpError)
        {
            Debug.LogError(quesRequest.error);
            yield break;
        }

        JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
        for (int i = 0; i < quesInfo.Count; i++)
            MultiPlayerBattleSceneController.allQA.Add(quesInfo[i]);

        MultiPlayerBattleSceneController.APIdone = true;
    }

    public static IEnumerator GetFriendStoryModeQuesAPI() {
        string QuesURL = baseStoryModeQuesAPIURL +
                                "world=" +MultiPlayerBattleSceneController.world +
                                    "&section=" + MultiPlayerBattleSceneController.section +
                                        "&level=" + MultiPlayerBattleSceneController.level;
        UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
        yield return quesRequest.SendWebRequest();

        if (quesRequest.isNetworkError || quesRequest.isHttpError)
        {
            Debug.LogError(quesRequest.error);
            yield break;
        }

        JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
        for (int i = 0; i < quesInfo.Count; i++)
            FriendBattleSceneController.allQA.Add(quesInfo[i]);

        FriendBattleSceneController.APIdone = true;
    }

    // Get Story Mode Questions and Answers
    public static IEnumerator GetLeaderboard() {
        string leaderboardURL = baseGetLeaderboardAPIURL;
        UnityWebRequest leaderboardRequest = UnityWebRequest.Get(leaderboardURL);
        yield return leaderboardRequest.SendWebRequest();

        if (leaderboardRequest.isNetworkError || leaderboardRequest.isHttpError)
        {
            Debug.LogError(leaderboardRequest.error);
            yield break;
        }

        JSONNode leaderboardInfo = JSON.Parse(leaderboardRequest.downloadHandler.text);
        for (int i = 0; i < leaderboardInfo.Count; i++){
            Debug.Log(leaderboardInfo[i]);
            LeaderboardContainer.allLeaderboardStudents.Add(leaderboardInfo[i]);
        }
        LeaderboardContainer.APIdone = true;
    }

    // ------------------------------ BOSS BATTLE ------------------------------ //

    // Get Assignment List
    public static IEnumerator GetAssignments(string matricNo) {

        string RequestAssignmentsURL = baseAssListAPIURL +
                                    "matricNo=" + matricNo;
                                    
        UnityWebRequest assignmentsRequest = UnityWebRequest.Get(RequestAssignmentsURL);
        yield return assignmentsRequest.SendWebRequest();

        if (assignmentsRequest.isNetworkError || assignmentsRequest.isHttpError)
        {
            Debug.LogError(assignmentsRequest.error);
            AssignmentContainer.APIdone = true;
            AssignmentContainer.noAssignment = true;
            yield break;
        }

        JSONNode assignments = JSON.Parse(assignmentsRequest.downloadHandler.text);
        AssignmentContainer.allAssignments.Clear();
        for (int i = 0; i < assignments.Count; i++){
            AssignmentContainer.allAssignments.Add(assignments[i]);
        }
        AssignmentContainer.APIdone = true;
    }

    public static IEnumerator GetAssignmentQuesAPI() {
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

        AssignmentBattleSceneController.APIdone = true;
    }

    // Post Assignment Result
    public static IEnumerator PostAssignmentResult(string assignmentId, string matricNo, string score) {
        WWWForm form = new WWWForm();
        form.AddField("assignmentId", assignmentId);
        form.AddField("matricNo", matricNo);
        form.AddField("score", score);

        string PostAssignmentResultURL = baseAssResultAPIURL;
                                    
        UnityWebRequest assignmentResultPostRequest = UnityWebRequest.Post(PostAssignmentResultURL, form);
        yield return assignmentResultPostRequest.SendWebRequest();

        if (assignmentResultPostRequest.isNetworkError || assignmentResultPostRequest.isHttpError)
        {
            Debug.LogError(assignmentResultPostRequest.error);
            yield break;
        }

        JSONNode assignmentResultInfo = JSON.Parse(assignmentResultPostRequest.downloadHandler.text);
        Debug.Log(assignmentResultInfo);
    }
}