using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A Checkbox <see cref="View" /> let users select one or more items from a list, or turn an item on or off and follows Material Design Guidelines <see href="https://m3.material.io/components/checkbox/overview"> </see>.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialCheckBox.jpg</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialCheckBox
///         TextSide="Left"
///         CheckedChangedCommand="{Binding CheckedChangedCommand}"
///         CheckedChangedCommandParameter="Selected or Unselected"
///         Text="Checkbox 1"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var checkBox = new MaterialCheckBox()
/// {
///     Text = "Checkbox 1"
///     TextSide = TextSide.Left,
///     CheckedChangedCommand = viewModel.CheckChangedCommand,
///     CheckedChangedCommandParameter = "Selected or Unselected"
/// };
///</code>
///
/// </example>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/CheckboxPage.xaml)
/// 
/// <todoList>
/// * [iOS] FontAttributes doesn't work.
/// </todoList>
public class MaterialCheckBox : ContentView, ITouchableView
{

    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCheckColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;

    #endregion Attributes

    #region Layout

    private readonly MaterialLabel _label;
    private readonly CustomCheckBox _checkbox;
    private readonly Grid _mainLayout;
    private readonly BoxView _boxView;

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Content" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(string), typeof(MaterialCheckBox), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="Color" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(MaterialCheckBox), defaultValueCreator: DefaultColor);

    /// <summary>
    /// The backing store for the <see cref="TickColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TickColorProperty = BindableProperty.Create(nameof(TickColor), typeof(Color), typeof(MaterialCheckBox), defaultValueCreator: DefaultCheckColor);

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialCheckBox), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialCheckBox), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="IsChecked" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(MaterialCheckBox), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialCheckBox self && newValue is bool value)
        {
            self.ChangeVisualState();

            self.CheckedChanged?.Invoke(self, new CheckedChangedEventArgs(value));
            if (self.CheckedChangedCommand != null && self.CheckedChangedCommand.CanExecute(self.CheckedChangedCommandParameter))
            {
                self.CheckedChangedCommand.Execute(self.CheckedChangedCommandParameter);
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialCheckBox), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialCheckBox self && newValue is bool)
        {
            self.ChangeVisualState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialCheckBox), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialCheckBox), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialCheckBox), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialCheckBox), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialCheckBox), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="TextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialCheckBox), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="TextSide"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(TextSide), typeof(MaterialCheckBox), defaultValue: TextSide.Right, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialCheckBox self && newValue is TextSide textSide)
        {
            self.TextSideChanged(textSide);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TouchAnimationType"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialCheckBox), defaultValueCreator: DefaultTouchAnimationType);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialCheckBox));

    /// <summary>
    /// The backing store for the <see cref="CheckedChangedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CheckedChangedCommandProperty = BindableProperty.Create(nameof(CheckedChangedCommand), typeof(ICommand), typeof(MaterialCheckBox));

    /// <summary>
    /// The backing store for the <see cref="CheckedChangedCommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CheckedChangedCommandParameterProperty = BindableProperty.Create(nameof(CheckedChangedCommandParameter), typeof(object), typeof(MaterialCheckBox));

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="CheckBox" /> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public CheckBox InternalCheckBox => _checkbox;

    /// <summary>
    /// Gets the <see cref="Content" /> for the RadioButton.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>We disabled the set for this property because doesn't have sense set the content because we are setting with the checkbox and label.</remarks>
    public new string Content => (string)GetValue(ContentProperty);

    /// <summary>
    /// Gets or sets the <see cref="Text" /> for the label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Microsoft.Maui.Graphics.Color" /> for the checkbox color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Microsoft.Maui.Graphics.Color" /> for the tick color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.OnPrimary">MaterialLightTheme.OnPrimary</see> - Dark: <see cref="MaterialDarkTheme.OnPrimary">MaterialDarkTheme.OnPrimary</see>
    /// </default>
    /// <remarks>Only is supported on iOS.</remarks>
    public Color TickColor
    {
        get => (Color)GetValue(TickColorProperty);
        set => SetValue(TickColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="TextColor" /> for the text of the label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark: <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="IsChecked" /> for the checkbox.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="IsEnabled" />  for the checkbox. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the label.
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
    /// Gets or sets the spacing between characters of the label. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see>
    /// </default>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Defines the location of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TextSide.Left">TextSide.Left</see>
    /// </default>
    public TextSide TextSide
    {
        get => (TextSide)GetValue(TextSideProperty);
        set => SetValue(TextSideProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when checkbox is clicked. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TouchAnimationTypes.Fade">TouchAnimationTypes.Fade</see>
    /// </default>
    public TouchAnimationTypes TouchAnimationType
    {
        get => (TouchAnimationTypes)GetValue(TouchAnimationTypeProperty);
        set => SetValue(TouchAnimationTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when checkbox is clicked. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public ITouchAnimation TouchAnimation
    {
        get => (ITouchAnimation)GetValue(TouchAnimationProperty);
        set => SetValue(TouchAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the checkbox changes its status. This is a bindable property.
    /// </summary>
    /// <remarks>This property is used to associate a command with an instance of a checkbox. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    public ICommand CheckedChangedCommand
    {
        get => (ICommand)GetValue(CheckedChangedCommandProperty);
        set => SetValue(CheckedChangedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="CheckedChangedCommandParameter"/> property. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public object CheckedChangedCommandParameter
    {
        get => GetValue(CheckedChangedCommandParameterProperty);
        set => SetValue(CheckedChangedCommandParameterProperty, value);
    }

    #endregion Properties

    #region Constructors

    public MaterialCheckBox()
    {
        _mainLayout = new()
        {
            VerticalOptions = LayoutOptions.Center,
            RowDefinitions = new()
            {
                new RowDefinition()
                {
                    Height = GridLength.Star
                }
            },
            ColumnDefinitions = new()
            {
                new ColumnDefinition()
                {
                    Width = GridLength.Star
                }
            }
        };

#if IOS
        _mainLayout.Padding = new Thickness(6);
#endif

        _checkbox = new()
        {
            Margin = new Thickness(0,0,10,0),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            MinimumHeightRequest = 20,
            MinimumWidthRequest = 20,
            IsEnabled = false,
        };
        _checkbox.SetValue(Grid.RowProperty, 0);
        _checkbox.SetValue(Grid.ColumnProperty, 0);

        _mainLayout.Children.Add(_checkbox);

#if ANDROID
        _boxView = new()
        {
            BackgroundColor = Colors.Transparent,
            Color = Colors.Transparent
        };
        _boxView.SetValue(Grid.RowProperty, 0);
        _boxView.SetValue(Grid.ColumnProperty, 0);

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += OnCheckBoxTapped;
        _boxView.GestureRecognizers.Add(tapGestureRecognizer);

        _mainLayout.Children.Add(_boxView);
#endif

        _checkbox.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(IsChecked), source: this));
        _checkbox.SetBinding(CheckBox.ColorProperty, new Binding(nameof(Color), source: this));
        _checkbox.SetBinding(CustomCheckBox.TickColorProperty, new Binding(nameof(TickColor), source: this));

        _label = new()
        {
            TextColor = TextColor,
            Text = Text,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
        };
        _label.SetValue(Grid.RowProperty, 0);
        _label.SetValue(Grid.ColumnProperty, 1);

        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(TextTransform), source: this));

        TextSideChanged(TextSide);
        ChangeVisualState();

        Behaviors.Add(new TouchBehavior());

        base.Content = _mainLayout;
    }

    #endregion Constructors

    #region ITouchable

    public async void OnTouch(TouchEventType gestureType)
    {
        if (!IsEnabled) return;
        await TouchAnimationManager.AnimateAsync(this, gestureType);

        if (gestureType == TouchEventType.Released)
        {
            IsChecked = !IsChecked;
        }
    }

    #endregion ITouchable

    #region Events

    /// <summary>
    /// Occurs when the checkbox is checked / unchecked
    /// </summary>
    public event EventHandler<CheckedChangedEventArgs>? CheckedChanged;

    #endregion Events

    #region Methods

    private void OnCheckBoxTapped(object? sender, TappedEventArgs e)
    {
        if(IsEnabled)
        {
            this.IsChecked = !this.IsChecked;
        }
    }

    private void TextSideChanged(TextSide textSide)
    {
        _mainLayout.Children.Clear();

        switch (textSide)
        {
            case TextSide.Left:
                _mainLayout.ColumnDefinitions = new()
                {
                    new()
                    {
                        Width = GridLength.Star
                    },
                    new()
                    {
                        Width = GridLength.Auto
                    }
                };

                _checkbox.SetValue(Grid.ColumnProperty, 1);

                _label.SetValue(Grid.ColumnProperty, 0);

                _mainLayout.Children.Add(_label);
                _mainLayout.Children.Add(_checkbox);
#if ANDROID
                _boxView.SetValue(Grid.ColumnProperty, 1);
                _mainLayout.Children.Add(_boxView);
#endif
                break;
            case TextSide.Right:
                _mainLayout.ColumnDefinitions = new()
                {
                    new()
                    {
                        Width = GridLength.Auto
                    },
                    new()
                    {
                        Width = GridLength.Star
                    }
                };
                _checkbox.SetValue(Grid.ColumnProperty, 0);

                _label.SetValue(Grid.ColumnProperty, 1);

                _mainLayout.Children.Add(_checkbox);

#if ANDROID
                _boxView.SetValue(Grid.ColumnProperty, 0);
                _mainLayout.Children.Add(_boxView);
#endif
                _mainLayout.Children.Add(_label);
                break;
        }
    }

    protected override void ChangeVisualState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, CheckboxCommonStates.Disabled);
        }
        else if (IsChecked)
        {
            VisualStateManager.GoToState(this, CheckboxCommonStates.Checked);
        }
        else
        {
            VisualStateManager.GoToState(this, CheckboxCommonStates.Unchecked);
        }
    }  

    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = CheckboxCommonStates.Disabled };

        disabledState.Setters.Add(
            MaterialCheckBox.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(
            MaterialCheckBox.ColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        var checkedState = new VisualState { Name = CheckboxCommonStates.Checked };

        checkedState.Setters.Add(
            MaterialCheckBox.ColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        checkedState.Setters.Add(
            MaterialCheckBox.TickColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnPrimary,
                Dark = MaterialDarkTheme.OnPrimary
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        var uncheckedState = new VisualState { Name = CheckboxCommonStates.Unchecked };
        uncheckedState.Setters.Add(
            MaterialCheckBox.ColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(checkedState);
        commonStatesGroup.States.Add(uncheckedState);

        var style = new Style(typeof(MaterialCheckBox));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}

public class CheckboxCommonStates : VisualStateManager.CommonStates
{
    public const string Checked = "Checked";
    public const string Unchecked = "Unchecked";
}
