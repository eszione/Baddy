using Baddy.Interfaces;
using Baddy.Models;

namespace Baddy.Data
{
    public class AppContext : IAppContext
    {
        public bool LoggedIn { get; set; }
        public Profile Profile { get; set; }
    }
}
