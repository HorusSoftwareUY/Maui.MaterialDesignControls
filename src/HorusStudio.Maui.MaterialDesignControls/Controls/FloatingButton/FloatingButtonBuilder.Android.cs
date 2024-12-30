using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Google.Android.Material.Snackbar;
using Button = Android.Widget.Button;
using HorusStudio.Maui.MaterialDesignControls.Extensions;
using LayoutDirection = Android.Views.LayoutDirection;

namespace HorusStudio.Maui.MaterialDesignControls;

public class FloatingButtonBuilder : Snackbar.Callback
{

    #region Properties
    protected Activity Activity { get; }
    protected FloatingButtonConfig Config { get; }
    
    private Action _dismissed;

    #endregion
    
    #region Constructors

    public FloatingButtonBuilder(Activity activity, FloatingButtonConfig config)
    {
        Activity = activity ?? throw new ArgumentNullException(nameof(activity));
        Config = config ?? throw new ArgumentNullException(nameof(config));
    }

    #endregion

    #region Overrides

    public override void OnShown(Google.Android.Material.Snackbar.Snackbar FloatingButton)
    {
        base.OnShown(FloatingButton);
        FloatingButton.View.Animate().Alpha(1f).SetDuration(300).Start();
    }

    public override void OnDismissed(Snackbar transientBottomBar, int e)
    {
        base.OnDismissed(transientBottomBar, e);
        transientBottomBar?.SetDuration(0);
        transientBottomBar.View.Animate().Alpha(1f).SetDuration(300).Start();
        _dismissed?.Invoke();
    }

    #endregion

    #region Build
    public virtual Google.Android.Material.Snackbar.Snackbar Build()
    {
        
        var snackbar = Google.Android.Material.Snackbar.Snackbar.Make(
            Activity,
            Activity.Window.DecorView.RootView,
            "",
            -2
        );

        if (snackbar.View.LayoutParameters is FrameLayout.LayoutParams layoutParams)
        {
            layoutParams.SetMargins(ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16*4), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16*4));

            if (Config.Type == MaterialFloatingButtonType.Large)
            {
                layoutParams.Height = 96*2;
                layoutParams.Width = 96*2;
                Config.IconSize = 36;
                Config.CornerRadius = 28;
                snackbar.View.SetPadding(ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16));

            }
            else if (Config.Type == MaterialFloatingButtonType.Small)
            {
                layoutParams.Height = 40*2;
                layoutParams.Width = 40*2;
                Config.IconSize = 24;
                Config.CornerRadius = 12;
                snackbar.View.SetPadding(ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16));

            }
            else
            {
                layoutParams.Height = 56*2;
                layoutParams.Width = 56*2;
                Config.IconSize = 24;
                Config.CornerRadius = 12;
                snackbar.View.SetPadding(ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16), ExtensionsConverters.DpToPixels(16));
            }
            
            if (Config.Position == MaterialFloatingButtonPosition.BottomRight)
            {
                layoutParams.Gravity = GravityFlags.Right | GravityFlags.Bottom;
            }
            else if (Config.Position == MaterialFloatingButtonPosition.TopRight)
            {
                layoutParams.Gravity = GravityFlags.Right | GravityFlags.Top;
            }
            else if (Config.Position == MaterialFloatingButtonPosition.TopLeft)
            {
                layoutParams.Gravity = GravityFlags.Left | GravityFlags.Top;
            }
            else
            {
                layoutParams.Gravity = GravityFlags.Left | GravityFlags.Bottom;
            }

            snackbar.View.LayoutParameters = layoutParams;
        }
        
        if (Config.BackgroundColor is not null)
        {
            snackbar.View.Background = GetDialogBackground();
        }
        
        var view = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;

        if (Config.Icon is not null)
        {
            var button = new Button(Activity);
            var icon = GetIcon(Config.Icon, Config.IconColor);
            icon.ScaleTo(Config.IconSize);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetCompoundDrawables(null, null, icon, null);
            button.CompoundDrawablePadding = ExtensionsConverters.DpToPixels(16);
            button.Touch += (sender, args) =>
            {
                Config.Action?.Invoke();
            };
            view.AddView(button,0);
            view.GetChildAt(1).LayoutParameters.Width = ExtensionsConverters.DpToPixels(Config.IconSize);
        }

        view.GetChildAt(1).SetPadding(0, 0, 0,0);
        if (Config.Type == MaterialFloatingButtonType.Small)
        {
            view.GetChildAt(0).SetPadding(0,0,16,0);
        }
        else if (Config.Type == MaterialFloatingButtonType.FAB)
        {
            view.GetChildAt(0).SetPadding(0,0,0,0);
        }

        snackbar.AddCallback(this);
        snackbar.View.Alpha = 0f;
        
        return snackbar;
    }
    
    protected virtual Drawable GetDialogBackground()
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(Config.BackgroundColor.ToInt());
        float[] radii = new float[] { (float)Config.CornerRadius.TopLeft, (float)Config.CornerRadius.TopRight, (float)Config.CornerRadius.BottomRight, (float)Config.CornerRadius.BottomLeft };
        backgroundDrawable.SetCornerRadius((float)Config.CornerRadius.BottomLeft);
        return backgroundDrawable;
    }
    
    protected virtual Drawable GetIcon(string icon, Color color)
    {
        var imgId = MauiApplication.Current.GetDrawableId(icon);
        var img = MauiApplication.Current.GetDrawable(imgId);
        
        img.SetColorFilter(color.ToPlatform(), FilterMode.SrcIn);

        return img;
    }

    #endregion

}