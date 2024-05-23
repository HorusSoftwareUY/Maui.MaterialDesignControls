namespace HorusStudio.Maui.MaterialDesignControls;

internal class CustomCheckBox : CheckBox
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="CheckColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CheckColorProperty = BindableProperty.Create(nameof(CheckColor), typeof(Color), typeof(CustomRadioButton), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color" /> for the stroke of the radio button.
    /// This is a bindable property.
    /// </summary>
    public Color CheckColor
    {
        get => (Color)GetValue(CheckColorProperty);
        set => SetValue(CheckColorProperty, value);
    }

    #endregion
}
