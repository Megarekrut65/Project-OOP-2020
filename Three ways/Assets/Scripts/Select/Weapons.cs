using System;

public class Weapons
{
    private int maxLvl = 15;
    public int indexOfAvatar;
    private int lvlOfSword;
    private int lvlOfShield;
    public Weapons(int indexOfAvatar = 0, int lvlOfSword = 1, int lvlOfShield = 1)
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
    int CountShieldChance()
    {
        int add = 0;
        switch(indexOfAvatar)
        {
            case 0: add = 3; 
            break;
            case 1: add = 1;
            break;
            case 2: add = 4;
            break;
            default:
            break;
        }

        return (lvlOfShield * 3 + add);
    }
    int CountSwordChance()
    {
        int add = 0;
        switch(indexOfAvatar)
        {
            case 0: add = 6; 
            break;
            case 1: add = 4;
            break;
            case 2: add = 3;
            break;
            default:
            break;
        }
        return (lvlOfSword * 2 + add);
    }
    public int CountChance(int indexOfSteel)
    {
        int chance = 0;
        switch(indexOfSteel)
        {
            case 0: chance = CountSwordChance();
            break;
            case 1: chance = CountShieldChance();
            break;
            default:
            break;
        }
        return chance;
    }
    public int CountPrice(int indexOfSteel)
    {
        int startPrice = 100;
        switch(indexOfSteel)
        {
            case 0: startPrice = (lvlOfSword * startPrice + indexOfAvatar * lvlOfSword * 10);
            break;
            case 1: startPrice = (lvlOfShield * startPrice + indexOfAvatar * lvlOfShield * 10);
            break;
            default:
            break;
        }
        return startPrice + maxLvl;
    }
    //need make function for inform about max lvl
    public void AddLvl(int indexOfSteel)
    {
        switch(indexOfSteel)
        {
            case 0: if(lvlOfSword < maxLvl) lvlOfSword++;
            break;
            case 1: if(lvlOfShield < maxLvl) lvlOfShield++;
            break;
            default:
            break;
        }
    }
    public int GetLvl(int indexOfSteel)
    {
        switch(indexOfSteel)
        {
            case 0: return lvlOfSword;
            case 1: return lvlOfShield;
            default: return 0;
        }
    }
}
