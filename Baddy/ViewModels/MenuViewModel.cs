using Baddy.Interfaces;
using System.Collections.Generic;
using MenuItem = Baddy.Models.MenuItem;

namespace Baddy.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public MenuViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IMenuService menuService) : base(appContext, navigationService)
        {
            MenuItems = menuService.GetMenuItems();
        }
    }
}
