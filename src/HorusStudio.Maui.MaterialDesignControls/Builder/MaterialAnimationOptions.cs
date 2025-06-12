namespace HorusStudio.Maui.MaterialDesignControls;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialAnimationOptions
{
    public double? Parameter { get; set; }
    public AnimationTypes? Type { get; set; }
    public ErrorAnimationTypes? ErrorAnimationType { get; set; }
}