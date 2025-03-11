using Android.App;
using Android.Views;
using HorusStudio.Maui.MaterialDesignControls;
using ImageButton = Android.Widget.ImageButton;
using Microsoft.Maui.Platform;

namespace Google.Android.Material.Snackbar;

public static class SnackbarExtensions
{
    public static ImageButton? CreateImageButton(this Activity activity, 
        ImageSource source, 
        int size, 
        Microsoft.Maui.Graphics.Color color, 
        Thickness padding, 
        Action? action)
    {
        var icon = source.ToDrawable(size, color);
        if (icon is null) return null;
        
        var button = new ImageButton(activity);
        button.SetImageDrawable(icon);
        button.SetPadding(padding);
        button.SetBackgroundColor(Colors.Transparent.ToPlatform());
        if (action is not null)
        {
            button.Click += (sender, args) => action.Invoke();    
        }
        
        return button;
    }

    public static ImageButton? AddIcon(this SnackbarContentLayout contentLayout, 
        Activity activity, 
        ImageSource source,
        int size, 
        Microsoft.Maui.Graphics.Color color, 
        Thickness padding, 
        Action? action,
        int index)
    {
        var iconView = activity.CreateImageButton(source, size, color,
            padding, action);
            
        if (iconView is not null)
        {
            contentLayout.AddView(iconView, index);
        }
        
        return iconView;
    }

    public static ImageButton? AddIcon(this SnackbarContentLayout contentLayout,
        Activity activity,
        MaterialSnackbarConfig.IconConfig config,
        int index)
    {
        var iconView = AddIcon(contentLayout, activity, config.Source, config.Size, config.Color, new Thickness(0), config.Action, index);
        if (iconView?.LayoutParameters != null)
        {
            iconView.LayoutParameters.Width = config.Size.DpToPixels();
            iconView.LayoutParameters.Height = ViewGroup.LayoutParams.MatchParent;
        }
        return iconView;
    }
}