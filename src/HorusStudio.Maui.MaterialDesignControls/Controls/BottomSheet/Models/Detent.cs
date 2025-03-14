
namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public abstract class Detent : BindableObject
{
    public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(Detent), defaultValue: true);
    public static readonly BindableProperty IsDefaultProperty = BindableProperty.Create(nameof(IsDefault), typeof(bool), typeof(Detent));

    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
    
    public bool IsDefault
    {
        get => (bool)GetValue(IsDefaultProperty);
        set => SetValue(IsDefaultProperty, value);
    }

    public abstract double GetHeight(MaterialBottomSheet page, double maxSheetHeight);
}
