using Android.App;
using Android.Content;
using Android.OS;

namespace Baddy.Droid.Helpers
{
    public class SchedulerHelper
    {
        public static void StartScheduler(Context context, Intent intent, int seconds)
        {
            var source = PendingIntent.GetBroadcast(context, 0, intent, 0);
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + seconds * 1000, source);
        }

        public static void StopScheduler(Context context, Intent intent)
        {
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(PendingIntent.GetBroadcast(context, 0, intent, 0));
        }
    }
}