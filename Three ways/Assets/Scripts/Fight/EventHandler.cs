using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventHandler : MonoBehaviour
{
    public bool leftSelected;
    public bool rightSelected;
    void Start()
    {
        leftSelected = false;
        rightSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftSelected && rightSelected) Debug.Log("Attack!");
    }
}
