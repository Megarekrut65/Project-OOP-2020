using System;
using System.IO;
public class MyChance
{
    public static bool ThereIs(int percentage)
    {
        Random random = new Random();
        int chance = random.Next(1, 101);
        return (chance <= percentage);
    }
}
