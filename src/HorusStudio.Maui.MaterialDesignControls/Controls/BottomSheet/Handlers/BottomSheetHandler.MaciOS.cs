namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetHandler
{
    //partial void PlatformUpdateCornerRadius(MaterialBottomSheet view) => view?.Controller?.UpdateCornerRadius(view.CornerRadius);
    //partial void PlatformUpdateBackground(BottomSheetHandler _, MaterialBottomSheet view) => view?.Controller?.UpdateBackground();
    //partial void PlatformUpdateHasHandle(MaterialBottomSheet view) => view?.Controller?.UpdateHasHandle(view.HasHandle);

    // partial void Dismiss(MaterialBottomSheet view, object request)
    // {
    //     view?.CachedDetents.Clear();
    //     view?.Controller?.DismissViewController((bool)request, view.NotifyDismissed);
    // }
/*
    partial void PlatformMapSelectedDetent(MaterialBottomSheet view)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(15))
            view?.Controller?.UpdateSelectedIdentifierFromDetent();
    }
*/
    // partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view)
    // {
    //     if (OperatingSystem.IsIOSVersionAtLeast(15))
    //         view?.Controller?.UpdateSelectedDetent();
    // }
    
    
    
    
    partial void PlatformUpdateBackground(MaterialBottomSheet view) => view?.Controller?.UpdateBackground();
    
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view) => view?.Controller?.UpdateCornerRadius(view.CornerRadius);
    
    private void PlatformUpdateHasHandle(MaterialBottomSheet view) => view?.Controller?.UpdateHasHandle(view.HasHandle);

    partial void PlatformUpdateHandleColor(MaterialBottomSheet view)
    {
        //Not supported on iOS
    }
    
    partial void Dismiss(MaterialBottomSheet view, object request)
    {
        view?.CachedDetents.Clear();
        view?.Controller?.DismissViewController((bool)request, view.NotifyDismissed);
    }

    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15)) return;
        view?.Controller?.UpdateSelectedDetent();
    }
    
    partial void PlatformUpdateHasScrim(MaterialBottomSheet view) => view?.Controller?.UpdateHasBackdrop(view.HasScrim);
    
    partial void PlatformUpdateScrimColor(MaterialBottomSheet view) 
    {
        //Not supported on iOS
    }

    partial void PlatformUpdateScrimOpacity(MaterialBottomSheet view) 
    {
        //Not supported on iOS
    }
}