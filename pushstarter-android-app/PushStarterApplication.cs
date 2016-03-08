using System;
using Android.App;
using Android.Runtime;
using FHSDK;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.ACCESS_NETWORK_STATE")]

namespace pushstarter_android_app
{
    [Application(Name = "pushstarter_android_app.PushStarterApplication", Icon = "@mipmap/ic_launcher",
        Label = "@string/app_name", Theme = "@style/MyTheme.Base", AllowBackup = false)]
    public class PushStarterApplication : Application
    {
        public PushStarterApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override async void OnCreate()
        {
            base.OnCreate();
            await FHClient.Init();
        }
    }
}