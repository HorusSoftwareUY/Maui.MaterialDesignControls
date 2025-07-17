using Android.Graphics.Drawables;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A drawable with non-square corners on top left and right only.
/// </summary>
internal class TopCornerRadiusDrawable: GradientDrawable
{
    internal void SetTopCornerRadius(int radius) => SetCornerRadii([radius, radius, radius, radius, 0, 0, 0, 0]);
}