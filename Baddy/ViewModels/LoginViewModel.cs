using Baddy.Constants;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        private string cardNumber;
        public string CardNumber
        {
            get => cardNumber;
            set
            {
                Error = string.Empty;
                SetProperty(ref cardNumber, value);
                LoginCommand.ChangeCanExecute();
            }
        }

        private string pinNumber;
        public string PinNumber
        {
            get => pinNumber;
            set
            {
                Error = string.Empty;
                SetProperty(ref pinNumber, value);
                LoginCommand.ChangeCanExecute();
            }
        }

        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set
            {
                SetProperty(ref rememberMe, value);
            }
        }

        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;

        public LoginViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IAuthService authService,
            IProfileService profileService,
            IStorageService storageService) : base(appContext, navigationService, storageService)
        {
            _authService = authService;
            _profileService = profileService;

            Title = "Login";
            LoginCommand = new Command(async () => await Login(), () => CanLogin);
        }

        private bool CanLogin =>
            !IsBusy &&
            !string.IsNullOrWhiteSpace(CardNumber) && 
            !string.IsNullOrWhiteSpace(PinNumber) && 
            PinNumber.Length > 1;

        private async Task Login()
        {
            IsBusy = true;
            LoginCommand.ChangeCanExecute();

            try
            {
                Error = string.Empty;

                var loginResult = await _authService.Login(CardNumber, PinNumber);
                if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.Token))
                    throw new HttpException(HttpStatusCode.BadRequest, "Incorrect details");

                _appContext.LoggedIn = await _authService.Authorize(loginResult.Token);

                if (!_appContext.LoggedIn)
                    throw new HttpException(HttpStatusCode.Unauthorized);

                await HandleRememberMe();

                _appContext.Profile = await _profileService.Get();

                await _navigationService.RefreshMenu();

                await _navigationService.NavigateToHome();
            }
            catch (HttpException ex)
            {
                Error = ExceptionHelper.Handle(ex);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
                IsBusy = false;
                LoginCommand.ChangeCanExecute();
            }
        }

        private async Task HandleRememberMe()
        {
            if (RememberMe)
            {
                await _storageService.SaveKey(PropertyConstants.CardNumber, CardNumber);
                await _storageService.SaveKey(PropertyConstants.PinNumber, PinNumber);
            }
            else
            {
                await _storageService.DeleteKey(PropertyConstants.CardNumber);
                await _storageService.DeleteKey(PropertyConstants.PinNumber);
            }
        }
    }
}