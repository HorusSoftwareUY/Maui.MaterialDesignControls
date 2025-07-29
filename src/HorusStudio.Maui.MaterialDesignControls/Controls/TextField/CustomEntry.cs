namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An extended <see cref="Entry">Entry</see> without border lines on every platform.
/// </summary>
internal class CustomEntry : Entry
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="CursorColor">CursorColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(CustomEntry), defaultValue: null);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="Color">color</see> for the caret.
    /// This is a bindable property.
    /// </summary>
    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set => SetValue(CursorColorProperty, value);
    }

    #endregion

    #region Constructor

    public CustomEntry(){ }

    #endregion Constructor
}