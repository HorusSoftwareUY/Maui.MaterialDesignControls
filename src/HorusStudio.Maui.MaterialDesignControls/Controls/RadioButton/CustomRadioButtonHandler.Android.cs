using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomRadioButtonHandler
{
    public new static void MapStrokeColor(IRadioButtonHandler handler, IRadioButton radioButton)
    {
        if (radioButton is CustomRadioButton customRadioButton && customRadioButton.StrokeColor != null && handler.PlatformView != null && handler.PlatformView is Android.Widget.RadioButton androidRadioButton)
        {
            var strokeColor = customRadioButton.StrokeColor.ToPlatform();
            androidRadioButton.ButtonTintList = Android.Content.Res.ColorStateList.ValueOf(strokeColor);
        }
    }
}
