using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

using Google.Android.Material.Snackbar;

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

        if (Config.Action is not null)
        {
            if (e == Google.Android.Material.Snackbar.Snackbar.Callback.DismissEventTimeout)
            {
                Config.Action(SnackbarActionType.Timeout);
            }
            else if (e == Google.Android.Material.Snackbar.Snackbar.Callback.DismissEventConsecutive)
            {
                Config.Action(SnackbarActionType.Cancelled);
            }
        }
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

        if (Config.MessageColor is not null)
        {
            snackbar.SetTextColor(Config.MessageColor.ToInt());
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

    protected virtual void SetupSnackbarText(Google.Android.Material.Snackbar.Snackbar snackbar)
    {
        var l = (snackbar.View as Google.Android.Material.Snackbar.Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var text = l.GetChildAt(0) as TextView;
        text.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)Config.MessageFontSize);

        if (Config.MessageFontFamily is not null)
        {
            var typeface = Typeface.CreateFromAsset(Activity.Assets, Config.MessageFontFamily);
            text.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        if (Config.LeadingIcon is null) return;

        var icon = GetIcon(Config.LeadingIcon);
        icon.ScaleTo(Config.IconSize);
        text.SetCompoundDrawables(icon, null, null, null);
        text.CompoundDrawablePadding = Extensions.DpToPixels(IconPadding);
    }

    protected virtual void SetupSnackbarAction(Google.Android.Material.Snackbar.Snackbar snackbar)
    {
        var text = new SpannableString(Config.ActionText);
        text.SetSpan(new LetterSpacingSpan(0), 0, Config.ActionText.Length, SpanTypes.ExclusiveExclusive);

        if (Config.NegativeButtonTextColor is not null)
        {
            snackbar.SetActionTextColor(Config.NegativeButtonTextColor.ToInt());
        }
        
        snackbar.SetAction(text, v =>
        {
            Config.Action?.Invoke(SnackbarActionType.UserInteraction);
        });

        var l = (snackbar.View as Google.Android.Material.Snackbar.Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var button = l.GetChildAt(1) as Android.Widget.Button;
        button.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)Config.NegativeButtonFontSize);

        if (Config.NegativeButtonFontFamily is not null)
        {
            var typeface = Typeface.CreateFromAsset(Activity.Assets, Config.NegativeButtonFontFamily);
            button.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        if (Config.TrailingIcon is null) return;

        var icon = GetIcon(Config.TrailingIcon);
        icon.ScaleTo(ActionIconSize);
        button.SetCompoundDrawables(icon, null, null, null);
        button.CompoundDrawablePadding = Extensions.DpToPixels(ActionIconPadding);
    }

    protected virtual Drawable GetIcon(string icon)
    {
        var imgId = MauiApplication.Current.GetDrawableId(icon);
        var img = MauiApplication.Current.GetDrawable(imgId);

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