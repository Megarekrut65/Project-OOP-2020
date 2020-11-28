using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class SoundMode : MonoBehaviour
{
    private string soundPath = "sound.txt";
    private bool soundActive;
    public Image soundOn;
    public Image soundOff;
    public Image sound;
    void Start()
    {
        CorrectPathes.MakeCorrect(ref soundPath);
        soundActive = false;
        ReadSound();
    }
    void TurnOn()
    {
        if(sound != null ) sound.sprite = soundOn.sprite;
        soundActive = true;
        AudioListener.pause = false;
    }
    void TurnOff()
    {
        if(sound != null ) sound.sprite = soundOff.sprite;
        soundActive = false;
        AudioListener.pause = true;
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
            TurnOn();
        }
        else 
        {
            TurnOff();
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
    public void EditSound()
    {
        if(soundActive)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
        WriteSound();
    }
}
