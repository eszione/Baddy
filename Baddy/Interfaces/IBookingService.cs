using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Baddy.Models;
using Baddy.Models.Apis;

namespace Baddy.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> Get();
        Task<BookingConfirmation> Confirm();
        Task<BookingConfirmed> Create(IEnumerable<CreateBookingInfo> bookings);
        Task<BookingDeleted> Delete(string bookingNumber);
    }
}
