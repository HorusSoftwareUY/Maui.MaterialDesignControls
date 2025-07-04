using Microsoft.Maui.Handlers;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialPickerHandler
{
    public static void MapBorder(IPickerHandler handler, IPicker picker)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
    }

    public new static void MapHorizontalTextAlignment(IPickerHandler handler, IPicker picker)
    {
        if (picker is CustomPicker customPicker
            && handler != null
            && handler.PlatformView != null)
        {
            handler.PlatformView.TextAlignment = customPicker.HorizontalTextAlignment.ToUIKit();
        }
    }
    
    public void ClearText()
    {
        MainThreadExtensions.SafeRunOnUiThread(() => 
        { 
            if (VirtualView.Handler is PickerHandler handler 
                && handler.PlatformView is UITextField textField)
            {
                textField.Text = string.Empty;
            }
        });
    }
}
