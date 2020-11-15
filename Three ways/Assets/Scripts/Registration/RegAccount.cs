using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class RegAccount : MonoBehaviour
{
    public InputField inputNickName;
    public InputField inputEMail;
    public InputField inputPassword;
    public InputField inputPasswordAgain;
    public GameObject errors;
    public GameObject sendEmail;
    public GameObject wait;
    private string dataPath = "data.txt";

    bool CheckAlreadyCreatedAccount(string obj, string line, string field, ref InputField input, ref StreamReader reader)
    {
        if (line == field) 
        {
            IncorrectData(obj,"already exist!", ref input);
            reader.Close();
            return true;
        }
        return false;
    }
    bool FindPlayer(string nickName, string eMail)
    {
        FileStream file = new FileStream(dataPath, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            bool found = false;
            string line = reader.ReadLine();
            found = CheckAlreadyCreatedAccount("Nickname", line, nickName, ref inputNickName, ref reader);
            if(found) return true;
            found = CheckAlreadyCreatedAccount("EMail", line, eMail, ref inputEMail, ref reader);
            if(found) return true;
        }
        reader.Close();
        return false;
    }
    void IncorrectData(string obj, string message, ref InputField field)
    {
        field.text = "";
        errors.SetActive(true);
        errors.GetComponent<RegErrors>().SetError(obj + " " + message);
    }
    bool EmptyInput(string obj, ref InputField input)
    {
        if (input.text.Length > 3) return false;
        IncorrectData(obj, "The number of characters entered must be greater than three.", ref input);
        return true;
    }
    bool EmptyFields()
    {
        return (EmptyInput("Nickname:", ref inputNickName)||
        EmptyInput("Email:",ref inputEMail)|| 
        EmptyInput("Password:",ref inputPassword));
    }
    void Start()
    {
        CorrectPathes.MakeCorrect(ref dataPath);
    }
    void CreateAccount(string nickName, string eMail)
    {
        string password = inputPassword.text;         
        string passwordAgain = inputPasswordAgain.text;         
        if (password == passwordAgain)
        {
            PlayerInfo player = new PlayerInfo(nickName, password, eMail);
            wait.SetActive(true);
            sendEmail.SetActive(true);
            sendEmail.GetComponent<SendingEMail>().SendMessage(player);
        }
        else
        {
            IncorrectData("Passwords", "do not match!", ref inputPassword);
            inputPasswordAgain.text = "";
        }
    }
    public void Create()
    {      
        if(EmptyFields()) return;  
        string nickName = inputNickName.text;
        string eMail = inputEMail.text;
        if (!FindPlayer("NickName=" + nickName, "EMail=" + eMail))
        {
            CreateAccount(nickName, eMail);
        }
    }
}
