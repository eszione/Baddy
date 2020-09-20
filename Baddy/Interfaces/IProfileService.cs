using Baddy.Models;
using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> Get();
    }
}
