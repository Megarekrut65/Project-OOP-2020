﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text theText;
    private string resultPath = "result-info.txt";
    private string infoPath = "player-info.txt";
    private GameResult result;
    private PlayerInfo player;

    void Start()
    {
        CorrectPathes.MakeCorrect(ref resultPath, ref infoPath);
        result = new GameResult();
        result.ReadResult(resultPath);
        theText.text = result.GetString();
        player = new PlayerInfo(infoPath);
        player.AddResult(result);
        player.CreateInfoFile(infoPath);
    }
    public void ClickNext()
    {  
        SceneManager.LoadScene("Lobby");
    }
}
