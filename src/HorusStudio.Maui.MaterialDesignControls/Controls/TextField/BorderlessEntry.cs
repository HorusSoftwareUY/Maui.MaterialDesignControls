namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An <see cref="Entry" /> without border lines on every platform.
/// </summary>
internal class BorderlessEntry : Entry
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="CursorColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(CustomRadioButton), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color" /> for the caret color.
    /// This is a bindable property.
    /// </summary>
    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set => SetValue(CursorColorProperty, value);
    }

    #endregion

    #region Constructor

    public BorderlessEntry(){ }

    #endregion Constructor
}