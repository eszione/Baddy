using Baddy.Interfaces;
using System;
using System.Net.Mail;

namespace Baddy.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string recipientName, string recipientEmail, string subject, string body)
        {
            try
            {
                var mail = new MailMessage("badcrew.mailer@gmail.com", recipientEmail, subject, $"Hi {recipientName},\n\n{body}\n\nBadcrew");
                var smtpServer = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential("badcrew.mailer@gmail.com", "mailerbadcrew123")
                };

                smtpServer.Send(mail);
            }
            catch
            {
            }
        }
    }
}
