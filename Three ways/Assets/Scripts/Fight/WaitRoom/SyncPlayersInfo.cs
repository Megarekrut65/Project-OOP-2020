using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayersInfo : MonoBehaviour, IPunObservable
{
    private GameInfo gameInfo;
    private RoomInfo roomInfo;
    private GameObject board;
    private PhotonView photonView;
    private string roomPath = "room-info.txt"; 
    public string path = "game-info.txt";
    
    void ReadRoom()
    {
        CorrectPathes.MakeCorrect(ref roomPath);
        roomInfo = new RoomInfo();
        roomInfo.ReadInfo(roomPath);
    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine) 
        {
            board = GameObject.Find("LeftBoard");
            CorrectPathes.MakeCorrect(ref path);
            gameInfo = new GameInfo(path);
            ReadRoom();
        }
        else
        {
            board = GameObject.Find("RightBoard");
            roomInfo = new RoomInfo();
        } 
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(gameInfo);
            stream.SendNext(roomInfo);
        }
        else
        {
            gameInfo = (GameInfo)stream.ReceiveNext();
            roomInfo = (RoomInfo)stream.ReceiveNext();
        }
    }
    void Update()
    {
        if(roomInfo.isHost) 
        {
            board.GetComponent<InfoBoard>().SetRoom(roomInfo);
            Debug.Log("host=" + photonView.Owner.NickName);
        }
        board.GetComponent<InfoBoard>().SetData(photonView.Owner.NickName, gameInfo);
        if(photonView.IsMine)
        {
            gameInfo.isReady = board.GetComponent<InfoBoard>().info.isReady;
        }
        else
        {
            board.GetComponent<InfoBoard>().info.isReady = gameInfo.isReady;
        }
    }
}
