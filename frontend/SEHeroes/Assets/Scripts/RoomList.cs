using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomList : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _roomNameText;
    private TextMeshProUGUI RoomNameText
    {
        get { return _roomNameText; }
    }

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => ChallengeFriendRoomController.OnClickRoom(RoomNameText.text));
    }

    public bool Updated { get; set; }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
    public string RoomName { get; private set; }
    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}