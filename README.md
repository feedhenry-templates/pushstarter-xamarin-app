# pushstarter-xamarin-app

Author: Erik Jan de Wit   
Level: Intermediate  
Technologies: C#, Xamarin, iOS, Android, RHMAP
Summary: A demonstration of how to include basic push functionality with RHMAP.
Community Project : [Feed Henry](http://feedhenry.org)
Target Product: RHMAP  
Product Versions: RHMAP 3.7.0+   
Source: https://github.com/feedhenry-templates/pushstarter-ios-app  
Prerequisites: fh-dotnet-sdk : 3.+, Xamarin Studio : 5.2+, iOS SDK : iOS7+

## What is it?

The ```PushStarter``` project demonstrates how to include basic push functionality using [fh-dotnet-sdk](https://github.com/feedhenry/fh-dotnet-sdk) and Red Hat Mobile Application Platform. The developer should:
- enable push notifications in the iOS app within RHMAP, 
- enter required certificate,
- send test notification via RHMAP studio Push tab.
The iOS app catches the notification and displays them as a list.

If you do not have access to a RHMAP instance, you can sign up for a free instance at [https://openshift.feedhenry.com/](https://openshift.feedhenry.com/).

## How do I run it?  

### RHMAP Studio

This application and its cloud services are available as a project template in RHMAP as part of the "Push Notification Hello World" template.

### Local Clone (ideal for Open Source Development)

If you wish to contribute to this template, the following information may be helpful; otherwise, RHMAP and its build facilities are the preferred solution.

## Build instructions

1. Clone this project

2. Populate ```pushstarter-xamarin-app/fhconfig.plist``` with your values as explained [here](http://docs.feedhenry.com/v3/dev_tools/sdks/ios.html#ios-configure).

3. Open pushstarter-xamarin-app.sln

4. Run the project
 
## How does it work?

### FH registers for remote push notification

In ```pushstarter-xamarin-app/ViewController.cs``` you register a handler that will be called for push messages:

```csharp
	NSNotificationCenter.DefaultCenter.AddObserver (new NSString("sucess_registered"), (NSNotification obj) => { // [1]
		_isRegistered = true;
		TableView.ReloadData();
	});
	FH.RegisterPush (HandleNotification); // [2]
}

void HandleNotification(object sender, PushReceivedEvent e)
```
[1] event that is called to indicate that registration is complete
[2] HandleNotification handler to be called when receiving push notifications

In ```pushstarter-xamarin-app/AppDelegate.cs``` you finish the registration process and specify the deviceToken:

```csharp
public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
{
	FHClient.FinishRegistration (deviceToken);
}
```

### FH receives remote push notification

To receive notification, in ```pushstarter-xamarin-app/AppDelegate.cs```:

```csharp
public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
{
	FHClient.OnMessageReceived (userInfo);
}
```
This will convert the message to use the same API as on windows and call the Handler that you used in the register call.
