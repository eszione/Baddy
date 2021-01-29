using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace Baddy.Droid.Services
{
    public class BackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return base.OnStartCommand(intent, flags, startId);
        }
    }
}