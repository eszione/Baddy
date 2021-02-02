using Baddy.Constants;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;

        private bool loggedIn;
        public bool LoggedIn
        {
            get => loggedIn;
            set => SetProperty(ref loggedIn, value);
        }

        private Profile profile;
        public Profile Profile
        {
            get => profile;
            set => SetProperty(ref profile, value);
        }

        public Command ViewProfileCommand { get; set; }
        public Command ViewBookingsCommand { get; set; }
        public Command ScheduleBookingCommand { get; set; }

        public HomeViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService,
            IAuthService authService,
            AppState appState,
            IProfileService profileService) : base(appContext, navigationService, storageService)
        {
            _authService = authService;
            _profileService = profileService;

            LoggedIn = _appContext.LoggedIn;
            Profile = _appContext.Profile;

            Title = "Home";

            if (appState == AppState.Initialize)
                Task.Run(async () => await RememberMe());

            ViewProfileCommand = new Command(async() => await navigationService.NavigateTo<ProfileViewModel>());
            ViewBookingsCommand = new Command(async() => await navigationService.NavigateTo<BookingsViewModel>());
            ScheduleBookingCommand = new Command(async() => await navigationService.NavigateTo<ScheduleBookingViewModel>());
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

                    LoggedIn = _appContext.LoggedIn = await _authService.Authorize(loginResult.Token);

                    if (!LoggedIn)
                        throw new HttpException(HttpStatusCode.Unauthorized);

                    Profile = _appContext.Profile = await _profileService.Get();

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