using Baddy.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Baddy.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string recipientName, string recipientEmail)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Your booking was confirmed",
                    Body = $"Hi {recipientName},\nYour booking was confirmed\n\nBadcrew",
                    To = new List<string> { recipientEmail }
                };

                await Email.ComposeAsync(message);
            }
            catch
            {
            }
        }
    }
}
