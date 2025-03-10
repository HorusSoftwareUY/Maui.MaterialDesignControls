using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using HorusStudio.Maui.MaterialDesignControls.Converters;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A navigation drawer <see cref="View" /> let people switch between UI views on larger devices <see href="https://m3.material.io/components/navigation-drawer/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialNavigationDrawer.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialNavigationDrawer
///         Headline="Mail"
///         Command="{Binding TestCommand}"
///         ItemsSource="{Binding Items}" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var navigationDrawer = new MaterialNavigationDrawer
/// {
///     Headline="Mail"
///     Command = TestCommand,
///     ItemsSource = Items
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/NavigationDrawerPage.xaml)
/// 
/// </example>
public class MaterialNavigationDrawer : ContentView
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultHeadlineColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultHeadlineFontSize = _ => MaterialFontSize.TitleSmall;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultHeadlineCharacterSpacing = _ => MaterialFontTracking.TitleSmall;
    private const TextTransform DefaultHeadlineTextTransform = TextTransform.Default;
    private static readonly Thickness DefaultHeadlineMargin = new (4, 16);
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.LabelLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.LabelLarge;
    private const TextTransform DefaultTextTransform = TextTransform.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultActiveIndicatorBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialDarkTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultActiveIndicatorTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimaryContainer, Dark = MaterialDarkTheme.OnPrimaryContainer }.GetValueForCurrentTheme<Color>();
    private const float DefaultActiveIndicatorCornerRadius = 28.0f;
    private static readonly Thickness DefaultActiveIndicatorPadding = new(16,0);
    private const MaterialNavigationDrawerDividerType DefaultDivider = MaterialNavigationDrawerDividerType.Section;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultDividerColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant }.GetValueForCurrentTheme<Color>();
    private static readonly Thickness DefaultDividerMargin = new (16, 1);
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBadgeTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBadgeFontSize = _ => MaterialFontSize.LabelLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultDisabledColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Disable, Dark = MaterialDarkTheme.Disable }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationType = _ => MaterialAnimation.Type;
#nullable enable
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationParameter = _ => MaterialAnimation.Parameter;
#nullable disable
    private const double DefaultIconSize = 24.0;
    private const double DefaultItemHeightRequest = 56.0;
    private const double DefaultItemContentSpacing = 12.0;
    private const MaterialBadgeType DefaultBadgeType = MaterialBadgeType.Large;
    private static readonly Color DefaultBadgeBackground = Colors.Transparent;

    #endregion Attributes

    #region Layout

    private StackLayout _itemsContainer;

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="HeadlineTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineTextColorProperty = BindableProperty.Create(nameof(HeadlineTextColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultHeadlineColor);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontSizeProperty = BindableProperty.Create(nameof(HeadlineFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultHeadlineFontSize);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontFamilyProperty = BindableProperty.Create(nameof(HeadlineFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="HeadlineMargin" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineMarginProperty = BindableProperty.Create(nameof(HeadlineMargin), typeof(Thickness), typeof(MaterialNavigationDrawer), defaultValue: DefaultHeadlineMargin);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontAttributesProperty = BindableProperty.Create(nameof(HeadlineFontAttributes), typeof(FontAttributes), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontAutoScalingEnabledProperty = BindableProperty.Create(nameof(HeadlineFontAutoScalingEnabled), typeof(bool), typeof(MaterialNavigationDrawer), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="HeadlineCharactersSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineCharactersSpacingProperty = BindableProperty.Create(nameof(HeadlineCharactersSpacing), typeof(double), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultHeadlineCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="HeadlineTextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineTextTransformProperty = BindableProperty.Create(nameof(HeadlineTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: DefaultHeadlineTextTransform);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorBackgroundColorProperty = BindableProperty.Create(nameof(ActiveIndicatorBackgroundColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultActiveIndicatorBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorLabelColorProperty = BindableProperty.Create(nameof(ActiveIndicatorLabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultActiveIndicatorTextColor);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorCornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorCornerRadiusProperty = BindableProperty.Create(nameof(ActiveIndicatorCornerRadius), typeof(float), typeof(MaterialNavigationDrawer), defaultValue: DefaultActiveIndicatorCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="LabelFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(nameof(LabelFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontFamilyProperty = BindableProperty.Create(nameof(LabelFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="LabelFontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontAttributesProperty = BindableProperty.Create(nameof(LabelFontAttributes), typeof(FontAttributes), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LabelFontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontAutoScalingEnabledProperty = BindableProperty.Create(nameof(LabelFontAutoScalingEnabled), typeof(bool), typeof(MaterialNavigationDrawer), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="LabelCharactersSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelCharactersSpacingProperty = BindableProperty.Create(nameof(LabelCharactersSpacing), typeof(double), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="LabelTextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextTransformProperty = BindableProperty.Create(nameof(LabelTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: DefaultTextTransform);

    /// <summary>
    /// The backing store for the <see cref="DividerType" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DividerTypeProperty = BindableProperty.Create(nameof(DividerType), typeof(MaterialNavigationDrawerDividerType), typeof(MaterialNavigationDrawer), defaultValue: DefaultDivider);

    /// <summary>
    /// The backing store for the <see cref="DividerColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DividerColorProperty = BindableProperty.Create(nameof(DividerColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultDividerColor);

    /// <summary>
    /// The backing store for the <see cref="BadgeType" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeTypeProperty = BindableProperty.Create(nameof(BadgeType), typeof(MaterialBadgeType), typeof(MaterialNavigationDrawer), defaultValue: DefaultBadgeType);

    /// <summary>
    /// The backing store for the <see cref="BadgeTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeTextColorProperty = BindableProperty.Create(nameof(BadgeTextColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultBadgeTextColor);

    /// <summary>
    /// The backing store for the <see cref="BadgeFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeFontSizeProperty = BindableProperty.Create(nameof(BadgeFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultBadgeFontSize);

    /// <summary>
    /// The backing store for the <see cref="BadgeFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeFontFamilyProperty = BindableProperty.Create(nameof(BadgeFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="BadgeBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeBackgroundColorProperty = BindableProperty.Create(nameof(BadgeBackgroundColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultBadgeBackground);

    /// <summary>
    /// The backing store for the <see cref="SectionTemplate" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionTemplateProperty = BindableProperty.Create(nameof(SectionTemplate), typeof(DataTemplate), typeof(MaterialNavigationDrawer), defaultValue: null, defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="ItemTemplate" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ItemsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<MaterialNavigationDrawerItem>), typeof(MaterialNavigationDrawer), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindableObject, _, _) =>
    {
        if (bindableObject is MaterialNavigationDrawer self)
        {
            self.SetItemSource();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ItemHeightRequest" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemHeightRequestProperty = BindableProperty.Create(nameof(ItemHeightRequest), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultItemHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultAnimationType);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultAnimationParameter);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialNavigationDrawer));

    /// <summary>
    /// The backing store for the <see cref="DisabledLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DisabledLabelColorProperty = BindableProperty.Create(nameof(DisabledLabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValueCreator: DefaultDisabledColor);

    #endregion Bindable Properties

    #region Properties
    
    /// <summary>
    /// Gets or sets the <see cref="HeadlineTextColor" /> for the text of the headline. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color HeadlineTextColor
    {
        get => (Color)GetValue(HeadlineTextColorProperty);
        set => SetValue(HeadlineTextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the headline. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string HeadlineFontFamily
    {
        get => (string)GetValue(HeadlineFontFamilyProperty);
        set => SetValue(HeadlineFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the margin of the headline label. This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(4, 16)
    /// </default>
    public Thickness HeadlineMargin
    {
        get => (Thickness)GetValue(HeadlineMarginProperty);
        set => SetValue(HeadlineMarginProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public FontAttributes HeadlineFontAttributes
    {
        get => (FontAttributes)GetValue(HeadlineFontAttributesProperty);
        set => SetValue(HeadlineFontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public bool HeadlineFontAutoScalingEnabled
    {
        get => (bool)GetValue(HeadlineFontAutoScalingEnabledProperty);
        set => SetValue(HeadlineFontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextTransform.Default">TextTransform.Defaultl</see>
    /// </default>
    public TextTransform HeadlineTextTransform
    {
        get => (TextTransform)GetValue(HeadlineTextTransformProperty);
        set => SetValue(HeadlineTextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the headline. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontTracking.TitleSmall">MaterialFontTracking.TitleSmall</see>
    /// </default>
    public double HeadlineCharactersSpacing
    {
        get => (double)GetValue(HeadlineCharactersSpacingProperty);
        set => SetValue(HeadlineCharactersSpacingProperty, value);
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.TitleSmall">MaterialFontSize.TitleSmall</see>
    /// </default>
    public double HeadlineFontSize
    {
        get => (double)GetValue(HeadlineFontSizeProperty);
        set => SetValue(HeadlineFontSizeProperty, value);
    }

    /// <summary>
    /// Defines the active background color. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.PrimaryContainer">MaterialLightTheme.PrimaryContainer</see> - Dark: <see cref="MaterialDarkTheme.PrimaryContainer">MaterialDarkTheme.PrimaryContainer</see>
    /// </default>
    public Color ActiveIndicatorBackgroundColor
    {
        get => (Color)GetValue(ActiveIndicatorBackgroundColorProperty);
        set => SetValue(ActiveIndicatorBackgroundColorProperty, value);
    }

    /// <summary>
    /// Defines the active indicator label color. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OnPrimaryContainer">MaterialLightTheme.OnPrimaryContainer</see> - Dark: <see cref="MaterialDarkTheme.OnPrimaryContainer">MaterialDarkTheme.OnPrimaryContainer</see>
    /// </default>
    public Color ActiveIndicatorLabelColor
    {
        get => (Color)GetValue(ActiveIndicatorLabelColorProperty);
        set => SetValue(ActiveIndicatorLabelColorProperty, value);
    }

    /// <summary>
    /// Defines the active indicator corner radius. This is a bindable property.
    /// </summary>
    /// <default>
    /// 28.0f
    /// </default>
    public float ActiveIndicatorCornerRadius
    {
        get => (float)GetValue(ActiveIndicatorCornerRadiusProperty);
        set => SetValue(ActiveIndicatorCornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark: <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
    /// </default>
    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelFontSize" /> for the text of each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 14 - Phone: 11
    /// </default>
    public double LabelFontSize
    {
        get => (double)GetValue(LabelFontSizeProperty);
        set => SetValue(LabelFontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for each item label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string LabelFontFamily
    {
        get => (string)GetValue(LabelFontFamilyProperty);
        set => SetValue(LabelFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of each item label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public FontAttributes LabelFontAttributes
    {
        get => (FontAttributes)GetValue(LabelFontAttributesProperty);
        set => SetValue(LabelFontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    /// <default>
    /// <see langword="True" />
    /// </default>
    public bool LabelFontAutoScalingEnabled
    {
        get => (bool)GetValue(LabelFontAutoScalingEnabledProperty);
        set => SetValue(LabelFontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the casing of the label of each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextTransform.Default">TextTransform.Default</see>
    /// </default>
    public TextTransform LabelTextTransform
    {
        get => (TextTransform)GetValue(LabelTextTransformProperty);
        set => SetValue(LabelTextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of each item label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontTracking.LabelLarge">MaterialFontTracking.LabelLarge</see>
    /// </default>
    public double LabelCharactersSpacing
    {
        get => (double)GetValue(LabelCharactersSpacingProperty);
        set => SetValue(LabelCharactersSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets if dividers are visible between sections, items or not visible. This is a bindable property.
    /// </summary>
    /// <default>
    /// Dividers between sections: <see cref="MaterialNavigationDrawerDividerType.Section">NavigationDrawerDividerType.Section</see> 
    /// </default>
    public MaterialNavigationDrawerDividerType DividerType
    {
        get => (MaterialNavigationDrawerDividerType)GetValue(DividerTypeProperty);
        set => SetValue(DividerTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the divider. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OutlineVariant">MaterialLightTheme.OutlineVariant</see> - Dark: <see cref="MaterialDarkTheme.OutlineVariant">MaterialDarkTheme.OutlineVariant</see>
    /// </default>
    public Color DividerColor
    {
        get => (Color)GetValue(DividerColorProperty);
        set => SetValue(DividerColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="MaterialBadgeType" />. This is a bindable property.
    /// </summary>
    public MaterialBadgeType BadgeType
    {
        get => (MaterialBadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color" /> for the badge. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color BadgeTextColor
    {
        get => (Color)GetValue(BadgeTextColorProperty);
        set => SetValue(BadgeTextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="FontSize" /> for the badge label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see>
    /// </default>
    public double BadgeFontSize
    {
        get => (double)GetValue(BadgeFontSizeProperty);
        set => SetValue(BadgeFontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the badge label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string BadgeFontFamily
    {
        get => (string)GetValue(BadgeFontFamilyProperty);
        set => SetValue(BadgeFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color" /> for the badge background. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="Colors.Transparent">Colors.Transparent</see> - Dark: <see cref="Colors.Transparent">Colors.Transparent</see>
    /// </default>
    public Color BadgeBackgroundColor
    {
        get => (Color)GetValue(BadgeBackgroundColorProperty);
        set => SetValue(BadgeBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the section template.
    /// </summary>
    /// <default>
    /// <see langword="null" />
    /// </default>
    public DataTemplate SectionTemplate
    {
        get => (DataTemplate)GetValue(SectionTemplateProperty);
        set => SetValue(SectionTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the item template for each item from ItemsSource. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null" />
    /// </default>
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True" />
    /// </default>
    public IEnumerable<MaterialNavigationDrawerItem> ItemsSource
    {
        get => (IEnumerable<MaterialNavigationDrawerItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the height for each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// 56.0
    /// </default>
    public double ItemHeightRequest
    {
        get => (double)GetValue(ItemHeightRequestProperty);
        set => SetValue(ItemHeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets the command for each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null" />
    /// </default>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
    /// The default value is <see cref="AnimationTypes.Fade"/>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialAnimation.Type">MaterialAnimation.Type</see>
    /// </default>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialAnimation.Parameter">MaterialAnimation.Parameter</see>
    /// </default>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets or sets a custom animation to be executed when a icon is clicked.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color" /> for the label when is disabled. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Disable">MaterialLightTheme.Disable</see> - Dark: <see cref="MaterialDarkTheme.Disable">MaterialDarkTheme.Disable</see>
    /// </default>
    public Color DisabledLabelColor
    {
        get => (Color)GetValue(DisabledLabelColorProperty);
        set => SetValue(DisabledLabelColorProperty, value);
    }

    #endregion Properties

    #region Constructors

    public MaterialNavigationDrawer()
    {
        Content = CreateLayout();
    }

    #endregion Constructors

    #region Methods

    private View CreateLayout()
    {
        Utils.Logger.Debug("Creating navigation drawer layout");
        try
        {
            var container = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
            };
            
            _itemsContainer = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            container.Children.Add(_itemsContainer);
            
            return container;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating navigation drawer layout", ex, this);
            return null;
        }
    }
    
    private void SetItemSource()
    {
        _itemsContainer.Children.Clear();
        CreateItems();
    }

    private void CreateItems()
    {
        Utils.Logger.Debug("Setting items source");
        if (ItemsSource == null) return;

        Utils.Logger.Debug("Creating items");
        var groupedItems = ItemsSource.GroupBy(x => x.Headline).ToList();
        int sectionIndex = 0, totalSections = groupedItems.Count;
        var sectionAdded = false;

        foreach (var group in groupedItems)
        {
            var firstItem = group.FirstOrDefault();
            if (!string.IsNullOrEmpty(firstItem?.Headline)) sectionAdded = AddSection(firstItem);
            
            int itemIndex = 0, totalItems = group.Count();
            foreach (var item in group)
            {
                var itemAdded = AddItem(item);
                if (itemAdded && itemIndex++ < totalItems - 1) AddItemDivider();
            }
            Utils.Logger.Debug($"{itemIndex} of {totalItems} item(s) successfully added {(string.IsNullOrEmpty(firstItem?.Headline) ? string.Empty : $"to section '{firstItem.Headline}'")}");
            
            if (sectionAdded && sectionIndex++ < totalSections - 1)
            {
                AddSectionDivider();
            }
        }
    }

    private bool AddSection(MaterialNavigationDrawerItem item)
    {
        try
        {
            Utils.Logger.Debug($"Creating section '{item.Headline}'");
            return AddItem(item, SectionTemplate ?? GetDefaultSectionDataTemplate(item));
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException($"ERROR creating section '{item.Headline}'", ex, this);
        }
        return false;
    }

    private bool AddItem(MaterialNavigationDrawerItem item)
    {
        try
        {
            Utils.Logger.Debug($"Creating item '{item.Text}'");
            return AddItem(item, ItemTemplate ?? GetDefaultItemDataTemplate(item));
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException($"ERROR creating item '{item.Headline}'", ex, this);
        }
        return false;
    } 
    
    private bool AddItem(MaterialNavigationDrawerItem item, DataTemplate? itemTemplate)
    {
        if (itemTemplate?.CreateContent() is not View itemView) return false;
        
        itemView.BindingContext = item;
        _itemsContainer.Children.Add(itemView);
        return true;
    }
    
    private void AddSectionDivider()
    {
        try
        {
            if (DividerType == MaterialNavigationDrawerDividerType.None) return;
            
            Utils.Logger.Debug("Adding section divider");
            AddDivider();
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR adding section divider", ex, this);
        }
    }
    
    private void AddItemDivider()
    {
        try
        {
            if (DividerType != MaterialNavigationDrawerDividerType.Item) return;

            Utils.Logger.Debug("Adding item divider");
            AddDivider();
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR adding item divider", ex, this);
        }
    }

    private void AddDivider()
    {
        var divider = new MaterialDivider
        {
            Margin = DefaultDividerMargin
        };
        divider.SetBinding(MaterialDivider.ColorProperty, new Binding(nameof(DividerColor), source: this));
        _itemsContainer.Children.Add(divider);
    }

    private MaterialCard CreateItemLayout(MaterialNavigationDrawerItem item)
    {
        try
        {
            Utils.Logger.Debug("Creating item layout");
            var materialCard = new MaterialCard
            {
                Shadow = null,
                Padding = DefaultActiveIndicatorPadding,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            materialCard.SetBinding(IsEnabledProperty, new Binding(nameof(item.IsEnabled), source: item));
            materialCard.SetBinding(HeightRequestProperty, new Binding(nameof(ItemHeightRequest), source: this));
            materialCard.SetBinding(MinimumHeightRequestProperty, new Binding(nameof(ItemHeightRequest), source: this));
            materialCard.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(item.IsSelected), source: item, converter: new IsSelectedToFrameBackgroundConverter(this)));
            materialCard.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(ActiveIndicatorCornerRadius), source: this));
            materialCard.SetBinding(MaterialCard.AnimationProperty, new Binding(nameof(Animation), source: this));
            materialCard.SetBinding(MaterialCard.AnimationParameterProperty, new Binding(nameof(AnimationParameter), source: this));
            materialCard.SetBinding(MaterialCard.CustomAnimationProperty, new Binding(nameof(CustomAnimation), source: this));

            materialCard.Command = new Command(() =>
            {
                if (!item.IsEnabled) return;

                ToggleItemSelection(item);
                ExecuteCommand(item);
            });
        
            return materialCard;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating item layout", ex, this);
            return null;
        }
    }

    private void ExecuteCommand(MaterialNavigationDrawerItem item)
    {
        if (item.IsEnabled && Command?.CanExecute(item) == true)
        {
            Command.Execute(item);
        }
    }
    
    private void ToggleItemSelection(MaterialNavigationDrawerItem item)
    {
        try
        {
            if (item is null) return;
            
            Utils.Logger.Debug($"Selecting item {item.Text}");
            foreach (var selectedItem in ItemsSource.Where(x => x.IsSelected))
            {
                if (selectedItem.Equals(item)) continue;
                selectedItem.IsSelected = false;
            }
            item.IsSelected = true;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR selecting item", ex, this);
        }
    }
    
    private Grid CreateItemContent(MaterialNavigationDrawerItem item)
    {
        try
        {
            Utils.Logger.Debug("Creating item content");
            var contentContainer = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                ColumnDefinitions =
                [
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                ],
            };

            var leadingIcon = CreateLeadingIcon(item);
            leadingIcon.SetValue(Grid.ColumnProperty, 0);
            contentContainer.Children.Add(leadingIcon);
        
            var label = CreateItemLabel(item);
            label.SetValue(Grid.ColumnProperty, 1);
            contentContainer.Children.Add(label);
        
            var badge = CreateItemBadge(item);
            badge.SetValue(Grid.ColumnProperty, 2);
            contentContainer.Children.Add(badge);
        
            var trailingIcon = CreateTrailingIcon(item);
            trailingIcon.SetValue(Grid.ColumnProperty, 2);
            contentContainer.Children.Add(trailingIcon);

            return contentContainer;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating item content", ex, this);
            return null;
        }
    }
    
    private View CreateLeadingIcon(MaterialNavigationDrawerItem item) => CreateItemIcon(item, true);

    private View CreateTrailingIcon(MaterialNavigationDrawerItem item)=> CreateItemIcon(item, false);
    
    private View CreateItemIcon(MaterialNavigationDrawerItem item, bool isLeadingIcon)
    {
        var icon = new Image
        {
            HeightRequest = DefaultIconSize,
            MinimumHeightRequest = DefaultIconSize,
            WidthRequest = DefaultIconSize,
            MinimumWidthRequest = DefaultIconSize,
            VerticalOptions = LayoutOptions.Center
        };
        var tintColorBehavior = new IconTintColorBehavior();
        tintColorBehavior.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(item.IsEnabled), source: item, converter: new ItemEnabledToColorConverter(this)));
        icon.Behaviors.Add(tintColorBehavior);

        if (isLeadingIcon)
        {
            icon.Margin = new Thickness(0,0,DefaultItemContentSpacing,0);
            SetLeadingIconVisibilityPropertyBindings(icon, item);
            SetLeadingIconSourcePropertyBindings(icon, item);
        }
        else
        {
            icon.Margin = new Thickness(DefaultItemContentSpacing,0,0,0);
            SetTrailingIconVisibilityPropertyBindings(icon, item);
            SetTrailingIconSourcePropertyBindings(icon, item);
        }

        return icon;
    }
    
    private View CreateItemLabel(MaterialNavigationDrawerItem item)
    {
        var label = new MaterialLabel
        {
            VerticalTextAlignment = TextAlignment.Center
        };

        SetLabelTextColorPropertyBindings(label, item);

        label.SetBinding(Label.TextProperty, new Binding(nameof(item.Text), source: item));
        label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(LabelFontFamily), source: this));
        label.SetBinding(Label.FontSizeProperty, new Binding(nameof(LabelFontSize), source: this));
        label.SetBinding(Label.FontAttributesProperty, new Binding(nameof(LabelFontAttributes), source: this));
        label.SetBinding(Label.FontAutoScalingEnabledProperty, new Binding(nameof(LabelFontAutoScalingEnabled), source: this));
        label.SetBinding(Label.CharacterSpacingProperty, new Binding(nameof(LabelCharactersSpacing), source: this));
        label.SetBinding(Label.TextTransformProperty, new Binding(nameof(LabelTextTransform), source: this));

        return label;
    }

    private View CreateItemBadge(MaterialNavigationDrawerItem item)
    {
        var badge = new MaterialBadge();
        
        badge.SetBinding(IsVisibleProperty, new Binding(nameof(item.BadgeText), source: item, converter: new IsNotNullOrEmptyConverter()));
        badge.SetBinding(MaterialBadge.TypeProperty, new Binding(nameof(BadgeType), source: this));
        badge.SetBinding(MaterialBadge.TextColorProperty, new Binding(nameof(BadgeTextColor), source: this));
        badge.SetBinding(MaterialBadge.FontSizeProperty, new Binding(nameof(BadgeFontSize), source: this));
        badge.SetBinding(MaterialBadge.FontFamilyProperty, new Binding(nameof(BadgeFontFamily), source: this));
        badge.SetBinding(MaterialBadge.BackgroundColorProperty, new Binding(nameof(BadgeBackgroundColor), source: this));
        badge.SetBinding(MaterialBadge.TextProperty, new Binding(nameof(item.BadgeText), source: item));
        badge.Margin = new Thickness(DefaultItemContentSpacing,0,0,0);
        
        return badge;
    }
    
    private DataTemplate GetDefaultItemDataTemplate(MaterialNavigationDrawerItem item)
    {
        return new DataTemplate(() =>
        {
            var itemLayout = CreateItemLayout(item);
            var itemContent = CreateItemContent(item);
            itemLayout.Content = itemContent;
            
            return itemLayout;
        });
    }

    private DataTemplate GetDefaultSectionDataTemplate(MaterialNavigationDrawerItem item)
    {
        return new DataTemplate(() =>
        {
            var label = new MaterialLabel
            {
                VerticalTextAlignment = TextAlignment.Center,
                Padding = new Thickness(DefaultItemContentSpacing, 0)
            };

            label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(HeadlineTextColor), source: this));
            label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(HeadlineFontFamily), source: this));
            label.SetBinding(Label.FontSizeProperty, new Binding(nameof(HeadlineFontSize), source: this));
            label.SetBinding(Label.FontAttributesProperty, new Binding(nameof(HeadlineFontAttributes), source: this));
            label.SetBinding(Label.FontAutoScalingEnabledProperty, new Binding(nameof(HeadlineFontAutoScalingEnabled), source: this));
            label.SetBinding(Label.CharacterSpacingProperty, new Binding(nameof(HeadlineCharactersSpacing), source: this));
            label.SetBinding(Label.TextTransformProperty, new Binding(nameof(HeadlineTextTransform), source: this));
            label.SetBinding(MarginProperty, new Binding(nameof(HeadlineMargin), source: this));
            label.SetBinding(Label.TextProperty, new Binding(nameof(item.Headline), source: item));
            label.SetBinding(IsVisibleProperty, new Binding(nameof(item.Headline), source: item, converter: new IsNotNullOrEmptyConverter()));
            
            return label;
        });
    }

    #endregion Methods

    #region Setters

    private void SetLabelTextColorPropertyBindings(MaterialLabel label, MaterialNavigationDrawerItem item)
    {
        label.SetBinding(MaterialLabel.TextColorProperty, new MultiBinding
        {
            Bindings = new Collection<BindingBase>
            {
                new Binding(nameof(item.IsEnabled), source: item),
                new Binding(nameof(item.IsSelected), source: item)
            },
            Converter = new MultiValueConverter((values, targetType, parameter, culture) =>
            {
                var isEnabled = (bool)values[0];
                var isSelected = (bool)values[1];
                return isEnabled ? (isSelected ? ActiveIndicatorLabelColor : LabelColor) : DisabledLabelColor;
            })
        });
    }

    private void SetLeadingIconVisibilityPropertyBindings(Image image, MaterialNavigationDrawerItem item)
    {
        image.SetBinding(IsVisibleProperty, new MultiBinding
        {
            Bindings = new Collection<BindingBase>
            {
                new Binding(nameof(item.IsSelected), source: item),
                new Binding(nameof(item.SelectedLeadingIcon), source: item),
                new Binding(nameof(item.LeadingIcon), source: item),
            },
            Converter = new MultiValueConverter(GetIconVisibility)
        });
    }

    private void SetLeadingIconSourcePropertyBindings(Image image, MaterialNavigationDrawerItem item)
    {
        image.SetBinding(Image.SourceProperty, new MultiBinding
        {
            Bindings = new Collection<BindingBase>
            {
                new Binding(nameof(item.IsSelected), source: item),
                new Binding(nameof(item.SelectedLeadingIcon), source: item),
                new Binding(nameof(item.LeadingIcon), source: item),
            },
            Converter = new MultiValueConverter(GetIconSource)
        });
    }

    private void SetTrailingIconVisibilityPropertyBindings(Image image, MaterialNavigationDrawerItem item)
    {
        image.SetBinding(IsVisibleProperty, new MultiBinding
        {
            Bindings = new Collection<BindingBase>
            {
                new Binding(nameof(item.IsSelected), source: item),
                new Binding(nameof(item.SelectedTrailingIcon), source: item),
                new Binding(nameof(item.TrailingIcon), source: item),
            },
            Converter = new MultiValueConverter(GetIconVisibility)
        });
    }

    private void SetTrailingIconSourcePropertyBindings(Image image, MaterialNavigationDrawerItem item)
    {
        image.SetBinding(Image.SourceProperty, new MultiBinding
        {
            Bindings = new Collection<BindingBase>
            {
                new Binding(nameof(item.IsSelected), source: item),
                new Binding(nameof(item.SelectedTrailingIcon), source: item),
                new Binding(nameof(item.TrailingIcon), source: item),
            },
            Converter = new MultiValueConverter(GetIconSource)
        });
    }

    private object GetIconVisibility(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var isSelected = (bool)values[0];
            var selectedIcon = (ImageSource?)values[1];
            var unselectedIcon = (ImageSource?)values[2];

            selectedIcon ??= unselectedIcon;

            return (isSelected && selectedIcon is not null) ||
                   (!isSelected && unselectedIcon is not null);
        }
        catch
        {
            return false;
        }
    }
    
    private object GetIconSource(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var isSelected = (bool)values[0];
            var selectedIcon = (ImageSource?)values[1];
            var unselectedIcon = (ImageSource?)values[2];

            selectedIcon ??= unselectedIcon;

            return isSelected ? selectedIcon : unselectedIcon;
        }
        catch
        {
            return null;
        }
    }
    
    #endregion Setters

    #region Converters

    private class IsSelectedToFrameBackgroundConverter(MaterialNavigationDrawer drawer) : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? drawer.ActiveIndicatorBackgroundColor : Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    private class ItemEnabledToColorConverter(MaterialNavigationDrawer drawer) : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? drawer.LabelColor : drawer.DisabledLabelColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    private class MultiValueConverter(Func<object[], Type, object, CultureInfo, object> convert) : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return convert(values, targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion Converters

    /// <summary>
    /// Divider types for <see cref="MaterialNavigationDrawer" />
    /// </summary>
    public enum MaterialNavigationDrawerDividerType
    {
        /// <summary>Between sections</summary>
        Section,
        /// <summary>Between items</summary>
        Item,
        /// <summary>No divider</summary>
        None
    }
}