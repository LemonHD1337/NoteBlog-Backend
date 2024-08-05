using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using NoteBlog.Settings;

namespace NoteBlog.Services;

public class EmailSenderService : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSenderService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var client = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort);
            client.EnableSsl = _emailSettings.UseSsl;
        
            if (!string.IsNullOrEmpty(_emailSettings.Password))
            {
                client.Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.Password);
            }

            var mail = new MailMessage();

            mail.From = new MailAddress(_emailSettings.FromEmail);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;

            await client.SendMailAsync(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}