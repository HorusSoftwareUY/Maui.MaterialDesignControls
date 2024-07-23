namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialBadgeType
{
    /// <summary> Small Badge </summary>
    Small,
    /// <summary> Large Badge </summary>
    Large
}

/// <summary>
/// Badges show notifications, counts, or status information on navigation items and icons <see href="https://m3.material.io/components/badges/overview"> see here.</see>
/// </summary>
public class MaterialBadge : ContentView
{
    #region Attributes

    private readonly static MaterialBadgeType DefaultBadgeType = MaterialBadgeType.Large;
    private readonly static string DefaultText = string.Empty;
    private readonly static Color DefaultTextColor = MaterialLightTheme.OnError;
    private readonly static Color DefaultBackgroundColor = MaterialLightTheme.Error;
    private readonly static double DefaultFontSize = MaterialFontSize.LabelSmall;
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(6);
    private readonly static Thickness DefaultPadding = new Thickness(16, 0);
    
    private readonly Dictionary<MaterialBadgeType, object> _textColors = new()
    {
        { MaterialBadgeType.Small, new AppThemeBindingExtension { Light = MaterialLightTheme.OnError, Dark = MaterialDarkTheme.OnError } },
        { MaterialBadgeType.Large, new AppThemeBindingExtension { Light = MaterialLightTheme.OnError, Dark = MaterialDarkTheme.OnError } }
    };
    
    private readonly Dictionary<MaterialBadgeType, object> _backgroundColors = new()
    {
        { MaterialBadgeType.Small, new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialDarkTheme.Error } },
        { MaterialBadgeType.Large, new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialDarkTheme.Error } }
    };
    
    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialBadgeType), typeof(MaterialBadge), defaultValue: DefaultBadgeType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialBadge self)
        {
            if (Enum.IsDefined(typeof(MaterialBadgeType), oldValue) &&
                Enum.IsDefined(typeof(MaterialBadgeType), newValue) &&
                (MaterialBadgeType)oldValue != (MaterialBadgeType)newValue)
            {
                self.UpdateLayoutAfterTypeChanged((MaterialBadgeType)newValue);
            }
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Text" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialBadge), defaultValue: DefaultText, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialBadge self)
        {
            self.SetText(self.Type);
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="TextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialBadge), defaultValue: DefaultTextColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialBadge self)
        {
            self.SetTextColor(self.Type);
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialBadge), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialBadge), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBadge), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialBadge self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialBadge), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialBadge), defaultValue: DefaultPadding);
    
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the badge type according to <see cref="MaterialBadgeType"/> enum.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialBadgeType.Small"> MaterialBadgeType.Small </see>
    /// </default>
    public MaterialBadgeType Type
    {
        get => (MaterialBadgeType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the text displayed as the content of the badge.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>Changing the text of a badge will trigger a layout cycle.</remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the Badge.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="Color.Transparent"> Color.Transparent </see>
    /// </default>
    /// <remarks> The text color may be affected by the following cases:
    /// <para>Badge type is small, the text color is not defined.</para>
    /// <para>Badge type is Large, the text color is defined.</para>
    /// </remarks>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the size of the font for the text of this badge
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 0
    /// </default>
    /// <remarks> The font size may be affected by the following cases:
    /// <para>Badge type is small, the font size not change.</para>
    /// <para>Badge type is Large, the font is changed.</para>
    /// </remarks>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the font family for the text of this badge.
    /// This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the background color of the badge.
    /// This is a bindable property.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the corner radius for the Badge, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DefaultCornerRadius">6</see>
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the padding for the Badge.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DefaultPadding">16,0</see>
    /// </default>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }
    
    #endregion

    #region Layout

    private MaterialCard _frmContainer;
    private Label _lblText;
    
    #endregion

    public MaterialBadge()
    {
        CreateLayout();
        if (Type == DefaultBadgeType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }
    
    private void CreateLayout()
    {
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;
        
        this._frmContainer = new MaterialCard()
        {
            BackgroundColor = this.BackgroundColor,
            CornerRadius = this.CornerRadius,
            Padding = this.Padding
        };

        this._lblText = new Label()
        {
            TextColor = this.TextColor,
            FontSize = this.FontSize,
            FontFamily = this.FontFamily,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
        };

        _frmContainer.SetBinding(MaterialBadge.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _frmContainer.SetBinding(MaterialBadge.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        _frmContainer.SetBinding(MaterialBadge.PaddingProperty, new Binding(nameof(Padding), source: this));

        _lblText.SetBinding(MaterialBadge.TextProperty, new Binding(nameof(Text), source: this));
        _lblText.SetBinding(MaterialBadge.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _lblText.SetBinding(MaterialBadge.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _lblText.SetBinding(MaterialBadge.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        
        this._frmContainer.Content = this._lblText;
        this.Content = this._frmContainer;
        
        ResizeControl();
    }
    
    private void UpdateLayoutAfterTypeChanged(MaterialBadgeType type)
    {
        SetBackgroundColor(type);
        SetTextColor(type);
        SetText(type);
        SetSizeControl(type);
    }

    private void SetSizeControl(MaterialBadgeType type)
    {
        this.HeightRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        this.CornerRadius = new CornerRadius((type is MaterialBadgeType.Small) ? 3 : 8);
        this.MinimumWidthRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        this.MinimumHeightRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        this._lblText.IsVisible = (type is not MaterialBadgeType.Small);

        if (type is MaterialBadgeType.Small) this._frmContainer.Padding = new Thickness(0);
        if (type is MaterialBadgeType.Small) this.WidthRequest = 6;
    }

    private void ResizeControl()
    {
        this._frmContainer.Padding = (!string.IsNullOrEmpty(this._lblText.Text) && this._lblText.Text.Length >= 2)? new Thickness(4, 0) : new Thickness(0);
        this._frmContainer.WidthRequest = -1;
    }

    private void SetText(MaterialBadgeType type)
    {
        if (type is MaterialBadgeType.Large && !string.IsNullOrEmpty(Text))
        {
            _lblText.Text = Text; 
            ResizeControl();
        }
    }

    private void SetBackgroundColor(MaterialBadgeType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
            {
                // Default Material value according to Type
                if (background is Color backgroundColor)
                {
                    _frmContainer.BackgroundColor = backgroundColor;
                }
                else if (background is AppThemeBindingExtension theme)
                {
                    _frmContainer.BackgroundColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _frmContainer.BackgroundColor = BackgroundColor;
            }
        }
        else
        {
            // Unsupported for current Badge type, ignore
            _frmContainer.BackgroundColor = DefaultBackgroundColor;
        }
    }

    private void SetTextColor(MaterialBadgeType type)
    {
        if (_textColors.TryGetValue(type, out object text) && text != null)
        {
            if ((TextColor == null && DefaultTextColor == null) || TextColor.Equals(DefaultTextColor))
            {
                // Default Material value according to Type
                if (text is Color textColor)
                {
                    _lblText.TextColor = textColor;
                }
                else if (text is AppThemeBindingExtension theme)
                {
                    _lblText.TextColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _lblText.TextColor = TextColor;
            }
        }
        else
        {
            // Unsupported for current Badge type, ignore
            _lblText.TextColor = DefaultTextColor;
        }
    }
}