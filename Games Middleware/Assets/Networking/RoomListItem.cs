using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text roomName;
    public RoomInfo Info;

    public void SetUp(RoomInfo info)
    {
        roomName.text = info.Name;
        Info = info;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(Info);
    }
}
