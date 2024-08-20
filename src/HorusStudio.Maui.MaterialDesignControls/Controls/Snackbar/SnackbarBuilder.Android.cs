using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using ImageButton = Android.Widget.ImageButton;
using Google.Android.Material.Snackbar;
using LayoutDirection = Android.Views.LayoutDirection;
using View = Android.Views.View;

namespace HorusStudio.Maui.MaterialDesignControls;

public class SnackbarBuilder : Google.Android.Material.Snackbar.Snackbar.Callback
{
    public static Thickness DefaultScreenMargin { get; set; } = new Thickness(20, 50);
    public static double DefaultIconPadding { get; set; } = 10;
    public static double DefaultActionIconPadding { get; set; } = 10;
    public static double DefaultActionIconSize { get; set; } = 22;
    public static long DefaultFadeInFadeOutAnimationDuration { get; set; } = 300;

    public Thickness ScreenMargin { get; set; } = DefaultScreenMargin;
    public double IconPadding { get; set; } = DefaultIconPadding;
    public double ActionIconSize { get; set; } = DefaultActionIconSize;
    public double ActionIconPadding { get; set; } = DefaultActionIconPadding;
    public long FadeInFadeOutAnimationDuration { get; set; } = DefaultFadeInFadeOutAnimationDuration;
    
    private Action _dismissed;
    
    protected Activity Activity { get; }
    protected SnackbarConfig Config { get; }
    
    public SnackbarBuilder(Activity activity, SnackbarConfig config)
    {
        Activity = activity;
        Config = config;
    }
    
    public override void OnShown(Google.Android.Material.Snackbar.Snackbar snackbar)
    {
        base.OnShown(snackbar);

        var timer = new System.Timers.Timer
        {
            Interval = Config.Duration.TotalMilliseconds,
            AutoReset = false
        };
        timer.Elapsed += (s, a) =>
        {
            Activity.RunOnUiThread(() =>
            {
                snackbar.View.Animate().Alpha(0f).SetDuration(FadeInFadeOutAnimationDuration).Start();
            });
        };
        timer.Start();

        _dismissed = () =>
        {
            try
            {
                timer.Stop();
            }
            catch { }
        };

        snackbar.View.Animate().Alpha(1f).SetDuration(FadeInFadeOutAnimationDuration).Start();
    }

    public override void OnDismissed(Google.Android.Material.Snackbar.Snackbar snackbar, int e)
    {
        base.OnDismissed(snackbar, e);

        _dismissed?.Invoke();
    }

    public virtual Google.Android.Material.Snackbar.Snackbar Build()
    {
        var snackbar = Google.Android.Material.Snackbar.Snackbar.Make(
            Activity,
            Activity.Window.DecorView.RootView,
            Config.Message,
            (int)Config.Duration.TotalMilliseconds
        );

        SetupSnackbarText(snackbar);

        if (Config.TextColor is not null)
        {
            snackbar.SetTextColor(Config.TextColor.ToInt());
        }

        if (Config.Action is not null)
        {
            SetupSnackbarAction(snackbar);
        }

        if (Config.BackgroundColor is not null)
        {
            snackbar.View.Background = GetDialogBackground();
        }

        if (snackbar.View.LayoutParameters is FrameLayout.LayoutParams layoutParams)
        {
            layoutParams.SetMargins(Extensions.DpToPixels(ScreenMargin.Left), Extensions.DpToPixels(ScreenMargin.Top), Extensions.DpToPixels(ScreenMargin.Right), Extensions.DpToPixels(ScreenMargin.Bottom));
            layoutParams.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Bottom;

            if (Config.Position == SnackbarPosition.Top)
            {
                layoutParams.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Top;
            }

            snackbar.View.LayoutParameters = layoutParams;
        }
        
        var view = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var text = view.GetChildAt(0) as TextView;
        var buttonSnackbar = view.GetChildAt(1) as Android.Widget.Button;

        if (Config.LeadingIcon is not null)
        {
            var button = new ImageButton(Activity);
            var icon = GetIcon(Config.LeadingIcon, Config.IconTintColor);
            icon.ScaleTo(Config.IconSize);
            button.SetImageDrawable(icon);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetMaxHeight(Config.IconSize);
            button.SetMaxWidth(Config.IconSize);
            button.Touch += (sender, args) =>
            {
                Config.ActionLeading?.Invoke();
            };
            view.AddView(button,0);
        }

        if (Config.TrailingIcon is not null)
        {
            var button = new ImageButton(Activity);
            var icon = GetIcon(Config.TrailingIcon, Config.IconTintColor);
            icon.ScaleTo(ActionIconSize);
            button.SetImageDrawable(icon);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetMaxHeight(Config.IconSize);
            button.SetMaxWidth(Config.IconSize);
            button.Touch += (sender, args) =>
            {
                Config.ActionTrailing?.Invoke();
            };
            view.AddView(button,3);
        }

        snackbar.AddCallback(this);

        snackbar.View.Alpha = 0f;

        return snackbar;
    }

    protected virtual Drawable GetDialogBackground()
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(Config.BackgroundColor.ToInt());
        backgroundDrawable.SetCornerRadius(Extensions.DpToPixels(Config.CornerRadius));

        return backgroundDrawable;
    }

    protected virtual void SetupSnackbarText(Snackbar snackbar)
    {
        var l = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var text = l.GetChildAt(0) as TextView;
        var buttonSnackbar = l.GetChildAt(1) as Android.Widget.Button;
        text.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)Config.TextFontSize);

        text.SetCompoundDrawables(null, null, null, null);
        text.CompoundDrawablePadding = Extensions.DpToPixels(IconPadding);
    }

    protected virtual void SetupSnackbarAction(Snackbar snackbar)
    {
        var text = new SpannableString(Config.ActionText);
        text.SetSpan(new LetterSpacingSpan(0), 0, Config.ActionText.Length, SpanTypes.ExclusiveExclusive);

        if (Config.ActionTextColor is not null)
        {
            snackbar.SetActionTextColor(Config.ActionTextColor.ToInt());
        }
        
        snackbar.SetAction(text, v =>
        {
            Config.Action?.Invoke();
        });

        var l = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var button = l.GetChildAt(1) as Android.Widget.Button;
        button.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)Config.ActionFontSize);
        
        button.SetCompoundDrawables(null, null, null, null);
        button.CompoundDrawablePadding = Extensions.DpToPixels(ActionIconPadding);
    }

    protected virtual Drawable GetIcon(string icon, Color color)
    {
        var imgId = MauiApplication.Current.GetDrawableId(icon);
        var img = MauiApplication.Current.GetDrawable(imgId);
        
        img.SetColorFilter(color.ToPlatform(), FilterMode.SrcIn);

        return img;
    }
}

public class LetterSpacingSpan : MetricAffectingSpan
{
    private float _letterSpacing;

    public LetterSpacingSpan(float letterSpacing)
    {
        _letterSpacing = letterSpacing;
    }

    public float getLetterSpacing()
    {
        return _letterSpacing;
    }

    public override void UpdateDrawState(TextPaint ds)
    {
        Apply(ds);
    }

    public override void UpdateMeasureState(TextPaint paint)
    {
        Apply(paint);
    }

    private void Apply(TextPaint paint)
    {
        paint.LetterSpacing = _letterSpacing;
    }
}

public static class Extensions
{
    public static void ScaleTo(this Drawable drawable, double newSize)
    {
        double width = drawable.IntrinsicWidth;
        double height = drawable.IntrinsicHeight;

        var ratio = width / height;
        if (width < height)
        {
            drawable.SetBounds(0, 0, DpToPixels(newSize * ratio), DpToPixels(newSize));
        }
        else drawable.SetBounds(0, 0, DpToPixels(newSize), DpToPixels(newSize / ratio));
    }

    public static int DpToPixels(double number)
    {
        var density = Platform.CurrentActivity.Resources.DisplayMetrics.Density;

        return (int)(density * number);
    }
    
    public static void SafeRunOnUi(this Activity activity, Action action) => activity.RunOnUiThread(() =>
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    });
}