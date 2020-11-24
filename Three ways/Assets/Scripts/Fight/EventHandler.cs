﻿using System.Collections;
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
    private float waitForNext = 2.8f;
    public int maxHP = 5;
    private bool needWait;
    private bool wasFight;
    private bool isSeted;
    public GameObject leftPerson;
    public GameObject rightPerson;
    private Slider leftHP;
    private Slider rightHP;
    private string resultPath = "result-info.txt";
    private string infoPath = "player-info.txt";
    private PlayerInfo playerInfo;
    private Weapons weapons;
    public int minePoints = 0;
    public int otherPoints = 0;

    IEnumerator ShowControlers()
    {
        left.isSelected = false;
        yield return new WaitForSeconds(waitForNext); 
        left.isAttackChance = false;
        left.isProtectChance = false;
        yield return new WaitForSeconds(0.2f); 
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
    void Start()
    {
        CorrectPathes.MakeCorrect(ref resultPath, ref infoPath);
        playerInfo = new PlayerInfo(infoPath);
        weapons = playerInfo.GetCurrentWeapon();
        isSeted = false;
    }
    public void Begin()
    {
        SetObjects();
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
            wasFight = true;
            if(!rightPerson.GetComponent<Person>().Hitting()) NextPerson();
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
        if(left.isAttackChance) selectedLeft.text += "\nChance";
        if(right.isAttackChance) selectedRight.text += "\nChance";
    }
    void CheckFight()
    {
        if(!needWait&&left.isSelected && right.isSelected)
        {
            wasFight = false;
            needWait = true; 
            if(!leftPerson.GetComponent<Person>().Hitting()) NextPerson();
        }
    }
    void SetStun()
    {
        left.attackIndex = 5;
        left.protectIndex = 5;
        left.isAttackChance = false;
        left.isProtectChance = false;
    }
    void SetNotStun()
    {
        left.attackIndex = attackControler.GetComponent<SelectedWay>().index;
        left.protectIndex = protectControler.GetComponent<SelectedWay>().index;
        left.isAttackChance = MyChance.ThereIs(weapons.CountChance(0));
        left.isProtectChance = MyChance.ThereIs(weapons.CountChance(1));
    }
    void SettingFight()
    {
        attackControler.GetComponent<SelectedWay>().isSelected = false;
        protectControler.GetComponent<SelectedWay>().isSelected = false;
        if(leftPerson.GetComponent<Person>().isStuned)
        {
            SetStun();
        }
        else
        {  
            SetNotStun();
        }
        left.isSelected = true;
        Debug.Log(left.isAttackChance.ToString() + weapons.CountChance(0).ToString());
        Debug.Log(left.isProtectChance.ToString() +  weapons.CountChance(1).ToString());
    }
    void CheckSelectings()
    {
        if(attackControler.GetComponent<SelectedWay>().isSelected &&
        protectControler.GetComponent<SelectedWay>().isSelected)
        {
            SettingFight();
        }
    }
    void Win()
    {
        GameResult result = GameResult.CountWin(minePoints, otherPoints);
        result.WriteResult(resultPath);
    }
    void Lose()
    {
        GameResult result = GameResult.CountLose(minePoints, otherPoints);
        result.WriteResult(resultPath);
    }
    void CheckWiner()
    {
        if(leftHP.value <= 0 && rightHP.value <= 0) 
        {
            Win();
            return;
        }
        if(leftHP.value <= 0) Lose();
        if(rightHP.value <= 0) Win();
    }
    public void ForcedExit(bool mine)
    {
        if(mine) Lose();
        else Win();
        SceneManager.LoadScene("EndFight");
    }
    IEnumerator FightEnd()
    {
        StopCoroutine("ShowControlers");
        yield return new WaitForSeconds(3f);
        CheckWiner();
        SceneManager.LoadScene("EndFight");
        StopCoroutine("FightEnd");
    }
    void CheckHealth()
    {
        if(leftHP.value <= 0) leftPerson.GetComponent<Person>().DieAvatar();
        if(rightHP.value <= 0) rightPerson.GetComponent<Person>().DieAvatar();
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
    public bool isAttackChance;
    public bool isProtectChance;

    public GameEvent(int hp = 5)
    {
        isSelected = false;
        this.hp = hp;
        attackIndex = 0;
        protectIndex = 0;
        isAttackChance = false;
        isProtectChance = false;
    }
}