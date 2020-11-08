using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Errors : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    public GameObject join;
    public GameObject create;
    public GameObject menu;
    public GameObject wait;
    public GameObject manager;
    public Text errorText;

    public void OnPointerDown(PointerEventData eventData)
    {   
        transform.localScale = 1.1f * transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.1f;
        CloseError();
    }
    void CloseError()
    {
        join.SetActive(false);
        create.SetActive(false);
        wait.SetActive(false);
        manager.GetComponent<LobbyManager>().SetDisconnect();
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SetError(string message)
    {
        errorText.text = message;
        StartCoroutine("CloseBoard");
    }
    IEnumerator CloseBoard()
    {
        yield return new WaitForSeconds(10f);
        CloseError();
        StopCoroutine("CloseBoard");
    }
}
