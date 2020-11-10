using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayersInfo : MonoBehaviour, IPunObservable
{
    private GameInfo gameInfo;
    private GameObject board;
    private PhotonView photonView;
    public string path = "game-info.txt";
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine) 
        {
            board = GameObject.Find("LeftBoard");
            CorrectPathes.MakeCorrect(ref path);
            gameInfo = new GameInfo(path);
        }
        else
        {
            board = GameObject.Find("RightBoard");
        } 
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
