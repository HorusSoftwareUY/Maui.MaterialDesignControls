namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// An <see cref="Picker" /> without border lines on every platform.
/// </summary>
internal class CustomDatePicker : DatePicker
{
    #region Attributes

    private DateTime? customDate;

    public DateTime? CustomDate
    {
        get { return this.customDate; }
        set
        {
            this.customDate = value;
            if (this.customDate.HasValue)
            {
                base.Date = this.customDate.Value;
                EmptyDate = false;
            }
            else
                EmptyDate = true;
        }
    }

    public DateTime InternalDateTime
    {
        get { return base.Date; }
    }

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomDatePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomDatePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomDatePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="EmptyDate" /> bindable property.
    /// </summary>
    public static readonly BindableProperty EmptyDateProperty = BindableProperty.Create(nameof(EmptyDate), typeof(bool), typeof(CustomDatePicker), defaultValue: true);

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

    /// <summary>
    /// Gets or sets if have and empty date.
    /// This is a bindable property.
    /// </summary>
    public bool EmptyDate
    {
        get => (bool)GetValue(EmptyDateProperty);
        set => SetValue(EmptyDateProperty, value);
    }

    #endregion

    #region Constructor

    public CustomDatePicker() { }

    #endregion Constructor
}

