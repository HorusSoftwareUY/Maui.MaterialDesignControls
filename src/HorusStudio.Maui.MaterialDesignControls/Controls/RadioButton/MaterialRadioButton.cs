using System.ComponentModel;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Converters;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Radio buttons let people select one option from a set of options and follow Material Design Guidelines. <see href="https://m3.material.io/components/radio-button/overview">See more</see>.
/// We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs
/// </summary>
/// <remarks>The <see href="docs/Controls/horusstudio.maui.materialdesigncontrols.materialviewgroup.md">MaterialViewGroup</see> class allows grouping radio buttons, providing control over the selection type (Single or Multiple), item selection through bindings, and commands that trigger when the selection changes.</remarks>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialRadioButton.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialRadioButton
///         TextSide="Left"
///         CheckedChangedCommand="{Binding CheckedChangedCommand}"
///         Text="Radio button 1"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var radioButton = new MaterialRadioButton()
/// {
///     Text = "Radio button 1",
///     TextSide = TextSide.Left,
///     CheckedChangedCommand = viewModel.CheckChangedCommand
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ProgressIndicatorPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] FontAttributes doesn't work.
/// * Using a control template doesn't work when define a custom style for disabled state.
/// * The Selected property in Appium is not supported when using the AutomationId of this control, just like with the native MAUI control.
/// </todoList>
public class MaterialRadioButton : ContentView, ITouchableView, IGroupableView
{
    #region Attributes
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultStrokeColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;

    #endregion Attributes

    #region Layout

    private readonly MaterialLabel _label;
    private readonly CustomRadioButton _radioButton;
    private readonly Grid _mainLayout;

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Content">Content</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(string), typeof(MaterialRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="StrokeColor">StrokeColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(MaterialRadioButton), defaultValueCreator: DefaultStrokeColor);

    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ControlTemplate">ControlTemplate</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty ControlTemplateProperty = BindableProperty.Create(nameof(ControlTemplate), typeof(ControlTemplate), typeof(MaterialRadioButton), defaultValue: null, propertyChanged: (bindableObject, _, newValue) =>
    {
        if (bindableObject is MaterialRadioButton self && newValue is ControlTemplate)
        {
            self.OnControlTemplateChanged();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TextColor">TextColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialRadioButton), defaultValueCreator: DefaultTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="IsChecked">IsChecked</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(MaterialRadioButton), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    { 
        if(bindable is MaterialRadioButton self && newValue is bool value)
        {
            self.ChangeVisualState();
            
            self.CheckedChanged?.Invoke(self, new CheckedChangedEventArgs(value));
            if (self.CheckedChangedCommand != null && self.CheckedChangedCommand.CanExecute(value))
            {
                self.CheckedChangedCommand.Execute(value);
            }
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="IsEnabled">IsEnabled</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialRadioButton), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if(bindable is MaterialRadioButton self && newValue is bool)
        {
            self.ChangeVisualState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialRadioButton), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialRadioButton), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes">FontAttributes</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled">FontAutoScalingEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialRadioButton), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialRadioButton), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="TextTransform">TextTransform</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialRadioButton), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="TextSide">TextSide</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(TextSide), typeof(MaterialRadioButton), defaultValue: TextSide.Right, propertyChanged: (bindable, _, newValue) => 
    { 
        if(bindable is MaterialRadioButton self && newValue is TextSide textSide)
        {
            self.TextSideChanged(textSide);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Value">Value</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(MaterialRadioButton), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) => 
    { 
        if(bindableObject is MaterialRadioButton self)
        {
            self.GroupableViewPropertyChanged?.Invoke(self, new GroupableViewPropertyChangedEventArgs(nameof(Value), oldValue, newValue));
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialRadioButton), defaultValueCreator: DefaultTouchAnimationType);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialRadioButton));

    /// <summary>
    /// The backing store for the <see cref="CheckedChangedCommand">CheckedChangedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CheckedChangedCommandProperty = BindableProperty.Create(nameof(CheckedChangedCommand), typeof(ICommand), typeof(MaterialRadioButton));
    
    /// <summary>
    /// The backing store for the <see cref="AutomationId">AutomationId</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty AutomationIdProperty = BindableProperty.Create(nameof(AutomationId), typeof(string), typeof(MaterialRadioButton), null);
    
    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="RadioButton">RadioButton</see> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public RadioButton InternalRadioButton => _radioButton;

    /// <summary>
    /// Gets the <see cref="Content">Content</see> of the radio button.
    /// This is a bindable property.
    /// </summary>
    public new string Content => (string)GetValue(ContentProperty);

    /// <summary>
    /// Gets or sets a text for the radio button.
    /// This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Value));
        } 
    }

    /// <summary>
    /// Gets or sets the <see cref="ControlTemplate">template</see> for the radio button.
    /// This is a bindable property.
    /// </summary>
    public new ControlTemplate ControlTemplate
    {
        get => (ControlTemplate)GetValue(ControlTemplateProperty);
        set => SetValue(ControlTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke <see cref="Color">color</see> for the radio button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the text <see cref="Color">color</see> for the radio button.
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
    /// Gets or sets if the radio button is checked. 
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="false">False</see>
    /// </default>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
    
    /// <inheritdoc />
    /// <remarks>This property is used internally, and it's recommended to avoid setting it directly.</remarks>
    public bool IsSelected
    {
        get => IsChecked;
        set => IsChecked = value;
    }

    /// <summary>
    /// Gets or sets the radio button is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="false">False</see>
    /// </default>
    public new bool IsEnabled
    {
        get =>(bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the label.
    /// This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label.
    /// This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the label.
    /// This is a bindable property.
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
    /// <see langword="true">True</see>
    /// </default>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the label.
    /// This is a bindable property.
    /// </summary>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the label.
    /// This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value); 
    }

    /// <summary>
    /// Defines the location of the label. 
    /// This is a bindable property.
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
    /// Defines the value of the radio button
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialRadioButton.Text">MaterialRadioButton.Text</see>
    /// </default>
    /// <remarks>If a value is not explicitly set, the control will use the value of the <see cref="MaterialRadioButton.Text">Text</see> property if set or the <see cref="MaterialRadioButton.Id">Id</see> property as its default.</remarks>
    public object Value
    {
        get => GetValue(ValueProperty) ?? (!string.IsNullOrEmpty(Text) ? Text : Id);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when radio button is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TouchAnimationTypes.Fade">ouchAnimationTypes.Fade</see>
    /// </default>
    public TouchAnimationTypes TouchAnimationType
    {
        get => (TouchAnimationTypes)GetValue(TouchAnimationTypeProperty);
        set => SetValue(TouchAnimationTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when radio button is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ITouchAnimation TouchAnimation
    {
        get => (ITouchAnimation)GetValue(TouchAnimationProperty);
        set => SetValue(TouchAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the radio button changes its status.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// This property is used to associate a command with an instance of a radio button.
    /// This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.
    /// The command parameter is of type <see cref="bool">bool</see> and corresponds to the value of the <see cref="IsChecked">IsChecked</see> property.
    /// </remarks>
    public ICommand CheckedChangedCommand
    {
        get => (ICommand)GetValue(CheckedChangedCommandProperty);
        set => SetValue(CheckedChangedCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value that allows the automation framework to find and interact with this element.
    /// </summary>
    /// <remarks>
    /// This value may only be set once on an element.
    /// 
    /// When set on this control, the <see cref="AutomationId"/> is also used as a base identifier for its internal elements:
    /// - The <see cref="MaterialRadioButton"/> control uses the same <see cref="AutomationId"/> value.
    /// - The chip's text label uses the identifier "{AutomationId}_Text".
    /// 
    /// This convention allows automated tests and accessibility tools to consistently locate all subelements of the control.
    /// </remarks>
    public new string AutomationId
    {
        get => (string)GetValue(AutomationIdProperty);
        set => SetValue(AutomationIdProperty, value);
    }
    
    #endregion Properties

    #region Constructors

    public MaterialRadioButton()
    {
        _mainLayout = new()
        {
            Margin = new Thickness(0),
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

        _radioButton = new()
        {
            Margin = new Thickness(0),
            GroupName = Guid.NewGuid().ToString(),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(0),
            MinimumHeightRequest = 20,
            MinimumWidthRequest = 20,
            InputTransparent = true
        };
        _radioButton.CheckedChanged += RadioButton_CheckedChanged;
        _radioButton.SetValue(Grid.RowProperty, 0);
        _radioButton.SetValue(Grid.ColumnProperty, 0);

        _mainLayout.Children.Add(_radioButton);
        
        _radioButton.SetBinding(RadioButton.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _radioButton.SetBinding(RadioButton.IsCheckedProperty, new Binding(nameof(IsChecked), source: this));
        _radioButton.SetBinding(RadioButton.ValueProperty, new Binding(nameof(Value), source: this));
        _radioButton.SetBinding(RadioButton.ControlTemplateProperty, new Binding(nameof(ControlTemplate), source: this));
        _radioButton.SetBinding(CustomRadioButton.StrokeColorProperty, new Binding(nameof(StrokeColor), source: this));
        _radioButton.SetBinding(RadioButton.AutomationIdProperty, new Binding(nameof(AutomationId), source: this));
        
        _label = new()
        {
            TextColor = TextColor,
            Text = Text,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
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
        _label.SetBinding(MaterialLabel.AutomationIdProperty, new Binding(nameof(AutomationId), source: this, converter: new AutomationIdConverter(), converterParameter: "Text"));

        TextSideChanged(TextSide);

        Behaviors.Add(new TouchBehavior());

        base.Content = _mainLayout;
    }

    #endregion Constructors

    #region ITouchable

    public async void OnTouch(TouchEventType gestureType)
    {
        if (!IsEnabled) return;
        
        await TouchAnimationManager.AnimateAsync(this, gestureType);
        
        Touch?.Invoke(this, new TouchEventArgs(gestureType));
        
        if (gestureType == TouchEventType.Released)
        {
            if (GroupableViewPropertyChanged != null)
            {
                GroupableViewPropertyChanged.Invoke(this, new GroupableViewPropertyChangedEventArgs(nameof(IsSelected), IsChecked, !IsChecked));
            }
            else
            {
                IsChecked = !IsChecked;
            }
        }
    }

    #endregion ITouchable

    #region Events

    public event EventHandler<GroupableViewPropertyChangedEventArgs>? GroupableViewPropertyChanged;
    
    /// <summary>
    /// Occurs when the radio button is switched 
    /// </summary>
    public event EventHandler<CheckedChangedEventArgs>? CheckedChanged;
    
    /// <summary>
    /// Occurs when the radio button is touched.
    /// </summary>
    public event EventHandler<TouchEventArgs>? Touch;

    #endregion Events

    #region Methods
    
    private void RadioButton_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        IsChecked = e.Value;
    }

    private void OnControlTemplateChanged()
    {
        _mainLayout.Children.Clear();
        _mainLayout.ColumnDefinitions = new()
                {
                    new()
                    {
                        Width = GridLength.Star
                    }
                };

        _radioButton.SetValue(Grid.ColumnProperty, 0);
        _mainLayout.Children.Add(_radioButton);
        _radioButton.IsControlTemplateByDefault = false;
    }

    private void TextSideChanged(TextSide textSide)
    {
        switch (textSide)
        {
            case TextSide.Left:
                _mainLayout.Children.Clear();
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

                _radioButton.SetValue(Grid.ColumnProperty, 1);

                _label.SetValue(Grid.ColumnProperty, 0);

                _mainLayout.Children.Add(_label);
                _mainLayout.Children.Add(_radioButton);
                break;
            case TextSide.Right:
                _mainLayout.Children.Clear();
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
                _radioButton.SetValue(Grid.ColumnProperty, 0);

                _label.SetValue(Grid.ColumnProperty, 1);

                _mainLayout.Children.Add(_radioButton);
                _mainLayout.Children.Add(_label);
                break;
        }
    }

    protected override void ChangeVisualState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, RadioButtonCommonStates.Disabled);
        }
        else if (IsChecked)
        {
            VisualStateManager.GoToState(this, RadioButtonCommonStates.Checked);
        }
        else
        {
            VisualStateManager.GoToState(this, RadioButtonCommonStates.Unchecked);
        }
    }
    
    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var resourceDictionary = new MaterialRadioButtonStyles();
        return resourceDictionary.Values.OfType<Style>();
    }
    #endregion Styles
}

public class RadioButtonCommonStates : VisualStateManager.CommonStates
{
    public const string Checked = "Checked";
    public const string Unchecked = "Unchecked";
}