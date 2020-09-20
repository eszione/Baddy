using System.Collections.Generic;
using Baddy.Models;

namespace Baddy.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<MenuItem> GetMenuItems();
    }
}
