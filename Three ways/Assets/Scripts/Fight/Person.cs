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
    private Text leftNickName;
    private Text rightNickName;
    private Slider leftHP;
    private Slider rightHP;
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
            transform.position = new Vector3(-5.5f, -5f, 0f);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            ReadPlayer();
            gameEvent = new GameEvent(player.nickName);
        }
        else
        {
            transform.position = new Vector3(5.5f, -5f, 0f);     
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            gameEvent = new GameEvent();
        }          
    }
    void Start()
    {
        leftNickName = GameObject.Find("LeftNickName").GetComponent<Text>();
        rightNickName = GameObject.Find("RightNickName").GetComponent<Text>();
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
            leftNickName.text = player.nickName;
        }
        else
        {
            leftNickName.text = "Player0";
        }        
    }
    void Update()
    {
        if(!photonView.IsMine)
        {
            rightNickName.text = gameEvent.nickName;
            mainCamera.GetComponent<EventHandler>().SetRight(gameEvent.isSelected, gameEvent.attackIndex, gameEvent.protectIndex);
            return;
        } 
        if(attackControler.GetComponent<SelectedWay>().isSelected&&
        protectControler.GetComponent<SelectedWay>().isSelected)
        {
            attackControler.GetComponent<SelectedWay>().isSelected = false;
            protectControler.GetComponent<SelectedWay>().isSelected = false;
            gameEvent.isSelected = true;
            gameEvent.attackIndex = attackControler.GetComponent<SelectedWay>().index;
            gameEvent.protectIndex = protectControler.GetComponent<SelectedWay>().index;
        }
        else
        {
            gameEvent.isSelected = false;
        }
        mainCamera.GetComponent<EventHandler>().SetLeft(gameEvent.isSelected, gameEvent.attackIndex, gameEvent.protectIndex);
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