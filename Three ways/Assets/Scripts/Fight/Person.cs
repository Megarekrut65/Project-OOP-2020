using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Person : MonoBehaviour
{
    private PhotonView photonView;
    public string infoPath = "player-info.txt";
    private PlayerInfo player;
    private Text leftNickName;
    private Text rightNickName;
    private Slider leftHP;
    private Slider rightHP;
    void SetPlayer()
    {
        if (photonView.IsMine)
        {
            transform.position = new Vector3(-5.5f, -5f, 0f);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            ReadPlayer();
        }
        else
        {
            transform.position = new Vector3(5.5f, -5f, 0f);     
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }          
    }
    void Start()
    {
        leftNickName = GameObject.Find("LeftNickName").GetComponent<Text>();
        rightNickName = GameObject.Find("RightNickName").GetComponent<Text>();
        
        photonView = GetComponent<PhotonView>();
        SetPlayer();
    }

    void ReadPlayer()
    {
        player = new PlayerInfo(infoPath);
        if(player.correctRead) Debug.Log("Correct");
        else Debug.Log("Error");
        leftNickName.text = player.nickName;
    }
    void Update()
    {
        
    }
}
