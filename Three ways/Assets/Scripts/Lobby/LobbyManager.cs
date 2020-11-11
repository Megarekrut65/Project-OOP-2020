﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string roomPath = "room-info.txt";
    public Text roomCodeText;
    public InputField maxHP;
    public InputField roomCode;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    public string gamePath = "game-info.txt";
    private int numberOfRoom;
    private bool isConnect;
    private bool needConnect;
    public GameObject waiting;
    public Text waitingText;
    public GameObject errorsBoard;
    public GameObject creating;
    public GameObject joining;
    public GameObject lobbyMenu;

    void Start()
    {
        SetDisconnect();
        CorrectPathes.MakeCorrect(ref infoPath, ref roomPath, ref gamePath);
        PlayerSetting();
        SettingPhoton();
    }
    public void SetDisconnect()
    {
        PhotonNetwork.Disconnect();
        isConnect = false;
        needConnect = true;
    }
    void PlayerSetting()
    {
        player = new PlayerInfo(infoPath);
        if(!player.correctRead)
        {       
            player = CreateAccount();
        } 
        GameInfo gameInfo = new GameInfo(player.currentIndexOfAvatar, player.points);
        gameInfo.CreateInfoFile(gamePath); 
    }
    void SettingPhoton()
    {
        PhotonNetwork.NickName = player.nickName;
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
    }
    private PlayerInfo CreateAccount()
    {
        PlayerInfo newPlayer = new PlayerInfo(
            "Player" + UnityEngine.Random.Range(1000,9999).ToString(),
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
        numberOfRoom = UnityEngine.Random.Range(1000,9999);
        roomCodeText.text = "Room code: " + numberOfRoom.ToString();
        waiting.SetActive(false);
        Debug.Log("Connected to Master");   
    }
    public void CreateRoom()
    {
        if(!isConnect) return;
        waiting.SetActive(true);
        waitingText.text = "Creating...";
        RoomInfo roomInfo = new RoomInfo(numberOfRoom, Convert.ToInt32(maxHP.text));
        roomInfo.WriteInfo(roomPath);
        Debug.Log("Creating...");  
        PhotonNetwork.CreateRoom(numberOfRoom.ToString(), new Photon.Realtime.RoomOptions{MaxPlayers = 2});
    }
    public override void OnCreatedRoom()
    {
        waiting.SetActive(false);
        Debug.Log("Created!");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorsBoard.SetActive(true);
        errorsBoard.GetComponent<Errors>().SetError("Error" + ": " + message);
        Debug.Log("Error" + returnCode.ToString() + ": " + message);  
    }
    public void JoinToRoom()
    {
        if(!isConnect) return;
        waiting.SetActive(true);
        waitingText.text = "Joining...";
        Debug.Log("Joining...");
        if(roomCode.text.Length == 0) OnJoinRoomFailed(32758, " Room code is too short");
        else 
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.WriteInfo(roomPath, false);
            PhotonNetwork.JoinRoom(roomCode.text);
        }
    }
    public override void OnJoinedRoom()
    {
        waiting.SetActive(false);
        Debug.Log("Joined the room");
        PhotonNetwork.LoadLevel("Fight"); 
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorsBoard.SetActive(true);
        errorsBoard.GetComponent<Errors>().SetError("Error" + ": " + message);
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
        if(roomCodeText != null) roomCodeText.text = "Room code: XXXX";
    }
    void Update()
    {
        if(needConnect && !isConnect) 
        {
            waiting.SetActive(true);
            waitingText.text = "Connecting...";
            needConnect = false;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void Creating()
    {
        lobbyMenu.SetActive(false);
        creating.SetActive(true);
        maxHP.text = "20";
    }
    public void Joining()
    {
        lobbyMenu.SetActive(false);
        joining.SetActive(true);
    }
    public void ExitFromLobby()
    {
        PhotonNetwork.Disconnect();
    }
}
