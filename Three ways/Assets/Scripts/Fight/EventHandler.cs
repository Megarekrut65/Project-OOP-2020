using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public GameObject attackControler;
    public GameObject protectControler;
    public GameObject gameCanvas;
    public GameEvent left;
    public GameEvent right;
    private Text selectedLeft;
    private Text selectedRight;
    private float waitForNext = 3f;
    public int maxHP = 5;
    private bool needWait;
    private bool wasFight;
    private bool isSeted;
    public GameObject leftPerson;
    public GameObject rightPerson;
    public Text roomCodeText;
    private string roomPath = "room-info.txt";
    private Slider leftHP;
    private Slider rightHP;

    IEnumerator ShowControlers()
    {
        left.isSelected = false;
        yield return new WaitForSeconds(waitForNext); 
        gameCanvas.GetComponent<Canvas>().sortingOrder = 20;
        attackControler.SetActive(true);
        attackControler.GetComponent<SelectedWay>().Refresh();
        needWait = false;
        StopCoroutine("ShowControlers");
    }
    void SetObjects()
    {
        left = new GameEvent(maxHP);
        right = new GameEvent(maxHP);  
        leftHP = GameObject.Find("LeftHP").GetComponent<Slider>();
        rightHP = GameObject.Find("RightHP").GetComponent<Slider>();
        selectedLeft = GameObject.Find("LeftCheck").GetComponent<Text>();
        selectedRight = GameObject.Find("RightCheck").GetComponent<Text>();
    }
    void SetRoom()
    {
        CorrectPathes.MakeCorrect(ref roomPath);
        RoomInfo roomCode = new RoomInfo(roomPath);
        roomCodeText.text = "Room: " + roomCode.GetCode().ToString();
    }
    void Start()
    {
        isSeted = false;
    }
    public void Begin()
    {
        SetObjects();
        SetRoom();
        needWait = false;
        isSeted = true;
        StartCoroutine("ShowControlers");
    }
    public void NextPerson()
    {
        if(wasFight)
        {
            StartCoroutine("ShowControlers");
        } 
        else
        {
            rightPerson.GetComponent<Person>().Hitting();
            wasFight = true;
        }
    }
    void SetTemp()
    {
        if(left.isSelected) selectedLeft.text = "+";
        else selectedLeft.text = "-";
        if(right.isSelected) selectedRight.text = "+";
        else selectedRight.text = "-";
        selectedLeft.text += "\n" + left.hp.ToString();
        selectedLeft.text += "\n" +left.attackIndex.ToString();
        selectedLeft.text += "\n" +left.protectIndex.ToString();
        selectedRight.text += "\n" +right.hp.ToString();
        selectedRight.text += "\n" +right.attackIndex.ToString();
        selectedRight.text += "\n" +right.protectIndex.ToString();
    }
    void CheckFight()
    {
        if(!needWait&&left.isSelected && right.isSelected)
        {
            wasFight = false;
            leftPerson.GetComponent<Person>().Hitting(); 
            needWait = true; 
        }
    }
    void CheckSelectings()
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
    }
    IEnumerator FightEnd()
    {
        StopCoroutine("ShowControlers");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndFight");
        StopCoroutine("FightEnd");
    }
    void CheckHealth()
    {
        if(leftHP.value <= 0 || rightHP.value <= 0)
        {
            StartCoroutine("FightEnd");
        }
    }
    void Update()
    {
        if(!isSeted) return;
        CheckHealth();
        SetTemp();
        CheckSelectings();
        CheckFight();
    }
}
public struct GameEvent
{
    public bool isSelected;
    public int hp;
    public int attackIndex;//1-top, 2-centre, 3-botton
    public int protectIndex;

    public GameEvent(int hp = 5)
    {
        isSelected = false;
        this.hp = hp;
        attackIndex = 0;
        protectIndex = 0;
    }
}