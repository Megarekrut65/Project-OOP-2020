using System.Collections;
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
    public GameObject question;
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
        gameRoom.GetComponent<Animator>().SetBool("move", true);
        SetPlayer();
        mainCamera.GetComponent<EventHandler>().Begin();
    }
    public void AnswerYes()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void AnswerNo()
    {
        question.SetActive(false);
    }
    public void Leave()
    {
        if(isStarted) question.SetActive(true);
        else SceneManager.LoadScene("Lobby");
    }
    public override void OnLeftRoom()//call when current player left the room
    {
        if(isStarted) mainCamera.GetComponent<EventHandler>().ForcedExit(true);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)//call when other player left the room
    {
        if(isStarted) mainCamera.GetComponent<EventHandler>().ForcedExit(false);
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
        result.isAttackChance = BitConverter.ToBoolean(data, 13);
        result.isProtectChance = BitConverter.ToBoolean(data, 14);
        return result;
    }
    public static byte[] SerializeGameEvent(object obj)
    {
        GameEvent gameEvent = (GameEvent)obj;
        byte[] result = new byte[ 1 + 4 + 4 + 4 + 1 + 1];
        BitConverter.GetBytes(gameEvent.isSelected).CopyTo(result, 0);
        BitConverter.GetBytes(gameEvent.attackIndex).CopyTo(result, 1);
        BitConverter.GetBytes(gameEvent.protectIndex).CopyTo(result, 5);
        BitConverter.GetBytes(gameEvent.hp).CopyTo(result, 9);
        BitConverter.GetBytes(gameEvent.isAttackChance).CopyTo(result, 13);
        BitConverter.GetBytes(gameEvent.isProtectChance).CopyTo(result, 14);

        return result;
    }
    public static object DeserializeGameInfo(byte[] data)
    {
        GameInfo result = new GameInfo();
        result.isReady = BitConverter.ToBoolean(data, 0);
        result.indexOfAvatar = BitConverter.ToInt32(data, 1);
        result.points = BitConverter.ToInt32(data, 5);
        result.code = BitConverter.ToInt32(data, 9);
        result.maxHP = BitConverter.ToInt32(data, 13);
        result.isHost = BitConverter.ToBoolean(data, 17);

        return result;
    }
    public static byte[] SerializeGameInfo(object obj)
    {
        GameInfo gameInfo = (GameInfo)obj;
        byte[] result = new byte[ 1 + 4 + 4 + 4 + 4 + 1];
        BitConverter.GetBytes(gameInfo.isReady).CopyTo(result, 0);
        BitConverter.GetBytes(gameInfo.indexOfAvatar).CopyTo(result, 1);
        BitConverter.GetBytes(gameInfo.points).CopyTo(result, 5);
        BitConverter.GetBytes(gameInfo.code).CopyTo(result, 9);
        BitConverter.GetBytes(gameInfo.maxHP).CopyTo(result, 13);
        BitConverter.GetBytes(gameInfo.isHost).CopyTo(result, 17);

        return result;
    }
}
