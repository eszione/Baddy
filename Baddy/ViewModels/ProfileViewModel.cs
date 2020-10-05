using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using System;
using System.Threading.Tasks;

namespace Baddy.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private Profile _profile;
        public Profile Profile { get => _profile; set => SetProperty(ref _profile, value); }

        private readonly IProfileService _profileService;

        public ProfileViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IProfileService profileService,
            IStorageService storageService) : base(appContext, navigationService, storageService)
        {
            Title = "Profile";
            _profileService = profileService;

            Task.Run(async () =>
            {
                await SetProfile();
            });
        }

        private async Task SetProfile()
        {
            IsBusy = true;

            try
            {
                Profile = await _profileService.Get();
                _appContext.Profile = Profile;
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
            }
        }
    }
}