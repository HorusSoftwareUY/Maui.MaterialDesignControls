using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Converters;
using HorusStudio.Maui.MaterialDesignControls.Utils;

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

    private static readonly Color DefaultHeadlineColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultSectionLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultActiveIndicatorBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialDarkTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultActiveIndicatorLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimaryContainer, Dark = MaterialDarkTheme.OnPrimaryContainer }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultDividerColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultBadgeTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultDisabledLabelTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Disable, Dark = MaterialDarkTheme.Disable }.GetValueForCurrentTheme<Color>();
    private static readonly string DefaultFontFamily = MaterialFontFamily.Default;
    private static readonly Thickness DefaultHeadlineMargin = new Thickness(-1);
    private static readonly double DefaultHeadlineCharacterSpacing = MaterialFontTracking.TitleSmall;
    private static readonly TextTransform DefaultTextTransform = TextTransform.Default;
    private static readonly TextTransform DefaultLabelTextTransform = TextTransform.Default;
    private static readonly TextTransform DefaultSectionLabelTextTransform = TextTransform.Default;
    private static readonly Thickness DefaultSectionlabelMargin = new Thickness(-1);
    private static readonly double DefaultLabelCharacterSpacing = MaterialFontTracking.LabelLarge;
    private static readonly double DefaultHeadlineFontSize = MaterialFontSize.TitleSmall;
    private static readonly double DefaultLabelFontSize = MaterialFontSize.LabelLarge;
    private static readonly double DefaultBadgeFontSize = MaterialFontSize.LabelLarge;
    private static readonly double DefaultSectionLabelFontSize = MaterialFontSize.TitleSmall;
    private static readonly double DefaultSectionLabelCharacterSpacing = MaterialFontTracking.TitleSmall;
    private static readonly double DefaultItemHeightRequest = 56.0;
    private static readonly float DefaultActiveIndicatorCornerRadius = 28.0f;
    private static readonly AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
#nullable enable
    private static readonly double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable

    #endregion Attributes

    #region Layout

    private StackLayout _itemsContainer;

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Headline" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HeadlineTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineTextColorProperty = BindableProperty.Create(nameof(HeadlineTextColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultHeadlineColor);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontSizeProperty = BindableProperty.Create(nameof(HeadlineFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultHeadlineFontSize);

    /// <summary>
    /// The backing store for the <see cref="HeadlineFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineFontFamilyProperty = BindableProperty.Create(nameof(HeadlineFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: DefaultFontFamily);

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
    public static readonly BindableProperty HeadlineCharactersSpacingProperty = BindableProperty.Create(nameof(HeadlineCharactersSpacing), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultHeadlineCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="HeadlineTextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineTextTransformProperty = BindableProperty.Create(nameof(HeadlineTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: DefaultTextTransform);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorBackgroundColorProperty = BindableProperty.Create(nameof(ActiveIndicatorBackgroundColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultActiveIndicatorBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorLabelColorProperty = BindableProperty.Create(nameof(ActiveIndicatorLabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultActiveIndicatorLabelColor);

    /// <summary>
    /// The backing store for the <see cref="ActiveIndicatorCornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveIndicatorCornerRadiusProperty = BindableProperty.Create(nameof(ActiveIndicatorCornerRadius), typeof(float), typeof(MaterialNavigationDrawer), defaultValue: DefaultActiveIndicatorCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultLabelColor);

    /// <summary>
    /// The backing store for the <see cref="LabelFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(nameof(LabelFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultLabelFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontFamilyProperty = BindableProperty.Create(nameof(LabelFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: DefaultFontFamily);

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
    public static readonly BindableProperty LabelCharactersSpacingProperty = BindableProperty.Create(nameof(LabelCharactersSpacing), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultLabelCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="LabelTextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextTransformProperty = BindableProperty.Create(nameof(LabelTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: DefaultLabelTextTransform);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelColorProperty = BindableProperty.Create(nameof(SectionLabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultSectionLabelColor);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelFontSizeProperty = BindableProperty.Create(nameof(SectionLabelFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultSectionLabelFontSize);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelFontFamilyProperty = BindableProperty.Create(nameof(SectionLabelFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelFontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelFontAttributesProperty = BindableProperty.Create(nameof(SectionLabelFontAttributes), typeof(FontAttributes), typeof(MaterialNavigationDrawer), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelFontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelFontAutoScalingEnabledProperty = BindableProperty.Create(nameof(SectionLabelFontAutoScalingEnabled), typeof(bool), typeof(MaterialNavigationDrawer), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelCharactersSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelCharactersSpacingProperty = BindableProperty.Create(nameof(SectionLabelCharactersSpacing), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultSectionLabelCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelTextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelTextTransformProperty = BindableProperty.Create(nameof(SectionLabelTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: DefaultSectionLabelTextTransform);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelMargin" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelMarginProperty = BindableProperty.Create(nameof(SectionLabelMargin), typeof(Thickness), typeof(MaterialNavigationDrawer), defaultValue: DefaultSectionlabelMargin);

    /// <summary>
    /// The backing store for the <see cref="SectionDividerIsVisible" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionDividerIsVisibleProperty = BindableProperty.Create(nameof(SectionDividerIsVisible), typeof(bool), typeof(MaterialNavigationDrawer), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="ItemDividerIsVisible" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemDividerIsVisibleProperty = BindableProperty.Create(nameof(ItemDividerIsVisible), typeof(bool), typeof(MaterialNavigationDrawer), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="DividerColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DividerColorProperty = BindableProperty.Create(nameof(DividerColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultDividerColor);

    /// <summary>
    /// The backing store for the <see cref="BadgeType" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeTypeProperty = BindableProperty.Create(nameof(BadgeType), typeof(MaterialBadgeType), typeof(MaterialNavigationDrawer), defaultValue: MaterialBadgeType.Large);

    /// <summary>
    /// The backing store for the <see cref="BadgeTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeTextColorProperty = BindableProperty.Create(nameof(BadgeTextColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultBadgeTextColor);

    /// <summary>
    /// The backing store for the <see cref="BadgeFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeFontSizeProperty = BindableProperty.Create(nameof(BadgeFontSize), typeof(double), typeof(MaterialNavigationDrawer), defaultValue: DefaultBadgeFontSize);

    /// <summary>
    /// The backing store for the <see cref="BadgeFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeFontFamilyProperty = BindableProperty.Create(nameof(BadgeFontFamily), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="BadgeBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BadgeBackgroundColorProperty = BindableProperty.Create(nameof(BadgeBackgroundColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: Colors.Transparent);

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
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialNavigationDrawer), defaultValue: DefaultAnimationType);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialNavigationDrawer), defaultValue: DefaultAnimationParameter);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialNavigationDrawer));

    /// <summary>
    /// The backing store for the <see cref="DisabledLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DisabledLabelColorProperty = BindableProperty.Create(nameof(DisabledLabelColor), typeof(Color), typeof(MaterialNavigationDrawer), defaultValue: DefaultDisabledLabelTextColor);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the <see cref="Headline" /> for the headline. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string Headline
    {
        get => (string)GetValue(HeadlineProperty);
        set => SetValue(HeadlineProperty, value);
    }

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
    /// Thickness(-1)
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
    /// Gets or sets the <see cref="Color" /> for the section label. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color SectionLabelColor
    {
        get => (Color)GetValue(SectionLabelColorProperty);
        set => SetValue(SectionLabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="FontSize" /> for the section label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.TitleSmall">MaterialFontSize.TitleSmall</see>
    /// </default>
    public double SectionLabelFontSize
    {
        get => (double)GetValue(SectionLabelFontSizeProperty);
        set => SetValue(SectionLabelFontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the section label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string SectionLabelFontFamily
    {
        get => (string)GetValue(SectionLabelFontFamilyProperty);
        set => SetValue(SectionLabelFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the section label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null" />
    /// </default>
    public FontAttributes SectionLabelFontAttributes
    {
        get => (FontAttributes)GetValue(SectionLabelFontAttributesProperty);
        set => SetValue(SectionLabelFontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    /// <default>
    /// <see langword="True" />
    /// </default>
    public bool SectionLabelFontAutoScalingEnabled
    {
        get => (bool)GetValue(SectionLabelFontAutoScalingEnabledProperty);
        set => SetValue(SectionLabelFontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the casing of the section label of each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextTransform.Default">TextTransform.Default</see>
    /// </default>
    public TextTransform SectionLabelTextTransform
    {
        get => (TextTransform)GetValue(SectionLabelTextTransformProperty);
        set => SetValue(SectionLabelTextTransformProperty, value);
    }

    /// <summary>
    /// Defines the margin of the section label. This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(-1)
    /// </default>
    public Thickness SectionLabelMargin
    {
        get => (Thickness)GetValue(SectionLabelMarginProperty);
        set => SetValue(SectionLabelMarginProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of each item label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontTracking.TitleSmall">MaterialFontTracking.TitleSmall</see>
    /// </default>
    public double SectionLabelCharactersSpacing
    {
        get => (double)GetValue(SectionLabelCharactersSpacingProperty);
        set => SetValue(SectionLabelCharactersSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets if show a divider between sections. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True" />
    /// </default>
    public bool SectionDividerIsVisible
    {
        get => (bool)GetValue(SectionDividerIsVisibleProperty);
        set => SetValue(SectionDividerIsVisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets if show a divider between items. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True" />
    /// </default>
    public bool ItemDividerIsVisible
    {
        get => (bool)GetValue(ItemDividerIsVisibleProperty);
        set => SetValue(ItemDividerIsVisibleProperty, value);
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
        var container = new StackLayout
        {
            Spacing = 0,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
        };

        CreateHeadline(container);
        
        _itemsContainer = new StackLayout
        {
            Spacing = 0,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
        };
        container.Children.Add(_itemsContainer);
        
        return container;
    }
    
    private void CreateHeadline(Layout container)
    {
        var lblHeadline = new MaterialLabel
        {
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = HeadlineMargin != new Thickness(-1) ? HeadlineMargin : new Thickness(0, 16),
            VerticalOptions = LayoutOptions.Center,
            TextColor = HeadlineTextColor,
            IsVisible = !string.IsNullOrWhiteSpace(Headline),
            Text = Headline,
            FontSize = HeadlineFontSize,
            FontFamily = HeadlineFontFamily,
            Padding = new Thickness(12, 0)
        };

        lblHeadline.SetBinding(MarginProperty, new Binding(nameof(HeadlineMargin), source: this, converter: new HeadlineMarginConverter()));
        lblHeadline.SetBinding(IsVisibleProperty, new Binding(nameof(Headline), source: this, converter: new IsNotNullOrEmptyConverter()));
        lblHeadline.SetBinding(Label.TextProperty, new Binding(nameof(Headline), source: this));
        lblHeadline.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(HeadlineTextColor), source: this));
        lblHeadline.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(HeadlineFontFamily), source: this));
        lblHeadline.SetBinding(Label.FontSizeProperty, new Binding(nameof(HeadlineFontSize), source: this));
        lblHeadline.SetBinding(Label.TextTransformProperty, new Binding(nameof(HeadlineTextTransform), source: this));
        lblHeadline.SetBinding(Label.FontAttributesProperty, new Binding(nameof(HeadlineFontAttributes), source: this));
        lblHeadline.SetBinding(Label.FontAutoScalingEnabledProperty, new Binding(nameof(HeadlineFontAutoScalingEnabled), source: this));
        lblHeadline.SetBinding(Label.CharacterSpacingProperty, new Binding(nameof(HeadlineCharactersSpacing), source: this));

        container.Children.Add(lblHeadline);
    }
    
    private void SetItemSource()
    {
        _itemsContainer.Children.Clear();
        CreateItems();
    }

    private void CreateItems()
    {
        if (ItemsSource == null) return;

        var groupedItems = ItemsSource.GroupBy(x => x.Section).ToList();
        int sectionIndex = 0, totalSections = groupedItems.Count;
        var sectionAdded = false;

        foreach (var group in groupedItems)
        {
            var firstItem = group.FirstOrDefault();
            if (!string.IsNullOrEmpty(firstItem?.Section)) sectionAdded = AddSection(firstItem);
            
            int itemIndex = 0, totalItems = group.Count();
            foreach (var item in group)
            {
                var itemAdded = AddItem(item);
                if (itemAdded && itemIndex++ < totalItems - 1) AddItemDivider();
            }

            if (sectionAdded && sectionIndex++ < totalSections - 1)
            {
                AddSectionDivider();
            }
        }
    }
    
    private bool AddSection(MaterialNavigationDrawerItem item) => AddItem(item, SectionTemplate ?? GetDefaultSectionDataTemplate(item.Section));
    
    private bool AddItem(MaterialNavigationDrawerItem item) => AddItem(item, ItemTemplate ?? GetDefaultItemDataTemplate(item));
    
    private bool AddItem(MaterialNavigationDrawerItem item, DataTemplate? itemTemplate)
    {
        if (itemTemplate?.CreateContent() is not View itemView) return false;
        
        itemView.BindingContext = item;
        _itemsContainer.Children.Add(itemView);
        return true;
    }
    
    private void AddSectionDivider()
    {
        if (!SectionDividerIsVisible) return;

        var divider = new MaterialDivider
        {
            Color = DividerColor,
            Margin = new Thickness(0, 0, 0, 16)
        };
        _itemsContainer.Children.Add(divider);
    }
    
    private void AddItemDivider()
    {
        if (!ItemDividerIsVisible) return;

        var divider = new MaterialDivider
        {
            Color = DividerColor,
            Margin = new Thickness(8, 0)
        };
        _itemsContainer.Children.Add(divider);
    }
    
    private MaterialCard CreateItemLayout(MaterialNavigationDrawerItem item)
    {
        var materialCard = new MaterialCard
        {
            Shadow = null,
            BorderColor = Colors.Transparent,
            Padding = new Thickness(16, 0),
            HeightRequest = ItemHeightRequest,
            MinimumHeightRequest = ItemHeightRequest,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            BackgroundColor = item.IsSelected ? ActiveIndicatorBackgroundColor : Colors.Transparent,
            CornerRadius = ActiveIndicatorCornerRadius,
            Animation = Animation,
            AnimationParameter = AnimationParameter,
            CustomAnimation = CustomAnimation,
            IsEnabled = item.IsEnabled
        };

        materialCard.SetBinding(IsEnabledProperty, new Binding(nameof(item.IsEnabled), source: item));
        materialCard.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(item.IsSelected), source: item, converter: new IsSelectedToFrameBackgroundConverter(this)));

        materialCard.Command = new Command(() =>
        {
            if ((item.IsSelected && item.ShowActiveIndicator) || !item.IsEnabled)
            {
                ExecuteCommand(item);
                return;
            }

            ToggleItemSelection(item);
            ExecuteCommand(item);
        });
        
        return materialCard;
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
        foreach (var selectedItem in ItemsSource.Where(x => x.IsSelected))
        {
            if (selectedItem.Equals(item)) continue;
            selectedItem.IsSelected = false;
        }
        item.IsSelected = !item.IsSelected;
    }
    
    private Grid CreateItemContent(MaterialNavigationDrawerItem item)
    {
        var contentContainer = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            ColumnSpacing = 12,
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                //new ColumnDefinition { Width = GridLength.Auto }
            ],
        };

        var leadingIcon = CreateLeadingIcon(item);
        leadingIcon.SetValue(Grid.ColumnProperty, 0);

        var label = CreateItemLabel(item);
        label.SetValue(Grid.ColumnProperty, 1);

        var badge = CreateItemBadge(item);
        badge.SetValue(Grid.ColumnProperty, 2);

        var trailingIcon = CreateTrailingIcon(item);
        trailingIcon.SetValue(Grid.ColumnProperty, 2);

        contentContainer.Children.Add(leadingIcon);
        contentContainer.Children.Add(label);
        contentContainer.Children.Add(badge);
        contentContainer.Children.Add(trailingIcon);

        return contentContainer;
    }
    
    private View CreateLeadingIcon(MaterialNavigationDrawerItem item) => CreateItemIcon(item, true);

    private View CreateTrailingIcon(MaterialNavigationDrawerItem item)=> CreateItemIcon(item, false);
    
    private View CreateItemIcon(MaterialNavigationDrawerItem item, bool isLeadingIcon)
    {
        var selectedSource = isLeadingIcon ? item.SelectedLeadingIcon : item.SelectedTrailingIcon;
        var unselectedSource = isLeadingIcon ? item.LeadingIcon : item.TrailingIcon;
        
        var icon = new Image
        {
            HeightRequest = 24,
            MinimumHeightRequest = 24,
            WidthRequest = 24,
            MinimumWidthRequest = 24,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = (item.IsSelected && !string.IsNullOrEmpty(selectedSource)) || (!item.IsSelected && !string.IsNullOrEmpty(unselectedSource)),
            Source = item.IsSelected ? selectedSource : unselectedSource,
        };

        if (isLeadingIcon)
        {
            SetLeadingIconVisibilityPropertyBindings(icon, item);
            SetLeadingIconSourcePropertyBindings(icon, item);
        }
        else
        {
            SetTrailingIconVisibilityPropertyBindings(icon, item);
            SetTrailingIconSourcePropertyBindings(icon, item);
        }

        return icon;
    }
    
    private View CreateItemLabel(MaterialNavigationDrawerItem item)
    {
        var label = new MaterialLabel
        {
            Text = item.Text.Trim(),
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = LabelFontSize,
            FontFamily = LabelFontFamily,
            TextColor = item.IsEnabled ? item.IsSelected ? ActiveIndicatorLabelColor : LabelColor : DisabledLabelColor
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
        var badge = new MaterialBadge
        {
            Type = BadgeType,
            TextColor = BadgeTextColor,
            FontSize = BadgeFontSize,
            FontFamily = BadgeFontFamily,
            BackgroundColor = BadgeBackgroundColor,
            Text = item.BadgeText,
            IsVisible = !string.IsNullOrWhiteSpace(item.BadgeText)
        };
        
        badge.SetBinding(IsVisibleProperty, new Binding(nameof(item.BadgeText), source: item, converter: new IsNotNullOrEmptyConverter()));
        badge.SetBinding(MaterialBadge.TypeProperty, new Binding(nameof(BadgeType), source: this));
        badge.SetBinding(MaterialBadge.TextColorProperty, new Binding(nameof(BadgeTextColor), source: this));
        badge.SetBinding(MaterialBadge.FontSizeProperty, new Binding(nameof(BadgeFontSize), source: this));
        badge.SetBinding(MaterialBadge.FontFamilyProperty, new Binding(nameof(BadgeFontFamily), source: this));
        badge.SetBinding(MaterialBadge.BackgroundColorProperty, new Binding(nameof(BadgeBackgroundColor), source: this));
        badge.SetBinding(MaterialBadge.TextProperty, new Binding(nameof(item.BadgeText), source: item));
        
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

    private DataTemplate GetDefaultSectionDataTemplate(string section)
    {
        return new DataTemplate(() =>
        {
            var label = new MaterialLabel
            {
                Text = section.Trim(),
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = SectionLabelFontSize,
                FontFamily = SectionLabelFontFamily,
                TextColor = SectionLabelColor,
                Padding = new Thickness(12, 0),
                Margin = SectionLabelMargin != new Thickness(-1)
                   ? SectionLabelMargin
                   : new Thickness(0, SectionDividerIsVisible ? 0 : 16, 0, 16),
                CharacterSpacing = SectionLabelCharactersSpacing,
                FontAttributes = SectionLabelFontAttributes,
                FontAutoScalingEnabled = SectionLabelFontAutoScalingEnabled
            };

            label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SectionLabelColor), source: this));
            label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(SectionLabelFontFamily), source: this));
            label.SetBinding(Label.FontSizeProperty, new Binding(nameof(SectionLabelFontSize), source: this));
            label.SetBinding(Label.FontAttributesProperty, new Binding(nameof(SectionLabelFontAttributes), source: this));
            label.SetBinding(Label.FontAutoScalingEnabledProperty, new Binding(nameof(SectionLabelFontAutoScalingEnabled), source: this));
            label.SetBinding(Label.CharacterSpacingProperty, new Binding(nameof(SectionLabelCharactersSpacing), source: this));
            label.SetBinding(Label.TextTransformProperty, new Binding(nameof(SectionLabelTextTransform), source: this));

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
            Converter = new MultiValueConverter((values, targetType, parameter, culture) =>
            {
                var isSelected = (bool)values[0];
                var selectedLeadingIcon = (string)values[1];
                var unselectedLeadingIcon = (string)values[2];

                return (isSelected && !string.IsNullOrEmpty(selectedLeadingIcon) || (!isSelected && !string.IsNullOrEmpty(unselectedLeadingIcon)));
            })
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
            Converter = new MultiValueConverter((values, targetType, parameter, culture) =>
            {
                var isSelected = (bool)values[0];
                var selectedLeadingIcon = (string)values[1];
                var unselectedLeadingIcon = (string)values[2];

                return isSelected ? selectedLeadingIcon : unselectedLeadingIcon;
            })
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
            Converter = new MultiValueConverter((values, targetType, parameter, culture) =>
            {
                var isSelected = (bool)values[0];
                var selectedTrailingIcon = (string)values[1];
                var unselectedTrailingIcon = (string)values[2];

                return (isSelected && !string.IsNullOrEmpty(selectedTrailingIcon) || (!isSelected && !string.IsNullOrEmpty(unselectedTrailingIcon)));
            })
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
            Converter = new MultiValueConverter((values, targetType, parameter, culture) =>
            {
                var isSelected = (bool)values[0];
                var selectedTrailingIcon = (string)values[1];
                var unselectedTrailingIcon = (string)values[2];

                return isSelected ? selectedTrailingIcon : unselectedTrailingIcon;
            })
        });
    }

    #endregion Setters

    /// <summary>
    /// this class is a custom class to keep the container and data mapped.
    /// </summary>
    private class NavigationDrawerContainerForObjects
    {
        public MaterialCard Container { get; set; }

        public Image LeadingIcon { get; set; }

        public Image TrailingIcon { get; set; }

        public MaterialLabel Label { get; set; }
    }

    #region Converters
    
    private class HeadlineMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness margin && margin != DefaultHeadlineMargin)
            {
                return margin;
            }

            return new Thickness(0, 16);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    private class IsSelectedToFrameBackgroundConverter : IValueConverter
    {
        private readonly MaterialNavigationDrawer _drawer;

        public IsSelectedToFrameBackgroundConverter(MaterialNavigationDrawer drawer)
        {
            _drawer = drawer;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? _drawer.ActiveIndicatorBackgroundColor : Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiValueConverter : IMultiValueConverter
    {
        private readonly Func<object[], Type, object, CultureInfo, object> _convert;

        public MultiValueConverter(Func<object[], Type, object, CultureInfo, object> convert)
        {
            _convert = convert;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return _convert(values, targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion Converters
}