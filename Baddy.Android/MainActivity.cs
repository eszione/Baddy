using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Baddy.Helpers;
using Unity;
using CommonServiceLocator;
using Unity.ServiceLocation;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Baddy.Interfaces;
using Baddy.Droid.Helpers;
using Baddy.Constants;
using Baddy.Enums;
using System;

namespace Baddy.Droid
{
    [Activity(Label = "Badcrew", Icon = "@mipmap/badcrew", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private static MainActivity Current;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Current = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);

            var container = RegisterDependencies();

            var appContext = container.Resolve<IAppContext>();
            appContext.Container = container;

            LoadApplication(new App(appContext));

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
            .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            SetupScheduling(container);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            DependencyHelper.RegisterDependencies(container);

            var unityServiceLocator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            return container;
        }

        private void SetupScheduling(IUnityContainer container)
        {
            var storageService = container.Resolve<IStorageService>();

            SetupSchedulerMessagingCenter(storageService);
            SetupForegroundMessagingCenter();

            if (storageService.ReadKey<bool>(ScheduleConstants.ForegroundToggleOnOff))
                SchedulerHelper.StartForegroundService(this);
            else if (storageService.ReadKey<bool>(ScheduleConstants.ScheduleToggleOnOff))
                StartScheduler(storageService);
        }
        
        private void SetupSchedulerMessagingCenter(IStorageService storageService)
        {
            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StartScheduler, (sender) =>
            {
                StartScheduler(storageService);
            });

            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StopScheduler, (sender) =>
            {
                SchedulerHelper.StopScheduler(this);
            });
        }

        private void SetupForegroundMessagingCenter()
        {
            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StartForegroundService, (sender) =>
            {
                if (this == Current)
                    SchedulerHelper.StartForegroundService(this);
            });

            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StopForegroundService, (sender) =>
            {
                SchedulerHelper.StopForegroundService(this);
            });
        }

        private void StartScheduler(IStorageService storageService)
        {
            var scheduleDay = storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            var scheduleTime = storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);
            var nextScheduleDate = DateTimeHelper.NextScheduledDate(DateTime.Now, scheduleDay, scheduleTime);

            SchedulerHelper.StartScheduler(this, nextScheduleDate);
        }
    }
}