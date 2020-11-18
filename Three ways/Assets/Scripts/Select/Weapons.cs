using System;

public class Weapons
{
    public int indexOfAvatar;
    public int lvlOfSword;
    public int lvlOfShield;
    public Weapons(int indexOfAvatar, int lvlOfSword, int lvlOfShield)
    {
        this.indexOfAvatar = indexOfAvatar;
        this.lvlOfSword = lvlOfSword;
        this.lvlOfShield = lvlOfShield;
    }
    public string CreateString()
    {
        string result =  "{index:" + indexOfAvatar.ToString() + 
        ";lvlSword:" + lvlOfSword.ToString() + 
        ";lvlShield:" + lvlOfShield.ToString() + "}";
        return result;
    }
    public Weapons(string line)
    {
        char[] trim = {'{' , '}'};
        line = line.Trim(trim);
        string[] parts = line.Split(';');
        indexOfAvatar = Convert.ToInt32(parts[0].Substring(6));
        lvlOfSword = Convert.ToInt32(parts[1].Substring(9));
        lvlOfShield = Convert.ToInt32(parts[2].Substring(10));
    }
}
