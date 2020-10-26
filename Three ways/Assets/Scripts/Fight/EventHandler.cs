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
    IEnumerator ShowControlers()
    {
        yield return new WaitForSeconds(2f);
        attackControler.SetActive(true);
        attackControler.GetComponent<SelectedWay>().Refresh();
        StopCoroutine("ShowControlers");
    }
    void Start()
    {
        leftSelected = false;
        rightSelected = false;
        StartCoroutine("ShowControlers");
    }

    void Update()
    {
        if(leftSelected && rightSelected) Debug.Log("Attack!");
    }
}
