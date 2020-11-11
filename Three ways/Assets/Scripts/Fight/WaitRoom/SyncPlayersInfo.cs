using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayersInfo : MonoBehaviour, IPunObservable
{
    private GameInfo gameInfo;
    private RoomInfo roomInfo;
    private GameObject board;
    private GameObject mainCamera;
    private PhotonView photonView;
    private string roomPath = "room-info.txt"; 
    private string path = "game-info.txt";
    
    void ReadRoom()
    {
        CorrectPathes.MakeCorrect(ref roomPath);
        roomInfo = new RoomInfo();
        roomInfo.ReadInfo(roomPath);
    }
    void SetMine()
    {
        board = GameObject.Find("LeftBoard");
        CorrectPathes.MakeCorrect(ref path);
        gameInfo = new GameInfo(path);
        ReadRoom();
    }
    void SetOther()
    {
        board = GameObject.Find("RightBoard");
        roomInfo = new RoomInfo();
    }
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine) SetMine();
        else SetOther();
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
        if(roomInfo.isHost) board.GetComponent<InfoBoard>().SetRoom(roomInfo);
        board.GetComponent<InfoBoard>().SetData(photonView.Owner.NickName, gameInfo);
        if(photonView.IsMine)
        {
            gameInfo.isReady = board.GetComponent<InfoBoard>().info.isReady;
            mainCamera.GetComponent<EventHandler>().minePoints = gameInfo.points;
        }
        else
        {
            board.GetComponent<InfoBoard>().info.isReady = gameInfo.isReady;
            mainCamera.GetComponent<EventHandler>().otherPoints = gameInfo.points;
        }
    }
}
