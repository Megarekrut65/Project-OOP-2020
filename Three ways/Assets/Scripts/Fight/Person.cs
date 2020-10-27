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
    public bool twoSelectings = false;
    private GameEvent gameEvent;
    private GameObject attackControler;
    private GameObject protectControler;
    private GameObject mainCamera;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(twoSelectings );
        }
        else
        {
            twoSelectings  = (bool) stream.ReceiveNext();
        }
    }
    void SetPlayer()
    {
        if (photonView.IsMine)
        {
            transform.position = new Vector3(-5.5f, -5f, 0f);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            ReadPlayer();
        }
        else
        {
            transform.position = new Vector3(5.5f, -5f, 0f);     
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }          
    }
    void Start()
    {
        gameEvent = new GameEvent();
        leftNickName = GameObject.Find("LeftNickName").GetComponent<Text>();
        rightNickName = GameObject.Find("RightNickName").GetComponent<Text>();
        photonView = GetComponent<PhotonView>();
        SetPlayer();
        mainCamera = GameObject.Find("Main Camera"); 
        attackControler = mainCamera.GetComponent<EventHandler>().attackControler;
        protectControler = mainCamera.GetComponent<EventHandler>().protectControler;
        gameEvent.isSelected = false;
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
            mainCamera.GetComponent<EventHandler>().rightSelected = twoSelectings;
            if(twoSelectings) twoSelectings = false;
            return;
        } 
        if(attackControler.GetComponent<SelectedWay>().isSelected&&
        protectControler.GetComponent<SelectedWay>().isSelected)
        {
            attackControler.GetComponent<SelectedWay>().isSelected = false;
            protectControler.GetComponent<SelectedWay>().isSelected = false;
            twoSelectings = true;
            //gameEvent.attackIndex = attackControler.GetComponent<SelectedWay>().index;
            mainCamera.GetComponent<EventHandler>().leftSelected = true;
            if(twoSelectings) twoSelectings = false;
        }
    }
}
public class GameEvent
{
    public bool isSelected;
    public int indexOfAvatar;
    public string nickName;
    public int attackIndex;//1-top, 2-centre, 3-botton
    public int protectionIndex;
    public int hp;
}