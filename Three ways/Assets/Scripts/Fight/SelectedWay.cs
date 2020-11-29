using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedWay : MonoBehaviour
{
    public int index;
    public int chance = 0;
    public bool isSelected;
    public bool isChance;
    public GameObject nextSelect;
    public bool needNext = false;
    public GameObject gameCanvas;
    public Slider timeSlider;
    private const int time = 60;
    public GameObject gameManager;

    void Start()
    {
        index = 0;
        isSelected = false;
        isChance = false;
    }
    IEnumerator RunTime()
    {
        while(true)
        {
            if(timeSlider.value <= 0) gameManager.GetComponent<GameManager>().AnswerYes();
            yield return new WaitForSeconds(1f);
            timeSlider.value--;
        }
    }
    public void Select(int index)
    {
        timeSlider.value = time;
        this.index = index;
        isChance = MyChance.ThereIs(chance);
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
        StartCoroutine("RunTime");
        timeSlider.value = time;
        index = 0;
        isSelected = false;
    }
}
