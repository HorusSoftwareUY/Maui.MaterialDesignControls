using Microsoft.Maui.Platform;

#if IOS
using UIKit;
#endif

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Handlers
{
	public class CustomMaterialTextFieldHandler : MaterialTextFieldHandler
    {
#if IOS
        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);

            var toolbar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                Translucent = true,
            };
            toolbar.SizeToFit();

            var doneButton = new UIBarButtonItem("Close", UIBarButtonItemStyle.Done, (sender, e) =>
            {
                platformView.ResignFirstResponder();
            });

            var flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            toolbar.SetItems(new[] { flexibleSpace, doneButton }, false);

            platformView.InputAccessoryView = toolbar;
        }
#endif
    }
}