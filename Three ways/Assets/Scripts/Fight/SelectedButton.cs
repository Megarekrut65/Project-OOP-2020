using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedButton : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    public int index;
    public GameObject controler;
    public void OnPointerDown(PointerEventData eventData)
    {
        controler.GetComponent<SelectedWay>().Select(index);
        transform.localScale = 1.1f * transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.1f;
    }
}
