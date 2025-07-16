using Microsoft.Maui.Handlers;

#if IOS || MACCATALYST
using PlatformView = UIKit.UIView;
#elif ANDROID
using PlatformView = Android.Views.View;
#else
using PlatformView = System.Object;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetHandler : PageHandler
{
    #region Properties
    
    public new MaterialBottomSheet? VirtualView { get; private protected set; }
    public new PlatformView? PlatformView { get; private protected set; }
    
    private static readonly IPropertyMapper<MaterialBottomSheet, BottomSheetHandler> PropertiesMapper =
        new PropertyMapper<MaterialBottomSheet, BottomSheetHandler>(ContentViewHandler.Mapper)
        {
            [nameof(MaterialBottomSheet.Background)] = MapBackground,
            [nameof(MaterialBottomSheet.HasHandle)] = MapHasHandle,
            [nameof(MaterialBottomSheet.HandleColor)] = MapHandleColor,
            [nameof(MaterialBottomSheet.HandleOpacity)] = MapHandleColor,
            [nameof(MaterialBottomSheet.HasScrim)] = MapHasScrim,
            [nameof(MaterialBottomSheet.ScrimColor)] = MapScrimColor,
            [nameof(MaterialBottomSheet.ScrimOpacity)] = MapScrimOpacity,
            [nameof(MaterialBottomSheet.SelectedDetent)] = MapSelectedDetent,
            [nameof(MaterialBottomSheet.CornerRadius)] = MapCornerRadius
        };
    
    private static readonly CommandMapper<MaterialBottomSheet, BottomSheetHandler> CommandsMapper =
        new(ContentViewHandler.CommandMapper)
        {
            [nameof(MaterialBottomSheet.DismissAsync)] = MapDismiss,
        };
    
    #endregion Properties
    
    public BottomSheetHandler(IPropertyMapper? mapper, CommandMapper? commandMapper) : base(mapper ?? PropertiesMapper, commandMapper ?? CommandsMapper) {}
    public BottomSheetHandler(IPropertyMapper? mapper) : base(mapper ?? PropertiesMapper, CommandsMapper) {}
    public BottomSheetHandler() : base(PropertiesMapper, CommandsMapper) {}
    
    #region Methods
    
    public static BottomSheetHandler CreateBottomSheetHandler(IMauiContext context)
    {
        var sheet = new BottomSheetHandler();
        sheet.SetMauiContext(context);
        return sheet;
    }

    /*
    public override void UpdateValue(string property)
    {
        base.UpdateValue(property);
        if (property is nameof(IContentView.Background) or nameof(VisualElement.BackgroundColor))
        {
            MapBackground(this, VirtualView);
            PlatformUpdateBackground(this, (BottomSheet)VirtualView);
        }
    }
    */
    
    private static void MapBackground(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateBackground(sheet);
    private static void MapCornerRadius(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateCornerRadius(sheet);
    private static void MapHasScrim(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateHasScrim(sheet);
    private static void MapScrimColor(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateScrimColor(sheet);
    private static void MapScrimOpacity(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateScrimOpacity(sheet);
    private static void MapHasHandle(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateHasHandle(sheet);
    private static void MapHandleColor(BottomSheetHandler handler, MaterialBottomSheet sheet) => handler.PlatformUpdateHandleColor(sheet);
    private static void MapDismiss(BottomSheetHandler handler, MaterialBottomSheet view, object? request) => handler.Dismiss(view, request ?? false);
    private static void MapSelectedDetent(BottomSheetHandler handler, MaterialBottomSheet view) => handler.PlatformUpdateSelectedDetent(view);
    internal void UpdateSelectedDetent(MaterialBottomSheet view) => PlatformUpdateSelectedDetent(view);

    partial void PlatformUpdateBackground(MaterialBottomSheet view);
    partial void PlatformUpdateHandleColor(MaterialBottomSheet view);
    partial void PlatformUpdateHasScrim(MaterialBottomSheet view);
    partial void PlatformUpdateScrimColor(MaterialBottomSheet view);
    partial void PlatformUpdateScrimOpacity(MaterialBottomSheet view);
    partial void PlatformUpdateSelectedDetent(MaterialBottomSheet view);
    partial void PlatformUpdateCornerRadius(MaterialBottomSheet view);
    partial void Dismiss(MaterialBottomSheet view, object request);

    #endregion Methods
}