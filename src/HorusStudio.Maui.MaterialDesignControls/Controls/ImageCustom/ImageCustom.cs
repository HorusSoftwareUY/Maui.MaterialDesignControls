using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

public class ImageCustom : Image
{
    #region attribute

    private readonly static Color DefaultTintColor = default;

    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(ImageCustom), defaultValue: DefaultTintColor);
    
    /// <summary>
    /// The backing store for the <see cref="TintColor" />
    /// bindable property.
    /// </summary>
    internal static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(ImageCustom), defaultValue: DefaultTintColor);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets a color icon
    /// This is a bindable property.
    /// </summary>
    public Color IconTintColor
    {
        get => (Color)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }
    
    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    #endregion

    #region Constructor

    public ImageCustom()
    {
        var IconTintColor = new IconTintColorBehavior();
        IconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TintColor), source: this));
        Behaviors.Add(IconTintColor);
    }

    #endregion
    
}