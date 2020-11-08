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
    }
    public void Left()
    {
        if(currentIndex < maxIndex)
        {
            objects[currentIndex].SetActive(false);
            currentIndex++;
            objects[currentIndex].SetActive(true);
            objects[currentIndex].GetComponent<Avatar>().SetSet();
        }
    }
    public void Right()
    {
        if(currentIndex > minIndex)
        {
            objects[currentIndex].SetActive(false);
            currentIndex--;
            objects[currentIndex].SetActive(true);
            objects[currentIndex].GetComponent<Avatar>().SetSet();
        }
    }
}
