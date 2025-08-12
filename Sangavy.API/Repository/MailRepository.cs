using Microsoft.AspNetCore.Html;
using Sangavy.API.Constants;
using System.Net;
using System.Net.Mail;

namespace Sangavy.API.Repository;

public class MailRepository
{
    public MailRepository()
    {

    }

    public bool SentMail(string content)
    {
        try
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true;

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(MailConstants.FromMailAddress);
            message.To.Add(new MailAddress(MailConstants.ToMailAddress));
            message.Subject = "Contact Us - Mail";
            message.IsBodyHtml = true;
            message.Body = content;
            smtp.Port = 587;
            smtp.Host = "smtp.sangavy.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(MailConstants.FromMailAddress, MailConstants.FromMailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }
}
