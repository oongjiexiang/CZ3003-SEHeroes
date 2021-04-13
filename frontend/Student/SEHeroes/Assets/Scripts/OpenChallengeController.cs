using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenChallengeController : MonoBehaviourPunCallbacks
{

    public void challengeFriend() {
        SceneManager.LoadScene(sceneName:"ChallengeFriendsRooms");
    }

    public void viewLeaderboard() {
        SceneManager.LoadScene(sceneName:"Leaderboard");
    }

    public void backButton() {
        SceneManager.LoadScene(sceneName:"MainMenu");
    }
}
