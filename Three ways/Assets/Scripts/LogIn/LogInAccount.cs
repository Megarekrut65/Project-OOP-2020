using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class LogInAccount : MonoBehaviour
{
    public InputField inputNickName;
    public InputField inputPassword;
    private string dataPath = "data.txt";
    private string infoPath = "player-info.txt";
    public string nameOfScene = "Main";
    public GameObject errors;
    PlayerInfo player;
    public GameObject[] avatars;

    public void IncorrectData(string obj, string message)
    {
        inputNickName.text = "";
        inputPassword.text = "";
        for(int i = 0; i < avatars.Length; i++)
            avatars[i].GetComponent<AvatarsAnimations>().Die();
        errors.SetActive(true);
        errors.GetComponent<LogInErrors>().SetError(obj + " " + message);
    }
    public void StopDieAvatars()
    {
        for(int i = 0; i < avatars.Length; i++)
            avatars[i].GetComponent<AvatarsAnimations>().StopDie();
    }
    void Start()
    {
        CorrectPathes.MakeCorrect(ref dataPath, ref infoPath);
        StartCoroutine("Hitting");
    }
    IEnumerator Hitting()
    {
        while(true)
        {
            yield return new WaitForSeconds(6f); 
            int random = UnityEngine.Random.Range(1, 9);
            if(random <= 3) avatars[0].GetComponent<AvatarsAnimations>().Hit();
            if(random > 3 && random <= 6) avatars[1].GetComponent<AvatarsAnimations>().Hit();
            if(random > 6)
            {
                for(int i = 0; i < avatars.Length; i++)
                avatars[i].GetComponent<AvatarsAnimations>().Hit();
            }
        }
    }
    bool FindPlayer(string nickName, string password)
    {
        FileStream file = new FileStream(dataPath, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string fileNickname = reader.ReadLine();
            if (fileNickname == nickName)
            {
                string filePassword = reader.ReadLine();
                if (filePassword == password)
                {
                    PlayerInfo player = new PlayerInfo(nickName, password, reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                    reader.Close();
                    player.CreateInfoFile(infoPath);                    
                    return true;
                }
                else
                {
                    reader.Close();
                    IncorrectData("Password","is incorrect!");
                    return false;
                }
            }
        }
        reader.Close();
        IncorrectData("Nickname","not found!");
        return false;       
    }
    private void CreateAccount()
    {
        PlayerInfo newPlayer = new PlayerInfo(
            "Player" + UnityEngine.Random.Range(1000,9999).ToString(),
            "1111", "@gmail.com");
            newPlayer.CreateInfoFile(infoPath);
            newPlayer.AppendToPlayersFile(dataPath);
    }
    public void OKButton()
    {
        /*CreateAccount();
        SceneManager.LoadScene(nameOfScene, LoadSceneMode.Single);          
        return;*/
        string nickname = "NickName=" + inputNickName.text;
        string password = "Password=" + inputPassword.text;
        if(FindPlayer(nickname, password))
        {            
            SceneManager.LoadScene(nameOfScene, LoadSceneMode.Single);          
        }      
    }
}

