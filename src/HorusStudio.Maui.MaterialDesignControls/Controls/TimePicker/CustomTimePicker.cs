namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A <see cref="TimePicker">TimePicker</see> without border on every platform.
/// </summary>
internal class CustomTimePicker : TimePicker
{
    #region Attributes

    private TimeSpan? _customTime;

    public TimeSpan? CustomTime
    {
        get => _customTime;
        set
        {
            _customTime = value;
            if (_customTime.HasValue)
            {
                base.Time = _customTime.Value;
            }
        }
    }

    public TimeSpan? InternalTime => base.Time;

    #endregion Attributes

    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment">HorizontalTextAlignment</see> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomTimePicker), defaultValue: null);

    #endregion

    #region Properties

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
}
