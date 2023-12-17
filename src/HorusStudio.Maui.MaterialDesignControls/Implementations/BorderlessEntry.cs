using System;

#if ANDROID

using Microsoft.Maui.Controls.Compatibility.Platform.Android;

#endif

namespace HorusStudio.Maui.MaterialDesignControls.Implementations
{
	public class BorderlessEntry: Entry
	{
		public static void RegisterHandler()
		{
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#elif IOS || MACCATALYST
                    handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                    handler.PlatformView.Layer.BorderWidth = 0;
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                    handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
                }
            });
        }
	}
}

