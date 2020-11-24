using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameOver : MonoBehaviour
{
    public Text theText;
    private string resultPath = "result-info.txt";
    private string infoPath = "player-info.txt";
    private string dataPath = "data.txt";
    private string newDataPath = "newData.txt";
    private string gamePath = "game-info.txt";
    private GameResult result;
    private PlayerInfo player;

    void Start()
    {
        string[] list = {resultPath, infoPath, dataPath, newDataPath, gamePath};
        CorrectPathes.MakeCorrect(ref list);
        resultPath = list[0];
        gamePath = list[4];
        result = new GameResult();
        result.ReadResult(resultPath);
        theText.text = result.GetString();
        player = new PlayerInfo(list[1]);
        player.AddResult(result);
        player.EditPlayerInPlayersFile(list[2], list[3], list[1]);
    }
    public void ClickNext()
    {  
        File.Delete(resultPath);
        File.Delete(gamePath);
        SceneManager.LoadScene("Main");
    }
}
