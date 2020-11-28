using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckingAccount : MonoBehaviour
{
    private PlayerInfo player;

    private string infoPath = "player-info.txt";

    void Start()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        player = new PlayerInfo(infoPath);
        if(!player.correctRead) SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
    }
}
