using Android.App;
using HorusStudio.Maui.MaterialDesignControls.Helpers;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomDatePickerHandler
{
    public static void MapBorder(IDatePickerHandler handler, IDatePicker picker)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customPicker)
        {
            handler.PlatformView.Gravity = TextAlignmentHelper.ConvertToGravityFlags(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapIsFocused(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler.PlatformView.IsFocused == datePicker.IsFocused) return;

        if (datePicker.IsFocused)
        {
            handler.PlatformView.RequestFocus();
        }
        else
        {
            handler.PlatformView.ClearFocus();
        }
    }

    private DatePickerDialog? _dialog;

    protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
    {
        _dialog = base.CreateDatePickerDialog(year, month, day);
        return _dialog;
    }

    protected override void ConnectHandler(MauiDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        if (_dialog != null)
        {
            _dialog.ShowEvent += OnDialogShown;
            _dialog.DismissEvent += OnDialogDismissed;
        }
    }

    protected override void DisconnectHandler(MauiDatePicker platformView)
    {
        if (_dialog != null)
        {
            _dialog.ShowEvent -= OnDialogShown;
            _dialog.DismissEvent -= OnDialogDismissed;
        }
        base.DisconnectHandler(platformView);

        _dialog = null;
    }

    private void OnDialogShown(object sender, EventArgs e)
    {
        this.VirtualView.IsFocused = true;
    }

    private void OnDialogDismissed(object sender, EventArgs e)
    {
        this.VirtualView.IsFocused = false;
    }
}

