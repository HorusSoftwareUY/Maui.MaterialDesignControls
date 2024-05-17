using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomRadioButtonHandler
{
    public new static void MapStrokeColor(IRadioButtonHandler handler, IRadioButton radioButton)
    {
        if (radioButton is CustomRadioButton customRadioButton && customRadioButton.StrokeColor != null)
        {
            var strokeColor = customRadioButton.StrokeColor.ToAndroid();
           
            if (handler.PlatformView != null && handler.PlatformView is Android.Widget.RadioButton androidRadioButton)
            {
                androidRadioButton.ButtonTintList = Android.Content.Res.ColorStateList.ValueOf(strokeColor);
            }
        }
    }
}
