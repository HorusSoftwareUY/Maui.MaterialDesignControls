
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

    /// <summary>
    /// The backing store for the <see cref="ApplyIconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ApplyIconTintColorProperty = BindableProperty.Create(nameof(ApplyIconTintColor), typeof(bool), typeof(CustomButton), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="IconSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(Size), typeof(CustomButton), defaultValue: Size.Zero);

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

    /// <summary>
    /// Gets or sets the if the icon applies the tint color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true"/>
    /// </default>
    public bool ApplyIconTintColor
    {
        get => (bool)GetValue(ApplyIconTintColorProperty);
        set => SetValue(ApplyIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon size.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Size.Zero"/>.
    /// </default>
    /// <remarks>With a default value of <see langword="Size.Zero"/>, the icon will be handled automatically by each platform's native behavior. If a size is specified, that size will be applied to the icon on all platforms.</remarks>
    public Size IconSize
    {
        get => (Size)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    #endregion
}