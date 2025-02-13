using Android.App;
using Android.Views;
using HorusStudio.Maui.MaterialDesignControls;
using Microsoft.Maui.Platform;
using ImageButton = Android.Widget.ImageButton;

namespace Google.Android.Material.Snackbar;

public static class SnackbarExtensions
{
    public static ImageButton? CreateImageButton(this Activity activity, ImageSource source, int size, Microsoft.Maui.Graphics.Color color, Thickness padding, Action? action)
    {
        var icon = source.ToDrawable(size, color);
        if (icon is null) return null;
        
        var button = new ImageButton(activity);
        button.SetImageDrawable(icon);
        button.SetPadding(padding);
        button.SetBackgroundColor(Colors.Transparent.ToPlatform());
        if (action is not null)
        {
            button.Click += (sender, args) => action();    
        }
        
        return button;
    }
    
    public static ImageButton? AddIcon(this SnackbarContentLayout contentLayout, Activity activity, SnackbarConfig.IconConfig config, int index)
    {
        var iconView = CreateImageButton(activity, config.Source, config.Size, config.Color,
            new Thickness(0), config.Action);
            
        if (iconView is not null)
        {
            contentLayout.AddView(iconView, index);
            if (iconView.LayoutParameters != null)
            {
                iconView.LayoutParameters.Width = config.Size.DpToPixels();
                iconView.LayoutParameters.Height = ViewGroup.LayoutParams.MatchParent;
            }
        }

        return iconView;
    }
}