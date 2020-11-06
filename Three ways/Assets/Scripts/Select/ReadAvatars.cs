using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadAvatars : MonoBehaviour
{
    private PlayerInfo player;
    public string accountPath;
    public GameObject[] objects;
    private int currentIndex = 0;
    private int minIndex = 0;
    private int maxIndex = 2;

    void Start()
    {
        CorrectPathes.MakeCorrect(ref accountPath);
        player = new PlayerInfo(accountPath);
    }

    public void Left()
    {
        if(currentIndex < maxIndex)
        {
            currentIndex++;
        }
    }
    public void Right()
    {
        if(currentIndex > minIndex)
        {
            currentIndex--;
        }
    }
}
