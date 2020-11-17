using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayersInfo : MonoBehaviour, IPunObservable
{
    private GameInfo gameInfo;
    private GameObject board;
    private GameObject mainCamera;
    private PhotonView photonView;
    private string path = "game-info.txt";
    
    void SetMine()
    {
        board = GameObject.Find("LeftBoard");
        CorrectPathes.MakeCorrect(ref path);
        gameInfo = new GameInfo(path);
    }
    void SetOther()
    {
        board = GameObject.Find("RightBoard");
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
        }
        else
        {
            gameInfo = (GameInfo)stream.ReceiveNext();
        }
    }
    void Update()
    {
        if(gameInfo.isHost) mainCamera.GetComponent<EventHandler>().maxHP = gameInfo.maxHP; 
        board.GetComponent<InfoBoard>().SetRoom(gameInfo.code);
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
