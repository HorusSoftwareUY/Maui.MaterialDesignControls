namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
[ContentProperty(nameof(Height))]
public partial class HeightDetent : Detent
{
    public static readonly BindableProperty HeightProperty = BindableProperty.Create(nameof(Height), typeof(double), typeof(HeightDetent));

    public double Height
    {
        get => (double)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public override double GetHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        return Height;
    }
}
