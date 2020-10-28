using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    public bool isTwoPlayers;
    private GameObject mainCamera;
    void SetPlayer()
    {
        Vector3 pos = Vector3.zero;
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        string playerName = "Person";
        if(player.correctRead) 
            playerName = playerPrefabs[player.currentIndexOfAvatar].name;
        PhotonNetwork.Instantiate(playerName, pos, Quaternion.identity);
    }
    void Start()
    {
        PhotonPeer.RegisterType(typeof(GameEvent), 100, SerializeGameEvent, DeserializeGameEvent);
        isTwoPlayers = false;
        mainCamera = GameObject.Find("Main Camera");
        SetPlayer();
    }

    void Update()
    {
        if(PhotonNetwork.CurrentRoom!= null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if(!isTwoPlayers)
            {
                mainCamera.GetComponent<EventHandler>().Begin();
            }
            isTwoPlayers = true;
        }         
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()//call when current player left the room
    {
        SceneManager.LoadScene("Lobby");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)//call when other player left the room
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
    public static object DeserializeGameEvent(byte[] data)
    {
        GameEvent result = new GameEvent();
        result.isSelected = BitConverter.ToBoolean(data, 0);
        result.attackIndex = BitConverter.ToInt32(data, 1);
        result.protectIndex = BitConverter.ToInt32(data, 5);
        int nameSize = BitConverter.ToInt32(data, 9);
        result.nickName = BitConverter.ToString(data, 13, nameSize);

        return result;
    }
    public static byte[] SerializeGameEvent(object obj)
    {
        GameEvent gameEvent = (GameEvent)obj;
        int nameSize = gameEvent.nickName.Length;
        byte[] result = new byte[nameSize + 1 + 4 + 4 + 4];
        BitConverter.GetBytes(gameEvent.isSelected).CopyTo(result, 0);
        BitConverter.GetBytes(gameEvent.attackIndex).CopyTo(result, 1);
        BitConverter.GetBytes(gameEvent.protectIndex).CopyTo(result, 5);
        BitConverter.GetBytes(nameSize).CopyTo(result, 9);
        char[] name = gameEvent.nickName.ToCharArray();
        Encoding.UTF8.GetBytes(name).CopyTo(result, 13);

        return result;
    }
}
