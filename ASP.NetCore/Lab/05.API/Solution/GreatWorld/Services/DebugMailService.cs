using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace GreatWorld.Services
{
  public class DebugMailService : IMailService
  {
    public Guid Id { get; } = Guid.NewGuid();

    public bool SendMail(string to, string from, string subject, string body)
    {
      try
      {
        Debug.WriteLine($"Sending Mail to {to} from {from} with subject {subject} and body {body} ");
        return Email(to, from, subject, body);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool Email(string to, string from, string subject, string body)
    {
      try
      {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress(from);
        message.To.Add(new MailAddress(to));
        message.Subject = subject;
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = body;
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(from, "Demo@1234");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}


