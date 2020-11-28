using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOpenURL : MonoBehaviour
{
    public string url = "http://";
    public void Open()
    {
        Application.OpenURL(url);
    }
}
