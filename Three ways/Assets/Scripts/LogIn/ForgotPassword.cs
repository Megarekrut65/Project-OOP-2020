using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPassword : MonoBehaviour
{
    private Sender sender;

    public void SetMail(string mail, string nickName)
    {
        sender = new Sender(mail);
        sender.SendForgotPassword(nickName);
    }
    public bool CheckCode(string code)
    {
        return sender.CheckCode(code);
    }
}
