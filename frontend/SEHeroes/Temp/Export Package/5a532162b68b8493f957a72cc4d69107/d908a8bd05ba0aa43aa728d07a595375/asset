using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class OpenChallengeController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private TextMeshProUGUI waitingStatusText = null;
    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 2;

    //private void Awake()=>PhotonNetwork.AutomaticallySyncScene = true;
    // Start is called before the first frame update
    void Start()
    {
        setPlayer();
        waitingStatusPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayer(){
        PhotonNetwork.NickName=ProgramStateController.matricNo;
    }
    public void FindOpponent(){
        isConnecting = true;
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);

        Debug.Log($"Disconnected due to : {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients waiting, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = MaxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != MaxPlayersPerRoom)
        {
            waitingStatusText.text = "waiting for opponent";
            Debug.Log("clinet is waiting for opponent");

        }
        else
        {
            waitingStatusText.text = "Opponent Found";
            Debug.Log("Matching is ready");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Matxh is ready to begin");
            waitingStatusText.text = "Opponent found";

            PhotonNetwork.LoadLevel("Scene_Main");
        }
    }
}
