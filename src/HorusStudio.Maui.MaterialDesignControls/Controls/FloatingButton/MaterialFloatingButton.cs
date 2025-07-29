using System.Runtime.CompilerServices;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialFloatingButtonType
{
    /// <summary>Use a FAB to represent the screen’s primary action</summary>
    FAB,
    /// <summary>A small FAB is used for a secondary, supporting action, or in place of a default FAB in compact window sizes</summary>
    Small,
    /// <summary>A large FAB is useful when the layout calls for a clear and prominent primary action, and where a larger footprint would help the user engage</summary>
    Large
}

public enum MaterialFloatingButtonPosition
{
    /// <summary>Top left</summary>
    TopLeft,
    /// <summary>Top right</summary>
    TopRight,
    /// <summary>Bottom left</summary>
    BottomLeft,
    /// <summary>Bottom right</summary>
    BottomRight
}

/// <summary>
/// Floating action buttons (FABs) help people take primary actions and follow Material Design Guidelines. <see href="https://m3.material.io/components/floating-action-button/overview">See more.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialFloatingButton.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialFloatingButton
///       Icon="IconButton"
///       Command="{Binding FloatingButtonActionCommand}"
///       x:Name="MaterialFloatingButton"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var MaterialFloatingButton = new MaterialFloatingButton()
/// {
///     Icon = "IconButton",
///     Command = ActionCommand
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/FloatingButtonPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [Android] Only one Floating Button visible per Page
/// </todoList>
public class MaterialFloatingButton : ContentView
{
    #region Attributes

    private const MaterialFloatingButtonType DefaultFloatingButtonType = MaterialFloatingButtonType.FAB;
    private const MaterialFloatingButtonPosition DefaultFloatingButtonPosition = MaterialFloatingButtonPosition.BottomRight;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ =>  new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialLightTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultIconColor = _ => new AppThemeBindingExtension{ Light = MaterialLightTheme.OnPrimaryContainer, Dark = MaterialDarkTheme.OnPrimaryContainer}.GetValueForCurrentTheme<Color>();
    private static readonly ImageSource DefaultIcon = string.Empty;
    private const double DefaultIconSize = 24;
    private const double DefaultCornerRadius = 16;
    private static readonly Thickness DefaultMargin = new(16);
    private static readonly Thickness DefaultPadding = new(16);

    private FloatingButtonImplementation? _floatingButtonImplementation;
    private Page? _parentPage;
    private bool _updateLayout = true;

    private static readonly IDictionary<MaterialFloatingButtonType, double> IconSizeMappings = new Dictionary<MaterialFloatingButtonType, double> 
    {
        { MaterialFloatingButtonType.FAB, 24 },
        { MaterialFloatingButtonType.Small, 24 },
        { MaterialFloatingButtonType.Large, 36 },
    };
        
    private static readonly IDictionary<MaterialFloatingButtonType, double> CornerRadiusMappings = new Dictionary<MaterialFloatingButtonType, double> 
    {
        { MaterialFloatingButtonType.FAB, 16 },
        { MaterialFloatingButtonType.Small, 12 },
        { MaterialFloatingButtonType.Large, 28 },
    };
        
    private static readonly IDictionary<MaterialFloatingButtonType, double> PaddingMappings = new Dictionary<MaterialFloatingButtonType, double> 
    {
        { MaterialFloatingButtonType.FAB, 16 },
        { MaterialFloatingButtonType.Small, 8 }, 
        { MaterialFloatingButtonType.Large, 30 },
    };
    
    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type">Type</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialFloatingButtonType), typeof(MaterialFloatingButton), defaultValue: DefaultFloatingButtonType);
    
    /// <summary>
    /// The backing store for the <see cref="Position">Position</see> bindable property.
    /// </summary>
    public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(MaterialFloatingButtonPosition), typeof(MaterialFloatingButton), defaultValue: DefaultFloatingButtonPosition);
    
    /// <summary>
    /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialFloatingButton), defaultValueCreator: DefaultBackgroundColor);
    
    /// <summary>
    /// The backing store for the <see cref="IconColor">IconColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(MaterialFloatingButton), defaultValueCreator: DefaultIconColor);
    
    /// <summary>
    /// The backing store for the <see cref="Icon">Icon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(MaterialFloatingButton), defaultValue: DefaultIcon);
    
    /// <summary>
    /// The backing store for the <see cref="IconSize">IconSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(MaterialFloatingButton), defaultValue: DefaultIconSize);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialFloatingButton), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="Command">Command</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialFloatingButton));
    
    /// <summary>
    /// The backing store for the <see cref="CommandParameter">CommandParameter</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialFloatingButton));
    
    /// <summary>
    /// The backing store for the <see cref="HeightRequest">HeightRequest</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialFloatingButton), defaultValue: -1d);

    /// <summary>
    /// The backing store for the <see cref="WidthRequest">WidthRequest</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(MaterialFloatingButton), defaultValue: -1d);
    
    /// <summary>
    /// The backing store for the <see cref="Margin">Margin</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty MarginProperty = BindableProperty.Create(nameof(Margin), typeof(Thickness), typeof(MaterialFloatingButton), defaultValue: DefaultMargin);
    
    /// <summary>
    /// The backing store for the <see cref="Padding">Padding</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialFloatingButton), defaultValue: DefaultPadding);

    #endregion

    #region Properties
    
    private bool Initialized => _parentPage != null;
    
    /// <summary>
    /// Gets or sets Type button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFloatingButtonType.FAB">MaterialFloatingButtonType.FAB</see>
    /// </default>
    public MaterialFloatingButtonType Type
    {
        get => (MaterialFloatingButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets Position button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFloatingButtonPosition.BottomRight">MaterialFloatingButtonPosition.BottomRight</see>
    /// </default>
    public MaterialFloatingButtonPosition Position
    {
        get => (MaterialFloatingButtonPosition)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }
    
    /// <summary>
    /// Gets or sets background color floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.PrimaryContainer">MaterialLightTheme.PrimaryContainer</see> - Dark = <see cref="MaterialDarkTheme.PrimaryContainer">MaterialDarkTheme.PrimaryContainer</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets icon color in floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnPrimaryContainer">MaterialLightTheme.OnPrimaryContainer</see> - Dark = <see cref="MaterialDarkTheme.OnPrimaryContainer">MaterialDarkTheme.OnPrimaryContainer</see>
    /// </default>
    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets icon in floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    /// <summary>
    /// Gets or sets desired icon size for the element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 24
    /// </default>
    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets corners in floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 16
    /// </default>
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets command when press floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of FAB. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command">Command</see> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
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
    /// <para><see cref="HeightRequest">HeightRequest</see> does not immediately change the Bounds of an element; setting the <see cref="HeightRequest">HeightRequest</see> will change the resulting height of the element during the next layout pass.</para>
    /// </remarks>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the desired width override of this element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// -1
    /// </default>
    /// <remarks>
    /// <para>which means the value is unset; the effective minimum width will be zero.</para>
    /// <para><see cref="WidthRequest">WidthRequest</see> does not immediately change the Bounds of an element; setting the <see cref="WidthRequest">WidthRequest</see> will change the resulting width of the element during the next layout pass.</para>
    /// </remarks>
    public new double WidthRequest
    {
        get => (double)GetValue(WidthRequestProperty);
        set => SetValue(WidthRequestProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the desired margin override of this element.
    /// This is a bindable property.
    /// </summary>
    public new Thickness Margin
    {
        get => (Thickness)GetValue(MarginProperty);
        set => SetValue(MarginProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the desired padding override of this element.
    /// This is a bindable property.
    /// </summary>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    #endregion

    ~MaterialFloatingButton()
    {
        if (_parentPage is null) return;
        
        _parentPage.Appearing -= Appearing;
        _parentPage.Disappearing -= Disappearing;
    }

    #region Methods
    
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        switch (propertyName)
        {
            case nameof(IsVisible):
                Logger.Debug($"Change visibility to '{IsVisible}'");
                if (IsVisible) Show();
                else Hide();
                break;
            
            case nameof(Type):
                Logger.Debug($"Set type '{Type}'");
                if (!IconSizeMappings.TryGetValue(Type, out var iconSize)
                    || !CornerRadiusMappings.TryGetValue(Type, out var cornerRadius)
                    || !PaddingMappings.TryGetValue(Type, out var padding))
                    throw new ArgumentOutOfRangeException();

                WithoutUpdatingLayout(() =>
                {
                    IconSize = iconSize;
                    CornerRadius = cornerRadius;
                    Padding = padding;
                    _updateLayout = true;
                });
                UpdateLayout();
                
                break;
            
            case nameof(BackgroundColor):
            case nameof(Margin):
            case nameof(CornerRadius):
            case nameof(Padding):
            case nameof(Icon):
            case nameof(IconSize):    
            case nameof(IconColor):
            case nameof(Position):
            case nameof(HeightRequest):
            case nameof(WidthRequest):
            case nameof(Command):
            case nameof(CommandParameter):
            case nameof(IsEnabled):
                UpdateLayout();
                break;
            
            case nameof(Window):
                if (Window is null || _parentPage is not null) return;
            
                _parentPage = this.GetParent<Page>();
                if (_parentPage is null) return;
            
                Logger.Debug($"Initialize component on '{_parentPage.GetType().FullName}' page");
                _parentPage.Appearing += Appearing;
                _parentPage.Disappearing += Disappearing;
                
                UpdateLayout();
                break;
            
            default:
                base.OnPropertyChanged(propertyName);
                break;
        }
    }

    private void WithoutUpdatingLayout(Action action)
    {
        _updateLayout = false;
        action();
        _updateLayout = true;
    }

    private void UpdateLayout()
    {
        if (!Initialized || !_updateLayout) return;
        
        Logger.Debug("Updating layout");
        try
        {
#if IOS || MACCATALYST
            if (_floatingButtonImplementation != null)
            {
                _floatingButtonImplementation.Dispose();
            }
#endif
            _floatingButtonImplementation = new(this);
            Show();
            Logger.Debug("Layout updated");
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR updating layout", ex);
        }
    }

    private void Show()
    {
        if (!IsVisible) return;
        _floatingButtonImplementation?.Show();
    }

    private void Hide()
    {
        _floatingButtonImplementation?.Dismiss();
    }

    private void Appearing(object? sender, EventArgs e) => Show();
    
    private void Disappearing(object? sender, EventArgs e) => Hide();
    
    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialFloatingButton.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SurfaceContainerHighest,
                Dark = MaterialDarkTheme.SurfaceContainerHighest
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.9f));

        disabledState.Setters.Add(
            MaterialFloatingButton.IconColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));
        
        commonStatesGroup.States.Add(new VisualState { Name = VisualStateManager.CommonStates.Normal });
        commonStatesGroup.States.Add(disabledState);

        var style = new Style(typeof(MaterialFloatingButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }
    
    #endregion
}
