using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A RadioButton <see cref="View" /> let people select one option from a set of options and follows Material Design Guidelines <see href="https://m3.material.io/components/radio-button/overview" />.
/// We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs
/// </summary>
public class MaterialRadioButton : ContentView, ITouchable
{
    //TODO: error with visual state manager stroke color doesnt refresh custom radio button on handler, if there soemthing disabled all controls appear disabled

    #region Attributes
    internal const string DefaultGroupName = "MaterialRadioButton.GroupName";
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultStrokeColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyMedium;
    private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    internal const string GroupNameChangedMessage = "MaterialRadioButtonGroupNameChanged";
    internal const string ValueChangedMessage = "MaterialRadioButtonValueChanged";
#nullable enable
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable

    #endregion Attributes

    #region Layout

    private MaterialLabel _label;
    private CustomRadioButton _radioButton;
    private Grid _mainLayout;
    private Grid _radioButtonContainer;

    #endregion Layout

    #region Bindable Properties
    /// <summary>
    /// The backing store for the <see cref="Content" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(string), typeof(MaterialRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="StrokeColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(MaterialRadioButton), defaultValue: DefaultStrokeColor);

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialRadioButton), defaultValue: null);

    //hide materiallabel
    /// <summary>
    /// The backing store for the <see cref="ControlTemplate" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty ControlTemplateProperty = BindableProperty.Create(nameof(ControlTemplate), typeof(ControlTemplate), typeof(MaterialRadioButton), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialRadioButton self && newValue is ControlTemplate controlTemplate)
        {
            self.OnControlTemplateChanged();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialRadioButton), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="GroupName" /> bindable property.
    /// </summary>
    public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(MaterialRadioButton), defaultValue: DefaultGroupName, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialRadioButton self && oldValue is string oldGroupName && newValue is string newGroupName)
        {
            self.OnGroupNamePropertyChanged(oldGroupName, newGroupName);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsChecked" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(MaterialRadioButton), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    { 
        if(bindable is MaterialRadioButton self && newValue is bool value)
        {
            self.InternalCheckedHandler(value);

            if (value)
                MaterialRadioButtonGroup.UpdateRadioButtonGroup(self);

            self.CheckedChanged?.Invoke(self, new CheckedChangedEventArgs(value));
            if (self.CommandCheckedChanged != null && self.CommandCheckedChanged.CanExecute(self.CommandCheckedChangedParameter))
            {
                self.CommandCheckedChanged.Execute(self.CommandCheckedChangedParameter);
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialRadioButton), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if(bindable is MaterialRadioButton self && newValue is bool isEnabled)
        {
            self.InternalEnabledHandler(isEnabled);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialRadioButton), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialRadioButton), defaultValue: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialRadioButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialRadioButton), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialRadioButton), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="TextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialRadioButton), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="TextSide"/> bindable property.
    /// </summary>
    public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(TextSide), typeof(MaterialRadioButton), defaultValue: TextSide.Right, propertyChanged: (bindable, oldValue, newValue) => 
    { 
        if(bindable is MaterialRadioButton self && newValue is TextSide textSide)
        {
            self.TextSideChanged(textSide);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Value"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(MaterialRadioButton), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) => 
    { 
        if(bindableObject is MaterialRadioButton self)
        {
            self.OnValuePropertyChanged();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialRadioButton), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
#nullable enable
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialRadioButton), defaultValue: DefaultAnimationParameter);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialRadioButton));

    /// <summary>
    /// The backing store for the <see cref="CommandCheckedChanged" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandCheckedChangedProperty = BindableProperty.Create(nameof(CommandCheckedChanged), typeof(ICommand), typeof(MaterialRadioButton));

    /// <summary>
    /// The backing store for the <see cref="CommandCheckedChangedParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandCheckedChangedParameterProperty = BindableProperty.Create(nameof(CommandCheckedChangedParameter), typeof(object), typeof(MaterialRadioButton));

    #endregion Bindable Properties

    #region Properties
    /// <summary>
    /// Gets or sets the <see cref="Content" /> for the label. This is a bindable property.
    /// </summary>
    public new string Content
    {
        get { return (string)GetValue(ContentProperty); }
    }

    /// <summary>
    /// Gets or sets the <see cref="Text" /> for the label. This is a bindable property.
    /// </summary>
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="ControlTemplate" /> for the radio button. This is a bindable property.
    /// </summary>
    public new ControlTemplate ControlTemplate
    {
        get { return (ControlTemplate)GetValue(ControlTemplateProperty); }
        set { SetValue(ControlTemplateProperty, value); }
    }


    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the stroke of the radio button. This is a bindable property.
    /// </summary>
    public Color StrokeColor
    {
        get { return (Color)GetValue(StrokeColorProperty); }
        set { SetValue(StrokeColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="TextColor" /> for the text of the label. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="string" /> GroupName for the radio button. 
    /// The default value is <see cref="DefaultGroupName"/>.
    /// This is a bindable property.
    /// </summary>
    public string GroupName
    {
        get { return (string)GetValue(GroupNameProperty); }
        set { SetValue(GroupNameProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="IsChecked" /> for the radio button. 
    /// This is a bindable property.
    /// </summary>
    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set { SetValue(IsCheckedProperty, value); }
    }


    /// <summary>
    /// Gets or sets <see cref="IsEnabled" />  for the radio button. This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the label. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get { return (double)GetValue(CharacterSpacingProperty); }
        set { SetValue(CharacterSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get { return (FontAttributes)GetValue(FontAttributesProperty); }
        set { SetValue(FontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool FontAutoScalingEnabled
    {
        get { return (bool)GetValue(FontAutoScalingEnabledProperty); }
        set { SetValue(FontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get { return (TextTransform)GetValue(TextTransformProperty); }
        set { SetValue(TextTransformProperty, value); }
    }

    /// <summary>
    /// Defines the location of the label. 
    /// The default value is <see cref="TextSide.Left"/>
    /// This is a bindable property.
    /// </summary>
    public TextSide TextSide
    {
        get { return (TextSide)GetValue(TextSideProperty); }
        set { SetValue(TextSideProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label. 
    /// The default value is <see cref="TextSide.Right"/>
    /// This is a bindable property.
    /// </summary>
    public object Value
    {
        get { return (object)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }


    /// <summary>
    /// Gets or sets an animation to be executed when radio button is clicked.
    /// The default value is <see cref="AnimationTypes.Fade"/>.
    /// This is a bindable property.
    /// </summary>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets or sets a custom animation to be executed when radio button is clicked.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }


    /// <summary>
    /// Gets or sets the command to invoke when the radio button changes its status. This is a bindable property.
    /// </summary>
    /// <remarks>This property is used to associate a command with an instance of a radio button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    public ICommand CommandCheckedChanged
    {
        get => (ICommand)GetValue(CommandCheckedChangedProperty);
        set => SetValue(CommandCheckedChangedProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="CommandCheckedChangedParameter"/> property.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public object CommandCheckedChangedParameter
    {
        get => GetValue(CommandCheckedChangedParameterProperty);
        set => SetValue(CommandCheckedChangedParameterProperty, value);
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
            }
        };

        _radioButtonContainer = new Grid()
        {
            MinimumHeightRequest = 48,
            MinimumWidthRequest = 48,
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
            GroupName = GroupName,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(0),
            MinimumHeightRequest = 20,
            MinimumWidthRequest = 20
        };
        _radioButton.CheckedChanged += RadioButton_CheckedChanged;
        _radioButton.SetValue(Grid.RowProperty, 0);
        _radioButton.SetValue(Grid.ColumnProperty, 0);

        _radioButtonContainer.Children.Add(_radioButton);

        _radioButton.SetBinding(RadioButton.GroupNameProperty, new Binding(nameof(GroupName), source: this));
        _radioButton.SetBinding(RadioButton.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _radioButton.SetBinding(RadioButton.IsCheckedProperty, new Binding(nameof(IsChecked), source: this));
        _radioButton.SetBinding(RadioButton.ValueProperty, new Binding(nameof(Value), source: this));
        _radioButton.SetBinding(RadioButton.ControlTemplateProperty, new Binding(nameof(ControlTemplate), source: this));
        _radioButton.SetBinding(CustomRadioButton.StrokeColorProperty, new Binding(nameof(StrokeColor), source: this));

        _label = new()
        {
            TextColor = TextColor,
            Text = Text,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
        };
        _label.SetValue(Grid.RowProperty, 0);

        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(TextTransform), source: this));

        TextSideChanged(TextSide);
        InternalCheckedHandler(IsChecked);
        InternalEnabledHandler(IsEnabled);

        Behaviors.Add(new TouchBehavior());

        base.Content = _mainLayout;
    }

    #endregion Constructors

    #region ITouchable

    public async void OnTouch(TouchType gestureType)
    {
        if (IsEnabled)
        {
            await TouchAnimation.AnimateAsync(this, gestureType);

            if (gestureType == TouchType.Released && !IsChecked)
            {
                IsChecked = !IsChecked;
            }
        }
    }

    #endregion ITouchable

    #region Events

    public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

    #endregion Events

    #region Methods

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        this.IsChecked = e.Value;
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

        _radioButtonContainer.SetValue(Grid.ColumnProperty, 0);
        _mainLayout.Children.Add(_radioButtonContainer);
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

                _radioButtonContainer.SetValue(Grid.ColumnProperty, 1);

                _label.SetValue(Grid.ColumnProperty, 0);

                _mainLayout.Children.Add(_label);
                _mainLayout.Children.Add(_radioButtonContainer);
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
                _radioButtonContainer.SetValue(Grid.ColumnProperty, 0);

                _label.SetValue(Grid.ColumnProperty, 1);

                _mainLayout.Children.Add(_radioButtonContainer);
                _mainLayout.Children.Add(_label);
                break;
        }
    }

    protected virtual void InternalEnabledHandler(bool isEnabled)
    {
        if (isEnabled)
        {
            VisualStateManager.GoToState(this, RadioButtonButtonCommonStates.Unchecked);
        }
        else
        {
            VisualStateManager.GoToState(this, RadioButtonButtonCommonStates.Disabled);
        }
    }

    protected virtual void InternalCheckedHandler(bool isChecked)
    {
        if (isChecked)
        {
            VisualStateManager.GoToState(this, RadioButtonButtonCommonStates.Checked);
        }
        else
        {
            VisualStateManager.GoToState(this, RadioButtonButtonCommonStates.Unchecked);
        }
    }

    void OnValuePropertyChanged()
    {
        if (string.IsNullOrEmpty(GroupName))
        {
            return;
        }
#pragma warning disable CS0618 // TODO: Remove when we internalize/replace MessagingCenter
        MessagingCenter.Send(this, ValueChangedMessage,
                    new MaterialRadioButtonValueChanged(MaterialRadioButtonGroup.GetVisualRoot(this)));
#pragma warning restore CS0618 // Type or member is obsolete
    }

    void OnGroupNamePropertyChanged(string oldGroupName, string newGroupName)
    {
#pragma warning disable CS0618 // TODO: Remove when we internalize/replace MessagingCenter
        if (!string.IsNullOrEmpty(newGroupName))
        {
            if (string.IsNullOrEmpty(oldGroupName))
            {
                MessagingCenter.Subscribe<MaterialRadioButton, MaterialRadioButtonGroupSelectionChanged>(this,
                    MaterialRadioButtonGroup.GroupSelectionChangedMessage, HandleRadioButtonGroupSelectionChanged);
                MessagingCenter.Subscribe<Element, MaterialRadioButtonGroupValueChanged>(this,
                    MaterialRadioButtonGroup.GroupValueChangedMessage, HandleRadioButtonGroupValueChanged);
            }

            MessagingCenter.Send(this, GroupNameChangedMessage,
                new MaterialRadioButtonGroupNameChanged(MaterialRadioButtonGroup.GetVisualRoot(this), oldGroupName));
        }
        else
        {
            if (!string.IsNullOrEmpty(oldGroupName))
            {
                MessagingCenter.Unsubscribe<MaterialRadioButton, MaterialRadioButtonGroupSelectionChanged>(this, MaterialRadioButtonGroup.GroupSelectionChangedMessage);
                MessagingCenter.Unsubscribe<Element, MaterialRadioButtonGroupValueChanged>(this, MaterialRadioButtonGroup.GroupValueChangedMessage);
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete
    }

    void HandleRadioButtonGroupValueChanged(Element layout, MaterialRadioButtonGroupValueChanged args)
    {
        if (IsChecked || string.IsNullOrEmpty(GroupName) || GroupName != args.GroupName || !object.Equals(Value, args.Value) || !object.Equals(Value, args.Value) || !MatchesScope(args))
        {
            return;
        }

        SetValue(IsCheckedProperty, true);
    }

    bool MatchesScope(MaterialRadioButtonScopeMessage message)
    {
        return MaterialRadioButtonGroup.GetVisualRoot(this) == message.Scope;
    }

    void HandleRadioButtonGroupSelectionChanged(MaterialRadioButton selected, MaterialRadioButtonGroupSelectionChanged args)
    {
        if (!IsChecked || selected == this || string.IsNullOrEmpty(GroupName) || GroupName != selected.GroupName || !MatchesScope(args))
        {
            return;
        }

        SetValue(IsCheckedProperty, false);
    }

    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = RadioButtonButtonCommonStates.Disabled };

        disabledState.Setters.Add(
            MaterialRadioButton.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(
            MaterialRadioButton.StrokeColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        var checkedState = new VisualState { Name = RadioButtonButtonCommonStates.Checked };

        checkedState.Setters.Add(
            MaterialRadioButton.StrokeColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        var uncheckedState = new VisualState { Name = RadioButtonButtonCommonStates.Unchecked };
        uncheckedState.Setters.Add(
            MaterialRadioButton.StrokeColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        var normalState = new VisualState { Name = RadioButtonButtonCommonStates.Normal };
        normalState.Setters.Add(
            MaterialRadioButton.StrokeColorProperty,
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
        commonStatesGroup.States.Add(normalState);

        var style = new Style(typeof(MaterialRadioButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }
    #endregion Styles
}

public class RadioButtonButtonCommonStates : VisualStateManager.CommonStates
{
    public const string Checked = "Checked";
    public const string Unchecked = "Unchecked";
}
