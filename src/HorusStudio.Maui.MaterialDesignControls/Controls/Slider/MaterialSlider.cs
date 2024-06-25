using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using Slider = Microsoft.Maui.Controls.Slider;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A slider <see cref="View" /> let users make selections from a range of values./>.
/// </summary>
public class MaterialSlider : ContentView
{
    #region Attributes

    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultThumbColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultMinimumTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultMaximumTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultValueIndicatorBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultValueIndicatorTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyMedium;
    private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
    private readonly static double DefaultValueIndicatorFontSize = MaterialFontSize.BodyMedium;
    private readonly static string DefaultValueIndicatorFormat = "N2";
    private bool MinimumImageIsVisible = false;
    private bool MinimumLabelIsVisible = false;
    private bool MaximumImageIsVisible = false;
    private bool MaximumLabelIsVisible = false;
    private readonly static int DefaultThumbWidth = 4;
    private readonly static int DefaultValueIndicatorSize = 44;
    private readonly static int DefaultThumbHeight = 44;

    private bool _isDragging = false;

    #endregion Attributes

    #region Layout

    private MaterialLabel _label;
    private MaterialLabel _minimumLabel;
    private Image _minimumImage;
    private CustomSlider _slider;
    private MaterialLabel _maximumLabel;
    private Image _maximumImage;
    private Grid _mainLayout;
    private Grid _containerLayout;
    private Image _backgroundImage;
    private Ellipse _valueIndicatorContainer;
    private MaterialLabel _valueIndicatorText;

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
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSlider), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValue: DefaultCharacterSpacing);

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
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSlider), defaultValue: DefaultFontSize);

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
    public static readonly BindableProperty MinimumLabelColorProperty = BindableProperty.Create(nameof(MinimumLabelColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="MinimumFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumFontFamilyProperty = BindableProperty.Create(nameof(MinimumFontFamily), typeof(string), typeof(MaterialSlider), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="MinimumCharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumCharacterSpacingProperty = BindableProperty.Create(nameof(MinimumCharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValue: DefaultCharacterSpacing);

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
    public static readonly BindableProperty MinimumFontSizeProperty = BindableProperty.Create(nameof(MinimumFontSize), typeof(double), typeof(MaterialSlider), defaultValue: DefaultFontSize);

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
    /// The backing store for the <see cref="MinimumTrackColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumTrackColorProperty = BindableProperty.Create(nameof(MinimumTrackColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultMinimumTrackColor);

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
    public static readonly BindableProperty MaximumLabelColorProperty = BindableProperty.Create(nameof(MaximumLabelColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="MaximumFontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumFontFamilyProperty = BindableProperty.Create(nameof(MaximumFontFamily), typeof(string), typeof(MaterialSlider), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumCharacterSpacingProperty = BindableProperty.Create(nameof(MaximumCharacterSpacing), typeof(double), typeof(MaterialSlider), defaultValue: DefaultCharacterSpacing);

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
    public static readonly BindableProperty MaximumFontSizeProperty = BindableProperty.Create(nameof(MaximumFontSize), typeof(double), typeof(MaterialSlider), defaultValue: DefaultFontSize);

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
    /// The backing store for the <see cref="MaximumTrackColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumTrackColorProperty = BindableProperty.Create(nameof(MaximumTrackColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultMaximumTrackColor);

    #endregion Maximum

    #region Track

    /// <summary>
    /// The backing store for the <see cref="TrackHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackHeightProperty =  BindableProperty.Create(nameof(TrackHeight), typeof(int), typeof(MaterialSlider), defaultValue: 16);

    /// <summary>
    /// The backing store for the <see cref="TrackCornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackCornerRadiusProperty = BindableProperty.Create(nameof(TrackCornerRadius), typeof(int), typeof(MaterialSlider), defaultValue: 6);

    /// <summary>
    /// The backing store for the <see cref="TrackImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrackImageSourceProperty = BindableProperty.Create(nameof(TrackImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null, propertyChanged: (bindableObject, _, newValue) => 
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
    public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultThumbColor);

    /// <summary>
    /// The backing store for the <see cref="ThumbImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbImageSourceeProperty = BindableProperty.Create(nameof(ThumbImageSource), typeof(ImageSource), typeof(MaterialSlider), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ThumbBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ThumbBackgroundColorProperty = BindableProperty.Create(nameof(ThumbBackgroundColor), typeof(Color), typeof(MaterialSlider), defaultValue: null);

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
    public static readonly BindableProperty ValueIndicatorBackgroundColorProperty = BindableProperty.Create(nameof(ValueIndicatorBackgroundColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorBackgroundColor, propertyChanged: (BindableObject, _, newValue) =>
    {
        if (BindableObject is MaterialSlider self && newValue is Color newBackgorundColor)
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
    public static readonly BindableProperty ValueIndicatorTextColorProperty = BindableProperty.Create(nameof(ValueIndicatorTextColor), typeof(Color), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorTextColor);

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorFontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorFontSizeProperty = BindableProperty.Create(nameof(ValueIndicatorFontSize), typeof(double), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorFontSize);

    /// <summary>
    /// The backing store for the <see cref="ValueIndicatorFormat" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueIndicatorFormatProperty = BindableProperty.Create(nameof(ValueIndicatorFormat), typeof(string), typeof(MaterialSlider), defaultValue: DefaultValueIndicatorFormat);

    #endregion ValueIndicator

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialSlider), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
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

    #endregion Bindable Properties

    #region Properties

    #region Label

    /// <summary>
    /// Gets or sets the text for the label. This is a bindable property.
    /// </summary>
    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of the label. This is a bindable property.
    /// </summary>
    public Color LabelColor
    {
        get { return (Color)GetValue(LabelColorProperty); }
        set { SetValue(LabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the label. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get { return (double)GetValue(CharacterSpacingProperty); }
        set { SetValue(CharacterSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get { return (FontAttributes)GetValue(FontAttributesProperty); }
        set { SetValue(FontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool FontAutoScalingEnabled
    {
        get { return (bool)GetValue(FontAutoScalingEnabledProperty); }
        set { SetValue(FontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    public TextTransform LabelTransform
    {
        get { return (TextTransform)GetValue(LabelTransformProperty); }
        set { SetValue(LabelTransformProperty, value); }
    }
    #endregion Label

    #region Minimum

    /// <summary>
    /// Gets or sets the text for the minimum label. This is a bindable property.
    /// </summary>
    public string MinimumLabel
    {
        get { return (string)GetValue(MinimumLabelProperty); }
        set { SetValue(MinimumLabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="MinimumLabelColor" /> for the text of the minimum label. This is a bindable property.
    /// </summary>
    public Color MinimumLabelColor
    {
        get { return (Color)GetValue(MinimumLabelColorProperty); }
        set { SetValue(MinimumLabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the minimum label. This is a bindable property.
    /// </summary>
    public string MinimumFontFamily
    {
        get { return (string)GetValue(MinimumFontFamilyProperty); }
        set { SetValue(MinimumFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the minimum label. This is a bindable property.
    /// </summary>
    public double MinimumCharacterSpacing
    {
        get { return (double)GetValue(MinimumCharacterSpacingProperty); }
        set { SetValue(MinimumCharacterSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the minimum label. This is a bindable property.
    /// </summary>
    public FontAttributes MinimumFontAttributes
    {
        get { return (FontAttributes)GetValue(MinimumFontAttributesProperty); }
        set { SetValue(MinimumFontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool MinimumFontAutoScalingEnabled
    {
        get { return (bool)GetValue(MinimumFontAutoScalingEnabledProperty); }
        set { SetValue(MinimumFontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the minimum label. This is a bindable property.
    /// </summary>
    public double MinimumFontSize
    {
        get { return (double)GetValue(MinimumFontSizeProperty); }
        set { SetValue(MinimumFontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the minimum label. This is a bindable property.
    /// </summary>
    public TextTransform MinimumLabelTransform
    {
        get { return (TextTransform)GetValue(MinimumLabelTransformProperty); }
        set { SetValue(MinimumLabelTransformProperty, value); }
    }

    /// <summary>
    /// Allows you to display a bitmap image instead of label on minimum side This is a bindable property.
    /// </summary>
    public ImageSource MinimumImageSource
    {
        get => (ImageSource)GetValue(MinimumImageSourceProperty);
        set => SetValue(MinimumImageSourceProperty, value);
    }

    /// <summary>
    /// Defines the minimum value of the slider
    /// The default value is <value>0</value>.
    /// This is a bindable property.
    /// </summary>
    public double Minimum
    {
        get { return (double)GetValue(MinimumProperty); }
        set { SetValue(MinimumProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the minimum track color. This is a bindable property.
    /// </summary>
    public Color MinimumTrackColor
    {
        get { return (Color)GetValue(MinimumTrackColorProperty); }
        set { SetValue(MinimumTrackColorProperty, value); }
    }

    #endregion Minimum

    #region Maximum

    /// <summary>
    /// Gets or sets the text for the maximum label. This is a bindable property.
    /// </summary>
    public string MaximumLabel
    {
        get { return (string)GetValue(MaximumLabelProperty); }
        set { SetValue(MaximumLabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="MaximumLabelColor" /> for the text of the maximum label. This is a bindable property.
    /// </summary>
    public Color MaximumLabelColor
    {
        get { return (Color)GetValue(MaximumLabelColorProperty); }
        set { SetValue(MaximumLabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the maximum label. This is a bindable property.
    /// </summary>
    public string MaximumFontFamily
    {
        get { return (string)GetValue(MaximumFontFamilyProperty); }
        set { SetValue(MaximumFontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the maximum label. This is a bindable property.
    /// </summary>
    public double MaximumCharacterSpacing
    {
        get { return (double)GetValue(MaximumCharacterSpacingProperty); }
        set { SetValue(MaximumCharacterSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the maximum label. This is a bindable property.
    /// </summary>
    public FontAttributes MaximumFontAttributes
    {
        get { return (FontAttributes)GetValue(MaximumFontAttributesProperty); }
        set { SetValue(MaximumFontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool MaximumFontAutoScalingEnabled
    {
        get { return (bool)GetValue(MaximumFontAutoScalingEnabledProperty); }
        set { SetValue(MaximumFontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the maximum label. This is a bindable property.
    /// </summary>
    public double MaximumFontSize
    {
        get { return (double)GetValue(MaximumFontSizeProperty); }
        set { SetValue(MaximumFontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the maximum label. This is a bindable property.
    /// </summary>
    public TextTransform MaximumLabelTransform
    {
        get { return (TextTransform)GetValue(MaximumLabelTransformProperty); }
        set { SetValue(MaximumLabelTransformProperty, value); }
    }

    /// <summary>
    /// Allows you to display a bitmap image instead of label on maximum side This is a bindable property.
    /// </summary>
    public ImageSource MaximumImageSource
    {
        get => (ImageSource)GetValue(MaximumImageSourceProperty);
        set => SetValue(MaximumImageSourceProperty, value);
    }

    /// <summary>
    /// Defines the maximum value of the slider
    /// The default value is <value>1</value>.
    /// This is a bindable property.
    /// </summary>
    public double Maximum
    {
        get { return (double)GetValue(MaximumProperty); }
        set { SetValue(MaximumProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the minimum track color. This is a bindable property.
    /// </summary>
    public Color MaximumTrackColor
    {
        get { return (Color)GetValue(MaximumTrackColorProperty); }
        set { SetValue(MaximumTrackColorProperty, value); }
    }

    #endregion Maximum

    #region Track

    /// <summary>
    /// Gets or sets <see cref="TrackHeight" />  for the slider control. This is a bindable property.
    /// </summary>
    public int TrackHeight
    {
        get { return (int)GetValue(TrackHeightProperty); }
        set { SetValue(TrackHeightProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="TrackCornerRadius" />  for the slider control. This is a bindable property.
    /// </summary>
    public int TrackCornerRadius
    {
        get { return (int)GetValue(TrackCornerRadiusProperty); }
        set { SetValue(TrackCornerRadiusProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="TrackImageSource" />  for the slider control. This is a bindable property.
    /// </summary>
    public ImageSource TrackImageSource
    {
        get { return (ImageSource)GetValue(TrackImageSourceProperty); }
        set { SetValue(TrackImageSourceProperty, value); }
    }

    #endregion Track

    #region Thumb

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the thumb. This is a bindable property.
    /// </summary>
    public Color ThumbColor
    {
        get { return (Color)GetValue(ThumbColorProperty); }
        set { SetValue(ThumbColorProperty, value); }
    }

    /// <summary>
    /// Allows you to display a bitmap image on the thumb. This is a bindable property.
    /// As recomendation, on iOS you should set the thumb background color.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource ThumbImageSource
    {
        get => (ImageSource)GetValue(ThumbImageSourceeProperty);
        set => SetValue(ThumbImageSourceeProperty, value);
    }

    /// <summary>
    /// This property is mandatory that you set if you wanna a proper design.
    /// Allows you to set the color of the thumb shadow.
    /// You should set it equals to the background color of the slider's container.
    /// </summary>
    public Color ThumbBackgroundColor
    {
        get => (Color)GetValue(ThumbBackgroundColorProperty);
        set => SetValue(ThumbBackgroundColorProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb width
    /// The default value is <value>4</value>
    /// </summary>
    public int ThumbWidth
    {
        get => (int)GetValue(ThumbWidthProperty);
        set => SetValue(ThumbWidthProperty, value);
    }

    /// <summary>
    /// Allows you to set the thumb height
    /// The default value is <value>44</value>
    /// </summary>
    public int ThumbHeight
    {
        get => (int)GetValue(ThumbHeightProperty);
        set => SetValue(ThumbHeightProperty, value);
    }

    #endregion Thumb

    #region ValueIndicator

    /// <summary>
    /// This property is to set the background color of the value indicator
    /// </summary>
    public Color ValueIndicatorBackgroundColor
    {
        get => (Color)GetValue(ValueIndicatorBackgroundColorProperty);
        set => SetValue(ValueIndicatorBackgroundColorProperty, value);
    }

    /// <summary>
    /// Allows you to set the value indicator size
    /// The default value is <value>44</value>
    /// </summary>
    public int ValueIndicatorSize
    {
        get => (int)GetValue(ValueIndicatorSizeProperty);
        set => SetValue(ValueIndicatorSizeProperty, value);
    }

    /// <summary>
    /// Defines if show or not the value indicator
    /// </summary>
    public bool ShowValueIndicator
    {
        get { return (bool)GetValue(ShowValueIndicatorProperty); }
        set { SetValue(ShowValueIndicatorProperty, value); }
    }

    /// <summary>
    /// This property is to set the text color of the value indicator
    /// </summary>
    public Color ValueIndicatorTextColor
    {
        get => (Color)GetValue(ValueIndicatorTextColorProperty);
        set => SetValue(ValueIndicatorTextColorProperty, value);
    }

    /// <summary>
    /// This property is to set the value indicator's font size 
    /// </summary>
    public double ValueIndicatorFontSize
    {
        get => (double)GetValue(ValueIndicatorFontSizeProperty);
        set => SetValue(ValueIndicatorFontSizeProperty, value);
    }

    /// <summary>
    /// This property is to set the value indicator's format
    /// </summary>
    public string ValueIndicatorFormat
    {
        get => (string)GetValue(ValueIndicatorFormatProperty);
        set => SetValue(ValueIndicatorFormatProperty, value);
    }

    #endregion ValueIndicator

    /// <summary>
    /// Gets or sets <see cref="ShowIcons" />  for the slider control. This is a bindable property.
    /// This property is used to show the icons even when minimum/maximum label is seted. 
    /// If the value is true, show icons. Other case, no show icons even when they was seted.
    /// The default value is <value>false</value>
    /// </summary>
    public bool ShowIcons
    {
        get { return (bool)GetValue(ShowIconsProperty); }
        set { SetValue(ShowIconsProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="UserInteractionEnabled" />  for the slider control. This is a bindable property.
    /// </summary>
    public bool UserInteractionEnabled
    {
        get { return (bool)GetValue(UserInteractionEnabledProperty); }
        set { SetValue(UserInteractionEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="IsEnabled" />  for the slider control. This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the value of the slider
    /// The default value is 0
    /// This is a bindable property.
    /// </summary>
    public double Value
    {
        get { return (double)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
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

    #endregion Properties

    #region Constructors

    public MaterialSlider()
    {
        Padding = ShowValueIndicator ? new Thickness(0, ValueIndicatorSize / 1.5, 0, 10) : new Thickness(0);

        _mainLayout = new()
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

        _label = new()
        {
            TextColor = LabelColor,
            Text = Label,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(16, 0, 16, 4)
        };
        _label.SetValue(Grid.RowProperty, 0);

        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Label), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTransform), source: this));
        _label.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        _mainLayout.Children.Add(_label);

        _containerLayout = new()
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

        _containerLayout.SetValue(Grid.RowProperty, 1);

        _backgroundImage = new()
        {
            IsVisible = false,
            Aspect = Aspect.AspectFit,
            Margin = new Thickness(10, 0)
        };

        _backgroundImage.SetValue(Grid.RowProperty, 0);
        _backgroundImage.SetValue(Grid.ColumnProperty, 1);

        _containerLayout.Children.Add(_backgroundImage);
        _backgroundImage.SetBinding(Image.SourceProperty, new Binding(nameof(TrackImageSource), source: this));

        _minimumLabel = new()
        {
            IsVisible = false,
            TextColor = MinimumLabelColor,
            Text = MinimumLabel,
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

        _containerLayout.Children.Add(_minimumLabel);

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

        _containerLayout.Children.Add(_minimumImage);

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
        _slider.SetBinding(Slider.MinimumTrackColorProperty, new Binding(nameof(MinimumTrackColor), source: this));
        _slider.SetBinding(Slider.MaximumTrackColorProperty, new Binding(nameof(MaximumTrackColor), source: this));
        _slider.SetBinding(Slider.ThumbColorProperty, new Binding(nameof(ThumbColor), source: this));
        _slider.SetBinding(Slider.ThumbImageSourceProperty, new Binding(nameof(ThumbImageSource), source: this));
        _slider.SetBinding(Slider.DragCompletedCommandProperty, new Binding(nameof(DragCompletedCommand), source: this));
        _slider.SetBinding(Slider.DragStartedCommandProperty, new Binding(nameof(DragStartedCommand), source: this));
        _slider.SetBinding(CustomSlider.TrackHeightProperty, new Binding(nameof(TrackHeight), source: this));
        _slider.SetBinding(CustomSlider.TrackCornerRadiusProperty, new Binding(nameof(TrackCornerRadius), source: this));
        _slider.SetBinding(CustomSlider.UserInteractionEnabledProperty, new Binding(nameof(UserInteractionEnabled), source: this));
        _slider.SetBinding(CustomSlider.ThumbBackgroundColorProperty, new Binding(nameof(ThumbBackgroundColor), source: this));
        _slider.SetBinding(CustomSlider.ThumbWidthProperty, new Binding(nameof(ThumbWidth), source: this));
        _slider.SetBinding(CustomSlider.ThumbHeightProperty, new Binding(nameof(ThumbHeight), source: this));

        _slider.SetValue(Grid.RowProperty, 0);
        _slider.SetValue(Grid.ColumnProperty, 1);

        _slider.DragCompleted += OnDragCompleted;
        _slider.DragStarted += OnDragStarted;
        _slider.ValueChanged += OnValueChanged;

        _containerLayout.Children.Add(_slider);

        _maximumLabel = new()
        {
            IsVisible = false,
            TextColor = MaximumLabelColor,
            Text = MaximumLabel,
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

        _containerLayout.Children.Add(_maximumLabel);

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

        _containerLayout.Children.Add(_maximumImage);

        _valueIndicatorContainer = new Ellipse
        {
            WidthRequest = ValueIndicatorSize,
            HeightRequest = ValueIndicatorSize,
            Fill = new SolidColorBrush(ValueIndicatorBackgroundColor),
            Stroke = new SolidColorBrush(ValueIndicatorBackgroundColor),
            StrokeThickness = 2,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsVisible = false,
        };
        _valueIndicatorContainer.SetValue(Grid.RowSpanProperty, 2);

        _valueIndicatorContainer.SetBinding(Ellipse.WidthRequestProperty, new Binding(nameof(ValueIndicatorSize), source: this));
        _valueIndicatorContainer.SetBinding(Ellipse.HeightRequestProperty, new Binding(nameof(ValueIndicatorSize), source: this));

        _valueIndicatorText = new MaterialLabel
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = DefaultTextColor,
            FontSize = DefaultFontSize,
            IsVisible = false,
            MaxLines = 1,
        };
        _valueIndicatorText.SetValue(Grid.RowSpanProperty, 2);

        _valueIndicatorText.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(ValueIndicatorTextColor), source: this));
        _valueIndicatorText.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(ValueIndicatorFontSize), source: this));

        _mainLayout.Children.Add(_containerLayout);
        _mainLayout.Children.Add(_valueIndicatorContainer);
        _mainLayout.Children.Add(_valueIndicatorText);

        base.Content = _mainLayout;
        _mainLayout.IsClippedToBounds = false;
    }

    #endregion Constructors

    #region Events

    public event EventHandler<ValueChangedEventArgs> ValueChanged;
    public event EventHandler DragCompleted;
    public event EventHandler DragStarted;

    #endregion Events

    #region Methods

    private void SetMinimumPropertiesIsVisible()
    {
        MinimumImageIsVisible = ShowIcons && MinimumImageSource is not null;
        MinimumLabelIsVisible = !string.IsNullOrEmpty(MinimumLabel) && (!ShowIcons || !MinimumImageIsVisible);
        _minimumLabel.IsVisible = MinimumLabelIsVisible;
        _minimumImage.IsVisible = MinimumImageIsVisible;
    }

    private void SetMaximumPropertiesIsVisible()
    {
        MaximumImageIsVisible = ShowIcons && MaximumImageSource is not null;
        MaximumLabelIsVisible = !string.IsNullOrEmpty(MaximumLabel) && (!ShowIcons || !MaximumImageIsVisible);
        _maximumLabel.IsVisible = MaximumLabelIsVisible;
        _maximumImage.IsVisible = MaximumImageIsVisible;
    }

    private void OnDragStarted(object sender, EventArgs e)
    {
        _isDragging = true;
        DragStarted?.Invoke(sender, e);
    }

    private void OnDragCompleted(object sender, EventArgs e)
    {
        _valueIndicatorContainer.IsVisible = false;
        _valueIndicatorText.IsVisible = false;
        _isDragging = false;
        DragCompleted?.Invoke(sender, e);
    }

    private void OnValueChanged(object sender, ValueChangedEventArgs e)
    {
        this.Value = e.NewValue;
        UpdateThumbLabelPosition();

        ValueChanged?.Invoke(sender, e);
    }

    private void SetBackgroundImage()
    {
        this.MinimumTrackColor = Colors.Transparent;
        this.MaximumTrackColor = Colors.Transparent;
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

            _valueIndicatorText.Text = _slider.Value.ToString(ValueIndicatorFormat);

#if IOS || MACCATALYST
            _valueIndicatorContainer.TranslationY = ThumbHeight * -0.8;
            _valueIndicatorText.TranslationY = ThumbHeight * -0.8;
#else
            _valueIndicatorContainer.TranslationY = ThumbHeight / -1.5;
            _valueIndicatorText.TranslationY = ThumbHeight / -1.5;
#endif
        }
    }

#endregion Methods
}
