using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text logText;
    public Text statusText;
    public InputField roomCode;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    private int numberOfRoom;
    private bool isConnect;
    void Start()
    {
        isConnect = false;
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead)
        {       
            player = CreateAccount();
        } 
        PhotonNetwork.NickName = player.nickName;
        Log("Create player " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        numberOfRoom = Random.Range(10,99);
        Log("Room code: " + numberOfRoom.ToString()); 
    }
    private PlayerInfo CreateAccount()
    {
        PlayerInfo newPlayer = new PlayerInfo(
            "Player" + Random.Range(1000,9999).ToString(),
            "1111", "@gmail.com");
            newPlayer.CreateInfoFile(infoPath);

            return newPlayer;
    }
    public override void OnConnectedToMaster()
    {
        isConnect = true;
        statusText.color = new Color(0, 100, 0);
        statusText.text = "Conected!";
        Log("Connected to Master");   
    }
    public void CreateRoom()
    {
        if(!isConnect) return;
        Log("Creating...");  
        PhotonNetwork.CreateRoom(numberOfRoom.ToString(), new Photon.Realtime.RoomOptions{MaxPlayers = 2});
    }
    public void JoinToRoom()
    {
        if(!isConnect) return;
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
