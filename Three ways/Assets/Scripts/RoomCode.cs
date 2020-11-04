using System;
using System.IO;

public class RoomCode
{
    private string path;
    public int code;
    private void WriteCode()
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Room code=" + code.ToString());
        writer.Close();
    }
    private void ReadCode()
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if(reader.EndOfStream) code = 0;
        else code = Convert.ToInt32(reader.ReadLine().Substring(10));
        reader.Close();
    }
    public RoomCode(string path)
    {
        this.path = path;
        ReadCode();
    }
    public int GetCode()
    {
        return code;
    }
    public void EditCode(int newCode)
    {
        code = newCode;
        WriteCode();
    }
}
