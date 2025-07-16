namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialBottomSheet
{
    internal BottomSheetController? Controller { get; set; }

    // Cache the calculated detents as iOS likes to ask for detents often
    internal readonly IDictionary<int, float> CachedDetents = new Dictionary<int, float>();
}