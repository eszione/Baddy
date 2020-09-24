using Baddy.Interfaces;
using Baddy.Models;
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
            IProfileService profileService) : base(appContext, navigationService)
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
            Profile = await _profileService.Get();
            _appContext.Profile = Profile;
        }
    }
}