using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using HorusStudio.Maui.MaterialDesignControls.Extensions.Label;
using Microsoft.Maui.Platform;
using Activity = Android.App.Activity;
using Button = Android.Widget.Button;
using View = Android.Views.View;

namespace HorusStudio.Maui.MaterialDesignControls;

class MaterialSnackbarBuilder
{
    #region Constants
    
    private const int TextMaxLines = 20;
    private const int MinimumHeight = 60;
    
    #endregion Constants
    
    #region Attributes
    
    private readonly MaterialSnackbarConfig _config;
    private readonly Dialog _dialog;
    private Android.Widget.ImageButton? _leadingIconView;
    private Android.Widget.ImageButton? _trailingIconView;
    private TextView? _textView;
    private Button? _actionView;
    private Action? _onDismissed;
    private TaskCompletionSource? _showCompletionSource;
    
    #endregion Attributes

    public MaterialSnackbarBuilder(Activity activity, MaterialSnackbarConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(activity);

        _config = config;
        
        _onDismissed = config.OnDismissed;
        
        var root = new LinearLayout(activity)
        {
            Orientation = Orientation.Horizontal,
            LayoutParameters = new LinearLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical
            }
        };
        
        root.SetMinimumHeight(MinimumHeight.DpToPixels());
        
        var insets = root!.GetInsets();
        root.SetRoundedBackground(config.BackgroundColor, config.CornerRadius)
            .SetMargin(config.Margin, insets);
        
        if (config.LeadingIcon is not null)
        {
            _leadingIconView = activity.CreateImageButton(config.LeadingIcon.Source, config.LeadingIcon.Size,
                config.LeadingIcon.Color, new Thickness(0), config.LeadingIcon.Action);
            
            if (!string.IsNullOrEmpty(config.AutomationId))
                _leadingIconView.ContentDescription = $"{config.AutomationId}_LeadingIcon";
            
            root.AddView(_leadingIconView);
        }
        
        var messageContainer = new LinearLayout(activity)
        {
            Orientation = Orientation.Vertical,
            LayoutParameters = new LinearLayout.LayoutParams(
                0,
                ViewGroup.LayoutParams.WrapContent,
                1f)
            {
                Gravity = GravityFlags.CenterVertical
            }
        };
        _textView = ConfigureText(activity, config);
        messageContainer.AddView(_textView);
        root.AddView(messageContainer);
        
        if (config.Action is not null)
        {
            _actionView = ConfigureAction(activity, config, Dismiss);
            root.AddView(_actionView);
        }
        
        if (config.TrailingIcon is not null)
        {
            _trailingIconView = activity.CreateImageButton(config.TrailingIcon.Source, config.TrailingIcon.Size,
                config.TrailingIcon.Color, new Thickness(0), config.TrailingIcon.Action);
            
            if (!string.IsNullOrEmpty(config.AutomationId))
                _trailingIconView.ContentDescription = $"{config.AutomationId}_TrailingIcon";
            
            root.AddView(_trailingIconView);
        }
        
        _textView!.SetMargin(new Thickness(_leadingIconView is not null ? config.Spacing : 0,0,0,0));
        _actionView?.SetMargin(new Thickness(config.Spacing,0,_trailingIconView is not null ? config.Spacing : 0,0));
        
        if (config.Action is not null)
        {
            root.SetPadding(new Thickness(config.Padding.Left, 0, config.Padding.Right, 0));
            _textView?.SetPadding(new Thickness(0, config.Padding.Top, 0, config.Padding.Bottom));
        }
        else
        {
            root.SetPadding(config.Padding);
        }

        _dialog = new Dialog(activity);
        _dialog.SetContentView(root);
        
        _dialog.Window?.ClearFlags(WindowManagerFlags.DimBehind);
        
        _dialog.Window?.SetGravity(config.Position);

        _dialog.DismissEvent += (s, e) =>
        {
            _showCompletionSource?.SetResult();
            _showCompletionSource = null;
            _onDismissed?.Invoke();
        };
    }

    public void Show()
    {
        _dialog?.Show();
        SetAutomaticDismiss();
    }

    public Task ShowAsync()
    {
        if (_dialog == null) return Task.CompletedTask;
        
        _showCompletionSource = new TaskCompletionSource();
        _dialog.Show();
        SetAutomaticDismiss();
        return _showCompletionSource.Task;
    }
    
    private void SetAutomaticDismiss()
    {
        if (_config.Duration.TotalMilliseconds <= 0
            || Looper.MainLooper == null) 
            return;
        
        new Handler(Looper.MainLooper).PostDelayed(() =>
        {
            Dismiss();
        }, (long)_config.Duration.TotalMilliseconds);
    }
    
    public void Dismiss() => _dialog?.Dismiss();
    
    public void Dispose() => _dialog?.Dispose();
    
    private static TextView ConfigureText(Activity activity, MaterialSnackbarConfig config)
    {
        var textView = new TextView(activity)
        {
            Id = Android.Views.View.GenerateViewId(),
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical
            }
        };
        
        textView.SetText(config.Message, TextDecorations.None, config.CharacterSpacing);
        textView.SetTextColor(config.TextColor.ToPlatform());
        textView.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)config.FontSize);
        textView.SetLineBreakMode(config.LineBreakMode, TextMaxLines);
        textView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        textView.SetIncludeFontPadding(false);
        textView.SetFontAttributes(config.FontAttributes);

        if (!string.IsNullOrEmpty(config.AutomationId))
            textView.ContentDescription = $"{config.AutomationId}_Message";

        return textView;
    }
    
    private static Button ConfigureAction(Activity activity, MaterialSnackbarConfig config, Action dismiss)
    {
        var actionButton = new Button(activity)
        {
            Id = View.GenerateViewId(),
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical
            }
        };
        
        actionButton.SetText(config.Action.Text, config.Action.TextDecorations);
        actionButton.SetTextColor(config.Action.Color.ToPlatform());
        actionButton.SetBackgroundColor(Colors.Transparent.ToPlatform());
        actionButton.SetTypeface(actionButton.Typeface, TypefaceStyle.Bold);
        actionButton.SetTextSize(Android.Util.ComplexUnitType.Dip, (float)config.FontSize);
        actionButton.Ellipsize = TextUtils.TruncateAt.Middle;
        actionButton.SetPadding(0);
        actionButton.SetIncludeFontPadding(false);
        actionButton.SetAllCaps(false);
        actionButton.Click += (s, e) =>
        {
            config.Action.Action.Invoke();
            dismiss.Invoke();
        };
        
        if (!string.IsNullOrEmpty(config.AutomationId))
            actionButton.ContentDescription = $"{config.AutomationId}_Action";
        
        return actionButton;
    }
}
