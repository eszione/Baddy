using Android.App;
using Android.Content;
using System;

namespace Baddy.Droid.Helpers
{
    public class SchedulerHelper
    {
        public static void StartScheduler(Context context, Intent intent, DateTime date)
        {
            if (PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.NoCreate) != null)
                StopScheduler(context, intent);

            var source = PendingIntent.GetBroadcast(context, 0, intent, 0);
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            var calendar = Java.Util.Calendar.Instance;
            calendar.Set(date.Year, date.Month - 1, date.Day, date.Hour, date.Minute, date.Second);

            alarmManager.SetExactAndAllowWhileIdle(AlarmType.Rtc, calendar.TimeInMillis, source);
        }

        public static void StopScheduler(Context context, Intent intent)
        {
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(PendingIntent.GetBroadcast(context, 0, intent, 0));
        }
    }
}