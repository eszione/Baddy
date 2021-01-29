using System;
using System.Threading.Tasks;
using Baddy.Interfaces;
using Baddy.ViewModels;
using Baddy.Views;
using Xamarin.Forms;

namespace Baddy.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IAppContext _appContext;

        public NavigationService(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public Task CloseMenu()
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;
            currentMaster.IsPresented = false;

            return Task.CompletedTask;
        }

        public async Task NavigateTo<T>(params object[] parameters)
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;

            switch (typeof(T))
            {
                case Type model when model == typeof(AboutViewModel):
                    currentMaster.Detail = new NavigationPage(new AboutPage(_appContext));
                    break;
                case Type model when model == typeof(LoginViewModel):
                    currentMaster.Detail = new NavigationPage(new LoginPage());
                    break;
                case Type model when model == typeof(ProfileViewModel):
                    currentMaster.Detail = new NavigationPage(new ProfilePage());
                    break;
                case Type model when model == typeof(CreateBookingViewModel):
                    currentMaster.Detail = new NavigationPage(new CreateBookingPage());
                    break;
                case Type model when model == typeof(BookingsViewModel):
                    currentMaster.Detail = new NavigationPage(new BookingsPage());
                    break;
                case Type model when model == typeof(ScheduleBookingViewModel):
                    currentMaster.Detail = new NavigationPage(new ScheduleBookingPage());
                    break;
                default:
                    await currentMaster.DisplayAlert("Navigation", $"Model of type: {typeof(T)} not supported!", "OK");
                    break;
            }
        }

        public Task NavigateToHome()
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;
            currentMaster.Detail = new NavigationPage(new AboutPage(_appContext));

            return Task.CompletedTask;
        }

        public Task RefreshMenu()
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;
            currentMaster.Flyout = new MenuPage();

            return Task.CompletedTask;
        }
    }
}
