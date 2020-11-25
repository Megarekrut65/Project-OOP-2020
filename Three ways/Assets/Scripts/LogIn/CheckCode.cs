using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCode : MonoBehaviour
{
    public GameObject forgotPass;
    public GameObject editPass;
    public GameObject mainCamera;
    public InputField code;

    void SetError(string obj, string message)
    {
        mainCamera.GetComponent<LogInAccount>().IncorrectData(obj, message);
    }
    public void Check()
    {
        if(forgotPass.GetComponent<ForgotPassword>().CheckCode(code.text))
        {
            editPass.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            code.text = "";
            SetError("Code", "is inccorect!");
        }
    }
}
