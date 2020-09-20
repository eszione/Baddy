using System.Collections.Generic;
using System.Threading.Tasks;
using Baddy.Constants;
using Baddy.Interfaces;
using Baddy.Models;

namespace Baddy.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHttpService _httpService;

        public BookingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Booking> Get()
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("function", "summary")
            };

            return await _httpService.Post<Booking>(parameters, UrlConstants.Main);
        }

        public async Task<BookingConfirmation> Confirm()
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("function", "booking_confirm"),
                new KeyValuePair<string, string>("sport_court", "A"),
                new KeyValuePair<string, string>("date", "2020 09 11"),
                new KeyValuePair<string, string>("bookings", "960,2,30,1.990,2,30,1")
            };

            return await _httpService.Post<BookingConfirmation>(parameters, UrlConstants.Main);
        }

        public async Task<BookingConfirmed> Create()
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("function", "booking_finish"),
                new KeyValuePair<string, string>("sport_court", "A"),
                new KeyValuePair<string, string>("date", "2020 09 11"),
                new KeyValuePair<string, string>("bookings", "960,2,60,1300,2020 09 11")
            };

            return await _httpService.Post<BookingConfirmed>(parameters, UrlConstants.Main);
        }

        public async Task<BookingDeleted> Delete(string bookingNumber)
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("function", "summary"),
                new KeyValuePair<string, string>("delete_str", bookingNumber),
            };

            return await _httpService.Post<BookingDeleted>(parameters, UrlConstants.Main);
        }
    }
}
