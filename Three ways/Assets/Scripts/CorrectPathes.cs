using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class CorrectPathes
{
    public static void MakeCorrect(ref string[] pathes)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        for(int i = 0; i < pathes.Length;i++)
        {
            pathes[i] = Path.Combine(Application.persistentDataPath, pathes[i]);
        }     
#else
        for(int i = 0; i < pathes.Length;i++)
        {
            pathes[i] = Path.Combine(Application.dataPath, pathes[i]);
        }
#endif
    }
    public static void MakeCorrect(ref string path1)
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        path1 = Path.Combine(Application.persistentDataPath, path1);            
#else
        path1 = Path.Combine(Application.dataPath, path1);     
#endif
    }
    public static void MakeCorrect(ref string path1, ref string path2)
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        path1 = Path.Combine(Application.persistentDataPath, path1);
        path2 = Path.Combine(Application.persistentDataPath, path2);
#else
        path1 = Path.Combine(Application.dataPath, path1);
        path2 = Path.Combine(Application.dataPath, path2);      
#endif
    }
    public static void MakeCorrect(ref string path1, ref string path2, ref string path3)
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        path1 = Path.Combine(Application.persistentDataPath, path1);
        path2 = Path.Combine(Application.persistentDataPath, path2);
        path3 = Path.Combine(Application.persistentDataPath, path3);
#else
        path1 = Path.Combine(Application.dataPath, path1);
        path2 = Path.Combine(Application.dataPath, path2);   
        path3 = Path.Combine(Application.dataPath, path3);    
#endif
    }
}
