using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialIconButtonType
{
    Filled, Tonal, Outlined, Standard, Custom
}

/// <summary>
/// An icon button <see cref="View" /> that reacts to touch events and follows Material Design Guidelines <see href="https://m3.material.io/components/icon-buttons/overview">.
/// </summary>
public class MaterialIconButton : ContentView, ITouchAndPressBehaviorConsumer
{
    // TODO: Shadow doesn't react to VisualStateManager changes

    #region Attributes

    private readonly static MaterialIconButtonType DefaultButtonType = MaterialIconButtonType.Standard;
    private readonly static Color DefaultTintColor = default;
    private readonly static Brush DefaultBackground = ContentView.BackgroundProperty.DefaultValue as Brush;
    private readonly static Color DefaultBackgroundColor = Colors.Transparent;
    private readonly static double DefaultBorderWidth = 0;
    private readonly static Color DefaultBorderColor = Colors.Transparent;
    private readonly static int DefaultCornerRadius = 20;
    private readonly static double DefaultHeightRequest = 40;
    private readonly static double DefaultWidthRequest = 40;
    private readonly static Thickness DefaultPadding = new(8);
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    private readonly static Color DefaultBusyIndicatorColor = MaterialLightTheme.Primary;
    private readonly static double DefaultBusyIndicatorSize = 24;
    private readonly static Shadow DefaultShadow = null;
    private readonly static ImageSource DefaultImageSource = Image.SourceProperty.DefaultValue as ImageSource;

    private readonly Dictionary<MaterialIconButtonType, object> _backgroundColors = new()
    {
        { MaterialIconButtonType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
        { MaterialIconButtonType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer } },
        { MaterialIconButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } }
    };

    private readonly Dictionary<MaterialIconButtonType, object> _iconColors = new()
    {
        { MaterialIconButtonType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } },
        { MaterialIconButtonType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSecondaryContainer, Dark = MaterialDarkTheme.OnSecondaryContainer } },
        { MaterialIconButtonType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant } },
        { MaterialIconButtonType.Standard, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant } },
        { MaterialIconButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } }
    };

    private readonly Dictionary<MaterialIconButtonType, Shadow> _shadows = new()
    {
        { MaterialIconButtonType.Custom, MaterialElevation.Level1 }
    };

    private readonly Dictionary<MaterialIconButtonType, object> _borderColors = new()
    {
        { MaterialIconButtonType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } },
        { MaterialIconButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } }
    };

    private readonly Dictionary<MaterialIconButtonType, double> _borderWidths = new()
    {
        { MaterialIconButtonType.Outlined, 1 },
        { MaterialIconButtonType.Custom, 1 }
    };

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialIconButtonType), typeof(MaterialIconButton), defaultValue: DefaultButtonType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialIconButton self)
        {
            if (Enum.IsDefined(typeof(MaterialIconButtonType), oldValue) &&
                Enum.IsDefined(typeof(MaterialIconButtonType), newValue) &&
                (MaterialIconButtonType)oldValue != (MaterialIconButtonType)newValue)
            {
                self.UpdateLayoutAfterTypeChanged((MaterialIconButtonType)newValue);
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialIconButton));

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialIconButton));

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialIconButton), defaultValue: DefaultTintColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetTintColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TintColor" /> bindable property.
    /// </summary>
    internal static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIconButton), defaultValue: DefaultTintColor);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialIconButton), defaultValue: DefaultBackground, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialIconButton), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialIconButton), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetBorderWidth(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialIconButton), defaultValue: DefaultBorderColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetBorderColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MaterialIconButton), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="ImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(MaterialIconButton), defaultValue: DefaultImageSource);

    /// <summary>
    /// The backing store for the <see cref="Padding" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialIconButton), defaultValue: DefaultPadding);

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialIconButton), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialIconButton), defaultValue: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialIconButton));

    /// <summary>
    /// The backing store for the <see cref="HeightRequest" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialIconButton), defaultValue: DefaultHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="WidthRequest" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(MaterialIconButton), defaultValue: DefaultWidthRequest);

    /// <summary>
    /// The backing store for the <see cref="IsBusy"/> bindable property.
    /// </summary>
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(MaterialIconButton), defaultValue: false, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self._border.IsVisible = !(bool)newValue;
            self._internalActivityIndicator.IsVisible = !self._border.IsVisible;
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BusyIndicatorColor"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BusyIndicatorColorProperty = BindableProperty.Create(nameof(BusyIndicatorColor), typeof(Color), typeof(MaterialIconButton), defaultValue: DefaultBusyIndicatorColor);

    /// <summary>
    /// The backing store for the <see cref="BusyIndicatorSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BusyIndicatorSizeProperty = BindableProperty.Create(nameof(BusyIndicatorSize), typeof(double), typeof(MaterialIconButton), defaultValue: DefaultBusyIndicatorSize);

    /// <summary>
    /// The backing store for the <see cref="CustomBusyIndicator"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomBusyIndicatorProperty = BindableProperty.Create(nameof(CustomBusyIndicator), typeof(View), typeof(MaterialIconButton), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialIconButton self)
        {
            if (self._mainLayout.Children.Count > 1)
            {
                self._mainLayout.Children.RemoveAt(1);

                self._internalActivityIndicator = newValue as View ?? self._activityIndicator;
                self._mainLayout.Add(self._internalActivityIndicator);
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Shadow" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialIconButton), defaultValue: DefaultShadow, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialIconButton self)
        {
            self.SetShadow(self.Type);
        }
    });

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the button type according to <see cref="MaterialIconButtonType"/> enum.
    /// The default value is <see cref="MaterialIconButtonType.Filled"/>. This is a bindable property.
    /// </summary>
    public MaterialIconButtonType Type
    {
        get => (MaterialIconButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the button is activated. This is a bindable property.
    /// </summary>
    /// <remarks>This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding for the button. This is a bindable property.
    /// </summary>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the button. This is a bindable property.
    /// </summary>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the button. This is a bindable property.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the button. This is a bindable property.
    /// </summary>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.
    /// </summary>
    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units. This is a bindable property.
    /// </summary>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the Button. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
 
    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color IconTintColor
    {
        get => (Color)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when button is clicked.
    /// The default value is <see cref="AnimationTypes.Fade"/>.
    /// This is a bindable property.
    /// </summary>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

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

    /// <summary>
    /// Gets or sets a custom animation to be executed when button is clicked.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired height override of this element. This is a bindable property.
    /// </summary>
    /// <remarks>
    /// <para>The default value is -1, which means the value is unset; the effective minimum height will be zero.</para>
    /// <para><see cref="HeightRequest"/> does not immediately change the <see cref="Bounds"/> of an element; setting the <see cref="HeightRequest"/> will change the resulting height of the element during the next layout pass.</para>
    /// </remarks>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired width override of this element. This is a bindable property.
    /// </summary>
    /// <remarks>
    /// <para>The default value is -1, which means the value is unset; the effective minimum width will be zero.</para>
    /// <para><see cref="WidthRequest"/> does not immediately change the <see cref="Bounds"/> of an element; setting the <see cref="HeightRequest"/> will change the resulting width of the element during the next layout pass.</para>
    /// </remarks>
    public new double WidthRequest
    {
        get => (double)GetValue(WidthRequestProperty);
        set => SetValue(WidthRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets if button is on busy state (executing Command).
    /// The default value is <see langword="false"/>.
    /// This is a bindable property.
    /// </summary>
    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the busy indicator.
    /// This is a bindable property.
    /// </summary>
    public Color BusyIndicatorColor
    {
        get => (Color)GetValue(BusyIndicatorColorProperty);
        set => SetValue(BusyIndicatorColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the size for the busy indicator.
    /// This is a bindable property.
    /// </summary>
    public double BusyIndicatorSize
    {
        get => (double)GetValue(BusyIndicatorSizeProperty);
        set => SetValue(BusyIndicatorSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom <see cref="View" /> for busy indicator.
    /// This is a bindable property.
    /// </summary>
    public View CustomBusyIndicator
    {
        get => (View)GetValue(CustomBusyIndicatorProperty);
        set => SetValue(CustomBusyIndicatorProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow effect cast by the element. This is a bindable property.
    /// </summary>
    public new Shadow Shadow
    {
        get { return (Shadow)GetValue(ShadowProperty); }
        set { SetValue(ShadowProperty, value); }
    }

    #endregion Properties

    #region Events

    private EventHandler _clicked;
    private EventHandler _pressed;
    private EventHandler _released;
    private readonly object _objectLock = new();

    /// <summary>
    /// Occurs when the button is clicked/tapped.
    /// </summary>
    public event EventHandler Clicked
    {
        add
        {
            lock (_objectLock)
            {
                _clicked += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _clicked -= value;
            }
        }
    }

    /// <summary>
    /// Occurs when the button is pressed.
    /// </summary>
    public event EventHandler Pressed
    {
        add
        {
            lock (_objectLock)
            {
                _pressed += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _pressed -= value;
            }
        }
    }

    /// <summary>
    /// Occurs when the button is released.
    /// </summary>
    public event EventHandler Released
    {
        add
        {
            lock (_objectLock)
            {
                _released += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _released -= value;
            }
        }
    }

    protected virtual void InternalPressedHandler(object sender, EventArgs e)
    {
        VisualStateManager.GoToState(this, ButtonCommonStates.Pressed);
    }

    protected virtual void InternalReleasedHandler(object sender, EventArgs e)
    {
        VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
    }

    protected virtual void InternalClickedHandler(object sender, EventArgs e)
    {
        VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
    }

    #endregion Events

    #region Layout

    private Grid _mainLayout;
    private Border _border;
    private Image _image;
    private MaterialProgressIndicator _activityIndicator;
    private View _internalActivityIndicator;

    #endregion Layout

    public MaterialIconButton()
    {
        CreateLayout();
        if (Type == DefaultButtonType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    private void CreateLayout()
    {
        Shadow = DefaultShadow;
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;

        // Icon
        _image = new()
        {
            Aspect = Aspect.AspectFit
        };

        _image.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));

        var iconTintColor = new IconTintColorBehavior();
        iconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TintColor), source: this));
        _image.Behaviors.Add(iconTintColor);

        // Container
        var shape = new RoundRectangle();
        shape.SetBinding(RoundRectangle.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        _border = new()
        {
            Background = Background,
            BackgroundColor = BackgroundColor,
            StrokeThickness = BorderWidth,
            Stroke = BorderColor,
            Padding = Padding,
            Shadow = Shadow,
            StrokeShape = shape,
            Content = _image
        };

        _border.SetBinding(Border.PaddingProperty, new Binding(nameof(Padding), source: this));
        _border.SetBinding(Border.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        _border.SetBinding(Border.WidthRequestProperty, new Binding(nameof(WidthRequest), source: this));
        _border.SetBinding(Border.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        Behaviors.Add(new TouchAndPressBehavior());

        _clicked += InternalClickedHandler;
        _pressed += InternalPressedHandler;
        _released += InternalReleasedHandler;

        // Busy indicator
        _activityIndicator = new();
        _activityIndicator.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
        _activityIndicator.SetBinding(MaterialProgressIndicator.HeightRequestProperty, new Binding(nameof(BusyIndicatorSize), source: this));
        _activityIndicator.SetBinding(MaterialProgressIndicator.WidthRequestProperty, new Binding(nameof(BusyIndicatorSize), source: this));

        _internalActivityIndicator = CustomBusyIndicator ?? _activityIndicator;
        _internalActivityIndicator.IsVisible = !_image.IsVisible;

        _mainLayout = new()
        {
            _border,
            _internalActivityIndicator
        };

        Content = _mainLayout;
    }

    private void UpdateLayoutAfterTypeChanged(MaterialIconButtonType type)
    {
        SetBackground(type);
        SetBackgroundColor(type);
        SetTintColor(type);
        SetBorderColor(type);
        SetBorderWidth(type);
        SetShadow(type);
    }

    private void SetBackground(MaterialIconButtonType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            if ((Background == null && DefaultBackground != null) || !Background.Equals(DefaultBackground))
            {
                // Set by user
                _border.Background = Background;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _border.Background = DefaultBackground;
        }
    }

    private void SetBackgroundColor(MaterialIconButtonType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
            {
                // Default Material value according to Type
                if (background is Color backgroundColor)
                {
                    _border.BackgroundColor = backgroundColor;
                }
                else if (background is AppThemeBindingExtension theme)
                {
                    _border.BackgroundColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _border.BackgroundColor = BackgroundColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _border.BackgroundColor = DefaultBackgroundColor;
        }
    }

    private void SetTintColor(MaterialIconButtonType type)
    {
        if (_iconColors.TryGetValue(type, out object tint) && tint != null)
        {
            if ((IconTintColor == null && DefaultTintColor == null) || IconTintColor.Equals(DefaultTintColor))
            {
                // Default Material value according to Type
                if (tint is Color tintColor)
                {
                    TintColor = tintColor;
                }
                else if (tint is AppThemeBindingExtension theme)
                {
                    TintColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                TintColor = IconTintColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            TintColor = DefaultTintColor;
        }
    }

    private void SetBorderColor(MaterialIconButtonType type)
    {
        if (_borderColors.TryGetValue(type, out object border) && border != null)
        {
            if ((BorderColor == null && DefaultBorderColor != null) || BorderColor.Equals(DefaultBorderColor))
            {
                // Default Material value according to Type
                if (border is Color borderColor)
                {
                    _border.Stroke = borderColor;
                }
                else if (border is AppThemeBindingExtension theme)
                {
                    _border.Stroke = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _border.Stroke = BorderColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _border.Stroke = DefaultBorderColor;
        }
    }

    private void SetBorderWidth(MaterialIconButtonType type)
    {
        if (_borderWidths.TryGetValue(type, out double width))
        {
            if (BorderWidth.Equals(DefaultBorderWidth))
            {
                // Default Material value according to Type
                _border.StrokeThickness = width;
            }
            else
            {
                // Set by user
                _border.StrokeThickness = BorderWidth;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _border.StrokeThickness = DefaultBorderWidth;
        }
    }

    private void SetShadow(MaterialIconButtonType type)
    {
        if (_shadows.TryGetValue(type, out Shadow shadow))
        {
            if ((Shadow == null && DefaultShadow == null) || Shadow.Equals(DefaultShadow))
            {
                // Default Material value according to Type
                _border.Shadow = shadow;
            }
            else
            {
                // Set by user
                _border.Shadow = Shadow;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _border.Shadow = DefaultShadow;
        }
    }

    #region ITouchAndPressBehaviorConsumer

    public void ConsumeEvent(EventType gestureType)
    {
        TouchAndPressAnimation.Animate(this, gestureType);
        if (!IsEnabled) return;

        switch (gestureType)
        {
            case EventType.Pressing:
                _pressed?.Invoke(this, null);
                break;
            case EventType.Released:
                if (_released != null)
                {
                    _released.Invoke(this, null);
                }
                else
                {
                    _clicked?.Invoke(this, null);
                }
                break;
            default:
                VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
                break;
        }
    }

    public void ExecuteAction()
    {
        if (!IsEnabled) return;

        if (Command != null && Command.CanExecute(CommandParameter))
            Command.Execute(CommandParameter);
       
        if (_released != null)
        {
            _released.Invoke(this, null);
        }
        else
        {
            _clicked?.Invoke(this, null);
        }
    }

    #endregion ITouchAndPressBehaviorConsumer

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialIconButton.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabledState.Setters.Add(
            MaterialIconButton.IconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(MaterialIconButton.ShadowProperty, null);

        disabledState.Setters.Add(
            MaterialIconButton.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };

        commonStatesGroup.States.Add(new VisualState { Name = ButtonCommonStates.Normal });
        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialIconButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}