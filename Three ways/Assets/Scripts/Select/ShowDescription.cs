using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowDescription : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    public int index;
    public GameObject board;

    public void OnPointerDown(PointerEventData eventData)
    {   
        transform.localScale = 1.1f * transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.1f;
        board.SetActive(true);
        board.GetComponent<DescriptionBoard>().SetData(index);
    }
}
