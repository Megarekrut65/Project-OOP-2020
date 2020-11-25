using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ErrorMessage : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    public Text text;
    public GameObject account;

    public void SetError(string message)
    {
        text.text = "Error: " + message;
        StartCoroutine("CloseError");
    }
    void Close()
    {
        account.SetActive(true);
        gameObject.SetActive(false);
    }
    IEnumerator CloseError()
    {
        yield return new WaitForSeconds(10f);
        Close();
        StopCoroutine("CloseError");
    }
    public void OnPointerDown(PointerEventData eventData)
    {   
        transform.localScale = 1.1f * transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.1f;
        Close();
    }
}
