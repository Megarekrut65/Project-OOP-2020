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
    private Slider hpSlider;
    private Text hpText;
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
        enemyPosition = new Vector3(4f, -5f, 0f); 
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        nickNameText = GameObject.Find("LeftNickName").GetComponent<Text>();
        hpSlider = GameObject.Find("LeftHP").GetComponent<Slider>();
        hpText = GameObject.Find("LeftHPText").GetComponent<Text>();  
        mainCamera.GetComponent<EventHandler>().leftPerson = gameObject;
    }
    void SetOtherPlayer()
    {
        minePostion = new Vector3(5.5f, -5f, 0f); 
        enemyPosition = new Vector3(-4f, -5f, 0f);
        transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        nickNameText = GameObject.Find("RightNickName").GetComponent<Text>();
        hpSlider = GameObject.Find("RightHP").GetComponent<Slider>();
        hpText = GameObject.Find("RightHPText").GetComponent<Text>();
        mainCamera.GetComponent<EventHandler>().rightPerson = gameObject;
    }
    void SetSame()
    {
        transform.position = minePostion;
        hpSlider.maxValue = mainCamera.GetComponent<EventHandler>().maxHP;
        hpText.text = hpSlider.maxValue.ToString();
        nickNameText.text = photonView.Owner.NickName;   
    }
    void SetPlayer()
    {  
        if (photonView.IsMine) SetMinePlayer();
        else SetOtherPlayer();
        SetSame(); 
    }
    public void Hitting()
    {
        startPostion = minePostion;
        endPosition = enemyPosition;
        progress = 0;
        isRun = true;
        animator.SetBool("run", isRun );
        wasHit = false;
    }
    public void GetHit(int enemyAttack)
    {
        if(enemyAttack != gameEvent.protectIndex)
        {
            if(photonView.IsMine)
            { 
                mainCamera.GetComponent<EventHandler>().left.hp--; 
                hpSlider.value = mainCamera.GetComponent<EventHandler>().left.hp;
                hpText.text = mainCamera.GetComponent<EventHandler>().left.hp.ToString();
            }
            else
            {
                hpSlider.value = gameEvent.hp - 1;
                hpText.text = (gameEvent.hp - 1).ToString();
            }
        }
    }
    public void Fight()
    {
        if(photonView.IsMine) 
        mainCamera.GetComponent<EventHandler>().rightPerson.GetComponent<Person>().GetHit(gameEvent.attackIndex);
        else  mainCamera.GetComponent<EventHandler>().leftPerson.GetComponent<Person>().GetHit(gameEvent.attackIndex);
    }
    public void StopHit()
    {
        animator.SetBool("run", false );
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
            if(!wasHit)
            {
                animator.SetInteger("hit", gameEvent.attackIndex);
            } 
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
                mainCamera.GetComponent<EventHandler>().right = gameEvent;
            }
            yield return new WaitForSeconds(0.01f);
        }       
    }
}