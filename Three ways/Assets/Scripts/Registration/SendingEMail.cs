using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SendingEMail : MonoBehaviour
{
    public InputField inputCode;
    private PlayerInfo player;
    private string dataPath = "data.txt";
    public GameObject regObjects;
    public GameObject errors;
    private Sender sender;

    void Start()
    {
        CorrectPathes.MakeCorrect(ref dataPath);
    }
    public void SendMessage(PlayerInfo player)
    {
        //GetComponent<AudioSource>().Play();
        this.player = player;
        regObjects.SetActive(false);
        return;
        sender = new Sender(player.eMail);
        sender.SendEMail(player.nickName);
    }
    public void ResendEMail()
    {
        return;
        sender.SendEMail(player.nickName);
    }
    public void CheckCodeButton()
    {
        player.AppendToPlayersFile(dataPath);
        SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
        return;
        //GetComponent<AudioSource>().Play();
        if (sender.CheckCode(inputCode.text))
        {
            player.AppendToPlayersFile(dataPath);
            SceneManager.LoadScene("LogIn", LoadSceneMode.Single);
        }
        else
        {
            errors.SetActive(true);
            errors.GetComponent<LogInErrors>().SetError("Code is inccorect!");
        }
    }
}
