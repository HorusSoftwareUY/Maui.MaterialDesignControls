namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialBadgeType
{
    /// <summary> Small Badge </summary>
    Small,
    /// <summary> Large Badge </summary>
    Large
}

/// <summary>
/// Badges show notifications, counts, or status information on navigation items and icons <see href="https://m3.material.io/components/badges/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialBadge.jpg</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialBadge
///        Text="badge"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var badgeSmall = new MaterialBadge()
/// {
///     Type = MaterialBadgeType.Small, 
///     Text = "Badge small"
/// };
/// </code>
/// </example>
public class MaterialBadge : ContentView
{
    #region Attributes

    private readonly static MaterialBadgeType DefaultBadgeType = MaterialBadgeType.Large;
    private readonly static string DefaultText = string.Empty;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnError, Dark = MaterialDarkTheme.OnError }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialDarkTheme.Error }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultFontSize = MaterialFontSize.LabelSmall;
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(8);
    private readonly static Thickness DefaultPadding = new Thickness(16, 0);
    
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
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialBadge), defaultValue: DefaultText, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialBadge self)
        {
            self.SetText(self.Type);
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialBadge), defaultValue: DefaultTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialBadge), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialBadge), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBadge), defaultValue: DefaultBackgroundColor);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialBadge), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="Padding" /> bindable property.
    /// </summary>
    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialBadge), defaultValue: DefaultPadding, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialBadge self)
        {
            self.SetPadding(self.Type);
        }
    });
    
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the badge type according to <see cref="MaterialBadgeType"/> enum. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialBadgeType.Large"> MaterialBadgeType.Large </see>
    /// </default>
    public MaterialBadgeType Type
    {
        get => (MaterialBadgeType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the text displayed as the content of the badge. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Empty"/>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the Badge. This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.OnError">MaterialLightTheme.OnError</see> - Dark: <see cref="MaterialDarkTheme.OnError">MaterialDarkTheme.OnError</see>
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
    /// Gets or sets the size of the font for the text of this badge. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelSmall">MaterialFontSize.LabelSmall</see> / Tablet: 14 - Phone: 11
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
    /// Gets or sets the font family for the text of this badge. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the background color of the badge. This is a bindable property.
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.Error">MaterialLightTheme.Error</see> - Dark: <see cref="MaterialDarkTheme.Error">MaterialDarkTheme.Error</see>
    /// </default>
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the corner radius for the Badge, in device-independent units. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DefaultCornerRadius">8</see>
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the padding for the Badge. This is a bindable property.
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

    #region Constructors

    public MaterialBadge()
    {
        CreateLayout();
        if (Type == DefaultBadgeType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    #endregion Constructors

    #region Methods

    private void CreateLayout()
    {
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;
        
        _frmContainer = new MaterialCard()
        {
            BackgroundColor = BackgroundColor,
            CornerRadius = CornerRadius,
            Padding = Padding
        };

        _lblText = new Label()
        {
            TextColor = TextColor,
            FontSize = FontSize,
            FontFamily = FontFamily,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
        };

        _frmContainer.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _frmContainer.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        _lblText.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
        _lblText.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _lblText.SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _lblText.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        
        _frmContainer.Content = _lblText;
        Content = _frmContainer;
        
        ResizeControl();
    }
    
    private void UpdateLayoutAfterTypeChanged(MaterialBadgeType type)
    {
        SetText(type);
        SetSizeControl(type);
    }

    private void SetSizeControl(MaterialBadgeType type)
    {
        HeightRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        CornerRadius = new CornerRadius((type is MaterialBadgeType.Small) ? 3 : 8);
        MinimumWidthRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        MinimumHeightRequest = (type is MaterialBadgeType.Small) ? 6 : 16;
        _lblText.IsVisible = (type is not MaterialBadgeType.Small);

        if (type is MaterialBadgeType.Small)
        {
            _frmContainer.Padding = new Thickness(0);
            WidthRequest = 6;
        }
    }

    private void ResizeControl()
    {
        _frmContainer.Padding = (!string.IsNullOrEmpty(_lblText.Text) && _lblText.Text.Length >= 2)? new Thickness(4, 0) : new Thickness(0);
        _frmContainer.WidthRequest = -1;
    }

    private void SetText(MaterialBadgeType type)
    {
        _lblText.Text = Text;
        ResizeControl();
    }

    private void SetPadding(MaterialBadgeType type)
    {
        _frmContainer.Padding = Padding;
    }

    #endregion Methods
}