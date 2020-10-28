using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    public GameObject attackControler;
    public GameObject protectControler;
    public GameObject gameCanvas;
    public GameEvent left;
    public GameEvent right;
    public Slider leftHP;
    public Slider rightHP;
    public Text selectedLeft;
    public Text selectedRight;
    IEnumerator ShowControlers()
    {
        yield return new WaitForSeconds(4f);
        gameCanvas.GetComponent<Canvas>().sortingOrder = 20;
        attackControler.SetActive(true);
        attackControler.GetComponent<SelectedWay>().Refresh();
        StopCoroutine("ShowControlers");
    }
    void Start()
    {
        left = new GameEvent("Player1");
        right = new GameEvent("Player2");   
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