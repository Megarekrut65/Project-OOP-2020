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
    public bool isTrue;
    private GameEvent gameEvent;
    private GameObject attackControler;
    private GameObject mainCamera;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(gameEvent.isSelected );
        }
        else
        {
            gameEvent.isSelected  = (bool) stream.ReceiveNext();
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
        isTrue = false;
        photonView = GetComponent<PhotonView>();
        attackControler = GameObject.Find("AttackControler");
        mainCamera = GameObject.Find("Main Camera");     
        gameEvent.isSelected = false;
        SetPlayer();
    }

    void ReadPlayer()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        leftNickName.text = player.nickName;
    }
    void Update()
    {
        if(!photonView.IsMine)
        {
            mainCamera.GetComponent<EventHandler>().rightSelected = gameEvent.isSelected;
            return;
        } 
        if(attackControler.GetComponent<SelectedWay>().isSelected)
        {
            gameEvent.isSelected = true;
            gameEvent.attackIndex = attackControler.GetComponent<SelectedWay>().index;
            mainCamera.GetComponent<EventHandler>().leftSelected = true;
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