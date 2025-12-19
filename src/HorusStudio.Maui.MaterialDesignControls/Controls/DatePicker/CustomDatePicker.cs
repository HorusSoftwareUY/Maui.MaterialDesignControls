
namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An extended <see cref="DatePicker">DatePicker</see> without border on every platform.
/// </summary>
internal class CustomDatePicker : DatePicker
{
    #region Attributes

    private DateTime? _customDate;

    public DateTime? CustomDate
    {
        get => _customDate;
        set
        {
            _customDate = value;
            if (_customDate.HasValue)
            {
                base.Date = _customDate.Value;
            }
        }
    }

    public DateTime? InternalDateTime => base.Date;

    #endregion Attributes

    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment">HorizontalTextAlignment</see> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomDatePicker), defaultValue: null);

    #endregion

    #region Properties
    
    /// <summary>
    /// Gets or sets the horizontal text alignment.
    /// This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    #endregion
}

