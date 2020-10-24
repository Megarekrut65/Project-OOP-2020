using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    void Start()
    {
        Vector3 pos = new Vector3(0f, 0f, 0f);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
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
