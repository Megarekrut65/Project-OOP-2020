using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPassword : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject errors;
    public InputField password;
    public InputField newPassword;
    public InputField newPasswordAgain;

    void EditPass()
    {
        if(mainCamera.GetComponent<SetPlayerData>().player.EditPassword(password.text, newPassword.text))
        {
            mainCamera.GetComponent<SetPlayerData>().EditPlayer();
            Clear();
            gameObject.SetActive(false);
        }
        else
        {
            SetErrors("Invalid password entered!");
        }
    }
    void Clear()
    {
        password.text = "";
        newPassword.text = "";
        newPasswordAgain.text = "";
    }
    void SetErrors(string message)
    {
        Clear();
        errors.SetActive(true);
        errors.GetComponent<ErrorMessage>().SetError(message);
        gameObject.SetActive(false);
    }
    public void Edit()
    {
        if(newPassword.text.Length <= 3)
        {
            SetErrors("The new password is too short!");
            return;
        }
        if(newPassword.text == newPasswordAgain.text)
        {
            EditPass();
        }
        else
        {
            SetErrors("New passwords are not equal!");
        }  
    }
}
