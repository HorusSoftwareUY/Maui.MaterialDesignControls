
using Microsoft.Maui.Layouts;

namespace HorusStudio.Maui.MaterialDesignControls.Controls.Chips;

/// <summary>
/// A Chips help people enter information, make selections, filter content, or trigger actions <see href="https://m3.material.io/components/chips/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialChips.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialChips
///        Type="Normal"
///        IconStateOnSelection="BothVisible"
///        LeadingIcon="plus.png"
///        Text="Suggestion both"
///        TrailingIcon="horus_logo.png"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var chips = new MaterialChips
/// {
///     Type = MaterialChipsType.Normal,
///     IconStateOnSelection = IconStateType.BothVisible,
///     LeadingIcon = "plus.png",
///     Text = "Suggestion both",
///     TrailingIcon="horus_logo.png"
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * Modify all comments before this line
/// </todoList>
public class MaterialChipsGroup : ContentView
{
    #region Attributes

    private readonly static bool DefaultToUpper = false;
    private readonly static Thickness DefaultPadding = new Thickness(12, 0);
    private readonly static Thickness DefaultChipsPadding = new Thickness(16, 0);
    private readonly static Thickness DefaultChipsMargin = new Thickness(6);
    private readonly static double DefaultChipsHeightRequest = 32.0;
    private readonly static double DefaultChipsFlexLayoutBasisPercentage = 0.0;
    private readonly static bool DefaultIsEnabled = true;
    private readonly static string DefaultLabelText = null;
    private readonly static IEnumerable<string> DefaultItemsSource = null;
    private readonly static string DefaultSelectedItem = null;
    private readonly static List<string> DefaultSelectedItems = null;
    private readonly static string DefaultSupportingText = null;
    private readonly static Color DefaultLabelTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialLightTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialLightTheme.Error }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultLabelSize = MaterialFontSize.BodySmall;
    private readonly static double DefaultSupportingSize = MaterialFontSize.BodySmall;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialLightTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialLightTheme.OnPrimary }.GetValueForCurrentTheme<Color>();
    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //private readonly static Color DefaultDisabledTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Disable, Dark = MaterialLightTheme.Disable }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisabledSelectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialLightTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialLightTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialLightTheme.Primary }.GetValueForCurrentTheme<Color>();
    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //private readonly static Color DefaultDisabledBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.DisableContainer, Dark = MaterialLightTheme.DisableContainer }.GetValueForCurrentTheme<Color>();
    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //private readonly static Color DefaultDisabledSelectedBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Disable, Dark = MaterialLightTheme.Disable }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialLightTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultFontSize = MaterialFontSize.LabelLarge;
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultCornerRadius = 16.0;
    private readonly static bool DefaultAnimateError = MaterialAnimation.AnimateOnError;
    private readonly static bool DefaultIsMultipleSelection = false;
    private readonly static AnimationTypes DefaultAnimation = MaterialAnimation.Type;
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;

    #endregion

    #region Bindable Properties

    public static readonly BindableProperty ToUpperProperty = BindableProperty.Create(nameof(ToUpper), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultToUpper);

    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultPadding);

    public static readonly BindableProperty ChipsPaddingProperty = BindableProperty.Create(nameof(ChipsPadding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultChipsPadding);

    public static readonly BindableProperty ChipsMarginProperty = BindableProperty.Create(nameof(ChipsMargin), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultChipsMargin);

    public static readonly BindableProperty ChipsHeightRequestProperty = BindableProperty.Create(nameof(ChipsHeightRequest), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsHeightRequest);

    public static readonly BindableProperty ChipsFlexLayoutBasisPercentageProperty = BindableProperty.Create(nameof(ChipsFlexLayoutPercentageBasis), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsFlexLayoutBasisPercentage);

    public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsEnabled);

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultLabelText);

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<string>), typeof(MaterialChipsGroup), defaultValue: DefaultItemsSource, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
           // self.SetState(self.Type);
        }
    });

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItem, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            // self.SetState(self.Type);
        }
    });

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(List<string>), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItems, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            // self.SetState(self.Type);
        }
    });

    //TODO: Validar como manejar el último parámetro que venía en esta propiedad: , validateValue: OnSupportingTextValidate
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingText);

    public static readonly BindableProperty LabelTextColorProperty = BindableProperty.Create(nameof(LabelTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultLabelTextColor);

    public static readonly BindableProperty SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingTextColor);

    public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultLabelSize);

    public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingSize);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultTextColor);

    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedTextColor);

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultDisabledTextColor);

    public static readonly BindableProperty DisabledSelectedTextColorProperty = BindableProperty.Create(nameof(DisabledSelectedTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultDisabledSelectedTextColor);

    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultBackgroundColor);

    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedBackgroundColor);

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultDisabledBackgroundColor);

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public static readonly BindableProperty DisabledSelectedBackgroundColorProperty = BindableProperty.Create(nameof(DisabledSelectedBackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultDisabledSelectedBackgroundColor);

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultBorderColor);

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultFontSize);

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultFontFamily);

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultCornerRadius);

    public static readonly BindableProperty AnimateErrorProperty = BindableProperty.Create(nameof(AnimateError), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultAnimateError);

    public static readonly BindableProperty IsMultipleSelectionProperty = BindableProperty.Create(nameof(IsMultipleSelection), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsMultipleSelection);

    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialChips), defaultValue: DefaultAnimation);

    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialChips), defaultValue: DefaultAnimationParameter);

    #endregion

    #region Properties

    public bool ToUpper
    {
        get { return (bool)GetValue(ToUpperProperty); }
        set { SetValue(ToUpperProperty, value); }
    }

    public new Thickness Padding
    {
        get { return (Thickness)GetValue(PaddingProperty); }
        set { SetValue(PaddingProperty, value); }
    }

    public Thickness ChipsPadding
    {
        get { return (Thickness)GetValue(ChipsPaddingProperty); }
        set { SetValue(ChipsPaddingProperty, value); }
    }

    public Thickness ChipsMargin
    {
        get { return (Thickness)GetValue(ChipsMarginProperty); }
        set { SetValue(ChipsMarginProperty, value); }
    }

    public double ChipsHeightRequest
    {
        get { return (double)GetValue(ChipsHeightRequestProperty); }
        set { SetValue(ChipsHeightRequestProperty, value); }
    }

    public double ChipsFlexLayoutPercentageBasis
    {
        get { return (double)GetValue(ChipsFlexLayoutBasisPercentageProperty); }
        set { SetValue(ChipsFlexLayoutBasisPercentageProperty, value); }
    }

    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    public string LabelText
    {
        get { return (string)GetValue(LabelTextProperty); }
        set { SetValue(LabelTextProperty, value); }
    }

    public IEnumerable<string> ItemsSource
    {
        get { return (IEnumerable<string>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public string SelectedItem
    {
        get { return (string)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public List<string> SelectedItems
    {
        get { return (List<string>)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public string SupportingText
    {
        get { return (string)GetValue(SupportingTextProperty); }
        set { SetValue(SupportingTextProperty, value); }
    }

    public Color LabelTextColor
    {
        get { return (Color)GetValue(LabelTextColorProperty); }
        set { SetValue(LabelTextColorProperty, value); }
    }

    public Color SupportingTextColor
    {
        get { return (Color)GetValue(SupportingTextColorProperty); }
        set { SetValue(SupportingTextColorProperty, value); }
    }

    public double LabelSize
    {
        get { return (double)GetValue(LabelSizeProperty); }
        set { SetValue(LabelSizeProperty, value); }
    }

    public double SupportingSize
    {
        get { return (double)GetValue(SupportingSizeProperty); }
        set { SetValue(SupportingSizeProperty, value); }
    }

    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    public Color SelectedTextColor
    {
        get { return (Color)GetValue(SelectedTextColorProperty); }
        set { SetValue(SelectedTextColorProperty, value); }
    }

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public Color DisabledTextColor
    //{
    //    get { return (Color)GetValue(DisabledTextColorProperty); }
    //    set { SetValue(DisabledTextColorProperty, value); }
    //}

    public Color DisabledSelectedTextColor
    {
        get { return (Color)GetValue(DisabledSelectedTextColorProperty); }
        set { SetValue(DisabledSelectedTextColorProperty, value); }
    }

    public new Color BackgroundColor
    {
        get { return (Color)GetValue(BackgroundColorProperty); }
        set { SetValue(BackgroundColorProperty, value); }
    }

    public Color SelectedBackgroundColor
    {
        get { return (Color)GetValue(SelectedBackgroundColorProperty); }
        set { SetValue(SelectedBackgroundColorProperty, value); }
    }

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public Color DisabledBackgroundColor
    //{
    //    get { return (Color)GetValue(DisabledBackgroundColorProperty); }
    //    set { SetValue(DisabledBackgroundColorProperty, value); }
    //}

    //TODO: Validar si la propiedad continua y cual es el equivalente del color
    //public Color DisabledSelectedBackgroundColor
    //{
    //    get { return (Color)GetValue(DisabledSelectedBackgroundColorProperty); }
    //    set { SetValue(DisabledSelectedBackgroundColorProperty, value); }
    //}

    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set { SetValue(BorderColorProperty, value); }
    }

    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    public double CornerRadius
    {
        get { return (double)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public bool AnimateError
    {
        get { return (bool)GetValue(AnimateErrorProperty); }
        set { SetValue(AnimateErrorProperty, value); }
    }

    public bool IsMultipleSelection
    {
        get { return (bool)GetValue(IsMultipleSelectionProperty); }
        set { SetValue(IsMultipleSelectionProperty, value); }
    }

    public AnimationTypes Animation
    {
        get { return (AnimationTypes)GetValue(AnimationProperty); }
        set { SetValue(AnimationProperty, value); }
    }

    public double? AnimationParameter
    {
        get { return (double?)GetValue(AnimationParameterProperty); }
        set { SetValue(AnimationParameterProperty, value); }
    }

    #endregion

    #region Events
    #endregion

    #region Layout

    private StackLayout _container;
    private MaterialLabel _lblLabel;
    private FlexLayout _flexContainer;
    private MaterialLabel _lblSupporting;

    #endregion

    #region Constructor

    public MaterialChipsGroup()
    {
        _container = new StackLayout()
        {
            Spacing = 2,
        };

        _lblLabel = new MaterialLabel()
        {
            IsVisible = false,
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = new Thickness(14, 0, 14, 2),
            HorizontalTextAlignment = TextAlignment.Start,
            TextColor = LabelTextColor,
            FontFamily = FontFamily,
            FontSize = LabelSize
        };

        _flexContainer = new FlexLayout()
        {
            Wrap = FlexWrap.Wrap,
            Direction = FlexDirection.Row,
            JustifyContent = FlexJustify.Start
        };

        _lblSupporting = new MaterialLabel()
        {
            IsVisible = false,
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = new Thickness(14, 2, 14, 0),
            HorizontalTextAlignment = TextAlignment.Start,
            TextColor = SupportingTextColor,
            FontFamily = FontFamily,
            FontSize = SupportingSize
        };
    }

    #endregion

    #region Setters
    #endregion

    #region Styles
    #endregion
}