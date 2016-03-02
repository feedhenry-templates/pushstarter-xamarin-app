using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace pushstarter_android_app
{
    [Activity(Label = "@string/notifications", MainLauncher = true, Theme = "@style/MyTheme.Base")]
    public class MessagesActivity : AppCompatActivity
    {
        private PushStarterApplication _application;
        private ListView _listview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.messages);

            _application = (PushStarterApplication) Application;
            var emptyView = FindViewById(Resource.Id.empty);
            _listview = FindViewById<ListView>(Resource.Id.messages);
            _listview.EmptyView = emptyView;
        }

        private void AddNewMessage(Bundle bundle)
        {
            _application.Messages.Add(bundle.GetString("alert"));
            DisplayMessages();
        }

        private void DisplayMessages()
        {
            var adapter = new ArrayAdapter(ApplicationContext, Resource.Layout.message_item, _application.Messages);
            _listview.Adapter = adapter;
        }
    }
}