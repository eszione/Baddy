using System.Collections.Generic;
using Baddy.Constants;
using Baddy.Enums;
using Baddy.Interfaces;
using Baddy.Models;
using Baddy.ViewModels;

namespace Baddy.Services
{
    public class MenuService : IMenuService
    {
        private readonly INavigationService _navigationService;

        public MenuService(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem
                {
                    Action = MenuAction.Login,
                    Name = MenuConstants.Login,
                    Handler = async () => 
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<LoginViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.Logout,
                    Name = MenuConstants.Logout,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<AboutViewModel>();
                    }
                }
            };
        }
    }
}
