using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;

public class Sender
{
    private string eMailOfSender;
    private string password;
    private string eMailOfPlayer;
    private int port;
    private int code;

    public Sender()
    {
        eMailOfSender = "magicalslimesboa@gmail.com";
        password = "9NvZZpZz1s";
        eMailOfPlayer = "magicalslimesboa@gmail.com";
        port = 587;
        code = 0;
    }
    public Sender(string eMail)
    {
        eMailOfSender = "magicalslimesboa@gmail.com";
        password = "9NvZZpZz1s";
        eMailOfPlayer = eMail;
        port = 587;
        Random random = new Random();
        code = random.Next(1000, 9999);
    }
    private void Send(string message, string title)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(eMailOfSender);
        mail.To.Add(eMailOfPlayer);
        mail.Subject = "Code " + title;
        mail.Body = message;
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = port;
        smtpServer.Credentials = new System.Net.NetworkCredential(eMailOfSender, password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        smtpServer.Send(mail);
    }
    public void SendEMail(string nickname)
    {
        if (code == 0) return;
        string title = "Three Ways";
        string message = "Hello, " + nickname + 
        ". You registered in the game 'Three Ways'. So, there is your code: " + 
        code.ToString() + " to confirm the account.";
        Send(message, title);
    }
    public void SendForgotPassword(string nickname)
    {
        if (code == 0) return;
        string title = "Three Ways";
        string message = "Hello, " + nickname + 
        ". If you want to change the password for the account here is your code: " + 
        code.ToString() + ", else ignore this message...";
        Send(message, title);
    }
    public bool CheckCode(string code)
    {
        if(code.Length != 4) return false;
        return (this.code == Convert.ToInt32(code));
    }
}
