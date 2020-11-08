using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAvatars : MonoBehaviour
{
    private PlayerInfo player;
    public string accountPath;
    public GameObject[] objects;
    public Image[] attacks; 
    public Image[] protects; 
    public string[] avatarNames;
    public Image attack;
    public Image protect;
    public Text avatarName;
    private int currentIndex = 0;
    private int minIndex = 0;
    private int maxIndex = 1;

    void Start()
    {
        CorrectPathes.MakeCorrect(ref accountPath);
        player = new PlayerInfo(accountPath);
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
