using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class PlayerInfo
{
    public string nickName;
    private string password;
    public string eMail;
    private int coins;
    public int points;
    public int currentIndexOfAvatar;
    public List<int> indexesOfPurchasedAvatars;
    public bool correctRead;

    public void AddResult(GameResult result)
    {
        points += result.newPoints;
        coins += result.coins;
    }
    public PlayerInfo()
    {
        nickName = "Player1";
        password = "";
        eMail = "";
        coins = 0;
        points = 0;
        currentIndexOfAvatar = 0;
        indexesOfPurchasedAvatars = new List<int>();
        correctRead = false;
    }
    public PlayerInfo(string nickName, string password, string eMail)
    {
        this.nickName = nickName;
        this.password = password;
        this.eMail = eMail;
        coins = 0;
        points = 100;
        currentIndexOfAvatar = 0;
        indexesOfPurchasedAvatars = new List<int>();
        indexesOfPurchasedAvatars.Add(currentIndexOfAvatar);
        correctRead = false;
    }
    PlayerInfo(string nickName, string password, string eMail,
    string coins, string points, string avatar, string purchasedAvatars)
    {
        this.nickName = nickName.Substring(9);
        this.password = password.Substring(9);
        this.eMail = eMail.Substring(6);
        this.points = Convert.ToInt32(points.Substring(7));
        this.coins = Convert.ToInt32(coins.Substring(6));
        this.currentIndexOfAvatar = Convert.ToInt32(avatar.Substring(7));
        StringToList(purchasedAvatars);
    }
    public PlayerInfo(string infoPath)
    {
        ReadPlayerFromFile(infoPath);
    }
    string ListToString()
    {
        string result = "PurchasedAvatars=";
        foreach (int index in indexesOfPurchasedAvatars)
        {
            result += index.ToString() + ",";
        }
        return result;
    }
    void StringToList(string line)
    {
        indexesOfPurchasedAvatars = new List<int>();
        string[] parts = line.Split("=".ToCharArray());
        if(parts.Length != 2) return;
        string[] avatars = parts[1].Split(",".ToCharArray());
        for(int i = 0; i < avatars.Length; i++)
        {
            int index;
            bool isInt = Int32.TryParse(avatars[i], out index);
            if(isInt) indexesOfPurchasedAvatars.Add(index);
        }
    }
    void WritePlayerToFile(ref StreamWriter writer)
    {
        writer.WriteLine("NickName=" + nickName);
        writer.WriteLine("Password=" + password);
        writer.WriteLine("EMail=" + eMail);
        writer.WriteLine("Coins=" + coins.ToString());
        writer.WriteLine("Points=" + points.ToString());
        writer.WriteLine("Avatar=" + currentIndexOfAvatar.ToString());
        writer.WriteLine(ListToString());
    }
    public void CreateInfoFile(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        WritePlayerToFile(ref writer);
        writer.Close();
    }
    public void AppendToPlayersFile(string path)
    {
        FileStream file = new FileStream(path, FileMode.Append);
        StreamWriter writer = new StreamWriter(file);
        WritePlayerToFile(ref writer);
        writer.Close();
    }
    void RefreshFile(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        reader.Close();
    }
    void ReadPlayer(ref StreamReader reader)
    {
        if (reader.EndOfStream) return;
        nickName = reader.ReadLine().Substring(9);
        if (reader.EndOfStream) return;
        password = reader.ReadLine().Substring(9);
        if (reader.EndOfStream) return;
        eMail = reader.ReadLine().Substring(6);
        if (reader.EndOfStream) return;
        coins = Convert.ToInt32(reader.ReadLine().Substring(6));
        if (reader.EndOfStream) return;
        points = Convert.ToInt32(reader.ReadLine().Substring(7));
        if (reader.EndOfStream) return;
        currentIndexOfAvatar = Convert.ToInt32(reader.ReadLine().Substring(7));
        if (reader.EndOfStream) return;
        StringToList(reader.ReadLine());
        correctRead = true;
    }
    void ReadPlayerFromFile(string path)
    {
        correctRead = false;
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        ReadPlayer(ref reader);
        reader.Close();
    }
    public void EditPlayerInPlayersFile(string path, string newPath, string infoPath)
    {
        CreateInfoFile(infoPath);
        RefreshFile(newPath);
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string fileNickName = reader.ReadLine();
            if ((fileNickName.Length <= 9) 
            || (fileNickName.Substring(0, 9) != "NickName="))
            {
                continue;
            }
            if (fileNickName.Substring(9) == nickName)
            {
                AppendToPlayersFile(newPath);
            }
            else
            {
                PlayerInfo newPlayer = new PlayerInfo(
                    fileNickName, 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine());
                newPlayer.AppendToPlayersFile(newPath);
            }
        }
        reader.Close();
        File.Delete(path);
        File.Move(newPath, path);
    }
    public void DeletePlayer(string path, string newPath, string infoPath)
    {
        File.Delete(infoPath);
        RefreshFile(newPath);
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string fileNickName = reader.ReadLine();
            if ((fileNickName.Length <= 9) || (fileNickName.Substring(0, 9) != "NickName="))
            {
                continue;
            }
            if (fileNickName.Substring(9) == nickName)
            {
                continue;
            }
            else
            {
                PlayerInfo newPlayer = new PlayerInfo(
                    fileNickName, 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine(), 
                    reader.ReadLine());
                newPlayer.AppendToPlayersFile(newPath);
            }
        }
        reader.Close();
        File.Delete(path);
        File.Move(newPath, path);
    }
    public bool CorrectPassword(string password)
    {
        return (this.password == password);
    }
}
