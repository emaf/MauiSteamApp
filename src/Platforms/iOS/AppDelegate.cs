using Foundation;
using UIKit;

namespace MauiStream;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	public override bool FinishedLaunching(UIApplication application, NSDictionary? launchOptions)
	{
		// Force tab bar to remain opaque and not transparent when scrolling
		if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
		{
			var appearance = new UITabBarAppearance();
			appearance.ConfigureWithOpaqueBackground();
			appearance.BackgroundColor = UIColor.FromRGB(23, 26, 33); // #171a21
			
			UITabBar.Appearance.StandardAppearance = appearance;
			UITabBar.Appearance.ScrollEdgeAppearance = appearance;
		}
		
		return base.FinishedLaunching(application, launchOptions);
	}
}
