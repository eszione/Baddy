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
using Android.Content;
using Baddy.Android.Services;
using Baddy.Droid.Helpers;
using Baddy.Constants;

namespace Baddy.Droid
{
    [Activity(Label = "Badcrew", Icon = "@mipmap/badcrew", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            var container = RegisterDependencies();

            var appContext = container.Resolve<IAppContext>();
            appContext.Container = container;

            LoadApplication(new App(appContext));

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
            .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            SetupScheduler(container);
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

        private void SetupScheduler(IUnityContainer container)
        {
            if (container.Resolve<IStorageService>().ReadKey<bool>(ScheduleConstants.ScheduleToggleOnOff))
                StartScheduler();

            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StartScheduler, (sender) =>
            {
                StartScheduler();
            });

            MessagingCenter.Subscribe<object>(this, ScheduleConstants.StopScheduler, (sender) =>
            {
                StopScheduler();
            });
        }

        private void StartScheduler()
        {
            var intent = new Intent(this, typeof(Scheduler));

            SchedulerHelper.StartScheduler(this, intent, 5);
        }

        private void StopScheduler()
        {
            var intent = new Intent(this, typeof(Scheduler));

            SchedulerHelper.StopScheduler(this, intent);
        }
    }
}