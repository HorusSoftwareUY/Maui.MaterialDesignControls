namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An <see cref="TimePicker" /> without border lines on every platform.
/// </summary>
internal class CustomTimePicker : TimePicker
{
    #region Attributes

    private TimeSpan? customTime;

    public TimeSpan? CustomTime
    {
        get { return this.customTime; }
        set
        {
            this.customTime = value;
            if (this.customTime.HasValue)
            {
                base.Time = this.customTime.Value;
            }
        }
    }

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomTimePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomTimePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomTimePicker), defaultValue: null);

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
    /// Gets or sets <see cref="Color" /> for the placeholder
    /// This is a bindable property.
    /// </summary>
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text aligment.
    /// This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    #endregion

    #region Constructor

    public CustomTimePicker() { }

    #endregion Constructor
}
