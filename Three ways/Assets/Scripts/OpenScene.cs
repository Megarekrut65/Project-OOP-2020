using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public string nameOfScene = "Main";

    public void OpenTheScene()
    {
        SceneManager.LoadScene(nameOfScene);
    }
}
