
using HorusStudio.Maui.MaterialDesignControls.Converters;
using Microsoft.Maui.Layouts;
using System.Collections;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Chips group facilitate the selection of one or more options within a group, optimizing space usage effectively <see href="https://m3.material.io/components/chips/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignPlugin/develop/screenshots/MaterialChips.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesign;assembly=HorusStudio.Maui.MaterialDesign"
/// 
/// &lt;material:MaterialChipsGroup
///        IsMultipleSelection="True"
///        ItemsSource="{Binding Colors}"
///        LabelText="Colors *"
///        SelectedItems="{Binding SelectedColors}"
///        SupportingText="Please select at least 4 colors"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var chips = new MaterialChipsGroup
/// {
///     IsMultipleSelection = true,
///     ItemsSource = Colors,
///     LabelText = "Colors *",
///     SelectedItems = SelectedColors,
///     SupportingText="Please select at least 4 colors"
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesign.Sample/Pages/ChipsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * For the SelectedItems to be updated correctly they must be initialized. Finding a way to make it work even when the list starts out null
/// </todoList>
public class MaterialChipsGroup : ContentView
{
    #region Attributes

    private static readonly Thickness DefaultPadding = new Thickness(12, 0);
    private static readonly Thickness DefaultChipsPadding = new Thickness(16, 0);
    private const double DefaultChipsHeightRequest = 32.0;
    private const double DefaultChipsFlexLayoutPercentageBasis = 0.0;
    private const bool DefaultIsEnabled = true;
    private static readonly string DefaultLabelText = null!;
    private static readonly IEnumerable DefaultItemsSource = null!;
    private static readonly object DefaultSelectedItem = null!;
    private static readonly IList DefaultSelectedItems = null!;
    private static readonly string DefaultSupportingText = null!;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultLabelTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialLightTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialLightTheme.Error }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultLabelSize = _ => MaterialFontSize.BodySmall;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingSize = _ => MaterialFontSize.BodySmall;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBorderColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.LabelLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private const double DefaultCornerRadius = 8.0;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimateError = _ => MaterialAnimation.AnimateOnError;
    private const bool DefaultIsMultipleSelection = false;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimation = _ => MaterialAnimation.Type;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationParameter = _ => MaterialAnimation.Parameter;
    private const Align DefaultAlign = Align.Start;
    private const int DefaultVerticalSpacing = 4;
    private const int DefaultHorizontalSpacing = 4;
    private static readonly string DefaultPropertyPath = null!;

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultPadding);

    /// <summary>
    /// The backing store for the <see cref="ChipsPadding" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsPaddingProperty = BindableProperty.Create(nameof(ChipsPadding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultChipsPadding);

    /// <summary>
    /// The backing store for the <see cref="ChipsHeightRequest" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsHeightRequestProperty = BindableProperty.Create(nameof(ChipsHeightRequest), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="ChipsFlexLayoutPercentageBasis" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsFlexLayoutBasisPercentageProperty = BindableProperty.Create(nameof(ChipsFlexLayoutPercentageBasis), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsFlexLayoutPercentageBasis);

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsEnabled);

    /// <summary>
    /// The backing store for the <see cref="LabelText" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultLabelText);

    /// <summary>
    /// The backing store for the <see cref="ItemsSource" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MaterialChipsGroup), defaultValue: DefaultItemsSource, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetItemsSource(newValue as IEnumerable);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedItem" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItem, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.UpdateItemSelection();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedItems" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItems, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.UpdateItemSelection();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectionChangedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(MaterialChipsGroup));

    /// <summary>
    /// The backing store for the <see cref="SupportingText" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingText, propertyChanged: async (bindable, _, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            await self.ValidateText(newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LabelTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextColorProperty = BindableProperty.Create(nameof(LabelTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValueCreator: DefaultLabelTextColor);

    /// <summary>
    /// The backing store for the <see cref="SupportingTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValueCreator: DefaultSupportingTextColor);

    /// <summary>
    /// The backing store for the <see cref="LabelSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(MaterialChipsGroup), defaultValueCreator: DefaultLabelSize);

    /// <summary>
    /// The backing store for the <see cref="SupportingSize" />
    /// bindable property.
    /// </summary>   
    public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialChipsGroup), defaultValueCreator: DefaultSupportingSize);

    /// <summary>
    /// The backing store for the <see cref="TextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValueCreator: DefaultBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChipsGroup), defaultValueCreator: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChipsGroup), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChipsGroup), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="AnimateError" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimateErrorProperty = BindableProperty.Create(nameof(AnimateError), typeof(bool), typeof(MaterialChipsGroup), defaultValueCreator: DefaultAnimateError);

    /// <summary>
    /// The backing store for the <see cref="IsMultipleSelection" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IsMultipleSelectionProperty = BindableProperty.Create(nameof(IsMultipleSelection), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsMultipleSelection);

    /// <summary>
    /// The backing store for the <see cref="Animation" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialChipsGroup), defaultValueCreator: DefaultAnimation);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialChipsGroup), defaultValueCreator: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="Align" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AlignProperty = BindableProperty.Create(nameof(Align), typeof(Align), typeof(MaterialChipsGroup), defaultValue: DefaultAlign);

    /// <summary>
    /// The backing store for the <see cref="VerticalSpacing" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(int), typeof(MaterialChipsGroup), defaultValue: DefaultVerticalSpacing, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetMargins();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="HorizontalSpacing" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(int), typeof(MaterialChipsGroup), defaultValue: DefaultHorizontalSpacing, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetMargins();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="PropertyPath" /> 
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty PropertyPathProperty = BindableProperty.Create(nameof(PropertyPath), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultPropertyPath);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the padding for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(12,0)
    /// </default>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(16,0)
    /// </default>
    public Thickness ChipsPadding
    {
        get => (Thickness)GetValue(ChipsPaddingProperty);
        set => SetValue(ChipsPaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets the height for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 32.0
    /// </default>
    public double ChipsHeightRequest
    {
        get => (double)GetValue(ChipsHeightRequestProperty);
        set => SetValue(ChipsHeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets the basis for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 0.0
    /// </default>
    public double ChipsFlexLayoutPercentageBasis
    {
        get => (double)GetValue(ChipsFlexLayoutBasisPercentageProperty);
        set => SetValue(ChipsFlexLayoutBasisPercentageProperty, value);
    }

    /// <summary>
    /// Gets or sets the state when the Chips is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the source of the items for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected item for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected items for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// This property needs to be initialized from the implementation. It cannot be null.
    /// </remarks>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public IList SelectedItems
    {
        get => (IList) GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the selection changes.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>
    /// The command parameter type varies based on <see cref="IsMultipleSelection"/>: when <see langword="True"/>, it is <see cref="IList<object>"/>; when <see langword="False"/>, it is <see cref="object"/>.
    /// </remarks>
    public ICommand SelectionChangedCommand
    {
        get => (ICommand)GetValue(SelectionChangedCommandProperty);
        set => SetValue(SelectionChangedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the font color of text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark = <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
    /// </default>
    public Color LabelTextColor
    {
        get => (Color)GetValue(LabelTextColorProperty);
        set => SetValue(LabelTextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font color of supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Error">MaterialLightTheme.Error</see> - Dark = <see cref="MaterialDarkTheme.Error">MaterialDarkTheme.Error</see>
    /// </default>
    public Color SupportingTextColor
    {
        get => (Color)GetValue(SupportingTextColorProperty);
        set => SetValue(SupportingTextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size of text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see> / Tablet = 15 / Phone = 12
    /// </default>
    public double LabelSize
    {
        get => (double)GetValue(LabelSizeProperty);
        set => SetValue(LabelSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size of supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see> / Tablet = 15 / Phone = 12
    /// </default>
    public double SupportingSize
    {
        get => (double)GetValue(SupportingSizeProperty);
        set => SetValue(SupportingSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font color of text for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the background color for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SurfaceContainerLow">MaterialLightTheme.SurfaceContainerLow</see> - Dark = <see cref="MaterialDarkTheme.SurfaceContainerLow">MaterialDarkTheme.SurfaceContainerLow</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the border color for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Outline">MaterialLightTheme.Outline</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.Outline</see>
    /// </default>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size of text for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet = 17 / Phone = 14
    /// </default>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family of text for the Chips.
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
    /// Gets or sets the corner radius for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 8.0
    /// </default>
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets if the error animation is enabled for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public bool AnimateError
    {
        get => (bool)GetValue(AnimateErrorProperty);
        set => SetValue(AnimateErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets if the multi selection is enabled for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public bool IsMultipleSelection
    {
        get => (bool)GetValue(IsMultipleSelectionProperty);
        set => SetValue(IsMultipleSelectionProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade"> AnimationTypes.Fade </see>
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
    /// Gets or sets the horizontal alignment for the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="Align.Start"> Align.Start </see>
    /// </default>
    public Align Align
    {
        get => (Align)GetValue(AlignProperty);
        set => SetValue(AlignProperty, value);
    }

    /// <summary>
    /// Gets or sets the vertical spacing between the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 4
    /// </default>
    public int VerticalSpacing
    {
        get => (int)GetValue(VerticalSpacingProperty);
        set => SetValue(VerticalSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal spacing between the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 4
    /// </default>
    public int HorizontalSpacing
    {
        get => (int)GetValue(HorizontalSpacingProperty);
        set => SetValue(HorizontalSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the property path.
    /// This property is used to map an object and display a property of it.
    /// </summary>
    /// <remarks>
    /// If it´s no defined, the control will use toString() method.
    /// </remarks>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string PropertyPath
    {
        get => (string)GetValue(PropertyPathProperty);
        set => SetValue(PropertyPathProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Occurs when the selection changes.
    /// </summary>
    public event EventHandler<MaterialChipsGroupSelectionEventArgs>? SelectionChanged;

    #endregion

    #region Layout

    private FlexLayout _flexContainer = null!;

    #endregion Layout

    #region Constructor

    public MaterialChipsGroup()
    {
        Content = CreateLayout();
    }

    #endregion Constructor

    #region Validations

    private async Task<bool> ValidateText(object value)
    {
        if (AnimateError && !string.IsNullOrEmpty(SupportingText) && SupportingText == (string)value)
        {
            await ShakeAnimation.AnimateAsync(Content);
        }

        return true;
    }

    #endregion Validations

    #region Methods

    private View? CreateLayout()
    {
        Utils.Logger.Debug("Creating chips group layout");
        try
        {
            var textLabel = new MaterialLabel
            {
                LineBreakMode = LineBreakMode.NoWrap,
                Margin = new Thickness(14, 0, 14, 2),
                HorizontalTextAlignment = TextAlignment.Start,
            };
            textLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(LabelText), source: this));
            textLabel.SetBinding(MaterialLabel.IsVisibleProperty, new Binding(nameof(LabelText), source: this, converter: new IsNotNullOrEmptyConverter()));
            textLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelTextColor), source: this));
            textLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(LabelSize), source: this));
            textLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));

            var lblSupporting = new MaterialLabel
            {
                LineBreakMode = LineBreakMode.NoWrap,
                Margin = new Thickness(14, 2, 14, 0),
                HorizontalTextAlignment = TextAlignment.Start,
            };
            lblSupporting.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(SupportingText), source: this));
            lblSupporting.SetBinding(MaterialLabel.IsVisibleProperty, new Binding(nameof(SupportingText), source: this, converter: new IsNotNullOrEmptyConverter()));
            lblSupporting.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SupportingTextColor), source: this));
            lblSupporting.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SupportingSize), source: this));
            lblSupporting.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            
            _flexContainer = new FlexLayout
            {
                Wrap = FlexWrap.Wrap,
                Direction = FlexDirection.Row,
            };
            _flexContainer.SetBinding(FlexLayout.JustifyContentProperty, new Binding(nameof(Align), source: this, converter: new AlignToFlexJustifyConverter()));
            
            var contentView = new ContentView
            {
                Content = _flexContainer,
            };
            contentView.SetBinding(ContentView.PaddingProperty, new Binding(nameof(Padding), source: this));
            
            var container = new VerticalStackLayout
            {
                textLabel,
                contentView,
                lblSupporting
            };
            container.Spacing = 2;
            
            return container;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating chips group layout", ex, this);
            return null;
        }
    }
    
    private void SetItemsSource(IEnumerable? items)
    {
        _flexContainer.Children.Clear();
        Utils.Logger.Debug("Setting items source");
        if (items == null) return;

        var selectedPathValues = GetSelectedPathValues();
        var selectedPathValue = GetSelectedPathValue();
        
        foreach (var item in items)
        {
            var chip = CreateItemLayout(item);
            SetItemSelection(chip, selectedPathValues, selectedPathValue);
            
            _flexContainer.Children.Add(chip);
        }

        var addedItems = _flexContainer.Children.Count;
        Utils.Logger.Debug($"{(addedItems > 0 ? addedItems : "No")} items added to chips group");
    }

    private MaterialChips CreateItemLayout(object item)
    {
        try
        {
            var newItem = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);
            Utils.Logger.Debug($"Creating item layout {newItem}");

            var materialChips = new MaterialChips(true)
            {
                Text = newItem ?? string.Empty,
                Type = MaterialChipsType.Filter,
                Margin = GetMargin()
            };
            materialChips.SetBinding(MaterialChips.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            materialChips.SetBinding(MaterialChips.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            materialChips.SetBinding(MaterialChips.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            materialChips.SetBinding(MaterialChips.PaddingProperty, new Binding(nameof(ChipsPadding), source: this));
            materialChips.SetBinding(MaterialChips.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
            materialChips.SetBinding(MaterialChips.AnimationProperty, new Binding(nameof(Animation), source: this));
            materialChips.SetBinding(MaterialChips.AnimationParameterProperty, new Binding(nameof(AnimationParameter), source: this));
            materialChips.SetBinding(MaterialChips.HeightRequestProperty, new Binding(nameof(ChipsHeightRequest), source: this));
            // materialChips.SetBinding(MaterialChips.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            // materialChips.SetBinding(MaterialChips.TextColorProperty, new Binding(nameof(TextColor), source: this));
            // materialChips.SetBinding(MaterialChips.BorderColorProperty, new Binding(nameof(BorderColor), source: this));

            materialChips.Command = new Command(() => SelectionCommand(materialChips));

            if (ChipsFlexLayoutPercentageBasis > 0 && ChipsFlexLayoutPercentageBasis <= 1)
            {
                FlexLayout.SetBasis(materialChips, new FlexBasis((float)ChipsFlexLayoutPercentageBasis, true));
            }
            
            return materialChips;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating item layout", ex, this);
            return null;
        }
    }

    private void SetItemSelection(MaterialChips chip, List<string> selectedValues, string? selectedValue)
    {
        if (IsMultipleSelection)
        {
            chip.IsSelected = selectedValues.Contains(chip.Text);
        }
        else
        {
            chip.IsSelected = chip.Text == selectedValue;
        }
    }

    private void SetMargins()
    {
        foreach (var view in _flexContainer.Children)
        {
            if (view is MaterialChips materialChips)
            {
                materialChips.Margin = GetMargin();
            }
        }
    }
    
    private void SelectionCommand(MaterialChips materialChips)
    {
        if (!IsEnabled) return;

        if (IsMultipleSelection)
        {
            var selectedItems = SelectedItems != null ? SelectedItems : new List<object>();

            foreach (var item in ItemsSource)
            {
                var propertyPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);

                if (propertyPathValue == materialChips.Text)
                {
                    if (selectedItems.Contains(item))
                    {
                        if (materialChips.IsSelected)
                        {
                            selectedItems.Add(item);
                        }
                        else
                        {
                            selectedItems.Remove(item);
                        }
                    }
                    else if (materialChips.IsSelected)
                    {
                        selectedItems.Add(item);
                    }

                    break;
                }
            }

            SelectedItems = selectedItems;

            UpdateItemSelection();
        }
        else
        {
            foreach (var item in ItemsSource)
            {
                var propertyPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);

                if (propertyPathValue == materialChips.Text)
                {
                    SelectedItem = materialChips.IsSelected ? item : null;
                    break;
                }
            }
        }
    }

    private void UpdateItemSelection()
    {
        if (IsMultipleSelection)
        {
            var selectedItemsText = string.Empty;
            if (SelectedItems != null)
            {
                foreach (var item in SelectedItems)
                {
                    var propertyPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);
                    selectedItemsText += $"{propertyPathValue},";
                }
            }

            foreach (var item in _flexContainer.Children)
            {
                if (item is MaterialChips itemMC)
                {
                    itemMC.IsSelected = selectedItemsText.Contains(itemMC.Text);
                }
            }

            if (SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(SelectedItems))
            {
                SelectionChangedCommand.Execute(SelectedItems);
            }

            SelectionChanged?.Invoke(this, new MaterialChipsGroupSelectionEventArgs(SelectedItems));
        }
        else
        {
            var selectedPathValue = GetSelectedPathValue();

            foreach (var item in _flexContainer.Children)
            {
                if (item is MaterialChips itemMC)
                {
                    itemMC.IsSelected = itemMC.Text == selectedPathValue;
                }
            }

            if (SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(SelectedItem))
            {
                SelectionChangedCommand.Execute(SelectedItem);
            }

            SelectionChanged?.Invoke(this, new MaterialChipsGroupSelectionEventArgs(SelectedItem));
        }
    }

    private List<string> GetSelectedPathValues()
    {
        var selectedPathValues = new List<string>();

        if (SelectedItems != null)
        {
            foreach (var item in SelectedItems)
            {
                selectedPathValues.Add(string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath));
            }
        }

        return selectedPathValues;
    }

    private string GetSelectedPathValue()
    {
        var selectedPathValue = string.Empty;

        if (SelectedItem != null)
        {
            selectedPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? SelectedItem.ToString() : GetPropertyValue(SelectedItem, PropertyPath);
        }

        return selectedPathValue;
    }

    private Thickness GetMargin()
    {
        switch (Align)
        {
            default:
            case Align.Start:
                return new Thickness(0, VerticalSpacing / 2, HorizontalSpacing, VerticalSpacing / 2);
            case Align.Center:
                return new Thickness(HorizontalSpacing / 2, VerticalSpacing / 2, HorizontalSpacing / 2, VerticalSpacing / 2);
            case Align.End:
                return new Thickness(HorizontalSpacing, VerticalSpacing / 2, 0, VerticalSpacing / 2);
        }
    }

    private string GetPropertyValue(object item, string propertyToSearch)
    {
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name.Equals(propertyToSearch, StringComparison.InvariantCultureIgnoreCase))
            {
                return property.GetValue(item, null).ToString();
            }
        }
        return item.ToString();
    }

    #endregion Methods
}