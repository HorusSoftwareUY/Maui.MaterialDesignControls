namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An <see cref="Picker" /> without border lines on every platform.
/// </summary>
internal class CustomPicker : Picker
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomPicker), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets place holder.
    /// This is a bindable property.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="Color" /> for the stroke of the radio button.
    /// This is a bindable property.
    /// </summary>
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    #endregion

    #region Constructor

    public CustomPicker() { }

    #endregion Constructor
}
