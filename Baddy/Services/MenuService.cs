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
        private readonly IAuthService _authService;

        public MenuService(
            INavigationService navigationService,
            IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem
                {
                    Action = MenuAction.Home,
                    Name = MenuConstants.Home,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateToHome();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.Login,
                    Visibility = MenuItemVisibility.Anonymous,
                    Name = MenuConstants.Login,
                    Handler = async () => 
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<LoginViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.Profile,
                    Visibility = MenuItemVisibility.LoggedIn,
                    Name = MenuConstants.Profile,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<ProfileViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.Bookings,
                    Visibility = MenuItemVisibility.LoggedIn,
                    Name = MenuConstants.Bookings,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<BookingsViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.CreateBooking,
                    Visibility = MenuItemVisibility.LoggedIn,
                    Name = MenuConstants.CreateBooking,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<CreateBookingViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.ScheduleBooking,
                    Visibility = MenuItemVisibility.LoggedIn,
                    Name = MenuConstants.ScheduleBooking,
                    Handler = async () =>
                    {
                        await _navigationService.CloseMenu();
                        await _navigationService.NavigateTo<ScheduleBookingViewModel>();
                    }
                },
                new MenuItem
                {
                    Action = MenuAction.Logout,
                    Visibility = MenuItemVisibility.LoggedIn,
                    Name = MenuConstants.Logout,
                    Handler = async () =>
                    {
                        _authService.Logoff();

                        await _navigationService.CloseMenu();
                        
                        await _navigationService.RefreshMenu();

                        await _navigationService.NavigateToHome();
                    }
                },
            };
        }
    }
}
