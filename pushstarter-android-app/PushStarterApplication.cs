using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;

namespace pushstarter_android_app
{
    [Application(Name = "pushstarter_android_app.PushStarterApplication", Icon = "@mipmap/ic_launcher", Label = "@string/app_name", Theme = "@style/MyTheme.Base", AllowBackup = false)]
    public class PushStarterApplication : Application
    {
        public List<string> Messages { get; } = new List<string>();

        public PushStarterApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle,transfer)
        {
        }
    }
}