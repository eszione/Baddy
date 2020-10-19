using System.Threading.Tasks;
using Baddy.Models;
using Baddy.Models.Apis;

namespace Baddy.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(string username, string password);
        Task<bool> Authorize(string token);
        void Logoff();
    }
}
