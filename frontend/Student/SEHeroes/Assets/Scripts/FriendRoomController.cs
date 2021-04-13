using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class FriendRoomController : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public GameObject waitingpanel;
    public TextMeshProUGUI roomN;
    public TMP_Dropdown worldDD;
    public TMP_Dropdown sectionDD;
    public TMP_Dropdown levelDD;
    public static string challengeWorld;
    public static string challengeSection;
    public static string challengeLevel;
    public static Vector3 position;
    public static int playerID;
    public static string player1char;
    public static string player2char;
    public static string player1matric;
    public static string player2matric;
    private bool p2notyet;

    private int MaxPlayersPerRoom = 2;
    // Start is called before the first frame update
    void Start()
    {
        waitingpanel.SetActive(false);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("We are connected already.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void backButton() {
        SceneManager.LoadScene(sceneName:"OpenChallengeMainMenu");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = ProgramStateController.matricNo;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if (!PhotonNetwork.InRoom)
        {
            Debug.Log("Notinroom");
        }
    }
    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };

        if (PhotonNetwork.CreateRoom(ProgramStateController.matricNo, roomOptions, TypedLobby.Default))
        {
            Debug.Log("Create room successfully");
        }
        else
        {
            Debug.Log("Create room failed");
        }
        challengeWorld = worldDD.options[worldDD.value].text;
        challengeSection = sectionDD.options[sectionDD.value].text;
        challengeLevel = levelDD.options[levelDD.value].text;
        Debug.Log(challengeLevel + " " + challengeSection + " " + challengeWorld);
    }

    public void OnClick_EnterRoom()
    {

        if (PhotonNetwork.JoinRoom(roomNameInput.text))
        {
            Debug.Log("Player Joined in the Room " + roomNameInput.text);
        }
        else
        {
            Debug.Log("Failed to join in the room, please fix the error!");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList);
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room);
        }
    }
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log("Create Room Failed: " + codeAndMessage[1]);

    }

    private void OnCreatedRoom(short returnCode, string message)
    {
        Debug.Log("Room Created Successfully");
        PhotonNetwork.JoinRoom(ProgramStateController.matricNo);
    }

    public static void OnClickRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            Debug.Log("Player Joined in the Room" + roomName);
        }
        else
        {
            Debug.Log("Failed to join in the room, please fix the error!");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == 1)
        {
            ExitGames.Client.Photon.Hashtable challengeinfo = new ExitGames.Client.Photon.Hashtable();
            challengeinfo.Add("world", challengeWorld);
            challengeinfo.Add("section", challengeSection);
            challengeinfo.Add("level", challengeLevel);
            challengeinfo.Add("player1char", ProgramStateController.characterType);
            challengeinfo.Add("player1matric",ProgramStateController.matricNo);
            player1char = ProgramStateController.characterType;
            player1matric = ProgramStateController.matricNo;
            PhotonNetwork.CurrentRoom.SetCustomProperties(challengeinfo);
            playerID = 1;
            position = ProgramStateController.player1Pos;
        }
        else
        {
            challengeLevel = PhotonNetwork.CurrentRoom.CustomProperties["level"].ToString();
            challengeSection = PhotonNetwork.CurrentRoom.CustomProperties["section"].ToString();
            challengeWorld = PhotonNetwork.CurrentRoom.CustomProperties["world"].ToString();
            player1char = PhotonNetwork.CurrentRoom.CustomProperties["player1char"].ToString();
            player1matric = PhotonNetwork.CurrentRoom.CustomProperties["player1matric"].ToString();

            ExitGames.Client.Photon.Hashtable challengeinfo = new ExitGames.Client.Photon.Hashtable();
            challengeinfo.Add("world", challengeWorld);
            challengeinfo.Add("section", challengeSection);
            challengeinfo.Add("level", challengeLevel);
            challengeinfo.Add("player1char", player1char);
            challengeinfo.Add("player2char", ProgramStateController.characterType);
            challengeinfo.Add("player1matric",player1matric);
            challengeinfo.Add("player2matric",ProgramStateController.matricNo); 
            player2char = ProgramStateController.characterType;
            player2matric = ProgramStateController.matricNo;
            PhotonNetwork.CurrentRoom.SetCustomProperties(challengeinfo);
            Debug.Log("After Join, " + challengeLevel + " " + challengeSection + " " + challengeWorld);
            playerID = 2;
            position = ProgramStateController.player2Pos;

        }

        if (playerCount != MaxPlayersPerRoom)
        {
            Debug.Log("clinet is waiting for opponent");
            Debug.Log(PhotonNetwork.CurrentRoom.Name + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            waitingpanel.SetActive(true);
            roomN.SetText("Room No.: " + PhotonNetwork.CurrentRoom.Name + " Players: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + MaxPlayersPerRoom);

        }
        else
        {
            Debug.Log("Matching is ready");
            if(playerID==1){
                player2char = PhotonNetwork.CurrentRoom.CustomProperties["player2char"].ToString();
                player2matric = PhotonNetwork.CurrentRoom.CustomProperties["player2matric"].ToString();}
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("ChallengeFriendsBattle");
        }
        else
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        }

    }
}
