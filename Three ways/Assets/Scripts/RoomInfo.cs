using System;
using System.IO;

public class RoomInfo
{
    public int code;
    public int maxHP;

    public void WriteInfo(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Room code=" + code.ToString());
        writer.WriteLine("Max hp=" + code.ToString());
        writer.Close();
    }
    public void ReadInfo(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
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
    }
}
