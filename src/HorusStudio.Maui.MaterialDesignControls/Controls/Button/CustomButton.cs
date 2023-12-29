
namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An extended <see cref="Button" /> that adds new features to native one.
/// </summary>
class CustomButton : Button
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="TextDecorations" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextDecorationsProperty = BindableProperty.Create(nameof(TextDecorations), typeof(TextDecorations), typeof(CustomButton), defaultValue: TextDecorations.None);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="TextDecorations" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    public TextDecorations TextDecorations
    {
        get => (TextDecorations)GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    #endregion
}