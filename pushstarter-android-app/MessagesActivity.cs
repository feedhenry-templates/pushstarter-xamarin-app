using System.Collections.Generic;
using AeroGear.Push;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using FHSDK;

namespace pushstarter_android_app
{
    [Activity(Label = "@string/notifications", MainLauncher = true, Theme = "@style/MyTheme.Base")]
    public class MessagesActivity : AppCompatActivity
    {
        private ListView _listview;
        private List<string> Messages { get; } = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FH.RegisterPush(HandleEvent);

            SetContentView(Resource.Layout.messages);

            var emptyView = FindViewById(Resource.Id.empty);
            _listview = FindViewById<ListView>(Resource.Id.messages);
            _listview.EmptyView = emptyView;
        }

        public void HandleEvent(object sender, PushReceivedEvent e)
        {
            RunOnUiThread(() =>
            {
                Messages.Add(e.Args.Message);
                DisplayMessages();
            });
        }

        private void DisplayMessages()
        {
            var adapter = new ArrayAdapter<string>(ApplicationContext, Resource.Layout.message_item, Messages);
            _listview.Adapter = adapter;
        }
    }
}