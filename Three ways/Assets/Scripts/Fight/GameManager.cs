using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    private PlayerInfo player;
    public string infoPath = "player-info.txt";
    void Start()
    {
        Vector3 pos = Vector3.zero;
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        string playerName = "Person";
        if(player.correctRead) 
            playerName = playerPrefabs[player.currentIndexOfAvatar].name;
       /* if(PhotonNetwork.CurrentRoom != null
    && PhotonNetwork.CurrentRoom.PlayerCount == 1)
        playerName = playerPrefabs[2].name;
        else playerName = playerPrefabs[1].name;*/
        PhotonNetwork.Instantiate(playerName, pos, Quaternion.identity);
    }

    void Update()
    {
        
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
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
