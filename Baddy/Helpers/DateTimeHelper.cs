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

        public static DateTime NextScheduledDate(DateTime now, Days day, TimeSpan time)
        {
            return now.Date.AddDays(GetDaysUntilNextScheduledDate(now, day)) + time;
        }

        private static int GetDaysUntilNextScheduledDate(DateTime now, Days day)
        {
            return ((int)day - (int)now.DayOfWeek + 7) % 7;
        }
    }
}
