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
