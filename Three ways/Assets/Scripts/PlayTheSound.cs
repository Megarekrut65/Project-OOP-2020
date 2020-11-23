using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheSound : MonoBehaviour
{
    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
