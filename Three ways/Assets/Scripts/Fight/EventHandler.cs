using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventHandler : MonoBehaviour, IPunObservable
{
    public GameObject attackControler;
    public GameObject protectControler;
    public GameObject gameCanvas;
    public GameEvent left;
    public GameEvent right;
    public Slider leftHP;
    public Slider rightHP;
    private PhotonView photonView;
    IEnumerator ShowControlers()
    {
        yield return new WaitForSeconds(4f);
        gameCanvas.GetComponent<Canvas>().sortingOrder = 20;
        attackControler.SetActive(true);
        attackControler.GetComponent<SelectedWay>().Refresh();
        StopCoroutine("ShowControlers");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(left);               
        }
        else
        {
            right = (GameEvent)stream.ReceiveNext();
        }
    }
    void Start()
    {
        left = new GameEvent("Player1");
        right = new GameEvent("Player2");   
        photonView = GetComponent<PhotonView>();
    }
    public void Begin()
    {
        StartCoroutine("ShowControlers");
    }
    void Fight(int enemyAttack, int protect, ref Slider hp)
    {
        if(enemyAttack != protect)
        {
            hp.value--;
        }
    }
    void Update()
    {
        if(attackControler.GetComponent<SelectedWay>().isSelected &&
        protectControler.GetComponent<SelectedWay>().isSelected)
        {
            attackControler.GetComponent<SelectedWay>().isSelected = false;
            protectControler.GetComponent<SelectedWay>().isSelected = false;
            left.isSelected = true;
            left.attackIndex = attackControler.GetComponent<SelectedWay>().index;
            left.protectIndex = protectControler.GetComponent<SelectedWay>().index;
        }
        if(left.isSelected && right.isSelected)
        {
            Fight(right.attackIndex,left.protectIndex, ref leftHP);
            Fight(left.attackIndex,right.protectIndex, ref rightHP);
            left.isSelected = false;
            right.isSelected = false;   
            StartCoroutine("ShowControlers");
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