using Baddy.Enums;
using Baddy.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MenuItem = Baddy.Models.MenuItem;

namespace Baddy.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public MenuViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IMenuService menuService,
            IStorageService storageService) : base(appContext, navigationService, storageService)
        {
            SetMenuItems(menuService.GetMenuItems());
        }

        private void SetMenuItems(IEnumerable<MenuItem> menuItems)
        {
            if (_appContext.LoggedIn)
                MenuItems = menuItems.Where(m => m.Visibility != MenuItemVisibility.Anonymous);
            else
                MenuItems = menuItems.Where(m => m.Visibility != MenuItemVisibility.LoggedIn);
        }
    }
}
