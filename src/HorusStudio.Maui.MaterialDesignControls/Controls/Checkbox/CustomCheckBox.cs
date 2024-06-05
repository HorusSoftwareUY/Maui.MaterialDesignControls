namespace HorusStudio.Maui.MaterialDesignControls;

internal class CustomCheckBox : CheckBox
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="TickColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TickColorProperty = BindableProperty.Create(nameof(TickColor), typeof(Color), typeof(CustomCheckBox), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color" /> for the tick of checkbox
    /// Only is supported on iOS
    /// This is a bindable property.
    /// </summary>
    public Color TickColor
    {
        get => (Color)GetValue(TickColorProperty);
        set => SetValue(TickColorProperty, value);
    }

    #endregion
}
