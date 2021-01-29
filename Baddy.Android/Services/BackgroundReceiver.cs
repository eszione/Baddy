using Android.Content;
using Android.OS;
using System;
using Xamarin.Forms;

namespace Baddy.Android.Services
{
    [BroadcastReceiver(Enabled = true)]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
           /* var pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();*/

            Console.WriteLine("Test");
            // MessagingCenter.Send<object>(this, "Book court");
            
            //wakeLock.Release();
        }
    }
}
