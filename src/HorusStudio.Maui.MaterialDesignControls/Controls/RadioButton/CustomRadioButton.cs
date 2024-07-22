namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An extended <see cref="RadioButton" /> that adds new features to native one.
/// </summary>
class CustomRadioButton : RadioButton
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="StrokeColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(CustomRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="IsControlTemplateByDefault" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsControlTemplateByDefaultProperty = BindableProperty.Create(nameof(IsControlTemplateByDefault), typeof(bool), typeof(CustomRadioButton), defaultValue: true);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color" /> for the stroke of the radio button.
    /// This is a bindable property.
    /// </summary>
    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="bool" /> to check if the user changes control template of the radio button.
    /// This is a bindable property.
    /// </summary>
    public bool IsControlTemplateByDefault
    {
        get => (bool)GetValue(IsControlTemplateByDefaultProperty);
        set => SetValue(IsControlTemplateByDefaultProperty, value);
    }

    #endregion

    public CustomRadioButton()
    {
        // We need to set the background color to transparent because by default the background is white or black on android and ios, respectively.
        BackgroundColor = Colors.Transparent;
    }
}
