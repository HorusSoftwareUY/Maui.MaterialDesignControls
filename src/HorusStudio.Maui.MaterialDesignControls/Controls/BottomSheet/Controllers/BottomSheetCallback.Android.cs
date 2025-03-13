using AView = Android.Views.View;
using Google.Android.Material.BottomSheet;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public class BottomSheetCallback(MaterialBottomSheet page) : BottomSheetBehavior.BottomSheetCallback
{
    readonly MaterialBottomSheet _page = page;

    public event EventHandler? StateChanged;

    public override void OnSlide(AView bottomSheet, float newState)
    {}

    public override void OnStateChanged(AView view, int newState)
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}