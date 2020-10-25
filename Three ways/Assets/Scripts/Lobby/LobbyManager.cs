using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text logText;
    public InputField roomCode;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    void Start()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(player.correctRead) PhotonNetwork.NickName = player.nickName;
        else PhotonNetwork.NickName = "Player0";
        Log("Create player " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");   
    }
    public void CreateRoom()
    {
        int Number = Random.Range(10000,99999);
        Log("Room: " + Number.ToString());   
        PhotonNetwork.CreateRoom(Number.ToString(), new Photon.Realtime.RoomOptions{MaxPlayers = 2});
    }
    public void JoinToRoom()
    {
        Log("Joining...");   
        PhotonNetwork.JoinRoom(roomCode.text);
    }
    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Fight"); 
    }
    void Log(string message)
    {
        logText.text += "\n" + message;
        Debug.Log(message);
    }
}
