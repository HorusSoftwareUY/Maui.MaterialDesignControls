using Android.App;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Google.Android.Material.Snackbar;
using Button = Android.Widget.Button;

namespace HorusStudio.Maui.MaterialDesignControls;

public class SnackbarBuilder : Snackbar.Callback
{
    //public static Thickness DefaultScreenMargin { get; set; } = new Thickness(20, 50);
    public static int DefaultIconPadding { get; set; } = 10;
    public static int DefaultActionIconPadding { get; set; } = 10;
    public static long DefaultFadeInFadeOutAnimationDuration { get; set; } = 300;

    //public Thickness ScreenMargin { get; set; } = DefaultScreenMargin;
    public int IconPadding { get; set; } = DefaultIconPadding;
    public int ActionIconPadding { get; set; } = DefaultActionIconPadding;
    public long FadeInFadeOutAnimationDuration { get; set; } = DefaultFadeInFadeOutAnimationDuration;
    
    private const int HorizontalMargin = 20;
    private const int VerticalMargin = 50;
    
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
            layoutParams.SetMargins(HorizontalMargin.DpToPixels(), VerticalMargin.DpToPixels(), HorizontalMargin.DpToPixels(), VerticalMargin.DpToPixels());

            layoutParams.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Bottom;

            if (Config.Position == SnackbarPosition.Top)
            {
                layoutParams.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Top;
            }

            snackbar.View.SetPadding(16.DpToPixels(), 10.DpToPixels(), 6.DpToPixels(), 5.DpToPixels());
            snackbar.View.LayoutParameters = layoutParams;
        }
        
        var view = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;

        if (Config.LeadingIcon is not null)
        {
            var button = new Button(Activity);
            var icon = GetIcon(Config.LeadingIcon, Config.LeadingIconTintColor);
            icon.ScaleTo(Config.IconSize);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetCompoundDrawables(null, null, icon, null);
            button.CompoundDrawablePadding = IconPadding.DpToPixels();
            button.Touch += (sender, args) =>
            {
                Config.ActionLeading?.Invoke();
            };
            view.AddView(button,0);
            view.GetChildAt(0).LayoutParameters.Width = Config.IconSize.DpToPixels();
        }

        if (Config.TrailingIcon is not null)
        {
            var button = new Button(Activity);
            var icon = GetIcon(Config.TrailingIcon, Config.TrailingIconTintColor);
            icon.ScaleTo(Config.IconSize);
            button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
            button.SetCompoundDrawables(icon, null, null, null);
            button.CompoundDrawablePadding = IconPadding.DpToPixels();
            button.Touch += (sender, args) =>
            {
                Config.ActionTrailing?.Invoke();
            };
            view.AddView(button,3);
            view.GetChildAt(3).LayoutParameters.Width = Config.IconSize.DpToPixels();
        }

        view.GetChildAt(1).SetPadding(view.GetChildAt(1).PaddingLeft, 0, view.GetChildAt(1).Right, view.GetChildAt(1).Bottom);

        snackbar.AddCallback(this);
        snackbar.View.Alpha = 0f;

        return snackbar;
    }

    protected virtual Drawable GetDialogBackground()
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(Config.BackgroundColor.ToInt());
        backgroundDrawable.SetCornerRadius(Config.CornerRadius.DpToPixels());

        return backgroundDrawable;
    }

    protected virtual void SetupSnackbarText(Snackbar snackbar)
    {
        var l = (snackbar.View as Snackbar.SnackbarLayout).GetChildAt(0) as SnackbarContentLayout;
        var text = l.GetChildAt(0) as TextView;
        text.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)Config.TextFontSize);
        text.Ellipsize = TextUtils.TruncateAt.End;
        text.SetCompoundDrawables(null, null, null, null);
        text.CompoundDrawablePadding = IconPadding.DpToPixels();
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
        button.SetTextSize(Android.Util.ComplexUnitType.Sp,
            (float)Config.ActionFontSize - ((Config.ActionFontSize > MaterialFontSize.LabelLarge) ? 6 : 0));
        button.Ellipsize = TextUtils.TruncateAt.Middle;
        button.SetCompoundDrawables(null, null, null, null);
        button.CompoundDrawablePadding = ActionIconPadding.DpToPixels();
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
