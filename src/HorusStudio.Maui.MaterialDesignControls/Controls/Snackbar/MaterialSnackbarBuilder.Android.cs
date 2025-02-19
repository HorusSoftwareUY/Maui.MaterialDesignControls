using Android.App;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Platform;
using Button = Android.Widget.Button;
using Color = Microsoft.Maui.Graphics.Color;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialSnackbarBuilder : Snackbar.Callback
{
    #region Constants
    
    private const int TextMaxLines = 20;
    
    #endregion Constants
    
    #region Attributes
    
    private readonly Snackbar? _snackbar;
    private Android.Widget.ImageButton? _leadingIconView;
    private Android.Widget.ImageButton? _trailingIconView;
    private TextView? _textView;
    private Button? _actionView;
    private Action? _onDismissed;
    private TaskCompletionSource? _showCompletionSource;
    
    #endregion Attributes
    
    public MaterialSnackbarBuilder(Activity activity, MaterialSnackbarConfig config)
    {
        _onDismissed = config.OnDismissed;

        var extraPadding = 12;
        config.Padding = new Thickness(config.Padding.Left, config.Padding.Top - extraPadding, config.Padding.Right, config.Padding.Bottom - extraPadding);
        
        _snackbar = Build(config, activity);
        _snackbar.AddCallback(this);
    }

    #region Snackbar.Callback

    public override void OnDismissed(Snackbar? control, int e)
    {
        base.OnDismissed(control, e);
        
        if (_showCompletionSource != null)
        {
            _showCompletionSource.SetResult();
            _showCompletionSource = null;
            return;
        };
        _onDismissed?.Invoke();
    }

    #endregion
    
    public void Show() => _snackbar?.Show();

    public Task ShowAsync()
    {
        if (_snackbar == null) return Task.CompletedTask;
        
        _showCompletionSource = new TaskCompletionSource();
        _snackbar.Show();
        return _showCompletionSource.Task;
    }
    
    public void Dismiss() => _snackbar?.Dismiss();
    
    private Snackbar Build(MaterialSnackbarConfig config, Activity? activity)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(activity);
        
        var rootView = activity.Window!.DecorView.RootView;
        var snackbar = Snackbar.Make(
            activity,
            rootView!,
            config.Message,
            Convert.ToInt32(config.Duration.TotalMilliseconds)
        );
        
        if (snackbar.View is Snackbar.SnackbarLayout snackbarView &&
            snackbarView.GetChildAt(0) is SnackbarContentLayout snackbarContent)
        {
            var insets = rootView!.GetInsets();
            snackbarView
                .SetRoundedBackground(config.BackgroundColor, config.CornerRadius)
                .SetMargin(config.Margin, insets)
                .SetPadding(config.Padding)
                .SetGravity(config.Position);

            _textView = ConfigureText(snackbar, snackbarContent, config.FontSize, config.TextColor);
        
            if (config.Action is not null)
            {
                _actionView = ConfigureAction(activity, snackbar, snackbarContent, config.Action);
            }
            if (config.LeadingIcon is not null)
            {
                _leadingIconView = snackbarContent.AddIcon(activity, config.LeadingIcon, 0);
            }
            if (config.TrailingIcon is not null)
            {
                _trailingIconView = snackbarContent.AddIcon(activity, config.TrailingIcon, snackbarContent.ChildCount);
            }

            _textView!.SetMargin(new Thickness(_leadingIconView is not null ? config.Spacing : 0,0,0,0));
            _actionView?.SetMargin(new Thickness(config.Spacing,0,_trailingIconView is not null ? config.Spacing : 0,0));

            snackbarView.SetVisibility(false);
        }
       
        snackbar.SetTextMaxLines(TextMaxLines);
        return snackbar;
    }

    private static TextView? ConfigureText(Snackbar snackbar, SnackbarContentLayout contentLayout, double fontSize, Color textColor)
    {
        snackbar.SetTextColor(textColor.ToInt());
        if (contentLayout.MessageView is not {} textView) return null;
        
        textView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        //textView.SetTypeface(actionButton.Typeface, TypefaceStyle.Normal);
        textView.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)fontSize);
        textView.Ellipsize = TextUtils.TruncateAt.End;
        textView.SetIncludeFontPadding(false);
        
        return textView;
    }

    private static Button? ConfigureAction(Activity activity, Snackbar snackbar, SnackbarContentLayout contentLayout, MaterialSnackbarConfig.ActionConfig config)
    {
        var text = new SpannableString(config.Text);
        text.SetSpan(new LetterSpacingSpan(0), 0, config.Text.Length, SpanTypes.ExclusiveExclusive);

        snackbar.SetActionTextColor(config.Color.ToInt());
        snackbar.SetAction(text, v => config.Action.Invoke());

        if (contentLayout.ActionView is not {} actionButton) return null;
        
        actionButton.SetBackgroundColor(Colors.Transparent.ToPlatform());
        actionButton.SetTypeface(actionButton.Typeface, TypefaceStyle.Bold);
        //var mediumTypeface = Typeface.CreateFromAsset(activity.Assets, MaterialFontFamily.Medium);
        actionButton.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)config.FontSize);
        actionButton.Ellipsize = TextUtils.TruncateAt.Middle;
        actionButton.SetPadding(0);
        actionButton.SetIncludeFontPadding(false);
        
        return actionButton;
    }
}

class LetterSpacingSpan(float letterSpacing) : MetricAffectingSpan
{
    public float LetterSpacing => letterSpacing;

    public override void UpdateDrawState(TextPaint? ds) => Apply(ds);

    public override void UpdateMeasureState(TextPaint paint) => Apply(paint);

    private void Apply(TextPaint? paint)
    {
        if (paint is null) return;
        paint.LetterSpacing = LetterSpacing;
    }
}
