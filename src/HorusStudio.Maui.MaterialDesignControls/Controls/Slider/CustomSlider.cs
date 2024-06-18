

namespace HorusStudio.Maui.MaterialDesignControls;
class CustomSlider : Slider
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="TrackHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackHeightProperty = BindableProperty.Create(nameof(TrackHeight), typeof(int), typeof(CustomSlider), defaultValue: 6);

    /// <summary>
    /// The backing store for the <see cref="TrackCornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackCornerRadiusProperty = BindableProperty.Create(nameof(TrackCornerRadius), typeof(int), typeof(CustomSlider), defaultValue: 3);

    /// <summary>
    /// The backing store for the <see cref="UserInteractionEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UserInteractionEnabledProperty = BindableProperty.Create(nameof(UserInteractionEnabled), typeof(bool), typeof(CustomSlider), defaultValue: true);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="UserInteractionEnabled" /> for the slider.
    /// This is a bindable property.
    /// </summary>
    public bool UserInteractionEnabled
    {
        get { return (bool)GetValue(UserInteractionEnabledProperty); }
        set { SetValue(UserInteractionEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="TrackHeight" /> for the slider.
    /// This is a bindable property.
    /// </summary>
    public int TrackHeight
    {
        get { return (int)GetValue(TrackHeightProperty); }
        set { SetValue(TrackHeightProperty, value); }
    }


    /// <summary>
    /// Gets or sets <see cref="TrackCornerRadius" /> for the slider.
    /// This is a bindable property.
    /// </summary>
    public int TrackCornerRadius
    {
        get { return (int)GetValue(TrackCornerRadiusProperty); }
        set { SetValue(TrackCornerRadiusProperty, value); }
    }

    #endregion





}
