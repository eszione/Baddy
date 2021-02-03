using Android.Content;
using Baddy.Constants;
using Baddy.Droid.Helpers;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using Baddy.Models.Apis;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baddy.Android.Services
{
    [BroadcastReceiver]
    public class Scheduler : BroadcastReceiver
    {
        private readonly IStorageService _storageService;
        private readonly IBookingService _bookingService;
        private readonly IEmailService _emailService;
        private readonly IAppContext _appContext;

        public Scheduler()
        {
            _storageService = ServiceLocator.Current.GetInstance<IStorageService>();
            _bookingService = ServiceLocator.Current.GetInstance<IBookingService>();
            _emailService = ServiceLocator.Current.GetInstance<IEmailService>();
            _appContext = ServiceLocator.Current.GetInstance<IAppContext>();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var currentDateTime = DateTime.Now;

            _ = HandleBooking(currentDateTime);

            var scheduleDay = _storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            var scheduleTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);

            var nextScheduleDateTime = DateTimeHelper.NextScheduledDate(currentDateTime, scheduleDay, scheduleTime);

            SchedulerHelper.StartScheduler(context, nextScheduleDateTime);

            SendEmail("Next scheduled booking", $"Your next booking is scheduled to run on: {nextScheduleDateTime.ToString(DateConstants.LongDateTimeFormat)}\n");
        }

        private bool CanBook(int duration, int court)
        {
            return duration > 0 && court > 0;
        }

        private void SendEmail(string subject, string body)
        {
            _emailService.SendEmail(_appContext.Profile.Name, _appContext.Profile.Email, subject, body);
        }

        private async Task HandleBooking(DateTime currentDateTime)
        {
            var bookingTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.BookingTime);
            var duration = _storageService.ReadKey<int>(ScheduleConstants.BookingDuration);
            var court = _storageService.ReadKey<int>(ScheduleConstants.Court);

            if (CanBook(duration, court))
            {
                while(true)
                {
                    try
                    {
                        var bookingDate = currentDateTime.Date.AddDays(14) + bookingTime;
                        var bookingConfirmed = await Book(bookingDate, court, duration);
                        var booking = bookingConfirmed?.Bookings?.FirstOrDefault();
                        if (booking != null)
                        {
                            SendEmail(
                                "Booking confirmed", 
                                $"Your booking was confirmed for {bookingDate.ToString(DateConstants.LongDateTimeFormat)}" +
                                $"\nCourt {court}, {duration} minutes\n\n" +
                                $"Current time is: {DateTime.Now.ToString(DateConstants.LongDateTimeFormat)}"
                            );
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        SendEmail("Booking failed", $"An error occurred during your booking: {ex.Message}");
                    }
                }
            }
        }

        private async Task<BookingConfirmed> Book(DateTime date, int court, int duration)
        {
            return await _bookingService.Create(new List<CreateBookingInfo>
            {
                new CreateBookingInfo
                {
                    Date = date,
                    Court = court,
                    Duration = duration
                }
            });
        }
    }
}
