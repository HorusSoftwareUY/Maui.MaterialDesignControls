using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;


/// <summary>
/// A bottom sheet <see cref="View" /> show secondary content anchored to the bottom of the screen and follows Material Design Guidelines <see href="https://m3.material.io/components/bottom-sheets/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialBottomSheet.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialBottomSheet
///     Type="Elevated"
///     Text="Confirm"
///     Command="{Binding ButtonCommand}"
///     IsBusy="{Binding ButtonCommand.IsRunning}"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var button = new MaterialBottomSheet
/// {
///     Type = MaterialButtonType.Filled,
///     Text = "Save",
///     Command = ButtonCommand,
///     IsBusy = ButtonCommand.IsRunning
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/BottomSheetPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] IconTintColor doesn't react to VisualStateManager changes.
/// </todoList>
public class MaterialBottomSheet : ContentView
{
    #region Attributes

#nullable enable
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable
    private readonly static Color DefaultShadowColor = Colors.Transparent;
    private readonly static Color DefaultBackgroundColor = Colors.Transparent;
    private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(12);
    private readonly static float DefaultBorderWidth = 0f;
    private readonly static Color DefaultBorderColor = Colors.Transparent;
    private readonly static Shadow DefaultShadow = null;

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialBottomSheet), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialBottomSheet), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialBottomSheet), defaultValue: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialBottomSheet), defaultValue: null);


    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the command to invoke when the card is clicked. This is a bindable property.
    /// </summary>
    /// <remarks>This property is used to associate a command with an instance of a card. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }


#nullable enable
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
#nullable disable

    /// <summary>
    /// Gets or sets a custom animation to be executed when card is clicked.
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
    /// Gets or sets a color that describes the background color of the card.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.SurfaceContainerHighest">MaterialLightTheme.SurfaceContainerHighest</see> - Dark: <see cref="MaterialDarkTheme.SurfaceContainerHighest">MaterialDarkTheme.SurfaceContainerHighest</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
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

    #endregion Properties

    #region Events

    private EventHandler _clicked;
    private EventHandler _pressed;
    private EventHandler _released;
    private readonly object _objectLock = new();

    /// <summary>
    /// Occurs when the card is clicked/tapped.
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
    /// Occurs when the card is pressed.
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
    /// Occurs when the card is released.
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

    #endregion Events

    #region Constructors

    public MaterialBottomSheet()
    {
        Padding = new Thickness(16);

        //Behaviors.Add(new TouchBehavior());

        //if (Type == DefaultCardType)
        //{
        //    UpdateLayoutAfterTypeChanged(Type);
        //}
    }

    #endregion Constructors

    #region Methods


    #endregion Methods

    #region ITouchable

    #endregion ITouchable

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialBottomSheet.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(MaterialBottomSheet.ShadowProperty, null);

        //disabledState.Setters.Add(
        //    MaterialBottomSheet.BorderColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.Surface,
        //        Dark = MaterialDarkTheme.Surface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.38f));

        //disabledState.Setters.Add(MaterialBottomSheet.OpacityProperty, 0.38f);

        var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };
        //pressedState.Setters.Add(MaterialBottomSheet.OpacityProperty, 1f);

        var normalState = new VisualState { Name = ButtonCommonStates.Normal };
        //normalState.Setters.Add(MaterialBottomSheet.OpacityProperty, 1f);

        commonStatesGroup.States.Add(normalState);
        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialBottomSheet));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}
