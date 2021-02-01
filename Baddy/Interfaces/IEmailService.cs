using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string recipientName, string recipientEmail);
    }
}
