using System;
using System.Threading.Tasks;
using Baddy.Enums;

namespace Baddy.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public MenuAction Action { get; set; }
        public MenuItemVisibility Visibility { get; set; }
        public Func<Task> Handler { get; set; }
    }
}
