namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
internal partial class BottomSheetHandler
{
    partial void PlatformMapBackground(MaterialBottomSheet view)
    {
        view.Controller.UpdateBackground();
    }
    
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view)
    {
        // Not supported on iOS
    }

    partial void Dismiss(MaterialBottomSheet view, object request)
    {
        view?.CachedDetents.Clear();
        view?.Controller?.DismissViewController((bool)request, view.NotifyDismissed);
    }

    partial void PlatformMapSelectedDetent(MaterialBottomSheet view)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            view.Controller.UpdateSelectedIdentifierFromDetent();
        }
    }

    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            view.Controller.UpdateSelectedDetent();
        }
    }
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view)
    {
        view.Controller.UpdateCornerRadius(view.CornerRadius);
    }
}