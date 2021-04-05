using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MultiplePlayersRoomController : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public GameObject waitingpanel;
    public TextMeshProUGUI waitingstatus;
    public TextMeshProUGUI roomN;
    public TMP_Dropdown worldDD;
    public TMP_Dropdown sectionDD;
    public TMP_Dropdown levelDD;
    public static string challengeWorld;
    public static string challengeSection;
    public static string challengeLevel;

    private int MaxPlayersPerRoom =2;
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
        } else
        {
            Debug.Log("Create room failed");
        }
        challengeWorld=worldDD.options[worldDD.value].text;
        challengeSection=sectionDD.options[sectionDD.value].text;
        challengeLevel=levelDD.options[levelDD.value].text;
        Debug.Log(challengeLevel+" "+challengeSection+" "+challengeWorld);
    }

    public void OnClick_EnterRoom()
    {
        
        if (PhotonNetwork.JoinRoom(roomNameInput.text))
        {
            Debug.Log("Player Joined in the Room "+roomNameInput.text);
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
            Debug.Log("Player Joined in the Room"+roomName);
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

        if(playerCount != MaxPlayersPerRoom)
        {
            Debug.Log("clinet is waiting for opponent");
            Debug.Log(PhotonNetwork.CurrentRoom.Name+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            waitingpanel.SetActive(true);
            roomN.SetText("Room No.: "+PhotonNetwork.CurrentRoom.Name+" Players: "+PhotonNetwork.CurrentRoom.PlayerCount.ToString()+"/"+MaxPlayersPerRoom);
        }
        else
        {
            Debug.Log("Matching is ready");
        }
        if(playerCount == 1){
            ExitGames.Client.Photon.Hashtable challengeinfo = new ExitGames.Client.Photon.Hashtable();
            challengeinfo.Add("world",challengeWorld);
            challengeinfo.Add("section",challengeSection);
            challengeinfo.Add("level",challengeLevel);
            PhotonNetwork.CurrentRoom.SetCustomProperties(challengeinfo);
        }else{
        challengeLevel = PhotonNetwork.CurrentRoom.CustomProperties["level"].ToString();
        challengeSection = PhotonNetwork.CurrentRoom.CustomProperties["section"].ToString();
        challengeWorld = PhotonNetwork.CurrentRoom.CustomProperties["world"].ToString();
        Debug.Log("After Join, "+challengeLevel+" "+challengeSection+" "+challengeWorld);}
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Match is ready to begin");
            waitingstatus.text="Matching is ready";
            roomN.SetText("Room No.: "+PhotonNetwork.CurrentRoom.Name+" Players: "+PhotonNetwork.CurrentRoom.PlayerCount.ToString()+"/"+MaxPlayersPerRoom);
        
            PhotonNetwork.LoadLevel("MultiplePlayerBattle");
        }
        else{
            Debug.Log(PhotonNetwork.CurrentRoom.Name+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            roomN.SetText("Room No.: "+PhotonNetwork.CurrentRoom.Name+" Players: "+PhotonNetwork.CurrentRoom.PlayerCount.ToString()+"/"+MaxPlayersPerRoom);
        
        }
    }
}
