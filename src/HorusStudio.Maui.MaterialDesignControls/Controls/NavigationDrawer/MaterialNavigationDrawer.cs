
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A navigation drawer <see cref="View" /> let people switch between UI views on larger devices/>.
/// </summary>
public class MaterialNavigationDrawer : ContentView
{
    #region Attributes

    private readonly static Color DefaultHeadlineColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();    
    private readonly static Color DefaultSectionLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultActiveIndicatorBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialDarkTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultActiveIndicatorLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimaryContainer, Dark = MaterialDarkTheme.OnPrimaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDividerColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBadgeTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisabledLabelTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Disable, Dark = MaterialDarkTheme.Disable }.GetValueForCurrentTheme<Color>();
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultHeadlineCharacterSpacing = MaterialFontTracking.TitleSmall;
    private readonly static double DefaultLabelCharacterSpacing = MaterialFontTracking.LabelLarge;
    private readonly static double DefaultHeadlineFontSize = MaterialFontSize.TitleSmall;
    private readonly static double DefaultLabelFontSize = MaterialFontSize.LabelLarge;
    private readonly static double DefaultBadgeFontSize = MaterialFontSize.LabelLarge;
    private readonly static double DefaultSectionLabelFontSize = MaterialFontSize.TitleSmall;
    private readonly static double DefaultSectionLabelCharacterSpacing = MaterialFontTracking.TitleSmall;
    private readonly static double DefaultItemHeightRequest = 56.0;
    private readonly static float DefaultActiveIndicatorCornerRadius = 28.0f;
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
#nullable enable
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable

    #endregion Attributes

    #region Layout

    private MaterialLabel _lblHeadline;

    private StackLayout _itemsContainer;

    private Dictionary<string, NavigationDrawerContainerForObjects> _containersWithItems = new Dictionary<string, NavigationDrawerContainerForObjects>();

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Headline" /> bindable property.
    /// </summary>
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(MaterialNavigationDrawer), defaultValue: null, propertyChanged: (bindableObject, _, newValue) =>
    {
        if (bindableObject is MaterialNavigationDrawer self && newValue is string headline)
        {
            self._lblHeadline.IsVisible = !string.IsNullOrWhiteSpace(headline);
        }
    });

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
    public static readonly BindableProperty HeadlineMarginProperty = BindableProperty.Create(nameof(HeadlineMargin), typeof(Thickness), typeof(MaterialNavigationDrawer), defaultValue: new Thickness(-1), propertyChanged: (bindableObject, _, newValue) => 
    { 
        if (bindableObject is MaterialNavigationDrawer self)
        {
            if (newValue is Thickness margin && margin != new Thickness(-1))
                self._lblHeadline.Margin = margin;
            else
                self._lblHeadline.Margin = new Thickness(0, 16);
        }
    });

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
    public static readonly BindableProperty HeadlineTextTransformProperty = BindableProperty.Create(nameof(HeadlineTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: TextTransform.Default);

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
    public static readonly BindableProperty LabelTextTransformProperty = BindableProperty.Create(nameof(LabelTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: TextTransform.Default);

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
    public static readonly BindableProperty SectionLabelTextTransformProperty = BindableProperty.Create(nameof(SectionLabelTextTransform), typeof(TextTransform), typeof(MaterialNavigationDrawer), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="SectionLabelMargin" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SectionLabelMarginProperty = BindableProperty.Create(nameof(SectionLabelMargin), typeof(Thickness), typeof(MaterialNavigationDrawer), defaultValue: new Thickness(-1));

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
    public string Headline
    {
        get { return (string)GetValue(HeadlineProperty); }
        set { SetValue(HeadlineProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="HeadlineTextColor" /> for the text of the headline. This is a bindable property.
    /// </summary>
    public Color HeadlineTextColor
    {
        get { return (Color)GetValue(HeadlineTextColorProperty); }
        set { SetValue(HeadlineTextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the headline. This is a bindable property.
    /// </summary>
    public string HeadlineFontFamily
    {
        get { return (string)GetValue(HeadlineFontFamilyProperty); }
        set { SetValue(HeadlineFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the margin of the headline label. This is a bindable property.
    /// </summary>
    public Thickness HeadlineMargin
    {
        get { return (Thickness)GetValue(HeadlineMarginProperty); }
        set { SetValue(HeadlineMarginProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    public FontAttributes HeadlineFontAttributes
    {
        get { return (FontAttributes)GetValue(HeadlineFontAttributesProperty); }
        set { SetValue(HeadlineFontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool HeadlineFontAutoScalingEnabled
    {
        get { return (bool)GetValue(HeadlineFontAutoScalingEnabledProperty); }
        set { SetValue(HeadlineFontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    public TextTransform HeadlineTextTransform
    {
        get { return (TextTransform)GetValue(HeadlineTextTransformProperty); }
        set { SetValue(HeadlineTextTransformProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the headline. This is a bindable property.
    /// </summary>
    public double HeadlineCharactersSpacing
    {
        get { return (double)GetValue(HeadlineCharactersSpacingProperty); }
        set { SetValue(HeadlineCharactersSpacingProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    public double HeadlineFontSize
    {
        get { return (double)GetValue(HeadlineFontSizeProperty); }
        set { SetValue(HeadlineFontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the active background color. This is a bindable property.
    /// </summary>
    public Color ActiveIndicatorBackgroundColor
    {
        get { return (Color)GetValue(ActiveIndicatorBackgroundColorProperty); }
        set { SetValue(ActiveIndicatorBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Defines the active indicator label color. This is a bindable property.
    /// </summary>
    public Color ActiveIndicatorLabelColor
    {
        get { return (Color)GetValue(ActiveIndicatorLabelColorProperty); }
        set { SetValue(ActiveIndicatorLabelColorProperty, value); }
    }

    /// <summary>
    /// Defines the active indicator corner radius. This is a bindable property.
    /// </summary>
    /// <default>
    /// 28.0f
    /// </default>
    public float ActiveIndicatorCornerRadius
    {
        get { return (float)GetValue(ActiveIndicatorCornerRadiusProperty); }
        set { SetValue(ActiveIndicatorCornerRadiusProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of each item. This is a bindable property.
    /// </summary>
    public Color LabelColor
    {
        get { return (Color)GetValue(LabelColorProperty); }
        set { SetValue(LabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelFontSize" /> for the text of each item. This is a bindable property.
    /// </summary>
    public double LabelFontSize
    {
        get { return (double)GetValue(LabelFontSizeProperty); }
        set { SetValue(LabelFontSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for each item label. This is a bindable property.
    /// </summary>
    public string LabelFontFamily
    {
        get { return (string)GetValue(LabelFontFamilyProperty); }
        set { SetValue(LabelFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of each item label. This is a bindable property.
    /// </summary>
    public FontAttributes LabelFontAttributes
    {
        get { return (FontAttributes)GetValue(LabelFontAttributesProperty); }
        set { SetValue(LabelFontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool LabelFontAutoScalingEnabled
    {
        get { return (bool)GetValue(LabelFontAutoScalingEnabledProperty); }
        set { SetValue(LabelFontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label of each item. This is a bindable property.
    /// </summary>
    public TextTransform LabelTextTransform
    {
        get { return (TextTransform)GetValue(LabelTextTransformProperty); }
        set { SetValue(LabelTextTransformProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of each item label. This is a bindable property.
    /// </summary>
    public double LabelCharactersSpacing
    {
        get { return (double)GetValue(LabelCharactersSpacingProperty); }
        set { SetValue(LabelCharactersSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the section label. This is a bindable property.
    /// </summary>
    public Color SectionLabelColor
    {
        get { return (Color)GetValue(SectionLabelColorProperty); }
        set { SetValue(SectionLabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="FontSize" /> for the section label. This is a bindable property.
    /// </summary>
    public double SectionLabelFontSize
    {
        get { return (double)GetValue(SectionLabelFontSizeProperty); }
        set { SetValue(SectionLabelFontSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the section label. This is a bindable property.
    /// </summary>
    public string SectionLabelFontFamily
    {
        get { return (string)GetValue(SectionLabelFontFamilyProperty); }
        set { SetValue(SectionLabelFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the section label. This is a bindable property.
    /// </summary>
    public FontAttributes SectionLabelFontAttributes
    {
        get { return (FontAttributes)GetValue(SectionLabelFontAttributesProperty); }
        set { SetValue(SectionLabelFontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool SectionLabelFontAutoScalingEnabled
    {
        get { return (bool)GetValue(SectionLabelFontAutoScalingEnabledProperty); }
        set { SetValue(SectionLabelFontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the section label of each item. This is a bindable property.
    /// </summary>
    public TextTransform SectionLabelTextTransform
    {
        get { return (TextTransform)GetValue(SectionLabelTextTransformProperty); }
        set { SetValue(SectionLabelTextTransformProperty, value); }
    }

    /// <summary>
    /// Defines the margin of the section label. This is a bindable property.
    /// </summary>
    public Thickness SectionLabelMargin
    {
        get { return (Thickness)GetValue(SectionLabelMarginProperty); }
        set { SetValue(SectionLabelMarginProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of each item label. This is a bindable property.
    /// </summary>
    public double SectionLabelCharactersSpacing
    {
        get { return (double)GetValue(SectionLabelCharactersSpacingProperty); }
        set { SetValue(SectionLabelCharactersSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets if show a divider between sections. This is a bindable property.
    /// </summary>
    public bool SectionDividerIsVisible
    {
        get { return (bool)GetValue(SectionDividerIsVisibleProperty); }
        set { SetValue(SectionDividerIsVisibleProperty, value); }
    }
    
    /// <summary>
    /// Gets or sets if show a divider between items. This is a bindable property.
    /// </summary>
    public bool ItemDividerIsVisible
    {
        get { return (bool)GetValue(ItemDividerIsVisibleProperty); }
        set { SetValue(ItemDividerIsVisibleProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the divider. This is a bindable property.
    /// </summary>
    public Color DividerColor
    {
        get { return (Color)GetValue(DividerColorProperty); }
        set { SetValue(DividerColorProperty, value); }
    }
    
    /// <summary>
    /// Gets or sets the <see cref="MaterialBadgeType" />. This is a bindable property.
    /// </summary>
    public MaterialBadgeType BadgeType
    {
        get { return (MaterialBadgeType)GetValue(BadgeTypeProperty); }
        set { SetValue(BadgeTypeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color" /> for the badge. This is a bindable property.
    /// </summary>
    public Color BadgeTextColor
    {
        get { return (Color)GetValue(BadgeTextColorProperty); }
        set { SetValue(BadgeTextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="FontSize" /> for the badge label. This is a bindable property.
    /// </summary>
    public double BadgeFontSize
    {
        get { return (double)GetValue(BadgeFontSizeProperty); }
        set { SetValue(BadgeFontSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the badge label. This is a bindable property.
    /// </summary>
    public string BadgeFontFamily
    {
        get { return (string)GetValue(BadgeFontFamilyProperty); }
        set { SetValue(BadgeFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color" /> for the badge background. This is a bindable property.
    /// </summary>
    public Color BadgeBackgroundColor
    {
        get { return (Color)GetValue(BadgeBackgroundColorProperty); }
        set { SetValue(BadgeBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the items source. This is a bindable property.
    /// </summary>
    public IEnumerable<MaterialNavigationDrawerItem> ItemsSource
    {
        get { return (IEnumerable<MaterialNavigationDrawerItem>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    /// <summary>
    /// Gets or sets the height for each item. This is a bindable property.
    /// </summary>
    /// <default>
    /// 56.0
    /// </default>
    public double ItemHeightRequest
    {
        get { return (double)GetValue(ItemHeightRequestProperty); }
        set { SetValue(ItemHeightRequestProperty, value); }
    }

    /// <summary>
    /// Gets or sets the command for each item. This is a bindable property.
    /// </summary>
    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
    /// The default value is <see cref="AnimationTypes.Fade"/>.
    /// This is a bindable property.
    /// </summary>
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
    public Color DisabledLabelColor
    {
        get { return (Color)GetValue(DisabledLabelColorProperty); }
        set { SetValue(DisabledLabelColorProperty, value); }
    }

    #endregion Properties

    #region Constructors

    public MaterialNavigationDrawer()
    {
        StackLayout container = new StackLayout()
        {
            Spacing = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.StartAndExpand,
        };

        this._itemsContainer = new StackLayout()
        {
            Spacing = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };

        this._lblHeadline = new MaterialLabel()
        {
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = HeadlineMargin != new Thickness(-1) ? HeadlineMargin : new Thickness(0, 16),
            VerticalOptions = LayoutOptions.Center,
            TextColor = this.HeadlineTextColor,
            IsVisible = !string.IsNullOrWhiteSpace(Headline),
            Text = Headline,
            FontSize = HeadlineFontSize,
            FontFamily = HeadlineFontFamily,
            Padding = new Thickness(12, 0)
        };

        _lblHeadline.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Headline), source: this));
        _lblHeadline.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(HeadlineTextColor), source: this));
        _lblHeadline.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(HeadlineFontFamily), source: this));
        _lblHeadline.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(HeadlineFontSize), source: this));
        _lblHeadline.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(HeadlineTextTransform), source: this));
        _lblHeadline.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(HeadlineFontAttributes), source: this));
        _lblHeadline.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(HeadlineFontAutoScalingEnabled), source: this));
        _lblHeadline.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(HeadlineCharactersSpacing), source: this));

        container.Children.Add(this._lblHeadline);
        container.Children.Add(this._itemsContainer);

        this.Content = container;
    }

    #endregion Constructors

    #region Methods

    private void SetItemSource()
    {
        _itemsContainer.Children.Clear();
        _containersWithItems.Clear();

        if (ItemsSource == null) return;

        var groupedItems = ItemsSource.GroupBy(x => x.Section);
        int itemIdx = 1;

        foreach (var group in groupedItems)
        {
            AddSectionLabel(group.Key);

            foreach (var item in group)
            {
                string key = $"{item.Section}-{item.Text}";
                if (_containersWithItems.ContainsKey(key)) continue;

                var frame = CreateFrame(item);
                var contentContainer = CreateContentContainer(item, out var label, out var iconLeading, out var iconTrailing);

                frame.Content = contentContainer;

                frame.Command = new Command(() =>
                {
                    if ((item.IsSelected && item.ShowActiveIndicator) || !item.IsEnabled) return;

                    DeselectOtherItems(item);

                    item.IsSelected = !item.IsSelected;
                    SetContentAndColors(frame, iconLeading, iconTrailing, label, item);

                    if (item.IsEnabled && Command?.CanExecute(item) == true)
                        Command.Execute(item);
                });

                _containersWithItems[key] = new NavigationDrawerContainerForObjects
                {
                    Container = frame,
                    LeadingIcon = iconLeading,
                    TrailingIcon = iconTrailing,
                    Label = label
                };

                SetContentAndColors(frame, iconLeading, iconTrailing, label, item);
                _itemsContainer.Children.Add(frame);

                AddItemDivider();
            }

            AddSectionDivider(itemIdx++, groupedItems.Count());
        }
    }

    private void AddSectionLabel(string section)
    {
        if (string.IsNullOrWhiteSpace(section)) return;

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
        label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SectionLabelFontSize), source: this));
        label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(SectionLabelFontAttributes), source: this));
        label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(SectionLabelFontAutoScalingEnabled), source: this));
        label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(SectionLabelCharactersSpacing), source: this));
        label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(SectionLabelTextTransform), source: this));

        _itemsContainer.Children.Add(label);
    }

    private MaterialCard CreateFrame(MaterialNavigationDrawerItem item)
    {
        return new MaterialCard
        {
            Shadow = null,
            BorderColor = Colors.Transparent,
            Padding = new Thickness(16, 0),
            HeightRequest = ItemHeightRequest,
            MinimumHeightRequest = ItemHeightRequest,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.Fill,
            BackgroundColor = item.IsSelected ? ActiveIndicatorBackgroundColor : Colors.Transparent,
            CornerRadius = ActiveIndicatorCornerRadius,
            Animation = Animation,
            AnimationParameter = AnimationParameter,
            CustomAnimation = CustomAnimation,
            IsEnabled = item.IsEnabled
        };
    }

    private Grid CreateContentContainer(MaterialNavigationDrawerItem item, out MaterialLabel label, out Image iconLeading, out Image iconTrailing)
    {
        var contentContainer = new Grid
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            ColumnSpacing = 12,
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            },
        };

        iconLeading = new Image
        {
            HeightRequest = 24,
            MinimumHeightRequest = 24,
            WidthRequest = 24,
            MinimumWidthRequest = 24,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };
        iconLeading.SetValue(Grid.ColumnProperty, 0);

        label = new MaterialLabel
        {
            Text = item.Text.Trim(),
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = LabelFontSize,
            FontFamily = LabelFontFamily,
            TextColor = item.IsEnabled ? item.IsSelected ? ActiveIndicatorLabelColor : LabelColor : DisabledLabelColor
        };
        label.SetValue(Grid.ColumnProperty, 1);

        label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(LabelFontFamily), source: this));
        label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(LabelFontSize), source: this));
        label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(LabelFontAttributes), source: this));
        label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(LabelFontAutoScalingEnabled), source: this));
        label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(LabelCharactersSpacing), source: this));
        label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTextTransform), source: this));

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
        badge.SetValue(Grid.ColumnProperty, 2);

        badge.SetBinding(MaterialBadge.TypeProperty, new Binding(nameof(BadgeType), source: this));
        badge.SetBinding(MaterialBadge.TextColorProperty, new Binding(nameof(BadgeTextColor), source: this));
        badge.SetBinding(MaterialBadge.FontSizeProperty, new Binding(nameof(BadgeFontSize), source: this));
        badge.SetBinding(MaterialBadge.FontFamilyProperty, new Binding(nameof(BadgeFontFamily), source: this));
        badge.SetBinding(MaterialBadge.BackgroundColorProperty, new Binding(nameof(BadgeBackgroundColor), source: this));

        iconTrailing = new Image
        {
            HeightRequest = 24,
            MinimumHeightRequest = 24,
            WidthRequest = 24,
            MinimumWidthRequest = 24,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };
        iconTrailing.SetValue(Grid.ColumnProperty, 3);

        contentContainer.Children.Add(iconLeading);
        contentContainer.Children.Add(label);
        contentContainer.Children.Add(badge);
        contentContainer.Children.Add(iconTrailing);

        return contentContainer;
    }

    private void DeselectOtherItems(MaterialNavigationDrawerItem selectedItem)
    {
        var selectedItems = ItemsSource.Where(x => x.IsSelected);
        if (!selectedItems.Any()) return;

        foreach (var item in selectedItems)
        {
            if (item.Equals(selectedItem)) continue;

            item.IsSelected = false;
            string key = $"{item.Section}-{item.Text}";
            if (_containersWithItems.TryGetValue(key, out var container))
            {
                SetContentAndColors(container.Container, container.LeadingIcon, container.TrailingIcon, container.Label, item);
            }
        }
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

    private void AddSectionDivider(int currentIndex, int totalSections)
    {
        if (!SectionDividerIsVisible || currentIndex == totalSections) return;

        var divider = new MaterialDivider
        {
            Color = DividerColor,
            Margin = new Thickness(0, 16)
        };
        _itemsContainer.Children.Add(divider);
    }

    private void SetContentAndColors(MaterialCard frame, Image leadingIcon, Image trailingIcon, MaterialLabel label, MaterialNavigationDrawerItem item)
    {
        SetIcons(leadingIcon, trailingIcon, item);

        if (!item.ShowActiveIndicator)
        {
            return;
        }

        frame.BackgroundColor = item.IsSelected ? ActiveIndicatorBackgroundColor : Colors.Transparent;
        label.TextColor = item.IsEnabled ? item.IsSelected ? ActiveIndicatorLabelColor : LabelColor : DisabledLabelColor;
    }

    private void UpdateIconVisibility(bool isSelected, bool selectedIconVisible, bool unselectedIconVisible, Image icon, ImageSource selectedIconSource, ImageSource unselectedIconSource)
    {
        if (isSelected)
        {
            if (selectedIconVisible)
            {
                icon.IsVisible = true;
                icon.Source = selectedIconSource;
            }
            else
            {
                icon.IsVisible = false;
            }
        }
        else
        {
            if (unselectedIconVisible)
            {
                icon.IsVisible = true;
                icon.Source = unselectedIconSource;
            }
            else
            {
                icon.IsVisible = false;
            }
        }
    }

    public void SetIcons(Image leadingIcon, Image trailingIcon, MaterialNavigationDrawerItem item)
    {
        UpdateIconVisibility(item.IsSelected, item.SelectedLeadingIconIsVisible, item.UnselectedLeadingIconIsVisible, leadingIcon, item.SelectedLeadingIcon, item.UnselectedLeadingIcon);
        UpdateIconVisibility(item.IsSelected, item.SelectedTrailingIconIsVisible, item.UnselectedTrailingIconIsVisible, trailingIcon, item.SelectedTrailingIcon, item.UnselectedTrailingIcon);
    }

    #endregion Methods

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
}


