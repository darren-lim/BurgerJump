using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class N_RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot m);
    private JoinRoomDelegate joinRoomCallback;

    [SerializeField]
    private Text roomNameText;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot m, JoinRoomDelegate joinRoom)
    {
        match = m;
        joinRoomCallback = joinRoom;
        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")"; 
    }

    public void JoinRoom()
    {
        joinRoomCallback.Invoke(match);
    }
}
