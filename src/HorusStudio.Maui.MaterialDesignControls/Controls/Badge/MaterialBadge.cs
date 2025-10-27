namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialBadgeType
{
    /// <summary> Small Badge </summary>
    Small,
    /// <summary> Large Badge </summary>
    Large
}

/// <summary>
/// Badges show notifications, counts, or status information on navigation items and icons and follow Material Design Guidelines. <see href="https://m3.material.io/components/badges/overview">See more</see>.
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
///        Type="MaterialBadgeType.Large"
///        Text="999+"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var badgeSmall = new MaterialBadge()
/// {
///     Type = MaterialBadgeType.Large, 
///     Text = "999+"
/// };
/// </code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/BadgePage.xaml)
/// 
/// </example>
public class MaterialBadge : ContentView
{
    #region Attributes

    private const MaterialBadgeType DefaultBadgeType = MaterialBadgeType.Large;
    private static readonly string DefaultText = string.Empty;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnError, Dark = MaterialDarkTheme.OnError }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialDarkTheme.Error }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.LabelSmall;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly CornerRadius DefaultCornerRadius = new(8);
    private static readonly Thickness DefaultPadding = new(4, 0);
    
    private const double DefaultSmallSize = 6;
    private const double DefaultSize = 16;
    private const double DefaultSmallRadius = 3;
    
    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type">Type</see> bindable property.
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
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialBadge), defaultValue: DefaultText);
    
    /// <summary>
    /// The backing store for the <see cref="TextColor">TextColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialBadge), defaultValueCreator: DefaultTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialBadge), defaultValueCreator: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialBadge), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBadge), defaultValueCreator: DefaultBackgroundColor);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialBadge), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="Padding">Padding</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialBadge), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="AutomationId">AutomationId</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty AutomationIdProperty = BindableProperty.Create(nameof(AutomationId), typeof(string), typeof(MaterialBadge), null);
    
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the badge <see cref="MaterialBadgeType">type</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialBadgeType.Large">MaterialBadgeType.Large</see>
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
    /// <see langword="string.Empty">string.Empty</see>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the <see cref="Color">color</see> for the text of the Badge.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OnError">MaterialLightTheme.OnError</see> - Dark: <see cref="MaterialDarkTheme.OnError">MaterialDarkTheme.OnError</see>
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
    /// Gets or sets the size of the font for the text of this badge.
    /// This is a bindable property.
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
    /// Gets or sets the font family for the text of this badge.
    /// This is a bindable property.
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
    /// Gets or sets a color that describes the background color of the badge.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Error">MaterialLightTheme.Error</see> - Dark: <see cref="MaterialDarkTheme.Error">MaterialDarkTheme.Error</see>
    /// </default>
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
    /// <see cref="DefaultCornerRadius">8</see>
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
    
    /// <summary>
    /// Gets or sets a value that allows the automation framework to find and interact with this element.
    /// </summary>
    /// <remarks>
    /// This value may only be set once on an element.
    /// </remarks>
    public new string AutomationId
    {
        get => (string)GetValue(AutomationIdProperty);
        set => SetValue(AutomationIdProperty, value);
    }
    
    #endregion

    #region Layout

    private MaterialCard _frmContainer = null!;
    private MaterialLabel _lblText = null!;

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
        Utils.Logger.Debug("Creating badge layout");
        try
        {
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;

            _lblText = new MaterialLabel
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            _lblText.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
            _lblText.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _lblText.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _lblText.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            _lblText.SetBinding(Label.AutomationIdProperty, new Binding(nameof(AutomationId), source: this));

            _frmContainer = new MaterialCard
            {
                Content = _lblText
            };
            _frmContainer.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            _frmContainer.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            _frmContainer.SetBinding(MaterialCard.PaddingProperty, new Binding(nameof(Padding), source: this));

            Content = _frmContainer;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating badge layout", ex, this);
        }
    }
    
    private void UpdateLayoutAfterTypeChanged(MaterialBadgeType type)
    {
        Utils.Logger.Debug($"Setting badge type '{type}'");
        try
        {
            var isSmall = type is MaterialBadgeType.Small;
            
            HeightRequest = isSmall ? DefaultSmallSize : DefaultSize;
            CornerRadius = isSmall ? new CornerRadius(DefaultSmallRadius) : DefaultCornerRadius;
            MinimumWidthRequest = isSmall ? DefaultSmallSize : DefaultSize;
            MinimumHeightRequest = isSmall ? DefaultSmallSize : DefaultSize;
            _lblText.IsVisible = !isSmall;

            if (isSmall)
            {
                Padding = 0;
                WidthRequest = DefaultSmallSize;
            }
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException($"ERROR setting badge type '{type}'", ex, this);
        }
    }

    #endregion Methods
}