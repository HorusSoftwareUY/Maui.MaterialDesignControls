using Microsoft.Maui.Handlers;

#if IOS || MACCATALYST
using PlatformView = UIKit.UIView;
#elif ANDROID
using PlatformView = Android.Views.View;
#else
using PlatformView = System.Object;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class BottomSheetHandler : ContentViewHandler
{
    public static new IPropertyMapper<MaterialBottomSheet, BottomSheetHandler> Mapper =
        new PropertyMapper<MaterialBottomSheet, BottomSheetHandler>(ContentViewHandler.Mapper)
        {
            [nameof(IContentView.Background)] = MapBackground,
            [nameof(MaterialBottomSheet.HandleColor)] = MapHandleColor,
            [nameof(MaterialBottomSheet.HasBackdrop)] = MapHasBackdrop,
            [nameof(MaterialBottomSheet.SelectedDetent)] = MapSelectedDetent,
            [nameof(MaterialBottomSheet.CornerRadius)] = MapCornerRadius,
        };

    private static void MapCornerRadius(BottomSheetHandler handler, MaterialBottomSheet sheet)
    {
        handler.PlatformUpdateCornerRadius(sheet);
    }

    private static void MapHasBackdrop(BottomSheetHandler handler, MaterialBottomSheet sheet)
    {
        handler.PlatformUpdateHasBackdrop(sheet);
    }

    private static void MapHandleColor(BottomSheetHandler handler, MaterialBottomSheet sheet)
    {
        handler.PlatformUpdateHandleColor(sheet);
    }

    public static new CommandMapper<MaterialBottomSheet, BottomSheetHandler> CommandMapper =
        new(ContentViewHandler.CommandMapper)
        {
            [nameof(MaterialBottomSheet.DismissAsync)] = MapDismiss,
        };

    private static void MapDismiss(BottomSheetHandler handler, MaterialBottomSheet view, object? request)
    {
        handler.Dismiss(view, request ?? false);
    }

    public static void MapSelectedDetent(BottomSheetHandler handler, MaterialBottomSheet view)
    {
        handler.PlatformMapSelectedDetent(view);
    }

    internal void UpdateSelectedDetent(MaterialBottomSheet view)
    {
        PlatformUpdateSelectedDetent(view);
    }

    partial void PlatformMapSelectedDetent(MaterialBottomSheet view);
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view);
    partial void PlatformUpdateHasBackdrop(MaterialBottomSheet view);
    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view);
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view);
    partial void Dismiss(MaterialBottomSheet view, object request);

    public BottomSheetHandler() : base(Mapper, CommandMapper)
    {
    }

    public BottomSheetHandler(IPropertyMapper? mapper)
        : base(mapper ?? Mapper, CommandMapper)
    {
    }

    public BottomSheetHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
        : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
    {
    }

    new MaterialBottomSheet? VirtualView { get; }
    new PlatformView? PlatformView { get; }

}