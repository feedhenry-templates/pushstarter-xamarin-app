using AeroGear.Push;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using FHSDK.Services;

namespace pushstarter_android_app
{
    [Service(Name = "pushstarter_android_app.PushMessageReceiver", Exported = false)]
    [IntentFilter(new[] {"com.google.android.c2dm.intent.RECEIVE"})]
    public class PushMessageReceiver : FeedHenryMessageReceiver
    {
        protected override void DefaultHandleEvent(object sender, PushReceivedEvent e)
        {
            var context = Application.Context;
            var resultIntent = context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);

            var contentIntent = PendingIntent.GetActivity(Application.Context, 0, resultIntent,
                PendingIntentFlags.UpdateCurrent);
            var appName = GetAppName(Application.Context);
            var builder =
                new NotificationCompat.Builder(Application.Context)
                    .SetSmallIcon(context.ApplicationInfo.Icon)
                    .SetContentTitle(appName)
                    .SetTicker(appName)
                    .SetAutoCancel(true)
                    .SetContentIntent(contentIntent)
                    .SetContentText(e.Args.Message);

            var manager = (NotificationManager) context.GetSystemService(NotificationService);
            manager.Notify(appName, 23, builder.Build());
        }

        private static string GetAppName(Context context)
        {
            return context.PackageManager.GetApplicationLabel(context.ApplicationInfo);
        }
    }
}