using Microsoft.Extensions.Logging;

namespace MauiStream;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers(handlers =>
			{
#if ANDROID
				// Remove the underline from Entry on Android
				Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
				{
					handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
					// Remove default padding and set gravity to center text vertically
					handler.PlatformView.SetPadding(0, 0, 0, 0);
					handler.PlatformView.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.Start;
				});
#elif IOS
				// Remove the InputAccessoryView (Done button toolbar) from Entry on iOS
				Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoInputAccessoryView", (handler, view) =>
				{
					handler.PlatformView.InputAccessoryView = null;
				});
#endif
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
