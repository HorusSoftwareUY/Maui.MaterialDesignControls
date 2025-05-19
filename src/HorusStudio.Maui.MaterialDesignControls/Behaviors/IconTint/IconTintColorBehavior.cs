#nullable enable

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

/// <summary>
/// A behavior that allows you to tint an icon with a specified <see cref="Color"/>.
/// </summary>
public partial class IconTintColorBehavior : PlatformBehavior<View>
{
    /// <summary>
    /// Attached Bindable Property for the <see cref="TintColor"/>.
    /// </summary>
    public static readonly BindableProperty TintColorProperty =
        BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(IconTintColorBehavior), default);

    /// <summary>
    /// Property that represents the <see cref="Color"/> that Icon will be tinted.
    /// </summary>
    public Color? TintColor
    {
        get => (Color?)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Attached Bindable Property for the <see cref="IsEnabled"/>.
    /// </summary>
    public static readonly BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(IconTintColorBehavior), true);

    /// <summary>
    /// Property that represents if the Icon will be tinted with the <see cref="TintColor"/>. The default value is True.
    /// </summary>
    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
}

