using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private string infoPath = "player-info.txt";
    public void ExitButton()
    {
        CorrectPathes.MakeCorrect(ref infoPath);
        PlayerInfo player = new PlayerInfo(infoPath);
        player.RefreshFile(infoPath);
        SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
    }
}
