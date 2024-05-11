using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialInputType
{
    Filled, Outlined
}

public enum MaterialInputTypeStates
{
    FilledDisabled, FilledFocused, FilledNormal, OutlinedDisabled, OutlinedFocused, OutlinedNormal
}

public abstract partial class MaterialInputBase : ContentView
{
    #region Attributes

    private readonly static MaterialInputType DefaultInputType = MaterialInputType.Filled;
    private readonly static bool DefaultIsEnabled = true;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = Colors.Green, Dark = Colors.Green }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultIconTintColor = new AppThemeBindingExtension { Light = Colors.Green, Dark = Colors.Green }.GetValueForCurrentTheme<Color>();
    private readonly static Brush DefaultBackground = Entry.BackgroundProperty.DefaultValue as Brush;
    private readonly static Color DefaultBackgroundColor = Colors.Transparent;
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = Colors.Green, Dark = Colors.Green }.GetValueForCurrentTheme<Color>();
    private readonly static CornerRadius DefaultCornerRadius = new(0);

    private readonly static TextAlignment DefaultMaterialInputBase = TextAlignment.Start;

    private readonly Dictionary<MaterialInputTypeStates, object> _backgroundColors = new()
    {
        { MaterialInputTypeStates.FilledDisabled, new AppThemeBindingExtension { Light = Colors.LightGray, Dark = Colors.LightGray } },
        { MaterialInputTypeStates.FilledFocused, new AppThemeBindingExtension { Light = Colors.LightBlue, Dark = Colors.LightBlue } },
        { MaterialInputTypeStates.FilledNormal, new AppThemeBindingExtension { Light = Colors.LightGreen, Dark = Colors.LightGreen } },
        { MaterialInputTypeStates.OutlinedDisabled, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedFocused, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } },
        { MaterialInputTypeStates.OutlinedNormal, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainer, Dark = MaterialDarkTheme.SurfaceContainer } }
    };

    private readonly Dictionary<MaterialInputType, CornerRadius> _cornerRadius = new()
    {
        { MaterialInputType.Filled, new CornerRadius(4,4,0,0) },
        { MaterialInputType.Outlined, new CornerRadius(4) }
    };

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialInputType), typeof(MaterialInputBase), defaultValue: DefaultInputType, defaultBindingMode: BindingMode.OneTime, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.UpdateLayoutAfterTypeChanged(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialInputBase), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetIsEnabled(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="SupportingText" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialInputBase), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialInputBase), defaultValue: DefaultBackground, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialInputBase), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetCornerRadius(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LeadingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconSourceProperty = BindableProperty.Create(nameof(LeadingIconSource), typeof(ImageSource), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="TrailingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconSourceProperty = BindableProperty.Create(nameof(TrailingIconSource), typeof(ImageSource), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="IsFocused"/> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="HorizontalTextAlignment"/> bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MaterialInputBase), defaultValue: DefaultMaterialInputBase);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the input type according to <see cref="MaterialInputType"/> enum.
    /// The default value is <see cref="MaterialInputBaseType.Filled"/>. This is a bindable property.
    /// </summary>
    public MaterialInputType Type
    {
        get => (MaterialInputType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets if the input is enabled or diabled. This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the input. This is a bindable property.
    /// </summary>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the input. This is a bindable property.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the input. This is a bindable property.
    /// </summary>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the input, in device-independent units. This is a bindable property.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units. This is a bindable property.
    /// </summary>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="MaterialIconButton"/>.</remarks>
    public ImageSource LeadingIconSource
    {
        get => (ImageSource)GetValue(LeadingIconSourceProperty);
        set => SetValue(LeadingIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="MaterialIconButton"/>.</remarks>
    public ImageSource TrailingIconSource
    {
        get => (ImageSource)GetValue(TrailingIconSourceProperty);
        set => SetValue(TrailingIconSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the placeholder of the input.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the label of the input.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the supporting text of the input.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the input.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the input. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the leading and trailing button's icons of the input. This is a bindable property.
    /// </summary>
    public Color? IconTintColor
    {
        get => (Color?)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }
#nullable disable

    /// <inheritdoc/>
    public new bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set => SetValue(IsFocusedProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment for the input. This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set { SetValue(HorizontalTextAlignmentProperty, value); }
    }

    /// <summary>
    /// Gets or sets the command to invoke when the input is tapped.
    /// </summary>
    /// <remarks>This property is used internally and it's recommended to avoid setting it directly.</remarks>
    public ICommand InputTapCommand { get; set; }

    #endregion Properties

    public MaterialInputBase()
    {
        InitializeComponent();

        if (Type == DefaultInputType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        System.Diagnostics.Debug.WriteLine($"============> OnPropertyChanged: {propertyName}");

        if (propertyName == nameof(Window) && Window != null)
        {
            // Window property is setted with a value when the view is appearing
            OnAppearing();
        }
        else if (propertyName == nameof(Window) && Window == null)
        {
            // Window property is setted on null when the view is disappearing
            OnDisappearing();
        }
    }

    private void OnAppearing()
    {
        // Add tap gesture to the input control to do focus
        var border = (Border)GetTemplateChild("InputBorder");
        if (border != null)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Command = InputTapCommand;
            border.GestureRecognizers.Add(tapGestureRecognizer);
        }

        OnControlAppearing();
    }

    protected abstract void OnControlAppearing();

    private void OnDisappearing()
    {
        OnControlDisappearing();
    }

    protected abstract void OnControlDisappearing();

    private void UpdateLayoutAfterTypeChanged(MaterialInputType type)
    {
        SetTemplate(type);
        SetIsEnabled(type);
        SetCornerRadius(type);

        UpdateLayoutAfterStatusChanged(type);
    }

    protected void UpdateLayoutAfterStatusChanged(MaterialInputType type)
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

    protected MaterialInputTypeStates GetCurrentTypeState(MaterialInputType type)
    {
        if (IsFocused)
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledFocused : MaterialInputTypeStates.OutlinedFocused;
        else if (!IsEnabled)
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledDisabled : MaterialInputTypeStates.OutlinedDisabled;
        else
            return type == MaterialInputType.Filled ? MaterialInputTypeStates.FilledNormal : MaterialInputTypeStates.OutlinedNormal;
    }

    protected string GetCurrentVisualState()
    {
        if (IsFocused)
            return VisualStateManager.CommonStates.Focused;
        else if (!IsEnabled)
            return VisualStateManager.CommonStates.Disabled;
        else
            return VisualStateManager.CommonStates.Normal;
    }

    private void SetIsEnabled(MaterialInputType type)
    {
        SetControlIsEnabled();
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterStatusChanged(type);
    }

    protected abstract void SetControlIsEnabled();

    private void SetBackground(MaterialInputType type)
    {
        System.Diagnostics.Debug.WriteLine($"============> SetBackground");

        var inputBorder = (Border)GetTemplateChild("InputBorder");
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
        System.Diagnostics.Debug.WriteLine($"============> SetBackgroundColor");

        var inputBorder = (Border)GetTemplateChild("InputBorder");
        if (inputBorder != null)
        {
            SetBackgroundColorToView(type, inputBorder);
        }

        if (type == MaterialInputType.Outlined)
        {
            var outlinedHint = (Label)GetTemplateChild("OutlinedHint");
            if (outlinedHint != null)
            {
                SetBackgroundColorToView(type, outlinedHint);
            }
        }
    }

    private void SetBackgroundColorToView(MaterialInputType type, View view)
    {
        if (_backgroundColors.TryGetValue(GetCurrentTypeState(type), out object background) && background != null)
        {
            if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
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
            view.BackgroundColor = DefaultBackgroundColor;
        }
    }

    protected void SetCornerRadius(MaterialInputType type)
    {
        var inputBorder = (Border)GetTemplateChild("InputBorder");
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

    #region Styles

    protected static VisualStateGroupList GetBaseStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        //var disabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };
        //disabledState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.OnSurface,
        //        Dark = MaterialDarkTheme.OnSurface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.12f));

        //disabledState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.OnSurface,
        //        Dark = MaterialDarkTheme.OnSurface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.38f));

        //disabledState.Setters.Add(
        //    MaterialInputBase.IconTintColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.OnSurface,
        //        Dark = MaterialDarkTheme.OnSurface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.38f));

        //disabledState.Setters.Add(MaterialButton.ShadowProperty, null);

        //disabledState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.OnSurface,
        //        Dark = MaterialDarkTheme.OnSurface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.12f));

        //var focusedState = new VisualState { Name = VisualStateManager.CommonStates.Focused };
        //focusedState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    Colors.Green);

        //focusedState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.Primary,
        //        Dark = MaterialDarkTheme.Primary
        //    }
        //    .GetValueForCurrentTheme<Color>());

        //focusedState.Setters.Add(MaterialInputBase.BorderWidthProperty, 2);
        //focusedState.Setters.Add(MaterialInputBase.CornerRadiusProperty, new CornerRadius(16));

        //var normalState = new VisualState { Name = VisualStateManager.CommonStates.Normal };
        //normalState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    Colors.Blue);

        //normalState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.Primary,
        //        Dark = MaterialDarkTheme.Primary
        //    }
        //    .GetValueForCurrentTheme<Color>());

        //normalState.Setters.Add(MaterialInputBase.BorderWidthProperty, 2);
        //normalState.Setters.Add(MaterialInputBase.CornerRadiusProperty, new CornerRadius(16));


        //var filledDisabledState = new VisualState { Name = InputCommonStates.FilledDisabled };
        //filledDisabledState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    Colors.LightGray);
        //filledDisabledState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.DarkGray);
        //filledDisabledState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.DarkGray);

        //var filledFocusedState = new VisualState { Name = InputCommonStates.FilledFocused };
        //filledFocusedState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    Colors.LightBlue);
        //filledFocusedState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.Blue);
        //filledFocusedState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.Blue);

        //var filledNormalState = new VisualState { Name = InputCommonStates.FilledNormal };
        //filledNormalState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    Colors.LightGreen);
        //filledNormalState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.Green);
        //filledNormalState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.Green);

        //commonStatesGroup.States.Add(filledNormalState);
        //commonStatesGroup.States.Add(filledDisabledState);
        //commonStatesGroup.States.Add(filledFocusedState);

        //var outlinedDisabledState = new VisualState { Name = InputCommonStates.OutlinedDisabled };
        //outlinedDisabledState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    MaterialLightTheme.SurfaceContainer);
        //outlinedDisabledState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.DarkGray);
        //outlinedDisabledState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.DarkGray);

        //var outlinedFocusedState = new VisualState { Name = InputCommonStates.OutlinedFocused };
        //outlinedFocusedState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    MaterialLightTheme.SurfaceContainer);
        //outlinedFocusedState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.Blue);
        //outlinedFocusedState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.Blue);

        //var outlinedNormalState = new VisualState { Name = InputCommonStates.OutlinedNormal };
        //outlinedNormalState.Setters.Add(
        //    MaterialInputBase.BackgroundColorProperty,
        //    MaterialLightTheme.SurfaceContainer);
        //outlinedNormalState.Setters.Add(
        //    MaterialInputBase.TextColorProperty,
        //    Colors.Green);
        //outlinedNormalState.Setters.Add(
        //    MaterialInputBase.BorderColorProperty,
        //    Colors.Green);

        //commonStatesGroup.States.Add(outlinedNormalState);
        //commonStatesGroup.States.Add(outlinedDisabledState);
        //commonStatesGroup.States.Add(outlinedFocusedState);

        //commonStatesGroup.States.Add(new VisualState { Name = VisualStateManager.CommonStates.Normal });
        //commonStatesGroup.States.Add(new VisualState { Name = VisualStateManager.CommonStates.Disabled });
        //commonStatesGroup.States.Add(new VisualState { Name = VisualStateManager.CommonStates.Focused });

        var filledDisabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };
        filledDisabledState.Setters.Add(
            MaterialInputBase.TextColorProperty,
            Colors.DarkGray);
        filledDisabledState.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            Colors.DarkGray);
        filledDisabledState.Setters.Add(
            MaterialInputBase.IconTintColorProperty,
            Colors.DarkGray);

        var filledFocusedState = new VisualState { Name = VisualStateManager.CommonStates.Focused };
        filledFocusedState.Setters.Add(
            MaterialInputBase.TextColorProperty,
            Colors.Blue);
        filledFocusedState.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            Colors.Blue);
        filledFocusedState.Setters.Add(
            MaterialInputBase.IconTintColorProperty,
            Colors.Blue);

        var filledNormalState = new VisualState { Name = VisualStateManager.CommonStates.Normal };
        filledNormalState.Setters.Add(
            MaterialInputBase.TextColorProperty,
            DefaultTextColor);
        filledNormalState.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            DefaultBorderColor);
        filledNormalState.Setters.Add(
            MaterialInputBase.IconTintColorProperty,
            DefaultIconTintColor);

        commonStatesGroup.States.Add(filledNormalState);
        commonStatesGroup.States.Add(filledDisabledState);
        commonStatesGroup.States.Add(filledFocusedState);

        return new VisualStateGroupList() { commonStatesGroup };
    }

    #endregion Styles
}