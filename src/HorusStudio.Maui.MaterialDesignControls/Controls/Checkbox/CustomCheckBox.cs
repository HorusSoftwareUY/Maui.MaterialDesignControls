namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// This class is a custom control that helps map some new CheckBox properties
/// </summary>
internal class CustomCheckBox : CheckBox
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="TickColor">TickColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TickColorProperty = BindableProperty.Create(nameof(TickColor), typeof(Color), typeof(CustomCheckBox), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color">color</see> for the tick of checkbox.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>Only is supported on iOS</remarks>
    public Color TickColor
    {
        get => (Color)GetValue(TickColorProperty);
        set => SetValue(TickColorProperty, value);
    }

    #endregion
}
