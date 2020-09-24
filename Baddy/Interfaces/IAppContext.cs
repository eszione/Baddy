using Baddy.Models;

namespace Baddy.Interfaces
{
    public interface IAppContext
    {
        bool LoggedIn { get; set; }
        Profile Profile { get; set; }
    }
}
