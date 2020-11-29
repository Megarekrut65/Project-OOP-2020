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
            case 0: add = 12 - 5/lvlOfSword; 
            break;
            case 1: add = 4;
            break;
            case 2: add = 8 - 4/lvlOfSword;
            break;
            default:
            break;
        }

        return (lvlOfShield * 2 + add );
    }
    int CountSwordChance()
    {
        int add = 0;
        switch(indexOfAvatar)
        {
            case 0: add = 16 - 10/lvlOfSword; 
            break;
            case 1: add = 9 - 5/lvlOfSword;
            break;
            case 2: add = 0;
            break;
            default:
            break;
        }
        return (lvlOfSword + add + 5);
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
    public bool IsMaxLvl(int indexOfSteel)
    {
        switch(indexOfSteel)
        {
            case 0: if(lvlOfSword == maxLvl) return true;
            break;
            case 1: if(lvlOfShield == maxLvl) return true;
            break;
            default:
            break;
        }
        return false;
    }
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
