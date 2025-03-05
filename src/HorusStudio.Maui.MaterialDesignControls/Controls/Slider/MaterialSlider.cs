using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using Slider = Microsoft.Maui.Controls.Slider;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A slider <see cref="View" /> that lets users make selections from a range of values and follows Material Design Guidelines <see href="https://m3.material.io/components/sliders/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSlider.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
///  &lt;material:MaterialSlider 
///                      Label="Only label"
///                        Value="{Binding Value}"
///                     BackgroundColor="{x:Static material:MaterialLightTheme.SurfaceContainer}" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var slider = new MaterialSlider
/// {
///    Label = "slider"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SliderPage.xaml)
/// 
/// </example>
public class MaterialSlider : ContentView
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultThumbColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultActiveTrackColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultInactiveTrackColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultValueIndicatorBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultValueIndicatorTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyMedium;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultValueIndicatorFontSize = _ => MaterialFontSize.BodyMedium;
    private const string DefaultValueIndicatorFormat = "{0:0.00}";
    private const int DefaultThumbWidth = 4;
    private const int DefaultValueIndicatorSize = 44;
    private const int DefaultThumbHeight = 44;

    private bool _isDragging;
    private bool _minimumImageIsVisible;
    private bool _minimumLabelIsVisible;
    private bool _maximumImageIsVisible;
    private bool _maximumLabelIsVisible;

    #endregion Attributes

    #region Layout

    private readonly MaterialLabel _minimumLabel;
    private readonly Image _minimumImage;
    private readonly CustomSlider _slider;
    private readonly MaterialLabel _maximumLabel;
    private readonly Image _maximumImage;
    private readonly Image _backgroundImage;
    private readonly Ellipse _valueIndicatorContainer;
    private readonly MaterialLabel _valueIndicatorText;

    #endregion Layout

    #region Bindable Properties

    #region Label

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSlider), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTransformProperty = BindableProperty.Create(nameof(LabelTransform), typeof(TextTransform), typeof(MaterialSlider), defaultValue: TextTransform.Default);

    #endregion Label

    #region Minimum

    /// <summary>
    /// The backing store for the <see cref="MinimumLabel" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumLabelProperty = BindableProperty.Create(nameof(MinimumLabel), typeof(string), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialSlider self)
        {
            self.SetMinimumPropertiesIsVisible();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="MinimumLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumLabelColorProperty = BindableProperty.Create(nameof(MinimumLabelColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="MinimumFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumFontFamilyProperty = BindableProperty.Create(nameof(MinimumFontFamily), typeof(string), typeof(MaterialSlider), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="MinimumCharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumCharacterSpacingProperty = BindableProperty.Create(nameof(MinimumCharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="MinimumFontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumFontAttributesProperty = BindableProperty.Create(nameof(MinimumFontAttributes), typeof(FontAttributes), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="MinimumFontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumFontAutoScalingEnabledProperty = BindableProperty.Create(nameof(MinimumFontAutoScalingEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="MinimumFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumFontSizeProperty = BindableProperty.Create(nameof(MinimumFontSize), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="MinimumLabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumLabelTransformProperty = BindableProperty.Create(nameof(MinimumLabelTransform), typeof(TextTransform), typeof(MaterialSlider), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="MinimumImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumImageSourceProperty = BindableProperty.Create(nameof(MinimumImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialSlider self)
        {
            self.SetMinimumPropertiesIsVisible();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Minimum"/> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(MaterialSlider), defaultValue: 0.0);

    /// <summary>
    /// The backing store for the <see cref="ActiveTrackColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActiveTrackColorProperty = BindableProperty.Create(nameof(ActiveTrackColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultActiveTrackColor);

    #endregion Minimum

    #region Maximum

    /// <summary>
    /// The backing store for the <see cref="MaximumLabel" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumLabelProperty = BindableProperty.Create(nameof(MaximumLabel), typeof(string), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialSlider self)
        {
            self.SetMaximumPropertiesIsVisible();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="MaximumLabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumLabelColorProperty = BindableProperty.Create(nameof(MaximumLabelColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="MaximumFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumFontFamilyProperty = BindableProperty.Create(nameof(MaximumFontFamily), typeof(string), typeof(MaterialSlider), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumCharacterSpacingProperty = BindableProperty.Create(nameof(MaximumCharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="MaximumFontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumFontAttributesProperty = BindableProperty.Create(nameof(MaximumFontAttributes), typeof(FontAttributes), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="MaximumFontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumFontAutoScalingEnabledProperty = BindableProperty.Create(nameof(MaximumFontAutoScalingEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="MaximumFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumFontSizeProperty = BindableProperty.Create(nameof(MaximumFontSize), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumLabelTransformProperty = BindableProperty.Create(nameof(MaximumLabelTransform), typeof(TextTransform), typeof(MaterialSlider), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="MaximumImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumImageSourceProperty = BindableProperty.Create(nameof(MaximumImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialSlider self)
        {
            self.SetMaximumPropertiesIsVisible();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Maximum"/> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(MaterialSlider), defaultValue: 1.0);

    /// <summary>
    /// The backing store for the <see cref="InactiveTrackColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty InactiveTrackColorProperty = BindableProperty.Create(nameof(InactiveTrackColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultInactiveTrackColor);

    #endregion Maximum

    #region Track

    /// <summary>
    /// The backing store for the <see cref="TrackHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackHeightProperty =  BindableProperty.Create(nameof(TrackHeight), typeof(int), typeof(MaterialSlider), defaultValue: 16);

    /// <summary>
    /// The backing store for the <see cref="TrackCornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackCornerRadiusProperty = BindableProperty.Create(nameof(TrackCornerRadius), typeof(int), typeof(MaterialSlider), defaultValue: 10);

    /// <summary>
    /// The backing store for the <see cref="TrackImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackImageSourceProperty = BindableProperty.Create(nameof(TrackImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindableObject, _, _) => 
    { 
        if (bindableObject is MaterialSlider self)
        {
            self.SetBackgroundImage();
        }
    });

    #endregion Track

    #region Thumb

    /// <summary>
    /// The backing store for the <see cref="ThumbColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultThumbColor);

    /// <summary>
    /// The backing store for the <see cref="ThumbImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbImageSourceeProperty = BindableProperty.Create(nameof(ThumbImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ThumbWidth" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbWidthProperty = BindableProperty.Create(nameof(ThumbWidth), typeof(int), typeof(MaterialSlider), defaultValue: DefaultThumbWidth);

    /// <summary>
    /// The backing store for the <see cref="ThumbHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbHeightProperty = BindableProperty.Create(nameof(ThumbHeight), typeof(int), typeof(MaterialSlider), defaultValue: DefaultThumbHeight);

    #endregion Thumb

    #region ValueIndicator

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorBackgroundColorProperty = BindableProperty.Create(nameof(ValueIndicatorBackgroundColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultValueIndicatorBackgroundColor, propertyChanged: (bindableObject, _, newValue) =>
    {
        if (bindableObject is MaterialSlider self && newValue is Color newBackgorundColor)
        {
            self._valueIndicatorContainer.Fill = new SolidColorBrush(newBackgorundColor);
            self._valueIndicatorContainer.Stroke = new SolidColorBrush(newBackgorundColor);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorSizeProperty = BindableProperty.Create(nameof(ValueIndicatorSize), typeof(int), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorSize);

    /// <summary>
    /// The backing store for the <see cref="ShowValueIndicator" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ShowValueIndicatorProperty = BindableProperty.Create(nameof(ShowValueIndicator), typeof(bool), typeof(MaterialSlider), defaultValue: true, propertyChanged: (bindableObject, _, newValue) => 
    {
        if(bindableObject is MaterialSlider self && newValue is bool value)
        {
            self.Padding = value ? new Thickness(0, self.ValueIndicatorSize / 1.5, 0, 10) : new Thickness(0);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorTextColorProperty = BindableProperty.Create(nameof(ValueIndicatorTextColor), typeof(Color), typeof(MaterialSlider), defaultValueCreator: DefaultValueIndicatorTextColor);

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorFontSizeProperty = BindableProperty.Create(nameof(ValueIndicatorFontSize), typeof(double), typeof(MaterialSlider), defaultValueCreator: DefaultValueIndicatorFontSize);

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorFormat" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorFormatProperty = BindableProperty.Create(nameof(ValueIndicatorFormat), typeof(string), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorFormat);

    #endregion ValueIndicator

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialSlider self && newValue is bool)
        {
            self.ChangeVisualState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="UserInteractionEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UserInteractionEnabledProperty = BindableProperty.Create(nameof(UserInteractionEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="ShowIcons" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ShowIconsProperty = BindableProperty.Create(nameof(ShowIcons), typeof(bool), typeof(MaterialSlider), defaultValue: false, propertyChanged: (bindable, _, _) => 
    { 
        if (bindable is MaterialSlider self)
        {
            self.SetMinimumPropertiesIsVisible();
            self.SetMaximumPropertiesIsVisible();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Value"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(MaterialSlider), defaultBindingMode: BindingMode.TwoWay, defaultValue: 0.0);

    /// <summary>
    /// The backing store for the <see cref="DragStartedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragStartedCommandProperty = BindableProperty.Create(nameof(DragStartedCommand), typeof(ICommand), typeof(MaterialSlider));

    /// <summary>
    /// The backing store for the <see cref="DragCompletedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragCompletedCommandProperty = BindableProperty.Create(nameof(DragCompletedCommand), typeof(ICommand), typeof(MaterialSlider));

    /// <summary>
    /// The backing store for the <see cref="ValueChangedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueChangedCommandProperty = BindableProperty.Create(nameof(ValueChangedCommand), typeof(ICommand), typeof(MaterialSlider));

    #endregion Bindable Properties

    #region Properties

    #region Label

    /// <summary>
    /// Gets or sets the text for the label. This is a bindable property.
    /// </summary>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Text</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Text</see>
    /// </default>
    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the label. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontTracking.BodyMedium">MaterialFontTracking.BodyMedium</see>
    /// </default>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true.
    /// </summary>
    /// <default>
    /// True
    /// </default>
    /// <remarks>Typically this should always be enabled for accessibility reasons.</remarks>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the <see cref="Label" />. This is a bindable property.
    /// </summary>
    public TextTransform LabelTransform
    {
        get => (TextTransform)GetValue(LabelTransformProperty);
        set => SetValue(LabelTransformProperty, value);
    }

    #endregion Label

    #region Minimum

    /// <summary>
    /// Gets or sets the text for the minimum label. This is a bindable property.
    /// </summary>
    public string MinimumLabel
    {
        get => (string)GetValue(MinimumLabelProperty);
        set => SetValue(MinimumLabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="MinimumLabelColor" /> for the text of the minimum label. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Text</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Text</see>
    /// </default>
    public Color MinimumLabelColor
    {
        get => (Color)GetValue(MinimumLabelColorProperty);
        set => SetValue(MinimumLabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the minimum label. This is a bindable property.
    /// </summary>
    public string MinimumFontFamily
    {
        get => (string)GetValue(MinimumFontFamilyProperty);
        set => SetValue(MinimumFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the minimum label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontTracking.BodyMedium">MaterialFontTracking.BodyMedium</see>
    /// </default>
    public double MinimumCharacterSpacing
    {
        get => (double)GetValue(MinimumCharacterSpacingProperty);
        set => SetValue(MinimumCharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the minimum label. This is a bindable property.
    /// </summary>
    public FontAttributes MinimumFontAttributes
    {
        get => (FontAttributes)GetValue(MinimumFontAttributesProperty);
        set => SetValue(MinimumFontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true.
    /// </summary>
    /// <default>
    /// True
    /// </default>
    /// <remarks>Typically this should always be enabled for accessibility reasons.</remarks>
    public bool MinimumFontAutoScalingEnabled
    {
        get => (bool)GetValue(MinimumFontAutoScalingEnabledProperty);
        set => SetValue(MinimumFontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the minimum label. This is a bindable property.
    /// </summary>
    public double MinimumFontSize
    {
        get => (double)GetValue(MinimumFontSizeProperty);
        set => SetValue(MinimumFontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the minimum label. This is a bindable property.
    /// </summary>
    public TextTransform MinimumLabelTransform
    {
        get => (TextTransform)GetValue(MinimumLabelTransformProperty);
        set => SetValue(MinimumLabelTransformProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image instead of a label on the minimum side. This is a bindable property.
    /// </summary>
    public ImageSource MinimumImageSource
    {
        get => (ImageSource)GetValue(MinimumImageSourceProperty);
        set => SetValue(MinimumImageSourceProperty, value);
    }

    /// <summary>
    /// Defines the minimum value of the slider.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 0
    /// </default>
    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the minimum track color. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color ActiveTrackColor
    {
        get => (Color)GetValue(ActiveTrackColorProperty);
        set => SetValue(ActiveTrackColorProperty, value);
    }

    #endregion Minimum

    #region Maximum

    /// <summary>
    /// Gets or sets the text for the maximum label. This is a bindable property.
    /// </summary>
    public string MaximumLabel
    {
        get => (string)GetValue(MaximumLabelProperty);
        set => SetValue(MaximumLabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="MaximumLabelColor" /> for the text of the maximum label. This is a bindable property.
    /// </summary>
    public Color MaximumLabelColor
    {
        get => (Color)GetValue(MaximumLabelColorProperty);
        set => SetValue(MaximumLabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the maximum label. This is a bindable property.
    /// </summary>
    public string MaximumFontFamily
    {
        get => (string)GetValue(MaximumFontFamilyProperty);
        set => SetValue(MaximumFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the maximum label. This is a bindable property.
    /// </summary>
    public double MaximumCharacterSpacing
    {
        get => (double)GetValue(MaximumCharacterSpacingProperty);
        set => SetValue(MaximumCharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the maximum label. This is a bindable property.
    /// </summary>
    public FontAttributes MaximumFontAttributes
    {
        get => (FontAttributes)GetValue(MaximumFontAttributesProperty);
        set => SetValue(MaximumFontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true.
    /// </summary>
    public bool MaximumFontAutoScalingEnabled
    {
        get => (bool)GetValue(MaximumFontAutoScalingEnabledProperty);
        set => SetValue(MaximumFontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the maximum label. This is a bindable property.
    /// </summary>
    public double MaximumFontSize
    {
        get => (double)GetValue(MaximumFontSizeProperty);
        set => SetValue(MaximumFontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the maximum label. This is a bindable property.
    /// </summary>
    public TextTransform MaximumLabelTransform
    {
        get => (TextTransform)GetValue(MaximumLabelTransformProperty);
        set => SetValue(MaximumLabelTransformProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image instead of a label on the maximum side. This is a bindable property.
    /// </summary>
    public ImageSource MaximumImageSource
    {
        get => (ImageSource)GetValue(MaximumImageSourceProperty);
        set => SetValue(MaximumImageSourceProperty, value);
    }

    /// <summary>
    /// Defines the maximum value of the slider.
    /// The default value is <value>1</value>.
    /// This is a bindable property.
    /// </summary>
    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the maximum track color. This is a bindable property.
    /// </summary>
    public Color InactiveTrackColor
    {
        get => (Color)GetValue(InactiveTrackColorProperty);
        set => SetValue(InactiveTrackColorProperty, value);
    }

    #endregion Maximum

    #region Track

    /// <summary>
    /// Gets or sets the <see cref="TrackHeight" /> for the slider control. This is a bindable property.
    /// </summary>
    public int TrackHeight
    {
        get => (int)GetValue(TrackHeightProperty);
        set => SetValue(TrackHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="TrackCornerRadius" /> for the slider control. This is a bindable property.
    /// </summary>
    public int TrackCornerRadius
    {
        get => (int)GetValue(TrackCornerRadiusProperty);
        set => SetValue(TrackCornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="TrackImageSource" /> for the slider control. This is a bindable property.
    /// </summary>
    public ImageSource TrackImageSource
    {
        get => (ImageSource)GetValue(TrackImageSourceProperty);
        set => SetValue(TrackImageSourceProperty, value);
    }

    #endregion Track

    #region Thumb

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the thumb. This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color ThumbColor
    {
        get => (Color)GetValue(ThumbColorProperty);
        set => SetValue(ThumbColorProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the thumb. This is a bindable property.
    /// As a recommendation, on iOS you should set the thumb background color.
    /// </summary>
    /// <remarks>For more options, see <see cref="ImageButton"/>.</remarks>
    public ImageSource ThumbImageSource
    {
        get => (ImageSource)GetValue(ThumbImageSourceeProperty);
        set => SetValue(ThumbImageSourceeProperty, value);
    }

    /// <summary>
    /// This property is mandatory to set if you want a proper design.
    /// Allows you to set the color of the thumb shadow.
    /// You should set it equal to the background color of the slider's container.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb width.
    /// </summary>
    /// <default>
    /// 4
    /// </default>
    public int ThumbWidth
    {
        get => (int)GetValue(ThumbWidthProperty);
        set => SetValue(ThumbWidthProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb height.
    /// The default value is <value>44</value>.
    /// </summary>
    /// <default>
    /// 44
    /// </default>
    public int ThumbHeight
    {
        get => (int)GetValue(ThumbHeightProperty);
        set => SetValue(ThumbHeightProperty, value);
    }

    #endregion Thumb

    #region ValueIndicator

    /// <summary>
    /// Sets the background color of the value indicator.
    /// </summary>
    public Color ValueIndicatorBackgroundColor
    {
        get => (Color)GetValue(ValueIndicatorBackgroundColorProperty);
        set => SetValue(ValueIndicatorBackgroundColorProperty, value);
    }

    /// <summary>
    /// Allows you to set the value indicator size.
    /// The default value is <value>44</value>.
    /// </summary>
    public int ValueIndicatorSize
    {
        get => (int)GetValue(ValueIndicatorSizeProperty);
        set => SetValue(ValueIndicatorSizeProperty, value);
    }

    /// <summary>
    /// Defines whether to show the value indicator.
    /// </summary>
    public bool ShowValueIndicator
    {
        get => (bool)GetValue(ShowValueIndicatorProperty);
        set => SetValue(ShowValueIndicatorProperty, value);
    }

    /// <summary>
    /// Sets the text color of the value indicator.
    /// </summary>
    public Color ValueIndicatorTextColor
    {
        get => (Color)GetValue(ValueIndicatorTextColorProperty);
        set => SetValue(ValueIndicatorTextColorProperty, value);
    }

    /// <summary>
    /// Sets the value indicator's font size.
    /// </summary>
    public double ValueIndicatorFontSize
    {
        get => (double)GetValue(ValueIndicatorFontSizeProperty);
        set => SetValue(ValueIndicatorFontSizeProperty, value);
    }

    /// <summary>
    /// Sets the value indicator's format. This uses the format from <see cref="string.Format(string, object?)"/> 
    /// to show the value in the specified format. 
    /// The default value is <value>{0:0.00}</value>.
    /// </summary>
    public string ValueIndicatorFormat
    {
        get => (string)GetValue(ValueIndicatorFormatProperty);
        set => SetValue(ValueIndicatorFormatProperty, value);
    }

    #endregion ValueIndicator

    /// <summary>
    /// Gets or sets the <see cref="ShowIcons" /> property for the slider control. This is a bindable property.
    /// This property is used to show the icons even when the minimum/maximum label is set. 
    /// If the value is true, icons are shown. Otherwise, icons are not shown even when they are set.
    /// The default value is <value>false</value>.
    /// </summary>
    /// <default>
    /// false
    /// </default>
    public bool ShowIcons
    {
        get => (bool)GetValue(ShowIconsProperty);
        set => SetValue(ShowIconsProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="UserInteractionEnabled" /> property for the slider control. This is a bindable property.
    /// </summary>
    /// <default>
    /// true
    /// </default>
    public bool UserInteractionEnabled
    {
        get => (bool)GetValue(UserInteractionEnabledProperty);
        set => SetValue(UserInteractionEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="IsEnabled" /> property for the slider control. This is a bindable property.
    /// </summary>
    /// <default>
    /// true
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Defines the value of the slider.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 0
    /// </default>
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the command that is executed at the beginning of a drag action. This is a bindable property.
    /// </summary>
    public ICommand DragStartedCommand
    {
        get => (ICommand)GetValue(DragStartedCommandProperty);
        set => SetValue(DragStartedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command that is executed at the end of a drag action. This is a bindable property.
    /// </summary>
    public ICommand DragCompletedCommand
    {
        get => (ICommand)GetValue(DragCompletedCommandProperty);
        set => SetValue(DragCompletedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command when <see cref="Value"/> changed. This is a bindable property.
    /// </summary>
    public ICommand ValueChangedCommand
    {
        get => (ICommand)GetValue(ValueChangedCommandProperty);
        set => SetValue(ValueChangedCommandProperty, value);
    }

    #endregion Properties

    #region Constructors

    public MaterialSlider()
    {
        Padding = ShowValueIndicator ? new Thickness(0, ValueIndicatorSize / 1.5, 0, 10) : new Thickness(0);

        Grid mainLayout = new()
        {
            IsClippedToBounds = true,
            Margin = new Thickness(0, 0),
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new()
            {
                new()
                {
                    Height =  GridLength.Auto
                },
                new()
                {
                    Height = GridLength.Star
                }
            },
            ColumnDefinitions = new()
            {
                new()
                {
                    Width = GridLength.Star
                }
            }
        };

        MaterialLabel label = new()
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(16, 0, 16, 4)
        };
        label.SetValue(Grid.RowProperty, 0);

        label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Label), source: this));
        label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTransform), source: this));
        label.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        mainLayout.Children.Add(label);

        Grid containerLayout = new()
        {
            Margin = new Thickness(0, 5, 0, 0),
            Padding = new Thickness(0),
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new()
            {
                new()
                {
                    Height =  GridLength.Star
                }
            },
            ColumnDefinitions = new()
            {
                new()
                {
                    Width = GridLength.Auto
                },
                new()
                {
                    Width = GridLength.Star
                },
                new()
                {
                    Width = GridLength.Auto
                }
            },
            VerticalOptions = LayoutOptions.Fill
        };

        containerLayout.SetValue(Grid.RowProperty, 1);

        _backgroundImage = new()
        {
            IsVisible = false,
            Aspect = Aspect.AspectFit,
            Margin = new Thickness(10, 0)
        };

        _backgroundImage.SetValue(Grid.RowProperty, 0);
        _backgroundImage.SetValue(Grid.ColumnProperty, 1);

        containerLayout.Children.Add(_backgroundImage);
        _backgroundImage.SetBinding(Image.SourceProperty, new Binding(nameof(TrackImageSource), source: this));

        _minimumLabel = new()
        {
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 5, 0)
        };

        _minimumLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(MinimumLabel), source: this));
        _minimumLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(MinimumLabelColor), source: this));
        _minimumLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(MinimumFontFamily), source: this));
        _minimumLabel.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(MinimumCharacterSpacing), source: this));
        _minimumLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(MinimumFontAttributes), source: this));
        _minimumLabel.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(MinimumFontAutoScalingEnabled), source: this));
        _minimumLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(MinimumFontSize), source: this));
        _minimumLabel.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(MinimumLabelTransform), source: this));
        _minimumLabel.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _minimumLabel.SetValue(Grid.RowProperty, 0);
        _minimumLabel.SetValue(Grid.ColumnProperty, 0);

        containerLayout.Children.Add(_minimumLabel);

        _minimumImage = new()
        {
            IsVisible = false,
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 24,
            HeightRequest = 24,
            Margin = new Thickness(0, 0, 5, 0)
        };
        _minimumImage.SetBinding(Image.SourceProperty, new Binding(nameof(MinimumImageSource), source: this));

        _minimumImage.SetValue(Grid.RowProperty, 0);
        _minimumImage.SetValue(Grid.ColumnProperty, 0);

        containerLayout.Children.Add(_minimumImage);

        _slider = new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
        };

        _slider.On<iOS>().SetUpdateOnTap(true);

        _slider.SetBinding(Slider.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _slider.SetBinding(Slider.MinimumProperty, new Binding(nameof(Minimum), source: this));
        _slider.SetBinding(Slider.MaximumProperty, new Binding(nameof(Maximum), source: this));
        _slider.SetBinding(Slider.ValueProperty, new Binding(nameof(Value), source: this));
        _slider.SetBinding(Slider.MinimumTrackColorProperty, new Binding(nameof(ActiveTrackColor), source: this));
        _slider.SetBinding(Slider.MaximumTrackColorProperty, new Binding(nameof(InactiveTrackColor), source: this));
        _slider.SetBinding(Slider.ThumbColorProperty, new Binding(nameof(ThumbColor), source: this));
        _slider.SetBinding(Slider.ThumbImageSourceProperty, new Binding(nameof(ThumbImageSource), source: this));
        _slider.SetBinding(Slider.DragCompletedCommandProperty, new Binding(nameof(DragCompletedCommand), source: this));
        _slider.SetBinding(Slider.DragStartedCommandProperty, new Binding(nameof(DragStartedCommand), source: this));
        _slider.SetBinding(CustomSlider.TrackHeightProperty, new Binding(nameof(TrackHeight), source: this));
        _slider.SetBinding(CustomSlider.TrackCornerRadiusProperty, new Binding(nameof(TrackCornerRadius), source: this));
        _slider.SetBinding(CustomSlider.UserInteractionEnabledProperty, new Binding(nameof(UserInteractionEnabled), source: this));
        _slider.SetBinding(CustomSlider.ThumbBackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _slider.SetBinding(CustomSlider.ThumbWidthProperty, new Binding(nameof(ThumbWidth), source: this));
        _slider.SetBinding(CustomSlider.ThumbHeightProperty, new Binding(nameof(ThumbHeight), source: this));

        _slider.SetValue(Grid.RowProperty, 0);
        _slider.SetValue(Grid.ColumnProperty, 1);

        _slider.DragCompleted += OnDragCompleted;
        _slider.DragStarted += OnDragStarted;
        _slider.ValueChanged += OnValueChanged;

        containerLayout.Children.Add(_slider);

        _maximumLabel = new()
        {
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(5, 0, 0, 0)
        };

        _maximumLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(MaximumLabel), source: this));
        _maximumLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(MaximumLabelColor), source: this));
        _maximumLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(MaximumFontFamily), source: this));
        _maximumLabel.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(MaximumCharacterSpacing), source: this));
        _maximumLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(MaximumFontAttributes), source: this));
        _maximumLabel.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(MaximumFontAutoScalingEnabled), source: this));
        _maximumLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(MaximumFontSize), source: this));
        _maximumLabel.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(MaximumLabelTransform), source: this));
        _maximumLabel.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        _maximumLabel.SetValue(Grid.RowProperty, 0);
        _maximumLabel.SetValue(Grid.ColumnProperty, 2);

        containerLayout.Children.Add(_maximumLabel);

        _maximumImage = new()
        {
            IsVisible = false,
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 24,
            HeightRequest = 24,
            Margin = new Thickness(5, 0, 0, 0)
        };
        _maximumImage.SetBinding(Image.SourceProperty, new Binding(nameof(MaximumImageSource), source: this));

        _maximumImage.SetValue(Grid.RowProperty, 0);
        _maximumImage.SetValue(Grid.ColumnProperty, 2);

        containerLayout.Children.Add(_maximumImage);

        _valueIndicatorContainer = new Ellipse
        {
            StrokeThickness = 2,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsVisible = false,
        };
        _valueIndicatorContainer.SetValue(Grid.RowSpanProperty, 2);

        _valueIndicatorContainer.SetBinding(Ellipse.WidthRequestProperty, new Binding(nameof(ValueIndicatorSize), source: this));
        _valueIndicatorContainer.SetBinding(Ellipse.HeightRequestProperty, new Binding(nameof(ValueIndicatorSize), source: this));
        _valueIndicatorContainer.SetBinding(Ellipse.FillProperty, new Binding(nameof(ValueIndicatorBackgroundColor), source: this));
        _valueIndicatorContainer.SetBinding(Ellipse.StrokeProperty, new Binding(nameof(ValueIndicatorBackgroundColor), source: this));

        _valueIndicatorText = new MaterialLabel
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false,
            MaxLines = 1,
        };
        _valueIndicatorText.SetValue(Grid.RowSpanProperty, 2);

        _valueIndicatorText.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(ValueIndicatorTextColor), source: this));
        _valueIndicatorText.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(ValueIndicatorFontSize), source: this));

        mainLayout.Children.Add(containerLayout);
        mainLayout.Children.Add(_valueIndicatorContainer);
        mainLayout.Children.Add(_valueIndicatorText);

        Content = mainLayout;
        mainLayout.IsClippedToBounds = false;
    }

    #endregion Constructors

    #region Events

    public event EventHandler<ValueChangedEventArgs>? ValueChanged;
    public event EventHandler? DragCompleted;
    public event EventHandler? DragStarted;

    #endregion Events

    #region Methods

    private void SetMinimumPropertiesIsVisible()
    {
        _minimumImageIsVisible = ShowIcons && MinimumImageSource is not null;
        _minimumLabelIsVisible = !string.IsNullOrEmpty(MinimumLabel) && (!ShowIcons || !_minimumImageIsVisible);
        _minimumLabel.IsVisible = _minimumLabelIsVisible;
        _minimumImage.IsVisible = _minimumImageIsVisible;
    }

    private void SetMaximumPropertiesIsVisible()
    {
        _maximumImageIsVisible = ShowIcons && MaximumImageSource is not null;
        _maximumLabelIsVisible = !string.IsNullOrEmpty(MaximumLabel) && (!ShowIcons || !_maximumImageIsVisible);
        _maximumLabel.IsVisible = _maximumLabelIsVisible;
        _maximumImage.IsVisible = _maximumImageIsVisible;
    }

    private void OnDragStarted(object? sender, EventArgs e)
    {
        _isDragging = true;
        DragStarted?.Invoke(sender, e);
    }

    private void OnDragCompleted(object? sender, EventArgs e)
    {
        _valueIndicatorContainer.IsVisible = false;
        _valueIndicatorText.IsVisible = false;
        _isDragging = false;
        DragCompleted?.Invoke(sender, e);
    }

    private void OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        this.Value = e.NewValue;
        UpdateThumbLabelPosition();

        ValueChanged?.Invoke(sender, e);
        if (ValueChangedCommand?.CanExecute(e) ?? false)
        {
            ValueChangedCommand.Execute(e);
        }
    }

    private void SetBackgroundImage()
    {
        this.ActiveTrackColor = Colors.Transparent;
        this.InactiveTrackColor = Colors.Transparent;
        _backgroundImage.IsVisible = TrackImageSource is not null;
    }

    private void UpdateThumbLabelPosition()
    {
        if (_isDragging && ShowValueIndicator)
        {
            _valueIndicatorContainer.IsVisible = true;
            _valueIndicatorText.IsVisible = true;

            double thumbX = (_slider.Value - _slider.Minimum) / (_slider.Maximum - _slider.Minimum) * (_slider.Width - _valueIndicatorContainer.Width);
            _valueIndicatorContainer.TranslationX = thumbX - _slider.Width / 2 + _valueIndicatorContainer.Width / 2;
            _valueIndicatorText.TranslationX = thumbX - _slider.Width / 2 + _valueIndicatorContainer.Width / 2;

            _valueIndicatorText.Text = string.Format(ValueIndicatorFormat, _slider.Value);

#if IOS || MACCATALYST
            _valueIndicatorContainer.TranslationY = ThumbHeight * -0.8;
            _valueIndicatorText.TranslationY = ThumbHeight * -0.8;
#else
            _valueIndicatorContainer.TranslationY = ThumbHeight / -1.5;
            _valueIndicatorText.TranslationY = ThumbHeight / -1.5;
#endif
        }
    }

    protected override void ChangeVisualState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, SliderCommonStates.Disabled);
        }
        else if (_isDragging)
        {
            VisualStateManager.GoToState(this, SliderCommonStates.Pressed);
        }
        else
        {
            VisualStateManager.GoToState(this, SliderCommonStates.Normal);
        }
    }

    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = SliderCommonStates.Disabled };

        disabledState.Setters.Add(
            MaterialSlider.ActiveTrackColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(
            MaterialSlider.InactiveTrackColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabledState.Setters.Add(
            MaterialSlider.ThumbColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        var pressedState = new VisualState { Name = SliderCommonStates.Pressed };

        pressedState.Setters.Add(
            MaterialSlider.ThumbColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        pressedState.Setters.Add(
            MaterialSlider.ActiveTrackColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        pressedState.Setters.Add(
            MaterialSlider.InactiveTrackColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
            .GetValueForCurrentTheme<Color>());

        pressedState.Setters.Add(
            MaterialSlider.ValueIndicatorBackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.InverseSurface,
                Dark = MaterialDarkTheme.InverseSurface
            }
            .GetValueForCurrentTheme<Color>());

        pressedState.Setters.Add(
            MaterialSlider.ValueIndicatorTextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.InverseOnSurface,
                Dark = MaterialDarkTheme.InverseOnSurface
            }
            .GetValueForCurrentTheme<Color>());

        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialSlider));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }
    #endregion Styles
}

class SliderCommonStates : VisualStateManager.CommonStates
{
    public const string Pressed = "Pressed";
}
