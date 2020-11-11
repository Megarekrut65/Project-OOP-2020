﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    private GameObject mainCamera;
    public GameObject leftBoard;
    public GameObject rightBoard;
    public GameObject waitRoom;
    public GameObject startingGame;
    public GameObject gameRoom;
    public GameObject playerInfo;
    private Vector3 pos = Vector3.zero;  
    private bool isStarted;

    void SetPlayer()
    {
        int index = leftBoard.GetComponent<InfoBoard>().info.indexOfAvatar;
        string playerName = playerPrefabs[index].name;
        PhotonNetwork.Instantiate(playerName, pos, Quaternion.identity);
    }
    void RegisterMyTypes()
    {
        PhotonPeer.RegisterType(typeof(GameEvent), 100, SerializeGameEvent, DeserializeGameEvent);
        PhotonPeer.RegisterType(typeof(GameInfo), 101, SerializeGameInfo, DeserializeGameInfo);
        PhotonPeer.RegisterType(typeof(RoomInfo), 102, SerializeRoomInfo, DeserializeRoomInfo);
    }
    void Start()
    {
        isStarted = false;
        RegisterMyTypes();
        PhotonNetwork.Instantiate(playerInfo.name, pos, Quaternion.identity);
        mainCamera = GameObject.Find("Main Camera");
    }
    void Update()
    {
        if(!isStarted&&leftBoard.GetComponent<InfoBoard>().info.isReady &&
        rightBoard.GetComponent<InfoBoard>().info.isReady)
        {
            isStarted = true;
            startingGame.SetActive(true);
            waitRoom.SetActive(false);
            startingGame.GetComponent<StartingGame>().Game();
        }
    }
    public void StartGame()
    {
        gameRoom.SetActive(true);
        SetPlayer();
        mainCamera.GetComponent<EventHandler>().Begin();
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()//call when current player left the room
    {
        SceneManager.LoadScene("EndFight");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)//call when other player left the room
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
    //serializes and deserializes
    public static object DeserializeGameEvent(byte[] data)
    {
        GameEvent result = new GameEvent();
        result.isSelected = BitConverter.ToBoolean(data, 0);
        result.attackIndex = BitConverter.ToInt32(data, 1);
        result.protectIndex = BitConverter.ToInt32(data, 5);
        result.hp = BitConverter.ToInt32(data, 9);

        return result;
    }
    public static byte[] SerializeGameEvent(object obj)
    {
        GameEvent gameEvent = (GameEvent)obj;
        byte[] result = new byte[ 1 + 4 + 4 + 4];
        BitConverter.GetBytes(gameEvent.isSelected).CopyTo(result, 0);
        BitConverter.GetBytes(gameEvent.attackIndex).CopyTo(result, 1);
        BitConverter.GetBytes(gameEvent.protectIndex).CopyTo(result, 5);
        BitConverter.GetBytes(gameEvent.hp).CopyTo(result, 9);

        return result;
    }
    public static object DeserializeGameInfo(byte[] data)
    {
        GameInfo result = new GameInfo();
        result.isReady = BitConverter.ToBoolean(data, 0);
        result.indexOfAvatar = BitConverter.ToInt32(data, 1);
        result.points = BitConverter.ToInt32(data, 5);

        return result;
    }
    public static byte[] SerializeGameInfo(object obj)
    {
        GameInfo gameInfo = (GameInfo)obj;
        byte[] result = new byte[ 1 + 4 + 4];
        BitConverter.GetBytes(gameInfo.isReady).CopyTo(result, 0);
        BitConverter.GetBytes(gameInfo.indexOfAvatar).CopyTo(result, 1);
        BitConverter.GetBytes(gameInfo.points).CopyTo(result, 5);

        return result;
    }
    public static object DeserializeRoomInfo(byte[] data)
    {
        RoomInfo result = new RoomInfo();
        result.isHost = BitConverter.ToBoolean(data, 0);
        result.code = BitConverter.ToInt32(data, 1);
        result.maxHP = BitConverter.ToInt32(data, 5);

        return result;
    }
    public static byte[] SerializeRoomInfo(object obj)
    {
        RoomInfo roomInfo = (RoomInfo)obj;
        byte[] result = new byte[ 1 + 4 + 4];
        BitConverter.GetBytes(roomInfo.isHost).CopyTo(result, 0);
        BitConverter.GetBytes(roomInfo.code).CopyTo(result, 1);
        BitConverter.GetBytes(roomInfo.maxHP).CopyTo(result, 5);

        return result;
    }
}
