namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
[ContentProperty(nameof(Ratio))]
public partial class RatioDetent : Detent
{
    public static readonly BindableProperty RatioProperty = BindableProperty.Create(nameof(Ratio), typeof(float), typeof(RatioDetent));

    public float Ratio
    {
        get => (float)GetValue(RatioProperty);
        set => SetValue(RatioProperty, value);
    }
    
    public override double GetHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        return maxSheetHeight * Ratio;
    }
}
