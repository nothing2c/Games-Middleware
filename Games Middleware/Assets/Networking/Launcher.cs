using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] InputField RoomNameField;
    [SerializeField] Text ErrorText;
    [SerializeField] Text RoomNameText;
    [SerializeField] Transform RoomListContent;
    [SerializeField] GameObject RoomListItemPrefab;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject StartButton;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting to master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNameField.text))
            return;

        PhotonNetwork.CreateRoom(RoomNameField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Transform t in PlayerListContent)
            Destroy(t.gameObject);

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(p);
        }

        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorText.text = "Room Creation failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform t in RoomListContent)
            Destroy(t.gameObject);

        foreach(RoomInfo r in roomList)
        {
            if (r.RemovedFromList)
                continue;

            Instantiate(RoomListItemPrefab, RoomListContent).GetComponent<RoomListItem>().SetUp(r);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
