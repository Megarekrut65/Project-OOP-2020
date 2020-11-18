using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAvatars : MonoBehaviour
{
    private PlayerInfo player;
    public GameObject[] objects;
    public Image[] attacks; 
    public Image[] protects; 
    public string[] avatarNames;
    public Image attack;
    public Image protect;
    public Text avatarName;
    public int currentIndex = 0;
    private int minIndex = 0;
    private int maxIndex = 2;
    public string infoPath = "player-info.txt";
    
    private PlayerInfo CreateAccount()
    {
        PlayerInfo newPlayer = new PlayerInfo(
            "Player" + UnityEngine.Random.Range(1000,9999).ToString(),
            "1111", "@gmail.com");
            newPlayer.CreateInfoFile(infoPath);

            return newPlayer;
    }
    public void SaveAvatar()
    {
        player.currentIndexOfAvatar = currentIndex;
        player.CreateInfoFile(infoPath);
    }
    void Start()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead)
        {       
            player = CreateAccount();
        } 
        EditAvatars();
    }
    void EditAvatars()
    {
        for(int i = 0; i <= maxIndex; i++)
        {
            objects[i].SetActive(false);
        }
        objects[currentIndex].SetActive(true);
        SetAll();
    }
    void SetAll()
    {
        objects[currentIndex].SetActive(true);
        objects[currentIndex].GetComponent<Avatar>().SetSet();
        attack.sprite = attacks[currentIndex].sprite;
        protect.sprite = protects[currentIndex].sprite;
        avatarName.text = avatarNames[currentIndex];
    }
    public void Right()
    {
        if(currentIndex < maxIndex)
        {
            objects[currentIndex].SetActive(false);
            currentIndex++;
            SetAll();     
        }
    }
    public void Left()
    {
        if(currentIndex > minIndex)
        {
            objects[currentIndex].SetActive(false);
            currentIndex--;
            SetAll();   
        }
    }
}
