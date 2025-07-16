using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

internal class BottomSheetContainerView : UIView
{
    private readonly MaterialBottomSheet _sheet;
    private readonly UIView _view;
    private UIWindow _window;

    // Can't get the sheet max height with large and medium detents
    // custom detents are not supported on iOS 15
    // can't use largestUndimmedIdentifier or selected detent with custom detents on iOS 16
    // So I guess we'll just have to calculate the sheet height ourselves then
    // This number was found by getting the full screen height, subtracting the sheet's UIView height and the top inset
    // This seems to be the spacing iOS leaves at the top of the screen when a sheet is fullscreen
    // TODO: Check if this is the same number for fullscreen sheets opened on top of another sheet
    const int SheetTopSpacing = 10;

    double CalculateTallestDetent(double heightConstraint)
    {
        var topPadding = _window.SafeAreaInsets.Top;
        var maximumDetentValue = heightConstraint - topPadding - SheetTopSpacing;
        return _sheet.GetEnabledDetents().Select(d => d.GetHeight(_sheet, maximumDetentValue)).Max();
    }

    internal BottomSheetContainerView(MaterialBottomSheet sheet, UIView view, UIWindow window)
    {
        _sheet = sheet;
        _view = view;
        _window = window;

        AddSubview(_view);
    }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
        if (_sheet?.Window != null)
        {
            var h = CalculateTallestDetent(_sheet.Window.Height - BottomSheetManager.KeyboardHeight);
            _view.Frame = new CGRect(0, 0, Bounds.Width, h);
            _sheet.Arrange(_view.Frame.ToRectangle());
            _sheet.Controller.Layout();
        }
    }
}