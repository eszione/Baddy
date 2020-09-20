using Baddy.Interfaces;

namespace Baddy.Data
{
    public class AppContext : IAppContext
    {
        public bool LoggedIn { get; set; }
    }
}
