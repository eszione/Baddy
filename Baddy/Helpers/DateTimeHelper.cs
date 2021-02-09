using Baddy.Constants;
using Baddy.Enums;
using System;

namespace Baddy.Helpers
{
    public class DateTimeHelper
    {
        public static double MinutesRounder(int minutes)
        {
            if (minutes < 15)
                return 0;

            if (minutes >= 15 && minutes <= 44)
                return 0.5;

            if (minutes > 44)
                return 1;

            return 0;
        }

        public static DateTime NextScheduledDate(DateTime currentDateTime, Days scheduledDay, TimeSpan time)
        {
            var scheduledDate = GetCurrentScheduledDate(currentDateTime, scheduledDay, time);

            return scheduledDate > currentDateTime.AddMinutes(DateConstants.BufferPeriod)
                ? scheduledDate.AddMinutes(-DateConstants.BufferPeriod)
                : scheduledDate.AddDays(7).AddMinutes(-DateConstants.BufferPeriod);
        }

        public static DateTime GetCurrentScheduledDate(DateTime currentDateTime, Days scheduledDay, TimeSpan time)
        {
            return currentDateTime.Date.AddDays(GetDaysUntilNextScheduledDate(currentDateTime, scheduledDay)) + time;
        }

        private static int GetDaysUntilNextScheduledDate(DateTime currentDateTime, Days scheduledDay)
        {
            return ((int)scheduledDay - (int)currentDateTime.DayOfWeek + 7) % 7;
        }
    }
}
