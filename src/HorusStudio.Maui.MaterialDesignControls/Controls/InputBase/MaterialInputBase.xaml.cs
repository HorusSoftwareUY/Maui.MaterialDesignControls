using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialInputType
{
    /// <summary>Filled input type</summary>
    Filled,
    /// <summary>Outlined input type</summary>
    Outlined
}

public enum MaterialInputTypeStates
{
    /// <summary>Filled disabled state</summary>
    FilledDisabled,
    /// <summary>Filled focused state</summary>
    FilledFocused,
    /// <summary>Filled normal state</summary>
    FilledNormal,
    /// <summary>Filled with error state</summary>
    FilledError,
    /// <summary>Filled focused with error state</summary>
    FilledErrorFocused,
    /// <summary>Outlined disabled state</summary>
    OutlinedDisabled,
    /// <summary>Outlined focused state</summary>
    OutlinedFocused,
    /// <summary>Outlined normal state</summary>
    OutlinedNormal,
    /// <summary>Outlined with error state</summary>
    OutlinedError,
    /// <summary>Outlined focused with error state</summary>
    OutlinedErrorFocused
}

public abstract partial class MaterialInputBase
{
    #region Attributes

    protected static readonly MaterialInputType DefaultInputType = MaterialInputType.Filled;
    protected static readonly bool DefaultIsEnabled = true;
    protected static readonly Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialLightTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    protected static readonly Color DefaultIconTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly Brush DefaultBackground = Entry.BackgroundProperty.DefaultValue as Brush;
    protected static readonly Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialLightTheme.SurfaceContainer }.GetValueForCurrentTheme<Color>();
    protected static readonly double DefaultBorderWidth = 1;
    protected static readonly Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly CornerRadius DefaultCornerRadius = new(0);
    protected static readonly TextAlignment DefaultHorizontalTextAlignment = TextAlignment.Start;
    protected static readonly string DefaultFontFamily = MaterialFontFamily.Default;
    protected static readonly double DefaultFontSize = MaterialFontSize.BodyLarge;
    protected static readonly Color DefaultPlaceholderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly Color DefaultLabelColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly double DefaultLabelSize = MaterialFontSize.BodySmall;
    #if ANDROID
    protected static readonly Thickness DefaultLabelMargin = new (0,-4,0,0);
    #elif IOS || MACCATALYST
    protected static readonly Thickness DefaultLabelMargin = new (0,-2,0,0);
    #endif
    protected static readonly Thickness DefaultLabelPadding = new (0);
    protected static readonly Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly double DefaultSupportingSize = MaterialFontSize.BodySmall;
    protected static readonly Thickness DefaultSupportingMargin = new (16, 4);
    protected static readonly double DefaultHeightRequest = 48.0;

    private readonly Dictionary<MaterialInputTypeStates, object> _backgroundColors = new()
    {
        { MaterialInputTypeStates.FilledDisabled, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest.WithAlpha(0.04f), Dark = MaterialLightTheme.SurfaceContainerHighest.WithAlpha(0.04f) } },
        { MaterialInputTypeStates.FilledFocused, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialLightTheme.SurfaceContainerHighest } },
        { MaterialInputTypeStates.FilledNormal, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialLightTheme.SurfaceContainerHighest } },
        { MaterialInputTypeStates.FilledError, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialLightTheme.SurfaceContainerHighest } },
        { MaterialInputTypeStates.FilledErrorFocused, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialLightTheme.SurfaceContainerHighest } },
        { MaterialInputTypeStates.OutlinedDisabled, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedFocused, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedNormal, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedError, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedErrorFocused, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
    };

    private readonly Dictionary<MaterialInputType, CornerRadius> _cornerRadius = new()
    {
        { MaterialInputType.Filled, new CornerRadius(4,4,0,0) },
        { MaterialInputType.Outlined, new CornerRadius(4) }
    };

    private readonly Dictionary<MaterialInputTypeStates, double> _borderWidths = new()
    {
        { MaterialInputTypeStates.FilledDisabled, 1 },
        { MaterialInputTypeStates.FilledFocused, 2 },
        { MaterialInputTypeStates.FilledNormal, 1},
        { MaterialInputTypeStates.FilledError, 1 },
        { MaterialInputTypeStates.FilledErrorFocused, 2 },
        { MaterialInputTypeStates.OutlinedDisabled, 1 },
        { MaterialInputTypeStates.OutlinedFocused, 2 },
        { MaterialInputTypeStates.OutlinedNormal, 1 },
        { MaterialInputTypeStates.OutlinedError, 1 },
        { MaterialInputTypeStates.OutlinedErrorFocused, 2 }
    };

    private readonly Dictionary<MaterialInputType, Thickness> _defaultLabelPadding = new()
    {
        { MaterialInputType.Filled, new Thickness(0) },
        { MaterialInputType.Outlined, new Thickness(4,1) }
    };
    
    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialInputType), typeof(MaterialInputBase), defaultValue: DefaultInputType, defaultBindingMode: BindingMode.OneTime, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.UpdateLayoutAfterTypeChanged(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialInputBase), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetIsEnabled(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputBase), propertyChanged: (bindableObject, _, newValue) =>
    {
        if(bindableObject is MaterialInputBase self && newValue is string value && string.IsNullOrWhiteSpace(self.Label))
        {
            self.Label = value;
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SupportingText" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialInputBase), defaultValue: DefaultBackground, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialInputBase), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetCornerRadius(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LeadingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconSourceProperty = BindableProperty.Create(nameof(LeadingIconSource), typeof(ImageSource), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="TrailingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconSourceProperty = BindableProperty.Create(nameof(TrailingIconSource), typeof(ImageSource), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="IsFocused"/> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment"/> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MaterialInputBase), defaultValue: DefaultHorizontalTextAlignment);

    /// <summary>
    /// The backing store for the <see cref="FontFamily"/> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialInputBase), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="FontSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderFontFamily"/> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialInputBase), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderSizeProperty = BindableProperty.Create(nameof(PlaceholderSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="PlaceholderColor"/> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultPlaceholderColor);

    /// <summary>
    /// The backing store for the <see cref="LabelColor"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultLabelColor);

    /// <summary>
    /// The backing store for the <see cref="LabelSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultLabelSize);
    
    /// <summary>
    /// The backing store for the <see cref="LabelFontFamily"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontFamilyProperty = BindableProperty.Create(nameof(LabelFontFamily), typeof(string), typeof(MaterialInputBase), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="LabelMargin"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelMarginProperty = BindableProperty.Create(nameof(LabelMargin), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultLabelMargin);
    
    /// <summary>
    /// The backing store for the <see cref="LabelPadding"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelPaddingProperty = BindableProperty.Create(nameof(LabelPadding), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultLabelPadding);

    /// <summary>
    /// The backing store for the <see cref="LabelLineBreakMode"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelLineBreakModeProperty = BindableProperty.Create(nameof(LabelLineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: LineBreakMode.NoWrap);

    /// <summary>
    /// The backing store for the <see cref="SupportingColor"/> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingColorProperty = BindableProperty.Create(nameof(SupportingColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultSupportingTextColor);

    /// <summary>
    /// The backing store for the <see cref="SupportingSize"/> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultSupportingSize);

    /// <summary>
    /// The backing store for the <see cref="SupportingFontFamily"/> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingFontFamilyProperty = BindableProperty.Create(nameof(SupportingFontFamily), typeof(string), typeof(MaterialInputBase), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="SupportingMargin"/> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingMarginProperty = BindableProperty.Create(nameof(SupportingMargin), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultSupportingMargin);

    /// <summary>
    /// The backing store for the <see cref="SupportingLineBreakMode"/> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingLineBreakModeProperty = BindableProperty.Create(nameof(SupportingLineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: LineBreakMode.NoWrap);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconCommand"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconCommandProperty = BindableProperty.Create(nameof(LeadingIconCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconCommandParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconCommandParameterProperty = BindableProperty.Create(nameof(LeadingIconCommandParameter), typeof(object), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconCommand"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconCommandProperty = BindableProperty.Create(nameof(TrailingIconCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconCommandParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconCommandParameterProperty = BindableProperty.Create(nameof(LeadingIconCommandParameter), typeof(object), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FocusedCommand"/> bindable property.
    /// </summary>
    public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create(nameof(FocusedCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnfocusedCommand"/> bindable property.
    /// </summary>
    public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(nameof(UnfocusedCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HasError"/> bindable property.
    /// </summary>
    public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialInputBase), defaultValue: false, propertyChanged: (bindableObject, _, _) =>
    {
        if (bindableObject is MaterialInputBase self)
        {
            self.UpdateLayoutAfterTypeChanged(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ShowTrailingIconOnlyOnError"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ShowTrailingIconOnlyOnErrorProperty = BindableProperty.Create(nameof(ShowTrailingIconOnlyOnError), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="HeightRequest" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="ShowPlaceholder"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ShowPlaceholderProperty = BindableProperty.Create(nameof(ShowPlaceholder), typeof(bool), typeof(MaterialInputBase), defaultValue: true);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the input type according to <see cref="MaterialInputType"/> enum.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialInputType.Filled"/>
    /// </default>
    public MaterialInputType Type
    {
        get => (MaterialInputType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets if the input is enabled or disabled. This is a bindable property.
    /// </summary>
    /// <default>
    /// True
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// Brush
    /// </default>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the input. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the input. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the input, in device-independent units. This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(0)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units. This is a bindable property.
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
    /// Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>For more options have a look at <see cref="MaterialIconButton"/>.</remarks>
    public ImageSource LeadingIconSource
    {
        get => (ImageSource)GetValue(LeadingIconSourceProperty);
        set => SetValue(LeadingIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>For more options have a look at <see cref="MaterialIconButton"/>.</remarks>
    public ImageSource TrailingIconSource
    {
        get => (ImageSource)GetValue(TrailingIconSourceProperty);
        set => SetValue(TrailingIconSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the placeholder of the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the label of the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the supporting text of the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the input. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark: <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the leading button icon of the input. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color? LeadingIconTintColor
    {
        get => (Color?)GetValue(LeadingIconTintColorProperty);
        set => SetValue(LeadingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the trailing button icon of the input. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color? TrailingIconTintColor
    {
        get => (Color?)GetValue(TrailingIconTintColorProperty);
        set => SetValue(TrailingIconTintColorProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets state focused entry
    /// </summary>
    /// <default>
    /// False
    /// </default>
    public new bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set => SetValue(IsFocusedProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment for the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextAlignment.Start"/>
    /// </default>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the input is tapped.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>This property is used internally, and it's recommended to avoid setting it directly.</remarks>
    public ICommand InputTapCommand { get; set; }

    /// <summary>
    /// Gets or sets font family for input. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default"/>
    /// </default>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets font size for input. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge"/> Tablet = 19 / Phone = 16
    /// </default>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets font family for placeholder. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default"/>
    /// </default>
    public string PlaceholderFontFamily
    {
        get => (string)GetValue(PlaceholderFontFamilyProperty);
        set => SetValue(PlaceholderFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets font size for placeholder. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge"/> Tablet = 19 / Phone = 16
    /// </default>
    public double PlaceholderSize
    {
        get => (double)GetValue(PlaceholderSizeProperty);
        set => SetValue(PlaceholderSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets text color for placeholder. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets text color for label. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets font size for label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall"/> Tablet = 15 / Phone = 12
    /// </default>
    public double LabelSize
    {
        get => (double)GetValue(LabelSizeProperty);
        set => SetValue(LabelSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets font family for label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default"/>
    /// </default>
    public string LabelFontFamily
    {
        get => (string)GetValue(LabelFontFamilyProperty);
        set => SetValue(LabelFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets margin for label. This is a bindable property.
    /// The default value is <value>0</value>
    /// </summary>
    /// <default>
    /// Thickness(0)
    /// </default>
    public Thickness LabelMargin
    {
        get => (Thickness)GetValue(LabelMarginProperty);
        set => SetValue(LabelMarginProperty, value);
    }
    
    /// <summary>
    /// Gets or sets padding for label. This is a bindable property.
    /// The default value is <value>0</value>
    /// </summary>
    /// <default>
    /// Filled: Thickness(0). Outlined: Thickness(4,1)
    /// </default>
    public Thickness LabelPadding
    {
        get => (Thickness)GetValue(LabelPaddingProperty);
        set => SetValue(LabelPaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets line break mode for label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="LineBreakMode.NoWrap"/>
    /// </default>
    public LineBreakMode LabelLineBreakMode
    {
        get => (LineBreakMode)GetValue(LabelLineBreakModeProperty);
        set => SetValue(LabelLineBreakModeProperty, value);
    }

    /// <summary>
    /// Gets or sets text color for supporting text. This is a bindable property.
    /// </summary>
    /// <default>
    ///  Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color SupportingColor
    {
        get => (Color)GetValue(SupportingColorProperty);
        set => SetValue(SupportingColorProperty, value);
    }

    /// <summary>
    /// Gets or sets font family for supporting text. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default"/>
    /// </default>
    public string SupportingFontFamily
    {
        get => (string)GetValue(SupportingFontFamilyProperty);
        set => SetValue(SupportingFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets font size for supporting text. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall"/> Tablet = 15 / Phone = 12
    /// </default>
    public double SupportingSize
    {
        get => (double)GetValue(SupportingSizeProperty);
        set => SetValue(SupportingSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets margin for supporting text. This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(16, 4)
    /// </default>
    public Thickness SupportingMargin
    {
        get => (Thickness)GetValue(SupportingMarginProperty);
        set => SetValue(SupportingMarginProperty, value);
    }

    /// <summary>
    /// Gets or sets line break mode for supporting text. This is a bindable property.
    /// </summary>    
    /// <default>
    /// <see cref="LineBreakMode.NoWrap"/>
    /// </default>
    public LineBreakMode SupportingLineBreakMode
    {
        get => (LineBreakMode)GetValue(SupportingLineBreakModeProperty);
        set => SetValue(SupportingLineBreakModeProperty, value);
    }

    /// <summary>
    /// Gets or sets a Leading icon command. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public ICommand LeadingIconCommand
    {
        get => (ICommand)GetValue(LeadingIconCommandProperty);
        set => SetValue(LeadingIconCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets a Leading icon command parameter. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public object LeadingIconCommandParameter
    {
        get => GetValue(LeadingIconCommandParameterProperty);
        set => SetValue(LeadingIconCommandParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a Trailing Icon command. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public ICommand TrailingIconCommand
    {
        get => (ICommand)GetValue(TrailingIconCommandProperty);
        set => SetValue(TrailingIconCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets a Trailing Icon command parameter. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public object TrailingIconCommandParameter
    {
        get => GetValue(TrailingIconCommandParameterProperty);
        set => SetValue(TrailingIconCommandParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a Command to be invoked when input is Focused. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public ICommand FocusedCommand
    {
        get => (ICommand)GetValue(FocusedCommandProperty);
        set => SetValue(FocusedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets a Command to be invoked when input is Unfocused. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public ICommand UnfocusedCommand
    {
        get => (ICommand)GetValue(UnfocusedCommandProperty);
        set => SetValue(UnfocusedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets if the input has an error. This is a bindable property.
    /// </summary>
    /// <default>
    /// False
    /// </default>
    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets if show the trailing icon only on error. This is a bindable property.
    /// </summary>    
    /// <default>
    /// false
    /// </default>
    public bool ShowTrailingIconOnlyOnError
    {
        get => (bool)GetValue(ShowTrailingIconOnlyOnErrorProperty);
        set => SetValue(ShowTrailingIconOnlyOnErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets the height request
    /// </summary>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets if show the placeholder. 
    /// <value>The default value is true.</value>
    /// </summary>
    public bool ShowPlaceholder
    {
        get => (bool)GetValue(ShowPlaceholderProperty);
        set => SetValue(ShowPlaceholderProperty, value);
    }

    #endregion Properties

    #region Constructor

    protected MaterialInputBase()
    {
        InitializeComponent();

        if (Type == DefaultInputType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    #endregion Constructor

    #region Methods

    private void SetLabelMargin(MaterialInputType type)
    {
        LabelMargin = GetDefaultLabelMargin(type);
    }

    private Thickness GetDefaultLabelMargin(MaterialInputType type)
    {
        _defaultLabelPadding.TryGetValue(type, out Thickness labelPadding);
        return type switch
        {
            MaterialInputType.Outlined => new Thickness(DefaultSupportingMargin.Left - DefaultLabelMargin.Left - labelPadding.Left, 0, 0, 0),
            _ => DefaultLabelMargin
        };
    }
    
    private void SetLabelPadding(MaterialInputType type)
    {
        if (_defaultLabelPadding.TryGetValue(type, out Thickness padding))
        {
            LabelPadding = padding;
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(Window) && Window != null)
        {
            // Window property is set with a value when the view is appearing
            OnAppearing();
        }
        else if (propertyName == nameof(Window) && Window == null)
        {
            // Window property is set on null when the view is disappearing
            OnDisappearing();
        }
    }

    private void OnAppearing()
    {
        // Add tap gesture to the input control to do focus
        var border = (Border)GetTemplateChild("InputBorder");
        if (border != null)
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = InputTapCommand
            };
            border.GestureRecognizers.Add(tapGestureRecognizer);
        }

        OnControlAppearing();
        SetControlTemplate(Type);
    }

    protected abstract void OnControlAppearing();

    private void OnDisappearing()
    {
        OnControlDisappearing();
    }

    protected abstract void OnControlDisappearing();

    protected void UpdateLayoutAfterTypeChanged(MaterialInputType type)
    {
        SetTemplate(type);
        SetIsEnabled(type);
        SetCornerRadius(type);
        SetLabelMargin(type);
        SetLabelPadding(type);
        SetBorderWidth(type);

        UpdateLayoutAfterStatusChanged(type);
    }

    private void SetBorderWidth(MaterialInputType type)
    {
        if (_borderWidths.TryGetValue(GetCurrentTypeState(type), out double borderWidth))
        {
            this.BorderWidth = borderWidth;
        }
    }

    private void UpdateLayoutAfterStatusChanged(MaterialInputType type)
    {
        SetBackground(type);
        SetBackgroundColor(type);
    }

    private void SetTemplate(MaterialInputType type)
    {
        if (type == MaterialInputType.Filled && Resources.TryGetValue("FilledControlTemplate", out object filledTemplate) && filledTemplate is ControlTemplate filledControlTemplate)
        {
            ControlTemplate = filledControlTemplate;
        }
        else if (type == MaterialInputType.Outlined && Resources.TryGetValue("OutlinedControlTemplate", out object outlinedTemplate) && outlinedTemplate is ControlTemplate outlinedControlTemplate)
        {
            ControlTemplate = outlinedControlTemplate;
        }
    }

    protected abstract void SetControlTemplate(MaterialInputType type);

    private MaterialInputTypeStates GetCurrentTypeState(MaterialInputType type)
    {
        if (IsFocused && HasError) 
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledErrorFocused : MaterialInputTypeStates.OutlinedErrorFocused;
        if (IsFocused)
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledFocused : MaterialInputTypeStates.OutlinedFocused;
        if (HasError)
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledError : MaterialInputTypeStates.OutlinedError;
        if (!IsEnabled)
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledDisabled : MaterialInputTypeStates.OutlinedDisabled;
        
        return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledNormal : MaterialInputTypeStates.OutlinedNormal;
    }

    protected string GetCurrentVisualState()
    {
        if (IsFocused)
            return HasError ? MaterialInputCommonStates.ErrorFocused : MaterialInputCommonStates.Focused;
        else if (HasError)
            return MaterialInputCommonStates.Error;
        else if (!IsEnabled)
            return MaterialInputCommonStates.Disabled;
        
        return MaterialInputCommonStates.Normal;
    }

    private void SetIsEnabled(MaterialInputType type)
    {
        SetControlIsEnabled();
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterStatusChanged(type);
    }

    protected abstract void SetControlIsEnabled();

    private void SetBackground(MaterialInputType type)
    {
        var inputBorder = (Border)GetTemplateChild("InputBorder");
        if (inputBorder != null)
        {
            SetBackgroundToView(type, inputBorder);
        }
    }

    private void SetBackgroundToView(MaterialInputType type, View view)
    {
        if (_backgroundColors.TryGetValue(GetCurrentTypeState(type), out object background) && background != null)
        {
            if ((Background == null && DefaultBackground != null) || !Background.Equals(DefaultBackground))
            {
                // Set by user
                view.Background = Background;
            }
        }
        else
        {
            // Unsupported for current input type, ignore
            view.Background = DefaultBackground;
        }
    }

    private void SetBackgroundColor(MaterialInputType type)
    {
        var inputBorder = (Border)GetTemplateChild("InputBorder");
        if (inputBorder != null)
        {
            SetBackgroundColorToView(type, inputBorder);
        }

        if (type == MaterialInputType.Outlined)
        {
            var outlinedHint = (Label)GetTemplateChild("OutlinedHint");
            if (outlinedHint != null)
            {
                SetBackgroundColorToView(type, outlinedHint);
            }
        }
    }

    private void SetBackgroundColorToView(MaterialInputType type, View view)
    {
        if (_backgroundColors.TryGetValue(GetCurrentTypeState(type), out object background) && background != null)
        {
            if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
            {
                // Default Material value according to Type
                if (background is Color backgroundColor)
                {
                    view.BackgroundColor = backgroundColor;
                }
                else if (background is AppThemeBindingExtension theme)
                {
                    view.BackgroundColor = theme.GetValueForCurrentTheme<Color>();
                }
            }
            else
            {
                // Set by user
                view.BackgroundColor = BackgroundColor;
            }
        }
        else
        {
            // Unsupported for current input type, ignore
            view.BackgroundColor = DefaultBackgroundColor;
        }
    }

    private void SetCornerRadius(MaterialInputType type)
    {
        var inputBorder = (Border)GetTemplateChild("InputBorder");
        if (inputBorder != null)
        {
            if (_cornerRadius.TryGetValue(type, out CornerRadius cornerRadius))
            {
                if (CornerRadius.Equals(DefaultCornerRadius))
                {
                    // Default Material value according to Type
                    inputBorder.StrokeShape = new RoundRectangle { CornerRadius = cornerRadius };
                }
                else if (type == MaterialInputType.Outlined)
                {
                    // Set by user
                    inputBorder.StrokeShape = new RoundRectangle { CornerRadius = CornerRadius };
                }
                else if (type == MaterialInputType.Filled
                    && (CornerRadius.BottomLeft > 0 || CornerRadius.BottomRight > 0))
                {
                    // Set by user
                    inputBorder.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius.TopLeft, CornerRadius.TopRight, 0, 0) };
                }
            }
            else
            {
                // Unsupported for current input type, ignore
                inputBorder.StrokeShape = new RoundRectangle { CornerRadius = DefaultCornerRadius };
            }
        }
    }

    #endregion Methods

    #region Styles

    protected static VisualStateGroupList GetBaseStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabled = new VisualState { Name = MaterialInputCommonStates.Disabled };

        disabled.Setters.Add(
            MaterialInputBase.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialInputBase.LabelColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialInputBase.SupportingColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialInputBase.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialInputBase.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(MaterialButton.ShadowProperty, null);

        disabled.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        var focused = new VisualState { Name = MaterialInputCommonStates.Focused };

        focused.Setters.Add(
            MaterialInputBase.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>());

        focused.Setters.Add(
            MaterialInputBase.LabelColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        focused.Setters.Add(
            MaterialInputBase.SupportingColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        focused.Setters.Add(
            MaterialInputBase.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        focused.Setters.Add(
            MaterialInputBase.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        focused.Setters.Add(MaterialButton.ShadowProperty, null);

        focused.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        var normal = new VisualState { Name = MaterialInputCommonStates.Normal };

        normal.Setters.Add(
            MaterialInputBase.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialInputBase.LabelColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialInputBase.SupportingColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialInputBase.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialInputBase.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(MaterialButton.ShadowProperty, null);

        normal.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        var error = new VisualState { Name = MaterialInputCommonStates.Error };

        error.Setters.Add(
            MaterialInputBase.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>());

        error.Setters.Add(
            MaterialInputBase.PlaceholderColorProperty,
            new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Error,
                    Dark = MaterialDarkTheme.Error
                }
                .GetValueForCurrentTheme<Color>());
        
        error.Setters.Add(
            MaterialInputBase.LabelColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Error,
                Dark = MaterialDarkTheme.Error
            }
            .GetValueForCurrentTheme<Color>());

        error.Setters.Add(
            MaterialInputBase.SupportingColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Error,
                Dark = MaterialDarkTheme.Error
            }
            .GetValueForCurrentTheme<Color>());

        error.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Error,
                Dark = MaterialDarkTheme.Error
            }
            .GetValueForCurrentTheme<Color>());

        error.Setters.Add(
            MaterialInputBase.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        error.Setters.Add(
            MaterialInputBase.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Error,
                Dark = MaterialDarkTheme.Error
            }
            .GetValueForCurrentTheme<Color>());
        
        var errorFocused = new VisualState { Name = MaterialInputCommonStates.ErrorFocused };
        error.Setters.ToList().ForEach(s => errorFocused.Setters.Add(s));

        commonStatesGroup.States.Add(disabled);
        commonStatesGroup.States.Add(normal);
        commonStatesGroup.States.Add(focused);
        commonStatesGroup.States.Add(error);
        commonStatesGroup.States.Add(errorFocused);
        
        return [commonStatesGroup];
    }

    #endregion Styles
}

public abstract class MaterialInputCommonStates : VisualStateManager.CommonStates
{
    public const string Error = "Error";
    public const string ErrorFocused = "ErrorFocused";
}