using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class InfoBoard : MonoBehaviour
{
    public GameObject waiting;
    public GameObject data;
    public Text nickNameText;
    public Text avatar;
    public Text points;
    public GameInfo info;
    public bool isSeted;
    public Text roomCodeText;

    public void SetRoom(int roomCode)
    {   
        roomCodeText.text = "Room: " + roomCode.ToString();
    }
    void Start()
    {
        info = new GameInfo();
        isSeted = false;
    }
    string GetAvatarName(int index)
    {
        switch(index)
        {
            case 0: return "Neanderthal";
            case 1: return "Knight";
            case 2: return "Musketeer";
        }

        return "null";
    }
    public void SetData(string nickName, GameInfo info)
    {   
        nickNameText.text = nickName;
        points.text = info.points.ToString();
        avatar.text = GetAvatarName(info.indexOfAvatar);
        if(isSeted) return;
        isSeted = true;
        this.info = info;
        waiting.SetActive(false);
        data.SetActive(true);
    }
    public void PlayerLeave()
    {
        isSeted = false;
        data.SetActive(false);
        waiting.SetActive(true);
    }
    public void Ready()
    {
        if(info.isReady) info.isReady = false;
        else info.isReady = true;
    }
    void Update()
    {
        if(info.isReady)
        {
            GetComponent<Image>().color = new Color(0,255,0,255);//124, 252, 0
        }
        else
        {
            GetComponent<Image>().color = new Color(255,0,0,255);
        }
    }
}
public struct GameInfo
{
    public int indexOfAvatar;
    public int points;
    public bool isReady;
    public int code;
    public int maxHP;
    public bool isHost;

    public GameInfo(int indexOfAvatar = 0, int points = 0, int code = 1000, int maxHP = 20, bool isHost = false)
    {
        this.indexOfAvatar = indexOfAvatar;
        this.points = points;
        this.code = code;
        this.maxHP = maxHP;
        this.isHost = isHost;
        isReady = false;
    }
    public GameInfo(string path)
    {
        indexOfAvatar = 0;
        points = 0;
        isReady = false;
        code = 1000;
        maxHP = 0;
        isHost = false;
        ReadInfoFromFile(path);
    }
    public void CreateInfoFile(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Avatar=" + indexOfAvatar);
        writer.WriteLine("Points=" + points);
        writer.WriteLine("Room code=" + code.ToString());
        writer.WriteLine("Max hp=" + maxHP.ToString());
        string status = "host";
        if(!isHost) status = "other";
        writer.WriteLine("Status=" + status);   
        writer.Close();
    }
    bool CheckHost(string status)
    {
        if(status == "host")  return true;
        return false;
    }
    void ReadInfoFromFile(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if (reader.EndOfStream) return;
        indexOfAvatar = Convert.ToInt32(reader.ReadLine().Substring(7));
        if (reader.EndOfStream) return;
        points = Convert.ToInt32(reader.ReadLine().Substring(7));
        if(reader.EndOfStream) code = 0;
        else code = Convert.ToInt32(reader.ReadLine().Substring(10));
        if(reader.EndOfStream) maxHP = 20;
        else maxHP = Convert.ToInt32(reader.ReadLine().Substring(7));
        if(reader.EndOfStream) isHost = false;
        else isHost = CheckHost(reader.ReadLine().Substring(7));
        reader.Close();
    }
}