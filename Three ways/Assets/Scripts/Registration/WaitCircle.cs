using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCircle : MonoBehaviour
{
    public GameObject board;
    public void CloseWait()
    {
        board.SetActive(false);
    }
}
