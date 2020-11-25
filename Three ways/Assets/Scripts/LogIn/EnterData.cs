using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class EnterData : MonoBehaviour
{
    public GameObject checkCode;
    public GameObject forgotPass;
    public InputField mail;
    private string dataPath = "data.txt";
    public GameObject mainCamera;
    public PlayerInfo player;
    public string password;
    
    void Start()
    {
        CorrectPathes.MakeCorrect(ref dataPath);
    }
    void SetError(string obj, string message)
    {
        mainCamera.GetComponent<LogInAccount>().IncorrectData(obj, message);
    }
    bool FindPlayer(string eMail)
    {
        FileStream file = new FileStream(dataPath, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string nickName = reader.ReadLine();
            password = reader.ReadLine();
            string mail = reader.ReadLine();
            if(mail == eMail)
            {
                player = new PlayerInfo(nickName, password, 
                                mail, reader.ReadLine(), reader.ReadLine(), 
                                reader.ReadLine(), reader.ReadLine());
                return true;
            }
        }
        reader.Close();
        SetError("Entered email","is not tied to any account!");
        mail.text = "";
        return false;       
    }
    public void SendCode()
    {
        if(FindPlayer("EMail=" + mail.text))
        {
            forgotPass.GetComponent<ForgotPassword>().SetMail(mail.text, player.nickName);
            password = password.Substring(9);
            checkCode.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
