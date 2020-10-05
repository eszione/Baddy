using Baddy.Interfaces;
using Baddy.Models;
using Unity;

namespace Baddy.Data
{
    public class AppContext : IAppContext
    {
        public IUnityContainer Container { get; set; }
        public bool LoggedIn { get; set; }
        public Profile Profile { get; set; }
    }
}
