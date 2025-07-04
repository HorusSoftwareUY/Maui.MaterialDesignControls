using CoreGraphics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
internal class BottomSheetController : UIViewController
{
    IMauiContext _windowMauiContext;
    MaterialBottomSheet _sheet;
    NSObject? _keyboardDidHideObserver;

    public BottomSheetController(IMauiContext windowMauiContext, MaterialBottomSheet sheet)
    {
        _windowMauiContext = windowMauiContext;
        _sheet = sheet;
        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            SheetPresentationController.Delegate = new BottomSheetControllerDelegate(_sheet);
        }
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        var container = _sheet.ToPlatform(_windowMauiContext);

        var cv = new BottomSheetContainer(_sheet, container);

        View.AddSubview(cv);

        cv.TranslatesAutoresizingMaskIntoConstraints = false;

        NSLayoutConstraint.ActivateConstraints(new[]
        {
            cv.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            cv.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            cv.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
            cv.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor)
        });

        UpdateBackground();
        _sheet.NotifyShowing();

        if (_keyboardDidHideObserver is null)
        {
            _keyboardDidHideObserver = UIKeyboard.Notifications.ObserveDidHide(KeyboardDidHide);
        }
    }

    void KeyboardDidHide(object sender, UIKeyboardEventArgs e)
    {
        Layout();
    }

    public void Layout()
    {
        _sheet.CachedDetents.Clear();
        if (OperatingSystem.IsIOSVersionAtLeast(16))
        {
            SheetPresentationController.InvalidateDetents();
        }
    }
    internal void UpdateBackground()
    {
        if (_sheet?.BackgroundBrush != null)
        {
            Paint paint = _sheet.BackgroundBrush;
            View.BackgroundColor = paint.ToColor().ToPlatform();
        }
        else
        {
            if (OperatingSystem.IsIOSVersionAtLeast(13))
            {
                View.BackgroundColor = UIColor.SystemBackground;
            }
        }
    }
    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        Layout();
    }

    [SupportedOSPlatform("ios15.0")]
    internal static UISheetPresentationControllerDetentIdentifier GetIdentifierForDetent(Detent d)
    {
        if (d is FullscreenDetent)
        {
            return UISheetPresentationControllerDetentIdentifier.Large;
        }
        else if (d is RatioDetent ratioDetent && ratioDetent.Ratio == .5)
        {
            return UISheetPresentationControllerDetentIdentifier.Medium;
        }
        return UISheetPresentationControllerDetentIdentifier.Unknown;
    }

    [SupportedOSPlatform("ios15.0")]
    internal void UpdateSelectedIdentifierFromDetent()
    {
        if (_sheet.SelectedDetent is null)
        {
            return;
        }
        SheetPresentationController.AnimateChanges(() =>
        {
            SheetPresentationController.SelectedDetentIdentifier = GetIdentifierForDetent(_sheet.SelectedDetent);
        });
    }

    [SupportedOSPlatform("ios15.0")]
    internal Detent GetSelectedDetent()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15))
        {
            return null;
        }
        var detents = _sheet.GetEnabledDetents();
        return SheetPresentationController.SelectedDetentIdentifier switch
        {
            UISheetPresentationControllerDetentIdentifier.Medium => detents.FirstOrDefault(d => d is RatioDetent ratioDetent && ratioDetent.Ratio == .5f),
            UISheetPresentationControllerDetentIdentifier.Large => detents.FirstOrDefault(d => d is FullscreenDetent),
            UISheetPresentationControllerDetentIdentifier.Unknown or _ => null,
        };
    }

    [SupportedOSPlatform("ios15.0")]
    internal void UpdateSelectedDetent()
    {
        _sheet.SelectedDetent = GetSelectedDetent();
    }

    internal void UpdateCornerRadius(double cornerRadius)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15))
        {
            return;
        }
        SheetPresentationController.PreferredCornerRadius = (NFloat)cornerRadius;
    }
    
    #region Helper classes
    
    [SupportedOSPlatform("ios15.0")]
    private class BottomSheetControllerDelegate : UISheetPresentationControllerDelegate
    {
        MaterialBottomSheet _sheet;

        public BottomSheetControllerDelegate(MaterialBottomSheet sheet)
        {
            _sheet = sheet;
        }
        public override void DidDismiss(UIPresentationController presentationController)
        {
            _sheet.CachedDetents.Clear();
            _sheet.NotifyDismissed();
        }

        public override void DidChangeSelectedDetentIdentifier(UISheetPresentationController sheetPresentationController)
        {
            ((BottomSheetHandler)_sheet.Handler).UpdateSelectedDetent(_sheet);
        }
    }
    
    private class BottomSheetContainer : UIView
    {
        MaterialBottomSheet _sheet;
        UIView _view;

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
            var window = UIApplication.SharedApplication.KeyWindow;
            var topPadding = window?.SafeAreaInsets.Top ?? 0;
            var maximumDetentValue = heightConstraint - topPadding - SheetTopSpacing;
        
            return _sheet.GetEnabledDetents().Select(d => d.GetHeight(_sheet, maximumDetentValue)).Max();
        }

        internal BottomSheetContainer(MaterialBottomSheet sheet, UIView view)
        {
            _sheet = sheet;
            _view = view;
            AddSubview(_view);
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (_sheet?.Window == null) return;
            
            var h = CalculateTallestDetent(_sheet.Window.Height - BottomSheetManager.KeyboardHeight);
            _view.Frame = new CGRect(0, 0, Bounds.Width, h);
            _sheet.Arrange(_view.Frame.ToRectangle());
            _sheet.Controller.Layout();
        }
    }
    
    #endregion Helper classes
}