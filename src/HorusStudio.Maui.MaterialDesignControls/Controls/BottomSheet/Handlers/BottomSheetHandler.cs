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
internal partial class BottomSheetHandler : ContentViewHandler
{
    #region Properties
    
    public new MaterialBottomSheet? VirtualView { get; private protected set; }
    public new PlatformView? PlatformView { get; private protected set; }
    
    private static readonly IPropertyMapper<MaterialBottomSheet, BottomSheetHandler> PropertiesMapper =
        new PropertyMapper<MaterialBottomSheet, BottomSheetHandler>(ContentViewHandler.Mapper)
        {
            [nameof(IContentView.Background)] = MapBackground,
            [nameof(MaterialBottomSheet.HandleColor)] = MapHandleColor,
            [nameof(MaterialBottomSheet.HasBackdrop)] = MapHasBackdrop,
            [nameof(MaterialBottomSheet.SelectedDetent)] = MapSelectedDetent,
            [nameof(MaterialBottomSheet.CornerRadius)] = MapCornerRadius,
        };

    private static readonly CommandMapper<MaterialBottomSheet, BottomSheetHandler> CommandsMapper =
        new(ContentViewHandler.CommandMapper)
        {
            [nameof(MaterialBottomSheet.DismissAsync)] = MapDismiss,
        };
    
    #endregion Properties
    
    public BottomSheetHandler() : base(PropertiesMapper, CommandsMapper)
    {
    }

    public BottomSheetHandler(IPropertyMapper? mapper)
        : base(mapper ?? PropertiesMapper, CommandsMapper)
    {
    }

    public BottomSheetHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
        : base(mapper ?? PropertiesMapper, commandMapper ?? CommandsMapper)
    {
    }
    
    #region Methods
    
    private static void MapCornerRadius(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateCornerRadius(sheet);
    private static void MapHasBackdrop(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateHasBackdrop(sheet);
    private static void MapHandleColor(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateHandleColor(sheet);
    private static void MapDismiss(BottomSheetHandler handler, MaterialBottomSheet view, object? request) => handler.Dismiss(view, request ?? false);
    private static void MapSelectedDetent(BottomSheetHandler handler, MaterialBottomSheet view) => handler.PlatformMapSelectedDetent(view);
    internal void UpdateSelectedDetent(MaterialBottomSheet view) => PlatformUpdateSelectedDetent(view);

    partial void PlatformMapSelectedDetent(MaterialBottomSheet view);
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view);
    partial void PlatformUpdateHasBackdrop(MaterialBottomSheet view);
    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view);
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view);
    partial void Dismiss(MaterialBottomSheet view, object request);

    #endregion Methods
}