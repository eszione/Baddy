using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using Baddy.Constants;
using Baddy.Droid.Helpers;
using Baddy.Enums;
using Baddy.Helpers;
using Baddy.Interfaces;
using CommonServiceLocator;
using System;

namespace Baddy.Droid.Services
{
    [Service]
    public class ForegroundScheduler : Service
    {
        private readonly IStorageService _storageService;

        public ForegroundScheduler()
        {
            _storageService = ServiceLocator.Current.GetInstance<IStorageService>();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override void OnCreate()
        {
            base.OnCreate();

            Start();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            StopScheduler();

            StopForeground(true);
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            StartScheduler();

            return StartCommandResult.Sticky;
        }

        private void Start()
        {
            var channel = new NotificationChannel("Badcrew", "Badcrew", NotificationImportance.Default)
            {
                Description = "Booking schedule is running"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);

            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channel.Id)
                .SetContentText("Booking schedule is running")
                .SetSmallIcon(Resource.Drawable.schedule)
                .SetOngoing(true); ;

            // Build the notification
            Notification notification = builder.Build();

            notificationManager.Notify(1, notification);

            // Enlist this instance of the service as a foreground service
            StartForeground(1, notification);
        }

        private void StartScheduler()
        {
            var scheduleDay = _storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            var scheduleTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);
            var nextScheduleDate = DateTimeHelper.NextScheduledDate(DateTime.Now, scheduleDay, scheduleTime);

            SchedulerHelper.StartScheduler(this, nextScheduleDate);
        }

        private void StopScheduler()
        {
            SchedulerHelper.StopScheduler(this);
        }
    }
}