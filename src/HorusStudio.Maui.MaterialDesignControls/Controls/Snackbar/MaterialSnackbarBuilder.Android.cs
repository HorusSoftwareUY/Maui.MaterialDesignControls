using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Google.Android.Material.Resources;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Platform;
using Button = Android.Widget.Button;
using Color = Microsoft.Maui.Graphics.Color;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialSnackbarBuilder : Snackbar.Callback
{
    #region Constants
    
    private const int TextMaxLines = 20;
    private const int ActionInternalPadding = 8;
    
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
    
    public MaterialSnackbarBuilder(Activity activity, SnackbarConfig config)
    {
        _onDismissed = config.OnDismissed;
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
    
    private Snackbar Build(SnackbarConfig config, Activity? activity)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(activity);
        
        var rootView = activity.Window!.DecorView.RootView;
        var snackbar = Snackbar.Make(
            activity,
            rootView!,
            config.Message,
            (int)config.Duration.TotalMilliseconds
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
                _leadingIconView = AddIcon(activity, config.LeadingIcon, snackbarContent, 0);
            }
            if (config.TrailingIcon is not null)
            {
                _trailingIconView = AddIcon(activity, config.TrailingIcon, snackbarContent, snackbarContent.ChildCount);
            }

            _textView!.SetMargin(new Thickness(_leadingIconView is not null ? config.Spacing : 0,0,0,0));
            if (_actionView is not null)
            {
                
                _actionView.SetMargin(new Thickness(config.Spacing - ActionInternalPadding,0,_trailingIconView is not null ? config.Spacing - ActionInternalPadding : 0,0));
            }
            
            snackbarView.SetVisibility(false);
        }
       
        snackbar.SetTextMaxLines(TextMaxLines);
        return snackbar;
    }

    private static Android.Widget.ImageButton? AddIcon(Activity activity, SnackbarConfig.IconConfig config, SnackbarContentLayout contentLayout, int index)
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
    
    private static Android.Widget.ImageButton? CreateImageButton(Activity activity, ImageSource source, int size, Color color, Thickness padding, Action? action)
    {
        var icon = source.ToDrawable(size, color);
        if (icon is null) return null;
        
        var button = new Android.Widget.ImageButton(activity);
        button.SetImageDrawable(icon);
        button.SetPadding(padding);
        button.SetBackgroundColor(Colors.Transparent.ToPlatform());
        if (action is not null)
        {
            button.Click += (sender, args) => action();    
        }
        
        return button;
    }
    
    private static TextView? ConfigureText(Snackbar snackbar, SnackbarContentLayout contentLayout, double fontSize, Color textColor)
    {
        snackbar.SetTextColor(textColor.ToInt());
        if (contentLayout.GetChildAt(0) is not TextView textView) return null;

        textView.SetBackgroundColor(Colors.Transparent.ToPlatform());
        //textView.SetTypeface(actionButton.Typeface, TypefaceStyle.Normal);
        textView.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)fontSize);
        textView.Ellipsize = TextUtils.TruncateAt.End;
        textView.SetPadding(0);
        textView.SetMargin(0);
        
        return textView;
    }

    private static Button? ConfigureAction(Activity activity, Snackbar snackbar, SnackbarContentLayout contentLayout, SnackbarConfig.ActionConfig config)
    {
        var text = new SpannableString(config.Text);
        text.SetSpan(new LetterSpacingSpan(0), 0, config.Text.Length, SpanTypes.ExclusiveExclusive);

        snackbar.SetActionTextColor(config.Color.ToInt());
        snackbar.SetAction(text, v => config.Action.Invoke());

        if (contentLayout.GetChildAt(1) is not Button actionButton) return null;
        
        actionButton.SetBackgroundColor(Colors.Transparent.ToPlatform());
        actionButton.SetTypeface(actionButton.Typeface, TypefaceStyle.Bold);
        //var mediumTypeface = Typeface.CreateFromAsset(activity.Assets, MaterialFontFamily.Medium);
        actionButton.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)config.FontSize);
        actionButton.Ellipsize = TextUtils.TruncateAt.Middle;
        
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
