using Baddy.Interfaces;
using System.Threading.Tasks;

namespace Baddy.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IBookingService _bookingService;

        public ProfileViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IBookingService bookingService) : base(appContext, navigationService)
        {
            _bookingService = bookingService;
            Title = "Profile";

            _ = GetBalance();
        }

        private async Task GetBalance()
        {
            var bookings = await _bookingService.Get();

        }
    }
}