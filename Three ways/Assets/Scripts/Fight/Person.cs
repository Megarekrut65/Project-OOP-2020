using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Person : MonoBehaviour
{
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.CurrentRoom != null
    && PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            transform.position = new Vector3(-7f, -5f, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector3(7f, -5f, 0f);     
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
