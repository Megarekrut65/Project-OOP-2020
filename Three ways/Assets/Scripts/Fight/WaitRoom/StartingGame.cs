using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingGame : MonoBehaviour
{
    public GameObject gameManager;
    public Text count;
    public void Game()
    {
        StartCoroutine("Count");
    }
    IEnumerator Count()
    {
        for(int i = 3; i >= 0; i--)
        {
            count.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        gameManager.GetComponent<GameManager>().StartGame();
        gameObject.SetActive(false); 
        StopCoroutine("Count");
    }
}
