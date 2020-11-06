using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeLeft : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    private GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    public void OnPointerDown(PointerEventData eventData)
    {   
        transform.localScale = 1.1f * transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.1f;
        mainCamera.GetComponent<ReadAvatars>().Left();
    }
}
