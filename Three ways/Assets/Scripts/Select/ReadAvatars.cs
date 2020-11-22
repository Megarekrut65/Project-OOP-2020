using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadAvatars : MonoBehaviour
{
    public PlayerInfo player;
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
    private string infoPath = "player-info.txt";
    private string dataPath = "data.txt";
    private string newDataPath = "new-data.txt";
    public GameObject nextButton;
    public GameObject buying;
    public Text buyPrice;
    public GameObject coinsText;

    int GetPrice()
    {
        int price = 0;
        switch (currentIndex)
        {
            case 0: price = 100;
                break;
            case 1: price = 800;
                break;
            case 2: price = 1500;
                break;
            default:
                break;
        }
        return price;
    }
    public void BuyAvatar()
    {
        if(player.BuyAvatar(GetPrice(), currentIndex))
        {
            coinsText.GetComponent<Animation>().Play("coins-buy");
            SaveAvatar();
        }
        else
        {
            buying.GetComponent<Animation>().Play("buying-have-not-money");
        }
    }
    public void SaveAvatar()
    {
        player.currentIndexOfAvatar = currentIndex;
        player.EditPlayerInPlayersFile(dataPath, newDataPath, infoPath);
        SetAll();
    }
    void Start()
    {
        CorrectPathes.MakeCorrect(ref infoPath, ref dataPath, ref newDataPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead)
        {       
            SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
        } 
        EditAvatars();
    }
    void EditAvatars()
    {
        for(int i = 0; i <= maxIndex; i++)
        {
            objects[i].SetActive(false);
        }
        SetAll();
    }
    void SetAll()
    {
        objects[currentIndex].SetActive(true);
        objects[currentIndex].GetComponent<Avatar>().SetSet();
        attack.sprite = attacks[currentIndex].sprite;
        protect.sprite = protects[currentIndex].sprite;
        avatarName.text = avatarNames[currentIndex];
        if(player.WasBought(currentIndex)) 
        {
            nextButton.SetActive(true);
            buying.SetActive(false);
        }
        else
        {
            nextButton.SetActive(false);
            buying.SetActive(true);
            buyPrice.text = "$" + GetPrice().ToString();
        }
        coinsText.GetComponent<Text>().text = "$" + player.coins;
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
