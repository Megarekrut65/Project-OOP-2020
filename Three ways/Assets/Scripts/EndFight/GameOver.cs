using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    public void ClckNext()
    {  
        SceneManager.LoadScene("Lobby");
    }
}
