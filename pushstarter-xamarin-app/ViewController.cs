using System;

using UIKit;
using FHSDK;
using FHSDK.Services;
using AeroGear.Push;
using System.Diagnostics;
using Foundation;
using ObjCRuntime;
using FHSDK.Services.Network;
using System.Collections.Generic;

namespace pushstarterxamarinapp
{
	public partial class ViewController : UITableViewController
	{
		private const string NotificationCellIdentifier = "NotificationCell";
		private List<string> _messages = new List<string>();
		private bool _isRegistered;
			
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NSNotificationCenter.DefaultCenter.AddObserver (new NSString("sucess_registered"), (NSNotification obj) => {
				_isRegistered = true;
				TableView.ReloadData();
			});
			FH.RegisterPush (HandleNotification);
		}

		void HandleNotification(object sender, PushReceivedEvent e)
		{
			_messages.Add(e.Args.Message);
			TableView.ReloadData();
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			UIView bgView = null;
			if (!_isRegistered) {
				var view = NavigationController.Storyboard.InstantiateViewController ("ProgressViewController");
				bgView = view.View;
			} else if (_messages.Count == 0) {
				var view = NavigationController.Storyboard.InstantiateViewController ("EmptyViewController");
				bgView = view.View;
			}

			if (bgView != null) {
				tableView.BackgroundView = bgView;
				tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
				return 0;
			}
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _messages.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.BackgroundView != null) {
				tableView.BackgroundView = null;
				tableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
			}

			var cell = new UITableViewCell (UITableViewCellStyle.Default, NotificationCellIdentifier); 
			cell.TextLabel.Text = _messages [indexPath.Row];

			return cell;
		}
	}
}