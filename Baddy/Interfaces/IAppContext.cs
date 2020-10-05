using Baddy.Models;
using Unity;

namespace Baddy.Interfaces
{
    public interface IAppContext
    {
        IUnityContainer Container { get; set; }
        bool LoggedIn { get; set; }
        Profile Profile { get; set; }
    }
}
