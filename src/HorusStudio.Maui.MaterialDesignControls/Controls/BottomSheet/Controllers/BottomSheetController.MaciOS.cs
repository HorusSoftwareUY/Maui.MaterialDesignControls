using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

internal sealed class BottomSheetController : UIViewController
{
    #region Fields & Properties
    
    private readonly UIWindow _window;
    private readonly IMauiContext _windowMauiContext;
    private readonly MaterialBottomSheet _sheet;
    private UISheetPresentationControllerDetentIdentifier _largestDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;

    //private NFloat _keyboardHeight = 0;
    private NSObject? _keyboardDidHideObserver;
    //private NSObject? _keyboardWillShowObserver;
    
    #endregion Fields & Properties
    
    public BottomSheetController(IMauiContext windowMauiContext, MaterialBottomSheet sheet, UIWindow window)
    {
        _windowMauiContext = windowMauiContext;
        _sheet = sheet;
        _window = window;
        
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        SheetPresentationController!.Delegate = new BottomSheetControllerDelegate(_sheet);
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if (View is null) return;
        var container = _sheet.ToPlatform(_windowMauiContext);
        var cv = new BottomSheetContainerView(_sheet, container, _window);
        View.AddSubview(cv);
        
        cv.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints(
        [
            cv.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            cv.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            cv.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
            cv.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor)
        ]);

        UpdateBackground();
        _sheet.NotifyShowing();

        _keyboardDidHideObserver ??= UIKeyboard.Notifications.ObserveDidHide(KeyboardDidHide);
        //_keyboardWillShowObserver ??= UIKeyboard.Notifications.ObserveWillShow(KeyboardWillShow);
    }

    private void KeyboardDidHide(object? sender, UIKeyboardEventArgs e) => Layout();
    
    /*
    private void KeyboardWillShow(object? sender, UIKeyboardEventArgs e) 
        => _keyboardHeight = e.FrameEnd.Height;
    */
    internal void Layout()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        _sheet.CachedDetents.Clear();
        SheetPresentationController!.InvalidateDetents();
    }
    
    internal void UpdateBackground()
    {
        var color = UIColor.SystemBackground;
        
        try
        {
            Paint paint = _sheet.BackgroundBrush;
            color = paint.ToColor()!.ToPlatform();
        }
        catch
        {
            // ignored
        }

        View!.BackgroundColor = color;
    }
    
    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        Layout();
    }

    internal void UpdateDetents()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        _largestDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
        
        var pageDetents = _sheet.GetEnabledDetents().ToList();
        var detents = pageDetents
            .Select((d, index) =>
            {
                if (d is FullscreenDetent)
                {
                    _largestDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                    return UISheetPresentationControllerDetent.CreateLargeDetent();
                }
                
                if (d is RatioDetent { Ratio: .5f })
                {
                    _largestDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;
                    return UISheetPresentationControllerDetent.CreateMediumDetent();
                }
                
                if (!OperatingSystem.IsIOSVersionAtLeast(16)) return null;
                
                return UISheetPresentationControllerDetent.Create($"detent{index}", context =>
                {
                    if (!_sheet.CachedDetents.TryGetValue(index, out var value))
                    {
                        value = (float)d.GetHeight(_sheet, context.MaximumDetentValue - BottomSheetManager.KeyboardHeight);
                        _sheet.CachedDetents.Add(index, value);
                    }
                        
                    return value;
                });
            })
            .Where(d => d is not null)
            .Select(d => d!)
            .ToList();

        if (detents.Count == 0)
        {
            _largestDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;
            detents.Add(UISheetPresentationControllerDetent.CreateMediumDetent());
        }

        SheetPresentationController!.Detents = detents?.ToArray() ?? [];
        UpdateSelectedIdentifierFromDetent();
    }
    
    internal static UISheetPresentationControllerDetentIdentifier GetIdentifierForDetent(Detent? d)
    {
        return d switch
        {
            FullscreenDetent => UISheetPresentationControllerDetentIdentifier.Large,
            RatioDetent { Ratio: .5f } => UISheetPresentationControllerDetentIdentifier.Medium,
            _ => UISheetPresentationControllerDetentIdentifier.Unknown
        };
    }

    internal void UpdateSelectedIdentifierFromDetent()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15) || _sheet.SelectedDetent is null) return;
        
        SheetPresentationController!.AnimateChanges(() =>
        {
            SheetPresentationController.SelectedDetentIdentifier = GetIdentifierForDetent(_sheet.SelectedDetent);
        });
    }

    internal Detent? GetSelectedDetent()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return null;
        
        var detents = _sheet.GetEnabledDetents();
        return SheetPresentationController!.SelectedDetentIdentifier switch
        {
            UISheetPresentationControllerDetentIdentifier.Medium => detents.FirstOrDefault(d => d is RatioDetent { Ratio: .5f }),
            UISheetPresentationControllerDetentIdentifier.Large => detents.FirstOrDefault(d => d is FullscreenDetent),
            _ => null
        };
    }

    internal void UpdateSelectedDetent()
    {
        _sheet.SelectedDetent = GetSelectedDetent();
    }

    internal void UpdateCornerRadius(double cornerRadius)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        SheetPresentationController!.PreferredCornerRadius = (NFloat)cornerRadius;
    }

    internal void UpdateHasHandle(bool hasHandle)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        SheetPresentationController!.PrefersGrabberVisible = hasHandle;
    }

    internal void UpdateHasBackdrop(bool hasBackdrop)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        if (!hasBackdrop)
        {
            SheetPresentationController!.LargestUndimmedDetentIdentifier = _largestDetentIdentifier;
        }
    }
    
    internal void UpdatePresentationMode(bool isCancelable)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(13)) return;
        ModalInPresentation = !isCancelable;
    }
}

[SupportedOSPlatform("ios15.0")]
internal class BottomSheetControllerDelegate(MaterialBottomSheet sheet) : UISheetPresentationControllerDelegate
{
    public override void DidDismiss(UIPresentationController presentationController)
    {
        sheet.CachedDetents.Clear();
        sheet.NotifyDismissed();
    }

    public override void DidChangeSelectedDetentIdentifier(UISheetPresentationController sheetPresentationController)
    {
        if (sheet?.Handler is not BottomSheetHandler handler) return;
        handler.UpdateSelectedDetent(sheet);
    }
}