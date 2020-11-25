using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetPlayerData : MonoBehaviour
{
    public Text nickName;
    public Text points;
    public Text coins;
    private string infoPath = "player-info.txt";
    private string dataPath = "data.txt";
    private string newDataPath = "newData.txt";
    public PlayerInfo player;
    public GameObject deleting;
    public GameObject account;
    void Start()
    {
        CorrectPathes.MakeCorrect(ref infoPath, ref dataPath, ref newDataPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead) SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
        SetData();
    }
    void SetData()
    {
        nickName.text = player.nickName;
        points.text = player.points.ToString();
        coins.text = "$" + player.coins.ToString();
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void DeleteAccount()
    {
        player.DeletePlayer(dataPath, newDataPath, infoPath);
        SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
    }
    public void DeleteButton()
    {
        deleting.SetActive(true);
        account.SetActive(false);
    }
    public void EditButton()
    {

    }
}
