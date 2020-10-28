﻿using System.Collections;
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
    private Slider hpSlider;
    private Text hpText;
    private int enemyAttack;
    private GameObject mainCamera;
    private Animator animator;
    public Vector2 startPostion;
    public Vector2 endPosition;
    public Vector2 minePostion;
    public Vector2 enemyPosition;
    public float step;
    private float progress;
    private bool isRun;
    private bool wasHit;
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
    void SetMinePlayer()
    {
        minePostion = new Vector3(-5.5f, -5f, 0f);
        enemyPosition = new Vector3(5.5f, -5f, 0f); 
        transform.position = minePostion;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        nickNameText = GameObject.Find("LeftNickName").GetComponent<Text>();
        hpSlider = GameObject.Find("LeftHP").GetComponent<Slider>();
        hpText = GameObject.Find("LeftHPText").GetComponent<Text>();
        ReadPlayer();
        mainCamera.GetComponent<EventHandler>().leftPerson = gameObject;
    }
    void SetOtherPlayer()
    {
        minePostion = new Vector3(5.5f, -5f, 0f); 
        enemyPosition = new Vector3(-5.5f, -5f, 0f);
        transform.position = minePostion;     
        transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        nickNameText = GameObject.Find("RightNickName").GetComponent<Text>();
        hpSlider = GameObject.Find("RightHP").GetComponent<Slider>();
        hpText = GameObject.Find("RightHPText").GetComponent<Text>();
        mainCamera.GetComponent<EventHandler>().rightPerson = gameObject;
    }
    void SetPlayer()
    {
        if (photonView.IsMine)
        {
            SetMinePlayer();
        }
        else
        {
            SetOtherPlayer();
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
    public void Hitting(int enemyAttack)
    {
        this.enemyAttack = enemyAttack;
        startPostion = minePostion;
        endPosition = enemyPosition;
        progress = 0;
        isRun = true;
        wasHit = false;
    }
    public void Fight()
    {
        if(enemyAttack != gameEvent.protectIndex)
        {
            gameEvent.hp--;
            hpSlider.value = gameEvent.hp;
            hpText.text = gameEvent.hp.ToString();
        }
    }
    public void StopHit()
    {
        animator.SetBool("hit", false);
        wasHit = true;
        startPostion = enemyPosition;
        endPosition = minePostion;
        progress = 0;
        isRun = true;
    }
    void Start()
    {
        isRun = false;
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        mainCamera = GameObject.Find("Main Camera");
        SetPlayer();
        StartCoroutine("EventExchange");
    }
    void FixedUpdate()
    {
        if(!isRun) return;
        transform.position = Vector2.Lerp(startPostion, endPosition, progress);
        progress += step;
        if(transform.position.x == endPosition.x)
        {
            isRun = false;
            if(!wasHit) animator.SetBool("hit", true);
            else mainCamera.GetComponent<EventHandler>().NextPerson();
        }
    }
    IEnumerator EventExchange()
    {
        while(true)
        {
            if(photonView.IsMine)
            {
                gameEvent = mainCamera.GetComponent<EventHandler>().left;
            }
            else
            {
                nickNameText.text = gameEvent.nickName;
                mainCamera.GetComponent<EventHandler>().right = gameEvent;
            }
            yield return new WaitForSeconds(0.01f);
        }       
    }
}