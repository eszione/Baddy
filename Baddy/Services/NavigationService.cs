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
        public Task CloseMenu()
        {
            var currentMaster = (MasterDetailPage)Application.Current.MainPage;
            currentMaster.IsPresented = false;

            return Task.CompletedTask;
        }

        public async Task NavigateTo<T>(params object[] parameters)
        {
            var currentMaster = (MasterDetailPage)Application.Current.MainPage;

            switch (typeof(T))
            {
                case Type model when model == typeof(AboutViewModel):
                    currentMaster.Detail = new NavigationPage(new AboutPage());
                    break;
                case Type model when model == typeof(LoginViewModel):
                    currentMaster.Detail = new NavigationPage(new LoginPage());
                    break;
                default:
                    await currentMaster.DisplayAlert("Navigation", $"Model of type: {typeof(T)} not supported!", "OK");
                    break;
            }
        }
    }
}
