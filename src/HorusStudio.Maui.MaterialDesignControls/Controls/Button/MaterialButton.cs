using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using static Microsoft.Maui.Controls.Button;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialButton" /> types
/// </summary>
public enum MaterialButtonType
{
    /// <summary>Elevated button</summary>
    Elevated, 
    /// <summary>Filled button</summary>
    Filled, 
    /// <summary>Filled tonal button</summary>
    Tonal, 
    /// <summary>Outlined button</summary>
    Outlined, 
    /// <summary>Text button</summary>
    Text, 
    /// <summary>Custom button</summary>
    Custom
}

/// <summary>
/// A button <see cref="View" /> that reacts to touch events and follows Material Design Guidelines <see href="https://m3.material.io/components/buttons/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialButton.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialButton
///     Type="Elevated"
///     Text="Confirm"
///     Command="{Binding ButtonCommand}"
///     IsBusy="{Binding ButtonCommand.IsRunning}"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var button = new MaterialButton
/// {
///     Type = MaterialButtonType.Filled,
///     Text = "Save",
///     Command = ButtonCommand,
///     IsBusy = ButtonCommand.IsRunning
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ButtonPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] IconTintColor doesn't react to VisualStateManager changes.
/// * Shadow doesn't react to VisualStateManager changes.
/// * ContentLayout is buggy.
/// * Add default Material behavior for pressed state on default styles (v2).
/// * [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)
/// </todoList>
public class MaterialButton : ContentView, ITouchable
{
    #region Attributes

    private static readonly MaterialButtonType DefaultButtonType = MaterialButtonType.Filled;
    private static readonly ButtonContentLayout DefaultContentLayout = new(ButtonContentLayout.ImagePosition.Left, 8);
    private static readonly Color DefaultTextColor = Color.FromRgba(1,1,1,.01);
    private static readonly Color? DefaultTintColor = null;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Medium;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.LabelLarge;
    private static readonly Brush? DefaultBackground = Button.BackgroundProperty.DefaultValue as Brush;
    private static readonly Color DefaultBackgroundColor = Color.FromRgba(1,1,1,.01);
    private const double DefaultBorderWidth = 0;
    private static readonly Color DefaultBorderColor = Color.FromRgba(1,1,1,.01);
    private const int DefaultCornerRadius = 20;
    private const double DefaultHeightRequest = 40;
    private static readonly Thickness DefaultPadding = new(24, 0);
    private static readonly Thickness DefaultLeftIconPadding = new(16, 0, 24, 0);
    private static readonly Thickness DefaultRightIconPadding = new(24, 0, 16, 0);
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationType = _ => MaterialAnimation.Type;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationParameter = _ => MaterialAnimation.Parameter;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBusyIndicatorColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private const double DefaultBusyIndicatorSize = 24;
    private static readonly Shadow DefaultShadow = null!;

    private readonly Dictionary<MaterialButtonType, object> _backgroundColors = new()
    {
        { MaterialButtonType.Elevated, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow } },
        { MaterialButtonType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
        { MaterialButtonType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer } },
        { MaterialButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } }
    };

    private readonly Dictionary<MaterialButtonType, object> _textColors = new()
    {
        { MaterialButtonType.Elevated, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
        { MaterialButtonType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } },
        { MaterialButtonType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSecondaryContainer, Dark = MaterialDarkTheme.OnSecondaryContainer } },
        { MaterialButtonType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
        { MaterialButtonType.Text, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
        { MaterialButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } }
    };

    private readonly Dictionary<MaterialButtonType, Shadow> _shadows = new()
    {
        { MaterialButtonType.Elevated, MaterialElevation.Level1 },
        { MaterialButtonType.Custom, MaterialElevation.Level1 }
    };

    private readonly Dictionary<MaterialButtonType, object> _borderColors = new()
    {
        { MaterialButtonType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } },
        { MaterialButtonType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } }
    };

    private readonly Dictionary<MaterialButtonType, double> _borderWidths = new()
    {
        { MaterialButtonType.Outlined, 1 },
        { MaterialButtonType.Custom, 1 }
    };

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialButtonType), typeof(MaterialButton), defaultValue: DefaultButtonType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialButton self)
        {
            if (Enum.IsDefined(typeof(MaterialButtonType), oldValue) &&
                Enum.IsDefined(typeof(MaterialButtonType), newValue) &&
                (MaterialButtonType)oldValue != (MaterialButtonType)newValue)
            {
                self.UpdateLayoutAfterTypeChanged((MaterialButtonType)newValue);
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialButton));

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialButton));

    /// <summary>
    /// The backing store for the <see cref="ContentLayout" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ContentLayoutProperty = BindableProperty.Create(nameof(ContentLayout), typeof(ButtonContentLayout), typeof(MaterialButton), defaultValue: DefaultContentLayout, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialButton self)
        {
            if ((newValue == null && oldValue != null) ||
                (newValue is ButtonContentLayout newLayout && (oldValue == null || (oldValue is ButtonContentLayout oldLayout && !oldLayout.Equals(newLayout)))))
            {
                self.UpdatePadding();
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialButton));

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialButton), defaultValue: DefaultTextColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetTextColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialButton), defaultValue: DefaultTintColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetTintColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TintColor" /> bindable property.
    /// </summary>
    internal static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialButton), defaultValue: DefaultTintColor);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialButton), Button.CharacterSpacingProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialButton), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialButton), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="TextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialButton), defaultValue: Button.TextTransformProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialButton), defaultValue: Button.FontAttributesProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialButton), defaultValue: Button.FontAutoScalingEnabledProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialButton), defaultValue: DefaultBackground, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialButton), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialButton), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetBorderWidth(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialButton), defaultValue: DefaultBorderColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetBorderColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MaterialButton), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="ImageSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(MaterialButton), propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialButton self)
        {
            if ((newValue == null && oldValue != null) ||
                (newValue is ImageSource newImage && (oldValue == null || (oldValue is ImageSource oldImage && !oldImage.Equals(newImage)))))
            {
                self.UpdatePadding();
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Padding" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialButton), defaultValue: DefaultPadding, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LineBreakMode"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode), typeof(LineBreakMode), typeof(MaterialButton), defaultValue: Button.LineBreakModeProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialButton), defaultValueCreator: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialButton), defaultValueCreator: DefaultAnimationParameter);
    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialButton));

    /// <summary>
    /// The backing store for the <see cref="HeightRequest" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialButton), defaultValue: DefaultHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="IsBusy"/> bindable property.
    /// </summary>
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(MaterialButton), defaultValue: false, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialButton self)
        {
            self._button.IsVisible = !(bool)newValue;
            self._activityIndicatorContainer.IsVisible = !self._button.IsVisible;
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BusyIndicatorColor"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BusyIndicatorColorProperty = BindableProperty.Create(nameof(BusyIndicatorColor), typeof(Color), typeof(MaterialButton), defaultValueCreator: DefaultBusyIndicatorColor);

    /// <summary>
    /// The backing store for the <see cref="BusyIndicatorSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BusyIndicatorSizeProperty = BindableProperty.Create(nameof(BusyIndicatorSize), typeof(double), typeof(MaterialButton), defaultValue: DefaultBusyIndicatorSize);

    /// <summary>
    /// The backing store for the <see cref="CustomBusyIndicator"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomBusyIndicatorProperty = BindableProperty.Create(nameof(CustomBusyIndicator), typeof(View), typeof(MaterialButton), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialButton self)
        {
            if (self._activityIndicatorContainer.Children.Count > 0)
            {
                self._activityIndicatorContainer.Children.Clear();
            }

            self._internalActivityIndicator = newValue as View ?? self._activityIndicator;
            self._internalActivityIndicator.HorizontalOptions = LayoutOptions.Center;
            self._internalActivityIndicator.VerticalOptions = LayoutOptions.Center;

            self._activityIndicatorContainer.Add(self._internalActivityIndicator);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Shadow" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialButton), defaultValue: DefaultShadow, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialButton self)
        {
            self.SetShadow(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TextDecorations" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextDecorationsProperty = BindableProperty.Create(nameof(TextDecorations), typeof(TextDecorations), typeof(MaterialButton), defaultValue: TextDecorations.None);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the button type according to <see cref="MaterialButtonType"/> enum.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialButtonType.Filled"/>
    /// </default>
    public MaterialButtonType Type
    {
        get => (MaterialButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the button is activated.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets an object that controls the position of the button image and the spacing between the button's image and the button's text.
    /// This is a bindable property.
    /// </summary>
    public ButtonContentLayout ContentLayout
    {
        get => (ButtonContentLayout)GetValue(ContentLayoutProperty);
        set => SetValue(ContentLayoutProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding for the button.
    /// This is a bindable property.
    /// </summary>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Determines how <see cref="Text"/> is shown when the length is overflowing the size of this button.
    /// This is a bindable property.
    /// </summary>
    public LineBreakMode LineBreakMode
    {
        get => (LineBreakMode)GetValue(LineBreakModeProperty);
        set => SetValue(LineBreakModeProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the button.
    /// This is a bindable property.
    /// </summary>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the button.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the button, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the Button.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OnPrimary">MaterialLightTheme.OnPrimary</see> - Dark: <see cref="MaterialDarkTheme.OnPrimary">MaterialDarkTheme.OnPrimary</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    public Color? IconTintColor
    {
        get => (Color?)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    public Color? TintColor
    {
        get => (Color?)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between each of the characters of <see cref="Text"/> when displayed on the button.
    /// This is a bindable property.
    /// </summary>
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
    /// Gets or sets the font family for the text of this entry.
    /// This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the font for the text of this entry.
    /// This is a bindable property.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Determines whether or not the font of this entry should scale automatically according to the operating system settings.
    /// This is a bindable property.
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
    /// Applies text transformation to the <see cref="Text"/> displayed on this button.
    /// This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when button is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade">AnimationTypes.Fade</see>.
    /// </default>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when button is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired height override of this element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// -1
    /// </default>
    /// <remarks>
    /// <para>which means the value is unset; the effective minimum height will be zero.</para>
    /// <para><see cref="HeightRequest"/> does not immediately change the Bounds of an element; setting the <see cref="HeightRequest"/> will change the resulting height of the element during the next layout pass.</para>
    /// </remarks>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets if button is on busy state (executing Command).
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
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
    public View? CustomBusyIndicator
    {
        get => (View?)GetValue(CustomBusyIndicatorProperty);
        set => SetValue(CustomBusyIndicatorProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow effect cast by the element.
    /// This is a bindable property.
    /// </summary>
    public new Shadow Shadow
    {
        get => (Shadow)GetValue(ShadowProperty);
        set => SetValue(ShadowProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="TextDecorations" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    public TextDecorations TextDecorations
    {
        get => (TextDecorations)GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    #endregion Properties

    #region Events

    private EventHandler? _clicked;
    private EventHandler? _released;
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
                _button.Clicked += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _clicked -= value;
                _button.Clicked -= value;
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
                _button.Pressed += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _button.Pressed -= value;
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
                _button.Released += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _released -= value;
                _button.Released -= value;
            }
        }
    }

    /// <summary>
    /// Occurs when the button is focused.
    /// </summary>
    public new event EventHandler<FocusEventArgs> Focused
    {
        add
        {
            lock (_objectLock)
            {
                _button.Focused += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _button.Focused -= value;
            }
        }
    }

    /// <summary>
    /// Occurs when the button is unfocused.
    /// </summary>
    public new event EventHandler<FocusEventArgs> Unfocused
    {
        add
        {
            lock (_objectLock)
            {
                _button.Unfocused += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _button.Unfocused -= value;
            }
        }
    }

    protected virtual void InternalFocusHandler(object? sender, FocusEventArgs e)
    {
        VisualStateManager.GoToState(this,
            e.IsFocused ? ButtonCommonStates.Focused : ButtonCommonStates.Normal);
    }

    protected virtual void InternalPressedHandler(object? sender, EventArgs e)
    {
        VisualStateManager.GoToState(this, ButtonCommonStates.Pressed);
        OnTouch(TouchType.Pressed);
    }

    protected virtual void InternalReleasedHandler(object? sender, EventArgs e)
    {
        VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
        OnTouch(TouchType.Released);
    }

    protected virtual void InternalClickedHandler(object? sender, EventArgs e)
    {
    }

    #endregion Events

    #region Layout

    private Grid _mainLayout = null!;
    private CustomButton _button = null!;
    private MaterialProgressIndicator _activityIndicator = null!;
    private View _internalActivityIndicator = null!;
    private Grid _activityIndicatorContainer = null!;

    #endregion Layout

    public MaterialButton()
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

        // Button
        _button = new()
        {
            Background = Background,
            BackgroundColor = BackgroundColor,
            TextColor = TextColor,
            BorderWidth = BorderWidth,
            BorderColor = BorderColor,
            Padding = Padding,
            Shadow = Shadow
        };

        _button.SetBinding(Button.ContentLayoutProperty, new Binding(nameof(ContentLayout), source: this));
        _button.SetBinding(Button.TextProperty, new Binding(nameof(Text), source: this));
        _button.SetBinding(Button.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _button.SetBinding(Button.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _button.SetBinding(Button.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _button.SetBinding(Button.TextTransformProperty, new Binding(nameof(TextTransform), source: this));
        _button.SetBinding(Button.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _button.SetBinding(Button.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _button.SetBinding(Button.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        _button.SetBinding(Button.ImageSourceProperty, new Binding(nameof(ImageSource), source: this));
        _button.SetBinding(Button.LineBreakModeProperty, new Binding(nameof(LineBreakMode), source: this));
        _button.SetBinding(Button.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        _button.SetBinding(Button.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _button.SetBinding(CustomButton.TextDecorationsProperty, new Binding(nameof(TextDecorations), source: this));

        _button.Clicked += InternalClickedHandler;
        _button.Pressed += InternalPressedHandler;
        _button.Released += InternalReleasedHandler;
        _button.Focused += InternalFocusHandler;
        _button.Unfocused += InternalFocusHandler;

        var iconTintColor = new IconTintColorBehavior();
        iconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TintColor), source: this));
        _button.Behaviors.Add(iconTintColor);

        // Busy indicator
        _activityIndicator = new();
        _activityIndicator.SetBinding(MaterialProgressIndicator.IsVisibleProperty, new Binding(nameof(IsBusy), source: this));
        _activityIndicator.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
        _activityIndicator.SetBinding(MaterialProgressIndicator.HeightRequestProperty, new Binding(nameof(BusyIndicatorSize), source: this));
        _activityIndicator.SetBinding(MaterialProgressIndicator.WidthRequestProperty, new Binding(nameof(BusyIndicatorSize), source: this));

        _internalActivityIndicator = CustomBusyIndicator ?? _activityIndicator;
        _internalActivityIndicator.HorizontalOptions = LayoutOptions.Center;
        _internalActivityIndicator.VerticalOptions = LayoutOptions.Center;

        _activityIndicatorContainer = new Grid { _internalActivityIndicator };
        _activityIndicatorContainer.IsVisible = !_button.IsVisible;

        // Main Layout
        var rowDefinition = new RowDefinition();
        rowDefinition.SetBinding(RowDefinition.HeightProperty, new Binding(nameof(HeightRequest), source: this));

        _mainLayout = new()
        {
            _button,
            _activityIndicatorContainer
        };
        _mainLayout.AddRowDefinition(rowDefinition);

        Content = _mainLayout;
    }

    private void UpdateLayoutAfterTypeChanged(MaterialButtonType type)
    {
        SetBackground(type);
        SetBackgroundColor(type);
        SetTextColor(type);
        SetTintColor(type);
        SetBorderColor(type);
        SetBorderWidth(type);
        SetShadow(type);
    }

    private void SetBackground(MaterialButtonType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            if ((Background == null && DefaultBackground != null) || !Background.Equals(DefaultBackground))
            {
                // Set by user
                _button.Background = Background;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.Background = DefaultBackground;
        }
    }

    private void SetBackgroundColor(MaterialButtonType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
            {
                // Default Material value according to Type
                if (background is Color backgroundColor)
                {
                    _button.BackgroundColor = backgroundColor;
                }
                else if (background is AppThemeBindingExtension theme)
                {
                    _button.BackgroundColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _button.BackgroundColor = BackgroundColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.BackgroundColor = DefaultBackgroundColor;
        }
    }

    private void SetTextColor(MaterialButtonType type)
    {
        if (_textColors.TryGetValue(type, out object text) && text != null)
        {
            if ((TextColor == null && DefaultTextColor == null) || TextColor.Equals(DefaultTextColor))
            {
                // Default Material value according to Type
                if (text is Color textColor)
                {
                    _button.TextColor = textColor;
                }
                else if (text is AppThemeBindingExtension theme)
                {
                    _button.TextColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _button.TextColor = TextColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.TextColor = DefaultTextColor;
        }
    }

    private void SetTintColor(MaterialButtonType type)
    {
        if (_textColors.TryGetValue(type, out object tint) && tint != null)
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

    private void SetBorderColor(MaterialButtonType type)
    {
        if (_borderColors.TryGetValue(type, out object border) && border != null)
        {
            if ((BorderColor == null && DefaultBorderColor != null) || BorderColor.Equals(DefaultBorderColor))
            {
                // Default Material value according to Type
                if (border is Color borderColor)
                {
                    _button.BorderColor = borderColor;
                }
                else if (border is AppThemeBindingExtension theme)
                {
                    _button.BorderColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                _button.BorderColor = BorderColor;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.BorderColor = DefaultBorderColor;
        }
    }

    private void SetBorderWidth(MaterialButtonType type)
    {
        if (_borderWidths.TryGetValue(type, out double width))
        {
            if (BorderWidth.Equals(DefaultBorderWidth))
            {
                // Default Material value according to Type
                _button.BorderWidth = width;
            }
            else
            {
                // Set by user
                _button.BorderWidth = BorderWidth;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.BorderWidth = DefaultBorderWidth;
        }
    }

    private void SetShadow(MaterialButtonType type)
    {
        if (_shadows.TryGetValue(type, out Shadow? shadow))
        {
            if ((Shadow == null && DefaultShadow == null) || Shadow.Equals(DefaultShadow))
            {
                // Default Material value according to Type
                _button.Shadow = shadow;
            }
            else
            {
                // Set by user
                _button.Shadow = Shadow;
            }
        }
        else
        {
            // Unsupported for current button type, ignore
            _button.Shadow = DefaultShadow;
        }
    }

    private void UpdatePadding()
    {
        var hasIcon = ImageSource != null && ContentLayout != null;

        if (Padding.Equals(DefaultPadding) ||
            ((Padding.Equals(DefaultLeftIconPadding) || Padding.Equals(DefaultRightIconPadding)) && hasIcon))
        {
            // Default Material value according to Type
            if (hasIcon && ContentLayout != null)
            {
                if (ContentLayout.Position == ButtonContentLayout.ImagePosition.Left)
                {
                    _button.Padding = DefaultLeftIconPadding;
                }
                else if (ContentLayout.Position == ButtonContentLayout.ImagePosition.Right
                    && (Padding == DefaultPadding || Padding == DefaultLeftIconPadding))
                {
                    _button.Padding = DefaultRightIconPadding;
                }
            }
        }
        else
        {
            // Set by user
            _button.Padding = Padding;
        }
    }

    #region ITouchable

    public async void OnTouch(TouchType gestureType)
    {
        await TouchAnimation.AnimateAsync(this, gestureType);

        if (gestureType == TouchType.Released)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
            else if (_released != null)
            {
                _released.Invoke(this, EventArgs.Empty);
            }
            else if (_clicked != null)
            {
                _clicked.Invoke(this, EventArgs.Empty);
            }
        }
    }

    #endregion ITouchable

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialButton.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabledState.Setters.Add(
            MaterialButton.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(
            MaterialButton.IconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(MaterialButton.ShadowProperty, null);

        disabledState.Setters.Add(
            MaterialButton.BorderColorProperty,
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

        var style = new Style(typeof(MaterialButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}

public abstract class ButtonCommonStates : VisualStateManager.CommonStates
{
    public const string Pressed = "Pressed";
}