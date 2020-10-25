using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWay : MonoBehaviour
{
    public int index;
    public bool isSelected;
    void Start()
    {
        index = 0;
        isSelected = false;
    }
    public void Select(int index)
    {
        this.index = index;
        isSelected = true;
    }
    public void Refresh()
    {
        index = 0;
        isSelected = false;
    }
}
