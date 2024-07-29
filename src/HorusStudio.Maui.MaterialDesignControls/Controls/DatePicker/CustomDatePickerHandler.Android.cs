using Android.App;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using HorusStudio.Maui.MaterialDesignControls.Utils;
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

    //protected override MauiDatePicker CreatePlatformView()
    //{
    //    var mauiDatePicker = new MauiDatePicker(Context)
    //    {
    //        Focusable = false,
    //        Clickable = true
    //    };
    //    UpdatePlaceholder(mauiDatePicker);

    //    mauiDatePicker.Click += (sender, args) =>
    //    {
    //        ShowDatePicker();
    //    };

    //    mauiDatePicker.FocusChange += (sender, args) => {
    //        if (VirtualView is CustomDatePicker customDatePicker)
    //        {
    //            customDatePicker.RaiseFocusChanged(new FocusEventArgs(customDatePicker, args.HasFocus));
    //        }
    //    };

    //    return mauiDatePicker;
    //}

    //private void ShowDatePicker()
    //{
    //    if (VirtualView is CustomDatePicker customDatePicker)
    //    {
    //        var today = DateTime.Today;
    //        var datePickerDialog = new DatePickerDialog(Context,
    //            (s, e) =>
    //            {
    //                customDatePicker.CustomDate = e.Date;
    //                PlatformView.Text = e.Date.ToString(customDatePicker.Format);
    //            },
    //            today.Year , today.Month - 1, today.Day);
            
    //        datePickerDialog.Show();
    //    }
    //}

    private void UpdatePlaceholder(MauiDatePicker datePicker)
    {
        if (VirtualView is CustomDatePicker customDatePicker)
        {
            if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
            {
                //PlatformView.Text = null;
                //PlatformView.Hint = customDatePicker.Placeholder;
                //PlatformView.SetHintTextColor(customDatePicker.PlaceholderColor.ToPlatform());

                datePicker.Text = null;
                datePicker.Hint = customDatePicker.Placeholder;
                datePicker.SetHintTextColor(customDatePicker.PlaceholderColor.ToPlatform());
            }
        }
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customPicker)
        {
            handler.PlatformView.Gravity = TextAlignmentHelper.ConvertToGravityFlags(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapPlaceholder(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customDatePicker && handler.PlatformView is MauiDatePicker datePicker)
        {
            if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
            {
                handler.PlatformView.Text = null;
                handler.PlatformView.Hint = customDatePicker.Placeholder;
                handler.PlatformView.SetHintTextColor(customDatePicker.PlaceholderColor.ToPlatform());
            }
        }
    }
}

