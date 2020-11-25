using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAccount : MonoBehaviour
{
    public GameObject mainCamera;
    public InputField password;
    public GameObject error;

    public void Delete()
    {
        if(mainCamera.GetComponent<SetPlayerData>().player.CorrectPassword(password.text))
        {
            mainCamera.GetComponent<SetPlayerData>().DeleteAccount();
        }
        else
        {
            error.SetActive(true);
            error.GetComponent<ErrorMessage>().SetError("Invalid password entered!");
            gameObject.SetActive(false);
        }
        password.text = "";
    }
}
