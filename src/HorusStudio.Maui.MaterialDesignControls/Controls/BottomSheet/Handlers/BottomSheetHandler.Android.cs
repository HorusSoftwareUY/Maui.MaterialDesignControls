
namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetHandler
{
    partial void PlatformUpdateBackground(MaterialBottomSheet view) => view?.Controller?.UpdateBackground(view.BackgroundBrush, view.CornerRadius);
    
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view) => view?.Controller?.UpdateBackground(view.BackgroundBrush, view.CornerRadius);
    
    private void PlatformUpdateHasHandle(MaterialBottomSheet view) => view?.Controller?.UpdateHasHandle(view.HasHandle);
    
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view) => view?.Controller?.UpdateHandle(view.HandleColor, view.HandleOpacity);
    
    partial void Dismiss(MaterialBottomSheet view, object request) => view?.Controller?.Dismiss((bool)request);

    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view) => view?.Controller?.UpdateSelectedDetent();

    //partial void PlatformMapSelectedDetent(MaterialBottomSheet view) => view?.Controller?.UpdateSelectedDetent();

    partial void PlatformUpdateHasScrim(MaterialBottomSheet view) => view?.Controller?.UpdateHasBackdrop(view.HasScrim);
    
    partial void PlatformUpdateScrimColor(MaterialBottomSheet view) => view?.Controller?.UpdateBackdropColor(view.ScrimColor);

    partial void PlatformUpdateScrimOpacity(MaterialBottomSheet view) => view?.Controller?.UpdateBackdropOpacity(view.ScrimOpacity);
}