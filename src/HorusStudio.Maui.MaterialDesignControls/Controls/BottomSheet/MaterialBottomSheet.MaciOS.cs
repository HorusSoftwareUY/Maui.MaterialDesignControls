using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class MaterialBottomSheet
{
    internal BottomSheetController Controller { get; set; }

    // Cache the calculated detents as iOS likes to ask for detents often
    internal readonly IDictionary<int, float> CachedDetents = new Dictionary<int, float>();
}