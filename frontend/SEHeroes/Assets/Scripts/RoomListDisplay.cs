using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomListDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _roomPrefab;
    private GameObject RoomPrefab
    {
        get { return _roomPrefab; }
    }

    private List<RoomList> _roomListButtons = new List<RoomList>();
    private List<RoomList> RoomListButtons
    {
        get { return _roomListButtons; }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList);
        foreach (RoomInfo room in roomList)
        {
            RoomReceived(room);
            Debug.Log(room);
        }
        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListButtons.FindIndex(x => x.RoomName == room.Name);
        Debug.Log(index);
        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate(RoomPrefab);
                roomListingObj.transform.SetParent(transform, false);

                RoomList roomListing = roomListingObj.GetComponent<RoomList>();
                RoomListButtons.Add(roomListing);
                Debug.Log("Name is "+room.Name);
                index = (RoomListButtons.Count - 1);

            }
        }

        if (index != -1)
        {
            RoomList roomListing = RoomListButtons[index];
            roomListing.SetRoomNameText(room.Name);
            roomListing.Updated = true;
        }
    }

    [PunRPC]
    public void RemoveOldRooms()
    {
        List<RoomList> removeRooms = new List<RoomList>();

        foreach (RoomList roomListing in RoomListButtons)
        {
            if (!roomListing.Updated)
            {
                removeRooms.Add(roomListing);
            }
            else
            {
                roomListing.Updated = false;
            }
        }

        foreach (RoomList roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            RoomListButtons.Remove(roomListing);
            Destroy(roomListingObj);
            Debug.Log(roomListingObj.name + " is Destroyed");
        }
    }
}