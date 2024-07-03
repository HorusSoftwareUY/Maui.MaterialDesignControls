﻿namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialTextField : MaterialInputBase
{
    //TODO: error on goback and open again the view.

    private BorderlessEntry _entry;

    public MaterialTextField()
    {
        _entry = new BorderlessEntry
        {
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        _entry.SetBinding(BorderlessEntry.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _entry.SetBinding(BorderlessEntry.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _entry.SetBinding(BorderlessEntry.TextProperty, new Binding(nameof(Text), source: this));
        _entry.SetBinding(BorderlessEntry.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _entry.SetBinding(BorderlessEntry.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _entry.SetBinding(BorderlessEntry.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));

        InputTapCommand = new Command(() => _entry.Focus());

#if ANDROID
        _entry.ReturnCommand = new Command(() =>
        {
            var view = _entry.Handler.PlatformView as Android.Views.View;
            view?.ClearFocus();
        });
#endif

        Content = _entry;
    }

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if(_entry != null)
        {
            if (type == MaterialInputType.Filled)
            {
                _entry.VerticalOptions = LayoutOptions.FillAndExpand;
                _entry.Margin = DeviceInfo.Platform == DevicePlatform.Android ?
                    new Thickness(0, 0, 0, -10) :
                    new Thickness(0, 0, 0, -1);
            }
            else if (type == MaterialInputType.Outlined)
            {
                _entry.VerticalOptions = LayoutOptions.Center;
                _entry.Margin = DeviceInfo.Platform == DevicePlatform.Android ?
                    new Thickness(0, -7.5) :
                    new Thickness(0);
            }
        }
    }

    protected override void SetControlIsEnabled()
    {
        if (_entry != null)
            _entry.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _entry.Focused += ContentFocusChanged;
        _entry.Unfocused += ContentFocusChanged;

        SetControlTemplate(Type);
    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _entry.Focused -= ContentFocusChanged;
        _entry.Unfocused -= ContentFocusChanged;
    }

    private void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        IsFocused = e.IsFocused;
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterStatusChanged(Type);
        
        if(CanExecuteFocusedCommand())
        {
            FocusedCommand.Execute(null);
        }
        else if(CanExecuteUnfocusedCommand())
        {
            UnfocusedCommand?.Execute(null);
        }
    }

    private bool CanExecuteFocusedCommand()
    {
        return IsFocused && (FocusedCommand?.CanExecute(null) ?? false);
    }

    private bool CanExecuteUnfocusedCommand()
    {
        return !IsFocused && (UnfocusedCommand?.CanExecute(null) ?? false);
    }

    internal static IEnumerable<Style> GetStyles()
    {
        var style = new Style(typeof(MaterialTextField)) { ApplyToDerivedTypes = true };
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, MaterialInputBase.GetBaseStyles());
        return new List<Style> { style };
    }
}