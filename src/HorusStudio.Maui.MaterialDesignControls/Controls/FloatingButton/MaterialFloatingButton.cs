using System.Runtime.CompilerServices;
using System.Windows.Input;

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
    /// <summary>Bottom right</summary>
    BottomRight,
    /// <summary>Bottom left</summary>
    BottomLeft
}

public class FloatingButtonConfig
{
    public MaterialFloatingButtonType Type { get; set; }
    public MaterialFloatingButtonPosition Position { get; set; }
    public Color BackgroundColor { get; set; }
    public Color IconColor { get; set; }
    public string Icon { get; set; }
    public CornerRadius CornerRadius { get; set; }
    public int IconSize { get; set; }
    public Action Action { get; set; }
    public object ActionParameter { get; set; }
}

/// <summary>
/// Floating action buttons (FABs) help people take primary actions <see href="https://m3.material.io/components/floating-action-button/overview">see here.</see>
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
///       ActionCommand="{Binding FloatingButtonActionCommand}"
///       x:Name="MaterialFloatingButton"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var MaterialFloatingButton = new MaterialFloatingButton()
/// {
///     Icon = "IconButton",
///     ActionCommand = ActionCommand
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/FloatingButtonPage.xaml)
/// 
/// </example>
public class MaterialFloatingButton : ContentView
{

    #region Attributes

    private readonly static MaterialFloatingButtonType DefaultFloatingButtonType = MaterialFloatingButtonType.FAB;
    private readonly static MaterialFloatingButtonPosition DefaultFloatingButtonPosition = MaterialFloatingButtonPosition.BottomRight;
    private readonly static Color DefaultBackgroundColor =  new AppThemeBindingExtension { Light = MaterialLightTheme.PrimaryContainer, Dark = MaterialLightTheme.PrimaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultIconColor = new AppThemeBindingExtension{ Light = MaterialLightTheme.OnPrimaryContainer, Dark = MaterialDarkTheme.OnPrimaryContainer}.GetValueForCurrentTheme<Color>();
    private readonly static ImageSource DefaultIcon = string.Empty;
    private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(16);
    private readonly static int DefaultIconSize = 24;
    
    #endregion

    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="Type" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialFloatingButtonType), typeof(MaterialFloatingButton), defaultValue: DefaultFloatingButtonType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Position" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(MaterialFloatingButtonPosition), typeof(MaterialFloatingButton), defaultValue: DefaultFloatingButtonPosition, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialFloatingButton), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="IconColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(MaterialFloatingButton), defaultValue: DefaultIconColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Icon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(MaterialFloatingButton), defaultValue: DefaultIcon, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialFloatingButton), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IconSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(int), typeof(MaterialFloatingButton), defaultValue: DefaultIconSize, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialFloatingButton self)
        {
            self.UpdateFloatingButton();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="ActionCommand" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(MaterialFloatingButton));
    
    /// <summary>
    /// The backing store for the <see cref="ActionCommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ActionCommandParameterProperty = BindableProperty.Create(nameof(ActionCommandParameter), typeof(object), typeof(MaterialFloatingButton));
    
    #endregion

    #region Properties
    
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
    /// <see langword="Null"/>
    /// </default>
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    /// <summary>
    /// Gets or sets corners in floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(16)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets size Icon Size
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
    /// Gets or sets command when press floating button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of FAB. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
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
    /// <see langword="Null"/>
    /// </default>
    public object ActionCommandParameter
    {
        get => GetValue(ActionCommandParameterProperty);
        set => SetValue(ActionCommandParameterProperty, value);
    }

    #endregion

    #region Events

    protected virtual void InternalPressedHandler(object sender, EventArgs e)
    { 
        ActionCommand?.Execute(ActionCommandParameter);
    }

    #endregion

    #region Constructor

    private readonly FloatingButtonImplementation _floatingButtonImplementation = new FloatingButtonImplementation();
    private FloatingButtonConfig _config = new();
    
    public MaterialFloatingButton()
    {
        IsVisible = false;
    }

    #endregion

    #region Setters

    private void UpdateFloatingButton()
    {
        UpdateAndInitializationControl();
    }

    private void UpdateAndInitializationControl()
    {
        string image = GetImageSourceString(Icon);

        void Action() => InternalPressedHandler(this, null);

        _config = new FloatingButtonConfig()
        {
            Type = Type,
            Position = Position,
            BackgroundColor = BackgroundColor,
            IconColor = IconColor,
            Icon = image,
            CornerRadius = CornerRadius,
            IconSize = IconSize,
            Action = Action
        };
        
        
        _floatingButtonImplementation.ShowFloatingButton(_config);
    }

    public void ShowFloatingButton()
    {
        UpdateAndInitializationControl();
    }

    public void HideFloatingButton()
    {
        _floatingButtonImplementation.DismissFloatingButton();
    }

    #endregion
    
    #region Methods
    
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == nameof(Window)
            && Window == null)
        {
           HideFloatingButton();
        }
        else
        {
            ShowFloatingButton();
        }
    }
    
    public string? GetImageSourceString(ImageSource imageSource)
    {
        if (imageSource is FileImageSource fileImageSource)
        {
            // For file-based images
            return fileImageSource.File;
        }
        else if (imageSource is UriImageSource uriImageSource)
        {
            // For URI-based images
            return uriImageSource.Uri.ToString();
        }
        else if (imageSource is StreamImageSource)
        {
            // StreamImageSource does not have a string representation
            return "Stream-based image source (no name)";
        }
        else
        {
            return null; // Unknown ImageSource type
        }
    }
    
    #endregion
    
    
}



