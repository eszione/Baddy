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
            var nextScheduledDate = currentDateTime.Date.AddDays(GetDaysUntilNextScheduledDate(currentDateTime, scheduledDay)) + time;

            return nextScheduledDate > currentDateTime
                ? nextScheduledDate
                : nextScheduledDate.AddDays(7);
        }

        private static int GetDaysUntilNextScheduledDate(DateTime currentDateTime, Days scheduledDay)
        {
            return ((int)scheduledDay - (int)currentDateTime.DayOfWeek + 7) % 7;
        }
    }
}
