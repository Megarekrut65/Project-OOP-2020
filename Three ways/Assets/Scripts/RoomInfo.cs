using System;
using System.IO;

public struct RoomInfo
{
    public int code;
    public int maxHP;
    public bool isHost;

    public void WriteInfo(string path, bool isHost = true)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        string status = "host";
        if(!isHost) status = "other";
        writer.WriteLine("Status=" + status);
        writer.WriteLine("Room code=" + code.ToString());
        writer.WriteLine("Max hp=" + maxHP.ToString());
        writer.Close();
    }
    bool CheckHost(string status)
    {
        if(status == "host")  return true;
        return false;
    }
    public void ReadInfo(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if(reader.EndOfStream) isHost = false;
        else isHost = CheckHost(reader.ReadLine().Substring(7));
        if(reader.EndOfStream) code = 0;
        else code = Convert.ToInt32(reader.ReadLine().Substring(10));
        if(reader.EndOfStream) maxHP = 5;
        else maxHP = Convert.ToInt32(reader.ReadLine().Substring(7));
        reader.Close();
    }
    public RoomInfo(int code = 1111, int maxHP = 0)
    {
        this.code = code;
        this.maxHP = maxHP;
        isHost = false;
    }
}
