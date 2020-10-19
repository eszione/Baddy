using Baddy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Baddy.Helpers
{
    public class PriceHelper
    {
        /// <summary>
        ///  Fees (30 minutes)
        /// Weekdays
        /// 4:30a.m -> 11:30a.m = $6.50
        /// 12:00p.m -> 3:30p.m = $4.00
        /// 4:00p.m -> 5:30p.m = $6.50
        /// 6:00p.m -> 10.30p.m = $7.50
        /// Weekends
        /// $7.50 flat rate
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static int Calculate(DateTime startDate, int duration)
        {
            var weekends = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

            if (weekends.Contains(startDate.DayOfWeek))
                return ConvertDurationToPrice(PricePerHour.WeekendRate, duration);

            var startTime = ConvertMinutes(startDate.Hour, startDate.Minute);
            var endDate = startDate.AddMinutes(duration);
            var endTime = ConvertMinutes(endDate.Hour, endDate.Minute);

            // Early
            if (endTime <= ConvertMinutes(12, 0))
                return ConvertDurationToPrice(PricePerHour.WeekdayMorning, duration);

            // Overlap of early and mid
            if (startTime < ConvertMinutes(12, 0) && endTime <= ConvertMinutes(16, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeekdayMorning, ConvertMinutes(12, 0) - startTime),
                        (PricePerHour.WeedayMid, endTime - ConvertMinutes(12, 0))
                    }
                );

            // Overlap of early, mid and afternoon
            if (startTime < ConvertMinutes(12, 0) && endTime > ConvertMinutes(16, 0) && endTime <= ConvertMinutes(18, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeekdayMorning, ConvertMinutes(12, 0) - startTime),
                        (PricePerHour.WeedayMid, ConvertMinutes(16, 0) - ConvertMinutes(12, 0)),
                        (PricePerHour.WeekdayAfternoon, endTime - ConvertMinutes(16, 0))
                    }
                );

            // Overlap of early, mid, afternoon and evening
            if (startTime < ConvertMinutes(12, 0) && endTime > ConvertMinutes(18, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeekdayMorning, ConvertMinutes(12, 0) - startTime),
                        (PricePerHour.WeedayMid, ConvertMinutes(16, 0) - ConvertMinutes(12, 0)),
                        (PricePerHour.WeekdayAfternoon, ConvertMinutes(18, 0) - ConvertMinutes(16, 0)),
                        (PricePerHour.WeekdayEvening, endTime - ConvertMinutes(18, 0))
                    }
                );

            // Mid
            if (startTime >= ConvertMinutes(12, 0) && endTime <= ConvertMinutes(16, 0))
                return ConvertDurationToPrice(PricePerHour.WeedayMid, duration);

            // Overlap of mid and afternoon
            if (startTime >= ConvertMinutes(12, 0) && startTime < ConvertMinutes(16, 0) && endTime <= ConvertMinutes(18, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeedayMid, ConvertMinutes(16, 0) - startTime),
                        (PricePerHour.WeekdayAfternoon, endTime - ConvertMinutes(16, 0))
                    }
                );

            // Overlap of mid, afternoon and evening
            if (startTime >= ConvertMinutes(12, 0) && startTime < ConvertMinutes(16, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeedayMid, ConvertMinutes(16, 0) - startTime),
                        (PricePerHour.WeekdayAfternoon, ConvertMinutes(18, 0) - ConvertMinutes(16, 0)),
                        (PricePerHour.WeekdayEvening, endTime - ConvertMinutes(18, 0))
                    }
                );

            // Afternoon
            if (startTime >= ConvertMinutes(16, 0) && endTime <= ConvertMinutes(18, 0))
                return ConvertDurationToPrice(PricePerHour.WeekdayAfternoon, duration);

            // Overlap of afternoon and evening
            if (startTime >= ConvertMinutes(16, 0) && startTime < ConvertMinutes(18, 0) && endTime > ConvertMinutes(18, 0))
                return ConvertDurationsToPrice(
                    new List<(PricePerHour, int)>
                    {
                        (PricePerHour.WeekdayAfternoon, ConvertMinutes(18, 0) - startTime),
                        (PricePerHour.WeekdayEvening, endTime - ConvertMinutes(18, 0))
                    }
                );

            // Evening
            if (startTime >= ConvertMinutes(18, 0))
                return ConvertDurationToPrice(PricePerHour.WeekdayEvening, duration);

            return 0;
        }

        private static int ConvertDurationToPrice(PricePerHour price, int duration)
        {
            return (int)(duration / (double)60 * (int)price);
        }

        private static int ConvertDurationsToPrice(IEnumerable<(PricePerHour price, int duration)> mappings)
        {
            return mappings.Sum(mapping => ConvertDurationToPrice(mapping.price, mapping.duration));
        }

        private static int ConvertMinutes(int hour, int minute)
        {
            return (int)new TimeSpan(hour, minute, 0).TotalMinutes;
        }
    }
}
