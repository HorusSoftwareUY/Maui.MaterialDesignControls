namespace HorusStudio.Maui.MaterialDesignControls;


/// <summary>
/// An extended <see cref="Editor">Editor</see> without active indicator
/// </summary>
internal class CustomEditor : Editor
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="CursorColor">CursorColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(CustomEditor), defaultValue: null);

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

    public CustomEditor() { }

    #endregion Constructor
}
