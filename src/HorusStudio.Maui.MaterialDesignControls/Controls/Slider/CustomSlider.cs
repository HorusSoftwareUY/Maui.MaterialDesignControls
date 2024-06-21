

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

    /// <summary>
    /// The backing store for the <see cref="ThumbBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbBackgroundColorProperty = BindableProperty.Create(nameof(ThumbBackgroundColor), typeof(Color), typeof(CustomSlider), defaultValue: null);


    /// <summary>
    /// The backing store for the <see cref="ThumbWidth" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbWidthProperty = BindableProperty.Create(nameof(ThumbWidth), typeof(int), typeof(MaterialSlider), defaultValue: 4);

    /// <summary>
    /// The backing store for the <see cref="ThumbHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbHeightProperty = BindableProperty.Create(nameof(ThumbHeight), typeof(int), typeof(MaterialSlider), defaultValue: 44);

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

    /// <summary>
    /// Allows you to set the color of the thumb shadow.
    /// You should set as the background color of the slider's container.
    /// </summary>
    public Color ThumbBackgroundColor
    {
        get => (Color)GetValue(ThumbBackgroundColorProperty);
        set => SetValue(ThumbBackgroundColorProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb width
    /// The default value is <value>4</value>
    /// </summary>
    public int ThumbWidth
    {
        get => (int)GetValue(ThumbWidthProperty);
        set => SetValue(ThumbWidthProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb height
    /// The default value is <value>44</value>
    /// </summary>
    public int ThumbHeight
    {
        get => (int)GetValue(ThumbHeightProperty);
        set => SetValue(ThumbHeightProperty, value);
    }

    #endregion
}
