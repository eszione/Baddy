using System.Threading.Tasks;
using Baddy.Models;

namespace Baddy.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> Get();
        Task<BookingConfirmation> Confirm();
        Task<BookingConfirmed> Create();
        Task<BookingDeleted> Delete(string bookingNumber);
    }
}
