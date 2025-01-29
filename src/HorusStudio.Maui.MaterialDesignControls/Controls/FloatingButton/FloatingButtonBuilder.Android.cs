using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Button = Android.Widget.Button;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonBuilder : Snackbar.Callback
{
    //#region Properties
    
    //Activity Activity { get; }
    //FloatingButtonConfig Config { get; }
    
    //#endregion
    
    #region Constructors

    //public FloatingButtonBuilder(Activity activity, FloatingButtonConfig config)
    //{
        //Activity = activity ?? throw new ArgumentNullException(nameof(activity));
        //Config = config ?? throw new ArgumentNullException(nameof(config));
    //}

    #endregion

    #region Overrides

    public override void OnShown(Snackbar control)
    {
        base.OnShown(control);
        control?.View?.Animate()?.Alpha(1f).SetDuration(300).Start();
    }

    public override void OnDismissed(Snackbar control, int e)
    {
        base.OnDismissed(control, e);
        control?.SetDuration(0);
        control?.View?.Animate()?.Alpha(1f).SetDuration(300).Start();
    }

    #endregion

    #region Build
    
    public Snackbar Build(Activity activity, FloatingButtonConfig config)
    {
        ArgumentNullException.ThrowIfNull(activity);
        ArgumentNullException.ThrowIfNull(config);

        var snackbar = Snackbar.Make(
            activity,
            activity.Window.DecorView.RootView,
            string.Empty,
            -2
        );

        if (snackbar.View.LayoutParameters is FrameLayout.LayoutParams layoutParams)
        {
            var defaultMargin = 16;
            var horizontalMargin = defaultMargin.DpToPixels();
            var verticalMargin = (defaultMargin*4).DpToPixels();
            layoutParams.SetMargins(horizontalMargin, verticalMargin, horizontalMargin, verticalMargin);

            var defaultPadding = 16;
            var padding = defaultPadding.DpToPixels();
            
            if (config.Type == MaterialFloatingButtonType.Large)
            {
                layoutParams.Height = 96*2;
                layoutParams.Width = 96*2;
                config.IconSize = 36;
                config.CornerRadius = 28;
                snackbar.View.SetPadding(padding, padding, padding, padding);

            }
            else if (config.Type == MaterialFloatingButtonType.Small)
            {
                layoutParams.Height = 40*2;
                layoutParams.Width = 40*2;
                config.IconSize = 24;
                config.CornerRadius = 12;
                snackbar.View.SetPadding(padding, padding, padding, padding);
            }
            else
            {
                layoutParams.Height = 56*2;
                layoutParams.Width = 56*2;
                config.IconSize = 24;
                config.CornerRadius = 12;
                snackbar.View.SetPadding(padding, padding, padding, padding);
            }
            
            if (config.Position == MaterialFloatingButtonPosition.BottomRight)
            {
                layoutParams.Gravity = GravityFlags.Right | GravityFlags.Bottom;
            }
            else if (config.Position == MaterialFloatingButtonPosition.TopRight)
            {
                layoutParams.Gravity = GravityFlags.Right | GravityFlags.Top;
            }
            else if (config.Position == MaterialFloatingButtonPosition.TopLeft)
            {
                layoutParams.Gravity = GravityFlags.Left | GravityFlags.Top;
            }
            else
            {
                layoutParams.Gravity = GravityFlags.Left | GravityFlags.Bottom;
            }

            snackbar.View.LayoutParameters = layoutParams;
        }
        
        snackbar.View.Background = GetDialogBackground(config);
        
        var view = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;

        if (!string.IsNullOrEmpty(config.Icon))
        {
            var iconSize = Convert.ToInt32(config.IconSize);
            
            var button = new Button(activity);
            var icon = GetIcon(config);
            icon.ScaleTo(iconSize);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetCompoundDrawables(null, null, icon, null);
            button.CompoundDrawablePadding = 16.DpToPixels();
            button.Touch += (sender, args) =>
            {
                config.Action?.Invoke();
            };
            view.AddView(button,0);
            view.GetChildAt(1).LayoutParameters.Width = iconSize.DpToPixels();
        }

        view.GetChildAt(1).SetPadding(0, 0, 0,0);
        if (config.Type == MaterialFloatingButtonType.Small)
        {
            view.GetChildAt(0).SetPadding(0,0,16,0);
        }
        else if (config.Type == MaterialFloatingButtonType.FAB)
        {
            view.GetChildAt(0).SetPadding(0,0,0,0);
        }

        snackbar.AddCallback(this);
        snackbar.View.Alpha = 0f;
        
        return snackbar;
    }
    
    protected Drawable GetDialogBackground(FloatingButtonConfig config)
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(config.BackgroundColor.ToInt());
        float[] radii = new float[] { (float)config.CornerRadius.TopLeft, (float)config.CornerRadius.TopRight, (float)config.CornerRadius.BottomRight, (float)config.CornerRadius.BottomLeft };
        backgroundDrawable.SetCornerRadius((float)config.CornerRadius.BottomLeft);
        return backgroundDrawable;
    }
    
    protected Drawable GetIcon(FloatingButtonConfig config)
    {
        var imgId = MauiApplication.Current.GetDrawableId(config.Icon);
        var img = MauiApplication.Current.GetDrawable(imgId);
        
        img.SetColorFilter(config.IconColor.ToPlatform(), FilterMode.SrcIn);

        return img;
    }

    #endregion
}