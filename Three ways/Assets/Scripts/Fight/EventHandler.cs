using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventHandler : MonoBehaviour
{
    public bool leftSelected;
    public bool rightSelected;
    public GameObject attackControler;
    public GameObject protectControler;
    public GameObject gameCanvas;
    private int leftAttack;
    private int rightAttack;
    private int leftProtect;
    private int rightProtect;
    public Slider leftHP;
    public Slider rightHP;
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
        leftSelected = false;
        rightSelected = false;   
    }
    public void Begin()
    {
        StartCoroutine("ShowControlers");
    }
    public void SetLeft(bool isSelect, int attack, int protect)
    {
        leftSelected = isSelect;
        leftAttack = attack;
        leftProtect = protect;
    }
    public void SetRight(bool isSelect, int attack, int protect)
    {
        rightSelected = isSelect;
        rightAttack = attack;
        rightProtect = protect;
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
        if(leftSelected && rightSelected)
        {
            Fight(rightAttack,leftProtect, ref leftHP);
            Fight(leftAttack,rightProtect, ref rightHP);
            leftSelected = false;
            rightSelected = false;   
            StartCoroutine("ShowControlers");
        }
    }
}
