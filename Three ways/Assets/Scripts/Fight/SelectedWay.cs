﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWay : MonoBehaviour
{
    public int index;
    public bool isSelected;
    public GameObject nextSelect;
    public bool needNext = false;
    public GameObject gameCanvas;

    void Start()
    {
        index = 0;
        isSelected = false;
    }
    public void Select(int index)
    {
        this.index = index;
        isSelected = true;
        if(needNext)
        {
            nextSelect.SetActive(true);
            nextSelect.GetComponent<SelectedWay>().Refresh();
        }
        else
        {
            gameCanvas.GetComponent<Canvas>().sortingOrder = 0;
        }
        gameObject.SetActive(false);
    }
    public void Refresh()
    {
        index = 0;
        isSelected = false;
    }
}
