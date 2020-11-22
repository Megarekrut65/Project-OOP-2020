using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DescriptionBoard : MonoBehaviour, 
IPointerDownHandler, IPointerUpHandler
{
    public Text title;
    public Text lvl;
    public Text price;
    public Text properties;
    public GameObject mainCamera;
    public GameObject theCanvas;
    public GameObject updateButton;
    public GameObject updating;
    private Weapons currentWeapon;
    private int currentSteel;
    private int currentAvatar;

    public void OnPointerDown(PointerEventData eventData)
    {   
        theCanvas.GetComponent<Canvas>().sortingOrder = 0;
        gameObject.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //it must be here with OnPointerDown
    }
    public void UpdateWeapon()
    {
        if(mainCamera.GetComponent<ReadAvatars>().player.BuyWeapon(currentWeapon.CountPrice(currentSteel), currentAvatar, currentSteel))
        {
            SetDescriptions();
            SetNumbers();
            mainCamera.GetComponent<ReadAvatars>().SaveAvatar();
        }
        else
        {
            updating.GetComponent<Animation>().Play("updating-have-not-money");
            updateButton.GetComponent<Animation>().Play("update-have-not-money");
        }
    }
    void SetDescriptions()
    {
        switch(currentSteel)
        {
            case 0:
            {
                SetTitleSword(currentAvatar);
                SetPropertiesSword(currentAvatar);
            }
            break;
            case 1:
            {
                SetTitleShield(currentAvatar);
                SetPropertiesShield(currentAvatar);
            }
            break;
            default:
            break;
        }
    }
    public void SetNumbers()
    {
        if(!currentWeapon.IsMaxLvl(currentSteel))
        {
            lvl.text = "Level: " + currentWeapon.GetLvl(currentSteel);
            price.text = "$" + currentWeapon.CountPrice(currentSteel);
        } 
        else 
        {
            lvl.text = "Level: " + currentWeapon.GetLvl(currentSteel) + "MAX";
            price.text = "";
            updateButton.SetActive(false);
        }
        
    }
    public void SetData(int indexOfSteel)
    {
        currentSteel = indexOfSteel;
        currentAvatar = mainCamera.GetComponent<ReadAvatars>().currentIndex;
        currentWeapon = mainCamera.GetComponent<ReadAvatars>().player.GetWeapon(currentAvatar);
        theCanvas.GetComponent<Canvas>().sortingOrder = 20;
        if(mainCamera.GetComponent<ReadAvatars>().player.WasBought(currentAvatar))
        {
            updateButton.SetActive(true);
        }
        else
        {
            updateButton.SetActive(false);
        }
        SetDescriptions();
        SetNumbers();
    }
    void SetTitleSword(int index)
    {
        switch(index)
        {
            case 0: title.text = "Mace";
            break;
            case 1: title.text = "Gladius";
            break;
            case 2: title.text = "Rapier";
            break;
            default:
            break;
        }
    }
    void SetPropertiesSword(int index)
    {
        int chance = Convert.ToInt32(currentWeapon.CountChance(0));
        string line = "";
        string propertieLine = "With a " + chance.ToString() + "% chance,";
        switch(index)
        {
            case 0: 
            {
                line = "Great and powerful weapon of that time.\n";
                propertieLine += " it pierces the shield.";
            }
            break;
            case 1:
            {
                line = "This weapon was a favorite among the gladiators who fought in arenas for entertainment.\n";
                propertieLine += " it steals from the enemy health equal to that inflicted.";
            }
            break;
            case 2: 
            {
                line = "A fast and sharp sword that pierces the enemy with quick blows.\n";
                propertieLine += " it deals twice as much damage.";
            }
            break;
            default:
            break;
        }
        properties.text = line + propertieLine;
    }
    void SetTitleShield(int index)
    {
        switch(index)
        {
            case 0: title.text = "Round shield";
            break;
            case 1: title.text = "Wankel shield";
            break;
            case 2: title.text = "Buckler shield";
            break;
            default:
            break;
        }
    }
    void SetPropertiesShield(int index)
    {
        int chance = Convert.ToInt32(currentWeapon.CountChance(1));
        string line = "";
        string propertieLine = "With a " + chance.ToString() + "% chance,";
        switch(index)
        {
            case 0: 
            {
                line = "Not very strong shield, but able to withstand several blows.\n";
                propertieLine += " it strikes at the enemy.";
            }
            break;
            case 1:
            {
                line = "Large and heavy shield.\n";
                propertieLine += " it stuns the enemy.";
            }
            break;
            case 2: 
            {
                line = "Small but effective shield.\n";
                propertieLine += " it estores health equal to the damage.";
            }
            break;
            default:
            break;
        }
        properties.text = line + propertieLine;
    }
}
