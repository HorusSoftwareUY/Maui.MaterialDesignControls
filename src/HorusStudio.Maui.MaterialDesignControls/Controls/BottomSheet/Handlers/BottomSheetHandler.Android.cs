using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
internal partial class BottomSheetHandler : ContentViewHandler
{
    partial void PlatformMapBackground(MaterialBottomSheet view)
    {
        view.Controller.UpdateBackground();
    }
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view)
    {
        view.Controller.UpdateHandleColor();
    }

    partial void Dismiss(MaterialBottomSheet view, object request)
    {
        view?.Controller?.Dismiss((bool)request);
    }

    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view)
    {
        view.Controller.UpdateSelectedDetent();
    }

    partial void PlatformMapSelectedDetent(MaterialBottomSheet view)
    {
        view.Controller.UpdateStateFromDetent();
    }

    partial void PlatformUpdateHasScrim(MaterialBottomSheet view)
    {
        view.Controller.UpdateHasScrim();
    }
    
    partial void PlatformUpdateScrimColor(MaterialBottomSheet view)
    {
        view.Controller.UpdateScrimColor();
    }

    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view)
    {
        view.Controller.UpdateBackground();
    }
}