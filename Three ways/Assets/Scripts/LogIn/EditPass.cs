using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPass : MonoBehaviour
{
    public GameObject forgotPass;
    public GameObject enterData;
    public GameObject mainCamera;
    public GameObject signIn;
    private string dataPath = "data.txt";
    private string newDataPath = "newData.txt";
    private string infoPath = "player-info.txt";
    public InputField newPass;
    public InputField newPassAgain;

    void Start()
    {
        CorrectPathes.MakeCorrect(ref dataPath, ref newDataPath, ref infoPath);
    }
    void SetError(string obj, string message)
    {
        mainCamera.GetComponent<LogInAccount>().IncorrectData(obj, message);
    }
    void NewPassword(string newPass)
    {
        string password = enterData.GetComponent<EnterData>().password;
        enterData.GetComponent<EnterData>().player.EditPassword(password, newPass);
        enterData.GetComponent<EnterData>().player.EditPlayerInPlayersFile(dataPath, newDataPath, infoPath);
        enterData.GetComponent<EnterData>().player.RefreshFile(infoPath);
        signIn.SetActive(true);
        forgotPass.SetActive(false);
    }
    public void EditPassword()
    {
        if(newPass.text.Length <= 3)
        {
            newPassAgain.text = "";
            newPass.text = "";
            SetError("The new password", "is too short!");
            return;
        }
        if(newPassAgain.text == newPass.text)
        {
            NewPassword(newPass.text);
        }
        else
        {
            newPassAgain.text = "";
            newPass.text = "";
            SetError("New passwords", "are not equal!");
        }
    }
}
