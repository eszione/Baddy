namespace Baddy.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string recipientName, string recipientEmail, string subject, string body);
    }
}
