using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Snackbars show short updates about app processes at the bottom of the screen <see href="https://m3.material.io/components/snackbar/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSnackbar.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialSnackbar
///       Text="Lorem ipsum dolor sit amet"
///       ActionText="No Action"
///       ActionCommand="{Binding SnackbarActionCommand}"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var snackbar = new MaterialSnackbar()
/// {
///     Text = "Lorem ipsum dolor sit amet",
///     ActionText = "No Action",
///     ActionCommand = ActionCommand
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SnackbarPage.xaml)
/// 
/// </example>
public class MaterialSnackbar : ContentView
{
    #region Attributes
    private static readonly int DefaultIconSize = 24;
    private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(4);
    private static readonly Thickness DefaultPadding = new Thickness(16,0,8,0);
    private static readonly ImageSource DefaultLeadingIcon = null;
    private static readonly ImageSource DefaultTrailingIcon = null;
    private static readonly string DefaultActionText = "Action";
    private static readonly Color DefaultIconTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialLightTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    private static readonly string DefaultText = string.Empty;
    private static readonly Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultActionTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
    private static readonly string DefaultFontFamily = MaterialFontFamily.Default;
    private static readonly double DefaultFontSize = MaterialFontSize.LabelLarge;
    private static readonly Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    private static readonly double DefaultBorderWidth = 0;
    private static readonly Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    private static readonly Shadow DefaultShadow = MaterialElevation.Level3;
    private static readonly Color DefaultShadowColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Shadow, Dark = MaterialDarkTheme.Shadow }.GetValueForCurrentTheme<Color>();

    #endregion

    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="IconSize" /> Action icon
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(int), typeof(MaterialSnackbar), defaultValue: DefaultIconSize, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSnackbar self)
        {
            self.SetIconSize();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="ActionText" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ActionTextProperty = BindableProperty.Create(nameof(ActionText), typeof(string), typeof(MaterialSnackbar), defaultValue: DefaultActionText);
    
    /// <summary>
    /// The backing store for the action text color
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionTextColorProperty = BindableProperty.Create(nameof(ActionTextColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultActionTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily"  />Action
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionFontFamilyProperty = BindableProperty.Create(nameof(ActionFontFamily), typeof(string), typeof(MaterialSnackbar), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" /> Action
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionFontSizeProperty = BindableProperty.Create(nameof(ActionFontSize), typeof(double), typeof(MaterialSnackbar), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="ActionCommand" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(MaterialSnackbar));
    
    /// <summary>
    /// The backing store for the <see cref="ActionCommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionCommandParameterProperty = BindableProperty.Create(nameof(ActionCommandParameter), typeof(object), typeof(MaterialSnackbar));
    
    /// <summary>
    /// The backing store for the <see cref="LeadingCommand" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingCommandProperty = BindableProperty.Create(nameof(LeadingCommand), typeof(ICommand), typeof(MaterialSnackbar));
    
    /// <summary>
    /// The backing store for the <see cref="LeadingCommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingCommandParameterProperty = BindableProperty.Create(nameof(LeadingCommandParameter), typeof(object), typeof(MaterialSnackbar));

    /// <summary>
    /// The backing store for the <see cref="TrailingCommand" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingCommandProperty = BindableProperty.Create(nameof(TrailingCommand), typeof(ICommand), typeof(MaterialSnackbar));
    
    /// <summary>
    /// The backing store for the <see cref="TrailingCommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingCommandParameterProperty = BindableProperty.Create(nameof(TrailingCommandParameter), typeof(object), typeof(MaterialSnackbar));
    
    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialSnackbar), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialSnackbar), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="LeadingIcon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialSnackbar), defaultValue: DefaultLeadingIcon, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSnackbar self)
        {
            self.SetLeadingIcon();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="TrailingIcon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialSnackbar), defaultValue: DefaultTrailingIcon, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSnackbar self)
        {
            self.SetTrailingIcon();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Text" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSnackbar), defaultValue: DefaultText);
    
    /// <summary>
    /// The backing store for the text color
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSnackbar), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSnackbar), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultBackgroundColor);
    
    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialSnackbar), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultBorderColor);
    
    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ShadowColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialSnackbar), defaultValue: DefaultShadowColor);

    /// <summary>
    /// The backing store for the <see cref="Shadow" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialSnackbar), defaultValue: DefaultShadow);

    #endregion

    #region Properties
    
    /// <summary>
    /// Gets or sets size trailing and leading icon
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 24
    /// </default>
    public int IconSize
    {
        get => (int)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the command to invoke when the Action text is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of Snackbar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand ActionCommand
    {
        get => (ICommand)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="ActionCommand"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object ActionCommandParameter
    {
        get => GetValue(ActionCommandParameterProperty);
        set => SetValue(ActionCommandParameterProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the command to invoke when leading icon is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of Snackbar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand LeadingCommand
    {
        get => (ICommand)GetValue(LeadingCommandProperty);
        set => SetValue(LeadingCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="LeadingCommand"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object LeadingCommandParameter
    {
        get => GetValue(LeadingCommandParameterProperty);
        set => SetValue(LeadingCommandParameterProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the command to invoke when the trailing icon is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of Snackbar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand TrailingCommand
    {
        get => (ICommand)GetValue(TrailingCommandProperty);
        set => SetValue(TrailingCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="TrailingCommand"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object TrailingCommandParameter
    {
        get => GetValue(TrailingCommandParameterProperty);
        set => SetValue(TrailingCommandParameterProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the padding for the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(16,0)
    /// </default>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the corner radius for the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(4)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets image leading for the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ImageSource LeadingIcon
    {
        get => (ImageSource)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }
    
    /// <summary>
    /// Gets or sets image trailing for the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ImageSource TrailingIcon
    {
        get => (ImageSource)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }
    
    /// <summary>
    /// Gets or sets text action Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="Action"/>
    /// </default>
    public string ActionText
    {
        get => (string)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets text color the Action Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InversePrimary">MaterialLightTheme.InversePrimary</see> - Dark = <see cref="MaterialDarkTheme.InversePrimary">MaterialDarkTheme.InversePrimary</see>
    /// </default>
    public Color ActionTextColor
    {
        get => (Color)GetValue(ActionTextColorProperty);
        set => SetValue(ActionTextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the font family for the text Action of this Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string ActionFontFamily
    {
        get => (string)GetValue(ActionFontFamilyProperty);
        set => SetValue(ActionFontFamilyProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the size of the font for the text Action of this Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 14 - Phone: 11
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double ActionFontSize
    {
        get => (double)GetValue(ActionFontSizeProperty);
        set => SetValue(ActionFontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets text the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="string.Empty"/>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets text color the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark = <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the font family for the text of this Snackbar.
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
    /// Gets or sets the size of the font for the text of this Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 14 - Phone: 11
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the background color of the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InverseSurface">MaterialLightTheme.InverseSurface</see> - Dark = <see cref="MaterialDarkTheme.InverseSurface">MaterialDarkTheme.InverseSurface</see>
    /// </default>
    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the width of the border, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 1
    /// </default>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InverseSurface">MaterialLightTheme.InverseSurface</see> - Dark = <see cref="MaterialDarkTheme.InverseSurface">MaterialDarkTheme.InverseSurface</see>
    /// </default>
    /// <remarks>
    /// <para>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>
    /// <para>On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" />is set to a non-default color.</para>
    /// </remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the leading icon color of the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark = <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public Color LeadingIconTintColor
    {
        get => (Color)GetValue(LeadingIconTintColorProperty);
        set => SetValue(LeadingIconTintColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the trailing icon color of the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark = <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public Color TrailingIconTintColor
    {
        get => (Color)GetValue(TrailingIconTintColorProperty);
        set => SetValue(TrailingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the shadow color of the Snackbar.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Shadow">MaterialLightTheme.Shadow</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.Shadow</see>
    /// </default>
    public Color ShadowColor
    {
        get => (Color)GetValue(ShadowColorProperty);
        set => SetValue(ShadowColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow effect cast by the element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialElevation.Level3">MaterialElevation.Level3</see>
    /// </default>
    public Shadow Shadow
    {
        get => (Shadow)GetValue(ShadowProperty);
        set => SetValue(ShadowProperty, value);
    }

    #endregion

    #region Layout

    private MaterialCard _container;
    private Grid _grid;
    private Label _textLabel;
    private MaterialIconButton _leadingIcon;
    private MaterialIconButton _trailingIcon;
    private MaterialButton _actionText;

    #endregion

    #region Constructor

    public MaterialSnackbar()
    {
        Createlayout();
        UpdateLayout();
    }

    private void Createlayout()
    {
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Center;
        
        _container = new MaterialCard()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
            CornerRadius = CornerRadius,
            BackgroundColor = BackgroundColor,
            Padding = Padding,
            StrokeThickness = BorderWidth,
            Stroke = new SolidColorBrush(BorderColor),
            HeightRequest = HeightRequest,
            WidthRequest = WidthRequest,
            MinimumHeightRequest = 48,
            Type = MaterialCardType.Elevated,
            Shadow = Shadow,
            ShadowColor = ShadowColor
        };

        _leadingIcon = new MaterialIconButton()
        {
            Type = MaterialIconButtonType.Standard,
            Padding = new Thickness(0),
            Margin = new Thickness(0,0,12,0),
            ImageSource = LeadingIcon,
            IsVisible = false,
            HeightRequest = 24,
            WidthRequest = 24,
            BackgroundColor = Colors.Transparent,
            IconTintColor = LeadingIconTintColor,
        };
        
        _textLabel = new Label()
        {
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start,
            BackgroundColor = Colors.Transparent,
            Text = Text,
            Margin = new Thickness(0),
            TextColor = TextColor,
            HorizontalOptions = LayoutOptions.Fill,
            LineBreakMode = LineBreakMode.WordWrap
        };
        
        _actionText = new MaterialButton()
        {
            Type = MaterialButtonType.Text,
            Padding = new Thickness(0,0,8,0),
            Text = ActionText,
            HorizontalOptions = LayoutOptions.End,
            BackgroundColor = Colors.Transparent
        };
        
        _trailingIcon = new MaterialIconButton()
        {
            Type = MaterialIconButtonType.Standard,
            Padding = new Thickness(0),
            Margin = new Thickness(12,0,0,0),
            ImageSource = TrailingIcon,
            IsVisible = false,
            HeightRequest = 24,
            WidthRequest = 24,
            BackgroundColor = Colors.Transparent,
            IconTintColor = TrailingIconTintColor,
        };

        _grid = new Grid()
        {
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            ColumnSpacing = 0,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };
        
        _textLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
        _textLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _textLabel.SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _textLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));

        _container.SetBinding(MaterialCard.PaddingProperty, new Binding(nameof(Padding), source: this));
        _container.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        _container.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _container.SetBinding(MaterialCard.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        _container.SetBinding(MaterialCard.WidthRequestProperty, new Binding(nameof(WidthRequest), source: this));
        _container.SetBinding(MaterialCard.StrokeThicknessProperty, new Binding(nameof(BorderWidth), source: this));
        _container.SetBinding(MaterialCard.StrokeProperty, new Binding(nameof(BorderColor), source: this));
        _container.SetBinding(MaterialCard.ShadowProperty, new Binding(nameof(Shadow), source: this));
        _container.SetBinding(MaterialCard.ShadowColorProperty, new Binding(nameof(ShadowColor), source: this));
        _container.SetBinding(MaterialCard.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        
        _leadingIcon.SetBinding(MaterialIconButton.ImageSourceProperty, new Binding(nameof(LeadingIcon), source: this));
        _leadingIcon.SetBinding(MaterialIconButton.TintColorProperty, new Binding(nameof(LeadingIconTintColor), source: this));
        _leadingIcon.SetBinding(MaterialIconButton.CommandProperty, new Binding(nameof(LeadingCommand), source: this));
        _leadingIcon.SetBinding(MaterialIconButton.CommandParameterProperty, new Binding(nameof(LeadingCommandParameter), source: this));
        
        _trailingIcon.SetBinding(MaterialIconButton.ImageSourceProperty, new Binding(nameof(TrailingIcon), source: this));
        _trailingIcon.SetBinding(MaterialIconButton.TintColorProperty, new Binding(nameof(TrailingIconTintColor), source: this));
        _trailingIcon.SetBinding(MaterialIconButton.CommandProperty, new Binding(nameof(TrailingCommand), source: this));
        _trailingIcon.SetBinding(MaterialIconButton.CommandParameterProperty, new Binding(nameof(TrailingCommandParameter), source: this));
        
        _actionText.SetBinding(MaterialButton.TextProperty, new Binding(nameof(ActionText), source: this));
        _actionText.SetBinding(MaterialButton.TextColorProperty, new Binding(nameof(ActionTextColor), source: this));
        _actionText.SetBinding(MaterialButton.FontFamilyProperty, new Binding(nameof(ActionFontFamily), source: this));
        _actionText.SetBinding(MaterialButton.FontSizeProperty, new Binding(nameof(ActionFontSize), source: this));
        _actionText.SetBinding(MaterialButton.CommandProperty, new Binding(nameof(ActionCommand), source: this));
        _actionText.SetBinding(MaterialButton.CommandParameterProperty, new Binding(nameof(ActionCommandParameter), source: this));
        
        _grid.Add(_leadingIcon, 0, 0);
        _grid.Add(_textLabel, 1, 0);
        _grid.Add(_actionText, 2, 0);
        _grid.Add(_trailingIcon, 3, 0);

        _container.Content = _grid;
        Content = _container;
    }
    
    private void UpdateLayout()
    {
        SetLeadingIcon();
        SetTrailingIcon();
    }

    #endregion

    #region Setters

    private void SetLeadingIcon()
    {
        _leadingIcon.ImageSource = LeadingIcon;
        
        if (LeadingIcon != null && !LeadingIcon.IsEmpty)
        {
            _leadingIcon.IsVisible = true;
        }
        else
        {
            _leadingIcon.IsVisible = false;
        }
    }

    private void SetTrailingIcon()
    {
        _trailingIcon.ImageSource = TrailingIcon;
        
        if (TrailingIcon != null && !TrailingIcon.IsEmpty)
        {
            _trailingIcon.IsVisible = true;
        }
        else
        {
            _trailingIcon.IsVisible = false;
        }
    }

    private void SetIconSize()
    {
        if (IconSize > 24)
        {
            _container.MinimumHeightRequest = IconSize + 24;
        }
        else
        {
            _container.MinimumHeightRequest = 48;
        }
        
        _leadingIcon.HeightRequest = IconSize;
        _leadingIcon.WidthRequest = IconSize;
        _trailingIcon.HeightRequest = IconSize;
        _trailingIcon.WidthRequest = IconSize;
        
    }

    #endregion
    
}