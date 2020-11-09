using System;
using System.IO;

public class RoomInfo
{
    private string path;
    private int code;
    private int maxHP;
    private void WriteInfo()
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Room code=" + code.ToString());
        writer.WriteLine("Max hp=" + code.ToString());
        writer.Close();
    }
    private void ReadInfo()
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if(reader.EndOfStream) code = 0;
        else code = Convert.ToInt32(reader.ReadLine().Substring(10));
        if(reader.EndOfStream) maxHP = 5;
        else maxHP = Convert.ToInt32(reader.ReadLine().Substring(7));
        reader.Close();
    }
    public RoomInfo(string path)
    {
        this.path = path;
        ReadInfo();
    }
    public int GetCode()
    {
        return code;
    }
    public void EditCode(int newCode)
    {
        code = newCode;
        WriteInfo();
    }
}
