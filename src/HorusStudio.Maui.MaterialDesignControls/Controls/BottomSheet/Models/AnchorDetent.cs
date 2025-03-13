namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class AnchorDetent: Detent
{
    public static readonly BindableProperty AnchorProperty = BindableProperty.Create(nameof(Anchor), typeof(VisualElement), typeof(AnchorDetent));

    public VisualElement Anchor
    {
        get => (VisualElement)GetValue(AnchorProperty);
        set => SetValue(AnchorProperty, value);
    }
    
    private double _height = 0;

    public override double GetHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        UpdateHeight(page, maxSheetHeight);
        return _height;
    }

    partial void UpdateHeight(MaterialBottomSheet page, double maxSheetHeight);
}
