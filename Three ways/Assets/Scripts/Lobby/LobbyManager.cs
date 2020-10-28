using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text statusText;
    public Text roomCodeText;
    public Text nickNameText;
    public InputField roomCode;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    private int numberOfRoom;
    private bool isConnect;
    private bool needConnect;
    void Start()
    {
        isConnect = false;
        needConnect = true;
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead)
        {       
            player = CreateAccount();
        } 
        PhotonNetwork.NickName = player.nickName;
        nickNameText.text += PhotonNetwork.NickName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
    }
    private PlayerInfo CreateAccount()
    {
        PlayerInfo newPlayer = new PlayerInfo(
            "Player" + Random.Range(1000,9999).ToString(),
            "1111", "@gmail.com");
            newPlayer.CreateInfoFile(infoPath);

            return newPlayer;
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
    }
    public override void OnConnectedToMaster()
    {
        isConnect = true;
        statusText.color = new Color(0, 100, 0);
        statusText.text = "Connected!";
        numberOfRoom = Random.Range(1000,9999);
        roomCodeText.text = "Room code: " + numberOfRoom.ToString();
        Debug.Log("Connected to Master");   
    }
    public void CreateRoom()
    {
        if(!isConnect) return;
        Debug.Log("Creating...");  
        PhotonNetwork.CreateRoom(numberOfRoom.ToString(), new Photon.Realtime.RoomOptions{MaxPlayers = 2});
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created!");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error"+returnCode.ToString() + ": " + message);  
    }
    public void JoinToRoom()
    {
        if(!isConnect) return;
        Debug.Log("Joining...");
        if(roomCode.text.Length == 0) OnJoinRoomFailed(32758, " Game does not exist");
        else PhotonNetwork.JoinRoom(roomCode.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the room");
        PhotonNetwork.LoadLevel("Fight"); 
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error"+returnCode.ToString() + ": " + message);  
    }
    public override void OnLeftLobby()
    {
        Debug.Log("Lobby");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnect = false;
        needConnect = true;
        Debug.Log("Disconnected from Master: " + cause.ToString());
        if(statusText == null) return;
        statusText.color = new Color(255, 0, 0);
        statusText.text = "Disconnected!";
        roomCodeText.text = "Room code: xxxx";
    }
    void Update()
    {
        if(needConnect && !isConnect) 
        {
                needConnect = false;
                PhotonNetwork.ConnectUsingSettings();
        }
    }
}
