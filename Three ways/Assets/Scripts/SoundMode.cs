using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class SoundMode : MonoBehaviour
{
    private string soundPath = "sound.txt";
    private bool soundActive;
    void Start()
    {
        CorrectPathes.MakeCorrect(ref soundPath);
        soundActive = false;
        ReadSound();
    }
    void ReadSound()
    {
        FileStream file = new FileStream(soundPath, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if(reader.EndOfStream)
        {
            AudioListener.pause = false; 
            soundActive = true;
            reader.Close();
            WriteSound();
            return;
        }
        if (reader.ReadLine().Substring(5) == "true") 
        {
            AudioListener.pause = false;
            soundActive = true;
        }
        else 
        {
            AudioListener.pause = true;
            soundActive = false;
        }
        reader.Close();
    }
    void WriteSound()
    {
        FileStream file = new FileStream(soundPath, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        if(soundActive) writer.WriteLine("Mode=true");
        else writer.WriteLine("Mode=false");
        writer.Close();
    }
    public bool EditSound()
    {
        if(soundActive)
        {
            soundActive = false;
            AudioListener.pause = true;
        }
        else
        {
            soundActive = true;
            AudioListener.pause = false;
        }
        WriteSound();
        return soundActive;
    }
}
