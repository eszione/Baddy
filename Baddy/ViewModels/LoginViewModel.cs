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

        private readonly IAuthService _authService;

        public LoginViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IAuthService authService) : base(appContext, navigationService)
        {
            _authService = authService;
            Title = "Login";
            LoginCommand = new Command(async () => await Login(), () => CanLogin);
        }

        private bool CanLogin =>
            !string.IsNullOrWhiteSpace(CardNumber) && 
            !string.IsNullOrWhiteSpace(PinNumber) && 
            PinNumber.Length > 1;

        private async Task Login()
        {
            try
            {
                Error = string.Empty;

                var loginResult = await _authService.Login(CardNumber, PinNumber);
                if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.Token))
                    throw new HttpException(HttpStatusCode.BadRequest, "Incorrect details");

                _appContext.LoggedIn = await _authService.Authorize(loginResult.Token);

                if (!_appContext.LoggedIn)
                    throw new HttpException(HttpStatusCode.Unauthorized);

                await _navigationService.NavigateTo<ProfileViewModel>();
            }
            catch (HttpException ex)
            {
                Error = ExceptionHelper.Handle(ex);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
    }
}