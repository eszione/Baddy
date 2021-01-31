using Android.Content;
using Baddy.Constants;
using Baddy.Droid.Helpers;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using CommonServiceLocator;
using System;

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
            var isScheduled = _storageService.ReadKey<bool>(ScheduleConstants.ScheduleToggleOnOff);
            if (isScheduled)
            {
                var court = _storageService.ReadKey<int>(ScheduleConstants.Court);
                var day = _storageService.ReadKey<Days>(ScheduleConstants.BookingDay);
                var time = _storageService.ReadKey<TimeSpan>(ScheduleConstants.BookingTime);
                var duration = _storageService.ReadKey<int>(ScheduleConstants.BookingDuration);
                Console.WriteLine(DateTimeHelper.NextScheduledDate(DateTime.Now, day, time));
            }

            SchedulerHelper.StartScheduler(context, intent, 5);
        }
    }
}
