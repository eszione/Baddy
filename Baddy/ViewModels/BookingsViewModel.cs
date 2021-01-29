using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using Baddy.Models.Apis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class BookingsViewModel : BaseViewModel
    {
        private IList<BookingData> _bookings;
        public IList<BookingData> Bookings { get => _bookings; set => SetProperty(ref _bookings, value); }

        public Command<string> DeleteCommand { get; set; }

        private readonly IBookingService _bookingService;

        public BookingsViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService,
            IBookingService bookingService) : base(appContext, navigationService, storageService)
        {
            Title = "Bookings";
            _bookingService = bookingService;

            DeleteCommand = new Command<string>(async (bookingNo) => await Delete(bookingNo));
            RefreshCommand = new Command(async () => await Refresh());
        }

        public async Task SetBookings()
        {
            IsBusy = true;

            try
            {
                var booking = await _bookingService.Get();
                Bookings = booking.Data;
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

        public async Task Delete(string bookingNo)
        {
            await _bookingService.Delete(bookingNo);

            await SetBookings();
        }

        public async Task Refresh()
        {
            IsRefreshing = true;

            await SetBookings();

            IsRefreshing = false;
        }
    }
}