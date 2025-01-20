using Android.App;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomTimePickerHandler
{
    public static void MapBorder(ITimePickerHandler handler, ITimePicker timePicker)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    public static void MapHorizontalTextAlignment(ITimePickerHandler handler, ITimePicker timePicker)
    {
        if (timePicker is CustomTimePicker customPicker)
        {
            handler.PlatformView.Gravity = customPicker.HorizontalTextAlignment.ToGravityFlags();
        }
    }

    public static void MapIsFocused(ITimePickerHandler handler, ITimePicker timePicker)
    {
        if (handler.PlatformView.IsFocused == timePicker.IsFocused) return;

        if (timePicker.IsFocused)
        {
            handler.PlatformView.RequestFocus();
        }
        else
        {
            handler.PlatformView.ClearFocus();
        }

    }

    private TimePickerDialog? _dialog;

    protected override TimePickerDialog CreateTimePickerDialog(int hour, int minute)
    {
        _dialog = base.CreateTimePickerDialog(hour, minute);
        _dialog.ShowEvent += OnDialogShown;
        _dialog.DismissEvent += OnDialogDismissed;
        return _dialog;
    }

    protected override void DisconnectHandler(MauiTimePicker platformView)
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
