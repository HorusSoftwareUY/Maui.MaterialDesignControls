
namespace HorusStudio.Maui.MaterialDesignControls;
public class MaterialRating : ContentView
{
    #region Attributes
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultStrokeColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultStrokeThickness = 2.0;
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyMedium;
    private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
#nullable enable
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable

    #endregion Attributes

    #region Layout

    private MaterialLabel _label;
    private Grid _mainLayout;

    #endregion Layout

    #region Bindable Properties


    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialRating), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialRating), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialRating), defaultValue: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialRating), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTransformProperty = BindableProperty.Create(nameof(LabelTransform), typeof(TextTransform), typeof(MaterialRating), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialRating self && newValue is bool)
        {
            self.ChangeVisualState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialRating), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
#nullable enable
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialRating), defaultValue: DefaultAnimationParameter);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialRating));

    /// <summary>
    /// The backing store for the <see cref="StrokeColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(MaterialRating), defaultValue: DefaultStrokeColor);

    /// <summary>
    /// The backing store for the <see cref="StrokeThickness" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(MaterialRating), defaultValue: DefaultStrokeThickness);




    /// <summary>
    /// The backing store for the <see cref="Value"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(MaterialRadioButton), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialRadioButton self)
        {
            self.OnValuePropertyChanged();
        }
    });

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the <see cref="Label" /> for the label. This is a bindable property.
    /// </summary>
    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of the label. This is a bindable property.
    /// </summary>
    public Color LabelColor
    {
        get { return (Color)GetValue(LabelColorProperty); }
        set { SetValue(LabelColorProperty, value); }
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
    public TextTransform LabelTransform
    {
        get { return (TextTransform)GetValue(LabelTransformProperty); }
        set { SetValue(LabelTransformProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="IsEnabled" />  for the rating control. This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
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
    /// Gets or sets a custom animation to be executed when a icon is clicked.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the stroke of default start. This is a bindable property.
    /// </summary>
    public Color StrokeColor
    {
        get { return (Color)GetValue(StrokeColorProperty); }
        set { SetValue(StrokeColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="StrokeThickness" /> for the default start. This is a bindable property.
    /// </summary>
    public double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty); }
        set { SetValue(StrokeThicknessProperty, value); }
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
            TextColor = LabelColor,
            Text = Label,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
        };
        _label.SetValue(Grid.RowProperty, 0);

        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Label), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTransform), source: this));

        TextSideChanged(TextSide);
        //ChangeVisualState();

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

        var disabledState = new VisualState { Name = RadioButtonCommonStates.Disabled };

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

        var checkedState = new VisualState { Name = RadioButtonCommonStates.Checked };

        checkedState.Setters.Add(
            MaterialRadioButton.StrokeColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(1f));

        var uncheckedState = new VisualState { Name = RadioButtonCommonStates.Unchecked };
        uncheckedState.Setters.Add(
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

        var style = new Style(typeof(MaterialRadioButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }
    #endregion Styles
}