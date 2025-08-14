using System.Runtime.CompilerServices;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Animations;
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

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public abstract partial class MaterialInputBase : IValidableView
{
    #region Attributes

    protected const MaterialInputType DefaultInputType = MaterialInputType.Filled;
    protected const bool DefaultIsEnabled = true;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialLightTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultIconTintColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly Brush? DefaultBackground = Entry.BackgroundProperty.DefaultValue as Brush;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialLightTheme.SurfaceContainer }.GetValueForCurrentTheme<Color>();
    protected const double DefaultBorderWidth = 1;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultBorderColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly CornerRadius DefaultCornerRadius = new(0);
    protected const TextAlignment DefaultHorizontalTextAlignment = TextAlignment.Start;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultPlaceholderColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultLabelColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultLabelSize = _ => MaterialFontSize.BodySmall;
    #if ANDROID
    protected static readonly Thickness DefaultLabelMargin = new (0,-4,0,0);
    #elif IOS || MACCATALYST
    protected static readonly Thickness DefaultLabelMargin = new (0,-2,0,0);
    #endif
    protected static readonly Thickness DefaultLabelPadding = new (0);
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingSize = _ => MaterialFontSize.BodySmall;
    protected static readonly Thickness DefaultSupportingMargin = new (16, 4);
    protected const double DefaultHeightRequest = 48.0;
    protected const bool DefaultAlwaysShowLabel = false;
    protected static readonly BindableProperty.CreateDefaultValueDelegate DefaultErrorIcon = _ => MaterialIcon.Error;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultErrorAnimationType = _ => MaterialAnimation.ErrorAnimationType;

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
    /// The backing store for the <see cref="Type">Type</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialInputType), typeof(MaterialInputBase), defaultValue: DefaultInputType, defaultBindingMode: BindingMode.OneTime, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.UpdateLayoutAfterTypeChanged(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsEnabled">IsEnabled</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialInputBase), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetIsEnabled(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Label">Label</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Placeholder">Placeholder</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputBase), propertyChanged: (bindableObject, _, newValue) =>
    {
        if(bindableObject is MaterialInputBase self && newValue is string value && string.IsNullOrWhiteSpace(self.Label))
        {
            self.Label = value;
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="AlwaysShowLabel">AlwaysShowLabel</see> bindable property.
    /// </summary>
    public static readonly BindableProperty AlwaysShowLabelProperty = BindableProperty.Create(nameof(AlwaysShowLabel), typeof(bool), typeof(MaterialInputBase), defaultValue: DefaultAlwaysShowLabel);

    /// <summary>
    /// The backing store for the <see cref="SupportingText">SupportingText</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="TextColor">TextColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor">LeadingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor">TrailingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="Background">Background</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialInputBase), defaultValue: DefaultBackground, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultBackgroundColor, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBorderWidth(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal static readonly BindableProperty InternalBorderWidthProperty = BindableProperty.Create(nameof(InternalBorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor">BorderColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialInputBase), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetCornerRadius(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LeadingIcon">LeadingIcon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="TrailingIcon">TrailingIcon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialInputBase));
    
    /// <summary>
    /// The backing store for the <see cref="ErrorIcon">ErrorIcon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ErrorIconProperty = BindableProperty.Create(nameof(ErrorIcon), typeof(ImageSource), typeof(MaterialInputBase), defaultValueCreator: DefaultErrorIcon);

    /// <summary>
    /// The backing store for the <see cref="IsFocused">IsFocused</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment">HorizontalTextAlignment</see> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MaterialInputBase), defaultValue: DefaultHorizontalTextAlignment);

    /// <summary>
    /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialInputBase), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialInputBase), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes">FontAttributes</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialInputBase), defaultValue: null);
    
    /// <summary>
    /// The backing store for the <see cref="PlaceholderFontFamily">PlaceholderFontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialInputBase), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderSize">PlaceholderSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderSizeProperty = BindableProperty.Create(nameof(PlaceholderSize), typeof(double), typeof(MaterialInputBase), defaultValueCreator: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="PlaceholderColor">PlaceholderColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultPlaceholderColor);

    /// <summary>
    /// The backing store for the <see cref="PlaceholderLineBreakMode">PlaceholderLineBreakMode</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderLineBreakModeProperty = BindableProperty.Create(nameof(PlaceholderLineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: LineBreakMode.NoWrap);

    /// <summary>
    /// The backing store for the <see cref="LabelColor">LabelColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultLabelColor);

    /// <summary>
    /// The backing store for the <see cref="LabelSize">LabelSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(MaterialInputBase), defaultValueCreator: DefaultLabelSize);
    
    /// <summary>
    /// The backing store for the <see cref="LabelFontFamily">LabelFontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelFontFamilyProperty = BindableProperty.Create(nameof(LabelFontFamily), typeof(string), typeof(MaterialInputBase), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="LabelMargin">LabelMargin</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelMarginProperty = BindableProperty.Create(nameof(LabelMargin), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultLabelMargin);
    
    /// <summary>
    /// The backing store for the <see cref="LabelPadding">LabelPadding</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelPaddingProperty = BindableProperty.Create(nameof(LabelPadding), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultLabelPadding);

    /// <summary>
    /// The backing store for the <see cref="LabelLineBreakMode">LabelLineBreakMode</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelLineBreakModeProperty = BindableProperty.Create(nameof(LabelLineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: LineBreakMode.NoWrap);

    /// <summary>
    /// The backing store for the <see cref="SupportingColor">SupportingColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingColorProperty = BindableProperty.Create(nameof(SupportingColor), typeof(Color), typeof(MaterialInputBase), defaultValueCreator: DefaultSupportingTextColor);

    /// <summary>
    /// The backing store for the <see cref="SupportingSize">SupportingSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialInputBase), defaultValueCreator: DefaultSupportingSize);

    /// <summary>
    /// The backing store for the <see cref="SupportingFontFamily">SupportingFontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingFontFamilyProperty = BindableProperty.Create(nameof(SupportingFontFamily), typeof(string), typeof(MaterialInputBase), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="SupportingMargin">SupportingMargin</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingMarginProperty = BindableProperty.Create(nameof(SupportingMargin), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultSupportingMargin);

    /// <summary>
    /// The backing store for the <see cref="SupportingLineBreakMode">SupportingLineBreakMode</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingLineBreakModeProperty = BindableProperty.Create(nameof(SupportingLineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: LineBreakMode.NoWrap);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconCommand">LeadingIconCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconCommandProperty = BindableProperty.Create(nameof(LeadingIconCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconCommandParameter">LeadingIconCommandParameter</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconCommandParameterProperty = BindableProperty.Create(nameof(LeadingIconCommandParameter), typeof(object), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconCommand">TrailingIconCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconCommandProperty = BindableProperty.Create(nameof(TrailingIconCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconCommandParameter">TrailingIconCommandParameter</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconCommandParameterProperty = BindableProperty.Create(nameof(LeadingIconCommandParameter), typeof(object), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FocusedCommand">FocusedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create(nameof(FocusedCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnfocusedCommand">UnfocusedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(nameof(UnfocusedCommand), typeof(ICommand), typeof(MaterialInputBase), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HasError">HasError</see> bindable property.
    /// </summary>
    public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialInputBase), defaultValue: false, propertyChanged: (bindableObject, _, _) =>
    {
        if (bindableObject is MaterialInputBase self)
        {
            self.SetHasError(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ErrorAnimationType">ErrorAnimationType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ErrorAnimationTypeProperty = BindableProperty.Create(nameof(ErrorAnimationType), typeof(ErrorAnimationTypes), typeof(MaterialInputBase), defaultValueCreator: DefaultErrorAnimationType);

    /// <summary>
    /// The backing store for the <see cref="ErrorAnimation">ErrorAnimation</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ErrorAnimationProperty = BindableProperty.Create(nameof(ErrorAnimation), typeof(IErrorAnimation), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="HeightRequest">HeightRequest</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultHeightRequest);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the input type according to <see cref="MaterialInputType">MaterialInputType</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialInputType.Filled">MaterialInputType.Filled</see>
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
    /// Gets or sets a <see cref="Brush">Brush</see> that describes the background of the input. This is a bindable property.
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
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor">VisualElement.BackgroundColor</see> is set to a non-default color.</remarks>
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
    /// This property is for internal use by the control. <see cref="BorderWidth">BorderWidth</see> property should be used instead.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public double InternalBorderWidth
    {
        get => (double)GetValue(InternalBorderWidthProperty);
        set => SetValue(InternalBorderWidthProperty, value);
    }

    /// <summary>
    /// Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>For more options see <see cref="MaterialIconButton">MaterialIconButton</see>.</remarks>
    public ImageSource LeadingIcon
    {
        get => (ImageSource)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    /// <summary>
    /// Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>For more options see <see cref="MaterialIconButton">MaterialIconButton</see>.</remarks>
    public ImageSource TrailingIcon
    {
        get => (ImageSource)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }
    
    /// <summary>
    /// Allows you to display a trailing icon when input has error. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>For more options see <see cref="MaterialIconButton">MaterialIconButton</see>.</remarks>
    public ImageSource ErrorIcon
    {
        get => (ImageSource)GetValue(ErrorIconProperty);
        set => SetValue(ErrorIconProperty, value);
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
    /// Gets or sets if the label is always displayed.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// False
    /// </default>
    public bool AlwaysShowLabel
    {
        get => (bool)GetValue(AlwaysShowLabelProperty);
        set => SetValue(AlwaysShowLabelProperty, value);
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
    /// Gets or sets the <see cref="Color">color</see> for the text of the input.
    /// This is a bindable property.
    /// This is a bindable property.
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
    /// Gets or sets the <see cref="Color">color</see> for the leading button icon of the input.
    /// This is a bindable property.
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
    /// Gets or sets the <see cref="Color">color</see> for the trailing button icon of the input.
    /// This is a bindable property.
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
    /// Gets or sets the horizontal text alignment for the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextAlignment.Start">TextAlignment.Start</see>
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
    /// Gets or sets font family for input.
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
    /// Gets or sets font size for input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see>: Tablet = 19 / Phone = 16
    /// </default>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of this input is bold, italic, or neither.
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }
    
    /// <summary>
    /// Gets or sets font family for placeholder.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string PlaceholderFontFamily
    {
        get => (string)GetValue(PlaceholderFontFamilyProperty);
        set => SetValue(PlaceholderFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets font size for placeholder.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see>: Tablet = 19 / Phone = 16
    /// </default>
    public double PlaceholderSize
    {
        get => (double)GetValue(PlaceholderSizeProperty);
        set => SetValue(PlaceholderSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets text color for placeholder.
    /// This is a bindable property.
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
    /// Gets or sets line break mode for placeholder.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="LineBreakMode.NoWrap">LineBreakMode.NoWrap</see>
    /// </default>
    public LineBreakMode PlaceholderLineBreakMode
    {
        get => (LineBreakMode)GetValue(PlaceholderLineBreakModeProperty);
        set => SetValue(PlaceholderLineBreakModeProperty, value);
    }

    /// <summary>
    /// Gets or sets text color for label.
    /// This is a bindable property.
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
    /// Gets or sets font size for label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see>: Tablet = 15 / Phone = 12
    /// </default>
    public double LabelSize
    {
        get => (double)GetValue(LabelSizeProperty);
        set => SetValue(LabelSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets font family for label.
    /// This is a bindable property.
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
    /// Gets or sets margin for label.
    /// This is a bindable property.
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
    /// Gets or sets padding for label.
    /// This is a bindable property.
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
    /// <see cref="LineBreakMode.NoWrap">LineBreakMode.NoWrap</see>
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
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
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
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see>: Tablet = 15 / Phone = 12
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
    /// <see cref="LineBreakMode.NoWrap">LineBreakMode.NoWrap</see>
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
    /// Gets or sets the animation type to be executed when the control has an error.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="ErrorAnimationTypes.Shake">ErrorAnimationTypes.Shake</see>
    /// </default>
    /// <remarks>
    /// This property will only be considered if the <see cref="ErrorAnimation">ErrorAnimation</see> property is <see langword="null">null</see>.
    /// </remarks>
    public ErrorAnimationTypes ErrorAnimationType
    {
        get => (ErrorAnimationTypes)GetValue(ErrorAnimationTypeProperty);
        set => SetValue(ErrorAnimationTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when the control has an error.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// When this property is set, the <see cref="ErrorAnimationType">ErrorAnimationType</see> property is ignored.
    /// </remarks>
    public IErrorAnimation ErrorAnimation
    {
        get => (IErrorAnimation)GetValue(ErrorAnimationProperty);
        set => SetValue(ErrorAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the height request
    /// </summary>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }
    
    #endregion Properties

    #region Events
    
    public new event EventHandler<FocusEventArgs> Focused;
    public new event EventHandler<FocusEventArgs> Unfocused;
    
    #endregion Events
    
    #region Constructor

    protected MaterialInputBase()
    {
        InitializeComponent();

        UpdateLayoutAfterTypeChanged(Type);
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
        var border = (BorderButton)GetTemplateChild("InputBorder");
        if (border != null)
        {
            border.Command = InputTapCommand;
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

    private void SetHasError(MaterialInputType type)
    {
        UpdateLayoutAfterTypeChanged(type);

        if (HasError
            && (ErrorAnimationType != ErrorAnimationTypes.None || ErrorAnimation != null))
        {
            _ = ErrorAnimationManager.AnimateAsync(this);
        }
    }

    private void SetBorderWidth(MaterialInputType type)
    {
        if (!BorderWidth.Equals(DefaultBorderWidth))
        {
            // Set by user
            this.InternalBorderWidth = this.BorderWidth;
        }
        else if (_borderWidths.TryGetValue(GetCurrentTypeState(type), out double borderWidth))
        {
            // Default Material value according to Type
            this.InternalBorderWidth = borderWidth;
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
    
    protected virtual void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        IsFocused = e.IsFocused;
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterTypeChanged(Type);

        if (IsFocused)
        {
            if (CanExecuteFocusedCommand())
            {
                FocusedCommand?.Execute(null);
            }
            Focused?.Invoke(this, e);
        }
        else if (!IsFocused)
        {
            if (CanExecuteUnfocusedCommand())
            {
                UnfocusedCommand?.Execute(null);    
            }
            Unfocused?.Invoke(this, e);
        }
    }
    
    protected virtual bool CanExecuteFocusedCommand()
    {
        return FocusedCommand?.CanExecute(null) ?? false;
    }

    protected virtual bool CanExecuteUnfocusedCommand()
    {
        return UnfocusedCommand?.CanExecute(null) ?? false;
    }

    private void SetBackground(MaterialInputType type)
    {
        var inputBorder = (BorderButton)GetTemplateChild("InputBorder");
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
        var inputBorder = (BorderButton)GetTemplateChild("InputBorder");
        if (inputBorder != null)
        {
            SetBackgroundColorToView(type, inputBorder);
        }

        if (type == MaterialInputType.Outlined)
        {
            var outlinedHintContainerBackground = (ContentView)GetTemplateChild("OutlinedHintContainerBackground");
            if (outlinedHintContainerBackground != null)
            {
                SetBackgroundColorToView(type, outlinedHintContainerBackground);
            }
        }
    }

    private void SetBackgroundColorToView(MaterialInputType type, View view)
    {
        if (_backgroundColors.TryGetValue(GetCurrentTypeState(type), out object background) && background != null)
        {
            if (BackgroundColor == null || BackgroundColor.Equals(DefaultBackgroundColor))
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
            view.BackgroundColor = (Color)DefaultBackgroundColor.Invoke(this);
        }
    }

    private void SetCornerRadius(MaterialInputType type)
    {
        var inputBorder = (BorderButton)GetTemplateChild("InputBorder");
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
            MaterialInputBase.PlaceholderColorProperty,
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