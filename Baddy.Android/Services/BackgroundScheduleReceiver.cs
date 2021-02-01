using Android.Content;
using Baddy.Constants;
using Baddy.Droid.Helpers;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using Baddy.Models;
using CommonServiceLocator;
using System;
using System.Collections.Generic;

namespace Baddy.Android.Services
{
    [BroadcastReceiver]
    public class Scheduler : BroadcastReceiver
    {
        private readonly IStorageService _storageService;
        private readonly IBookingService _bookingService;

        public Scheduler()
        {
            _storageService = ServiceLocator.Current.GetInstance<IStorageService>();
            _bookingService = ServiceLocator.Current.GetInstance<IBookingService>();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var currentDateTime = DateTime.Now;

            var bookingTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.BookingTime);
            var duration = _storageService.ReadKey<int>(ScheduleConstants.BookingDuration);
            var court = _storageService.ReadKey<int>(ScheduleConstants.Court);

            if (CanBook(duration, court))
            {
                Console.WriteLine(currentDateTime.Date.AddDays(14) + bookingTime);
                /*var bookingConfirmed = _bookingService.Create(new List<CreateBookingInfo>
                {
                    new CreateBookingInfo
                    {
                        Date = currentDateTime.Date.AddDays(14) + bookingTime,
                        Court = court,
                        Duration = duration
                    }
                });*/
            }

            var scheduleDay = _storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            var scheduleTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);

            SchedulerHelper.StartScheduler(context, intent, DateTimeHelper.NextScheduledDate(currentDateTime, scheduleDay, scheduleTime));
        }

        private bool CanBook(int duration, int court)
        {
            return duration > 0 && court > 0;
        }
    }
}
