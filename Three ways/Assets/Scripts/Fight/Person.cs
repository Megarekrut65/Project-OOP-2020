using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Person : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    public string infoPath = "player-info.txt";
    private PlayerInfo player;
    private Text nickNameText;
    private GameEvent gameEvent;
    private GameObject attackControler;
    private GameObject protectControler;
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
            nickNameText = GameObject.Find("LeftNickName").GetComponent<Text>();
            transform.position = new Vector3(-5.5f, -5f, 0f);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            ReadPlayer();
            gameEvent = new GameEvent(player.nickName);
        }
        else
        {
            nickNameText = GameObject.Find("RightNickName").GetComponent<Text>();
            transform.position = new Vector3(5.5f, -5f, 0f);     
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            gameEvent = new GameEvent();
        }          
    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        SetPlayer();
        mainCamera = GameObject.Find("Main Camera"); 
        attackControler = mainCamera.GetComponent<EventHandler>().attackControler;
        protectControler = mainCamera.GetComponent<EventHandler>().protectControler;
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
    }
    void Update()
    {
        if(!photonView.IsMine && !mainCamera.GetComponent<EventHandler>().rightSelected)
        {
            nickNameText.text = gameEvent.nickName;
            mainCamera.GetComponent<EventHandler>().SetRight(gameEvent.isSelected, gameEvent.attackIndex, gameEvent.protectIndex);
            return;
        } 
        if(photonView.IsMine && 
        attackControler.GetComponent<SelectedWay>().isSelected &&
        protectControler.GetComponent<SelectedWay>().isSelected && 
        !mainCamera.GetComponent<EventHandler>().leftSelected)
        {
            attackControler.GetComponent<SelectedWay>().isSelected = false;
            protectControler.GetComponent<SelectedWay>().isSelected = false;
            gameEvent.isSelected = true;
            gameEvent.attackIndex = attackControler.GetComponent<SelectedWay>().index;
            gameEvent.protectIndex = protectControler.GetComponent<SelectedWay>().index;
            mainCamera.GetComponent<EventHandler>().SetLeft(gameEvent.isSelected, gameEvent.attackIndex, gameEvent.protectIndex);
        }
        else
        {
            gameEvent.isSelected = false;
        }
    }
}
public struct GameEvent
{
    public bool isSelected;
    public string nickName;
    public int attackIndex;//1-top, 2-centre, 3-botton
    public int protectIndex;

    public GameEvent(string nickName = "")
    {
        isSelected = false;
        this.nickName = nickName;
        attackIndex = 0;
        protectIndex = 0;
    }
}