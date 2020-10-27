using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Person : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    private GameEvent gameEvent;
    public string infoPath = "player-info.txt";
    private PlayerInfo player;
    private Text nickNameText;
    private GameObject mainCamera;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(gameEvent);
        }
        else
        {
            gameEvent = (GameEvent)stream.ReceiveNext();
        }
    }
    void SetPlayer()
    {
        if (photonView.IsMine)
        {
            transform.position = new Vector3(-5.5f, -5f, 0f);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            nickNameText = GameObject.Find("LeftNickName").GetComponent<Text>();
            ReadPlayer();
        }
        else
        {
            transform.position = new Vector3(5.5f, -5f, 0f);     
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            nickNameText = GameObject.Find("RightNickName").GetComponent<Text>();
        }          
    }
    void ReadPlayer()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(player.correctRead)
        {
            nickNameText.text = player.nickName;
        }
        else
        {
            nickNameText.text = "Player0";
        }  
        mainCamera.GetComponent<EventHandler>().left = new GameEvent(player.nickName);       
    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        mainCamera = GameObject.Find("Main Camera");
        SetPlayer();
    }
    void Update()
    {
        if(photonView.IsMine)
        {
            //gameEvent = mainCamera.GetComponent<EventHandler>().left;
        }
        else
        {
            nickNameText.text = gameEvent.nickName;
            //mainCamera.GetComponent<EventHandler>().right = gameEvent;
        }
    }
}