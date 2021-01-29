using Baddy.Constants;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        public Command OpenWebCommand { get; }

        public AboutViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService,
            IAuthService authService,
            AppState appState) : base(appContext, navigationService, storageService)
        {
            _authService = authService;

            Title = "About";

            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

            if (appState == AppState.Initialize)
                Task.Run(async () => await RememberMe());
        }

        private async Task RememberMe()
        {
            IsBusy = true;

            try
            {
                var cardNumber = _storageService.ReadKey<string>(PropertyConstants.CardNumber);
                var pinNumber = _storageService.ReadKey<string>(PropertyConstants.PinNumber);

                if (!string.IsNullOrWhiteSpace(cardNumber) && !string.IsNullOrWhiteSpace(pinNumber))
                {
                    var loginResult = await _authService.Login(cardNumber, pinNumber);
                    if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.Token))
                        throw new HttpException(HttpStatusCode.BadRequest, "Incorrect details");

                    _appContext.LoggedIn = await _authService.Authorize(loginResult.Token);

                    if (!_appContext.LoggedIn)
                        throw new HttpException(HttpStatusCode.Unauthorized);

                    await _navigationService.RefreshMenu();
                }
            }
            catch (HttpException ex)
            {
                await ExceptionHelper.HandleInvasive(ex);

                await _storageService.DeleteKey(PropertyConstants.CardNumber);
                await _storageService.DeleteKey(PropertyConstants.PinNumber);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}