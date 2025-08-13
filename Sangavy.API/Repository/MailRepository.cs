using Microsoft.AspNetCore.Html;
using Sangavy.API.Constants;
using System.Net;
using System.Net.Mail;

namespace Sangavy.API.Repository;

public class MailRepository
{
    private readonly ILogger<MailRepository> _logger;

    public MailRepository(ILogger<MailRepository> logger)
    {
        _logger = logger;
    }

    public bool SentMail(string content)
    {
        try
        {
            //System.Net.ServicePointManager.ServerCertificateValidationCallback =
            //        (sender, certificate, chain, sslPolicyErrors) => true;

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(MailConstants.FromMailAddress);
            message.To.Add(new MailAddress(MailConstants.ToMailAddress));
            message.Subject = "Contact Us - Mail";
            message.IsBodyHtml = true;
            message.Body = content;
            smtp.Port = 587;
            smtp.Host = "smtp.sangavy.com";
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(MailConstants.FromMailAddress, MailConstants.FromMailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

            _logger.LogInformation("Mail sent successfully at {Time}", DateTime.UtcNow);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            _logger.LogError(ex, "Error sending email at {Time}", DateTime.UtcNow);
            return false;
        }
    }
}
