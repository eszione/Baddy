using System;
using System.Linq;
using System.Threading.Tasks;
using Baddy.Interfaces;
using Baddy.PopupModels;
using Baddy.ViewModels;
using Baddy.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
            var flyoutPage = (FlyoutPage)Application.Current.MainPage;            
            switch (typeof(T))
            {
                case Type model when model == typeof(LoginViewModel):
                    await flyoutPage.Detail.Navigation.PushAsync(new LoginPage());
                    break;
                case Type model when model == typeof(ProfileViewModel):
                    await flyoutPage.Detail.Navigation.PushAsync(new ProfilePage());
                    break;
                case Type model when model == typeof(CreateBookingViewModel):
                    await flyoutPage.Detail.Navigation.PushAsync(new CreateBookingPage());
                    break;
                case Type model when model == typeof(BookingsViewModel):
                    await flyoutPage.Detail.Navigation.PushAsync(new BookingsPage());
                    break;
                case Type model when model == typeof(ScheduleBookingViewModel):
                    await flyoutPage.Detail.Navigation.PushAsync(new ScheduleBookingPage());
                    break;
                default:
                    await flyoutPage.DisplayAlert("Navigation", $"Model of type: {typeof(T)} not supported!", "OK");
                    break;
            }
        }

        public async Task ShowPopup<T>(bool isToast = false, params object[] parameters)
        {
            try
            {
                var popup = GeneratePopup<T>(parameters);
                if (popup != null)
                {
                    await PopupNavigation.Instance.PushAsync(popup, true);
                    if (isToast)
                    {
                        await Task.Delay(2000);
                        if (PopupNavigation.Instance.PopupStack.Any())
                            await PopupNavigation.Instance.PopAllAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayError(ex.Message);
            }
        }

        public Task NavigateToHome()
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;
            currentMaster.Detail = new NavigationPage(new HomePage(_appContext));

            return Task.CompletedTask;
        }

        public Task RefreshMenu()
        {
            var currentMaster = (FlyoutPage)Application.Current.MainPage;
            currentMaster.Flyout = new MenuPage();

            return Task.CompletedTask;
        }

        private PopupPage GeneratePopup<T>(params object[] parameters)
        {
            PopupPage popup;
            var popupType = typeof(T);
            if (popupType == typeof(ToastViewModel))
                popup = new ToastPopup(_appContext, (string)parameters[0])
                {
                    CloseWhenBackgroundIsClicked = true
                };
            else
                throw new Exception($"Popup type: {popupType} does not exist");

            return popup;
        }

        private async Task DisplayError(string message)
        {
            var flyoutPage = (FlyoutPage)Application.Current.MainPage;

            await flyoutPage.DisplayAlert("Error", message, "OK");
        }
    }
}
