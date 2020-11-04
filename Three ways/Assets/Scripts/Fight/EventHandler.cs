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
    private float waitForNext = 4f;
    public int maxHP = 5;
    private bool needWait;
    private bool wasFight;
    public GameObject leftPerson;
    public GameObject rightPerson;
    IEnumerator ShowControlers()
    {
        yield return new WaitForSeconds(waitForNext);
        left.isSelected = false;
        right.isSelected = false; 
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
        selectedLeft = GameObject.Find("LeftCheck").GetComponent<Text>();
        selectedRight = GameObject.Find("RightCheck").GetComponent<Text>();
    }
    void Start()
    {
        SetObjects();
        needWait = false;
    }
    public void Begin()
    {
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
    void CheckHealth()
    {
        if(left.hp <= 0 || right.hp <= 0)
        {
            SceneManager.LoadScene("EndFight");
        }
    }
    void Update()
    {
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