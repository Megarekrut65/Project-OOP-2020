using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventHandler : MonoBehaviour
{
    private bool leftSelected;
    private bool rightSelected;
    public GameObject attackControler;
    public GameObject protectControler;
    public GameObject gameCanvas;
    private int leftAttack;
    private int rightAttack;
    private int leftProtect;
    private int rightProtect;
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
    void Update()
    {
        if(leftSelected && rightSelected)
        {
            leftSelected = false;
            rightSelected = false;   
            StartCoroutine("ShowControlers");
        }
    }
}
