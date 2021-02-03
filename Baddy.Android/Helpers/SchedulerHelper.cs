using Android.App;
using Android.Content;
using Baddy.Android.Services;
using Baddy.Droid.Services;
using Java.Lang;
using System;
using System.Linq;

namespace Baddy.Droid.Helpers
{
    public class SchedulerHelper
    {
        public static void StartScheduler(Context context, DateTime date)
        {
            var intent = new Intent(context, typeof(Scheduler));

            if (PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.NoCreate) != null)
                StopScheduler(context);

            var source = PendingIntent.GetBroadcast(context, 0, intent, 0);
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            var calendar = Java.Util.Calendar.Instance;
            calendar.Set(date.Year, date.Month - 1, date.Day, date.Hour, date.Minute, date.Second);

            alarmManager.SetExactAndAllowWhileIdle(AlarmType.Rtc, calendar.TimeInMillis, source);
        }

        public static void StopScheduler(Context context)
        {
            var intent = new Intent(context, typeof(Scheduler));

            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(PendingIntent.GetBroadcast(context, 0, intent, 0));
        }

        public static void StartForegroundService(Context context)
        {
            if (IsForegroundServiceRunning(context))
                StopForegroundService(context);

            var intent = new Intent(context, typeof(ForegroundScheduler));
            context.StartForegroundService(intent);
        }

        public static void StopForegroundService(Context context)
        {
            var intent = new Intent(context, typeof(ForegroundScheduler));
            context.StopService(intent);
        }

        public static bool IsForegroundServiceRunning(Context context)
        {
            var manager = (ActivityManager)context.GetSystemService(Context.ActivityService);

            return manager.GetRunningServices(int.MaxValue)
                .Any(service => service.Service.ClassName.Equals(Class.FromType(typeof(ForegroundScheduler)).CanonicalName));
        }
    }
}