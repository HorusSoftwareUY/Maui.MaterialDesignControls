using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialInputType
{
    Filled, Outlined
}

public partial class MaterialInputBase : ContentView
{
    #region Attributes

    private readonly static MaterialInputType DefaultInputType = MaterialInputType.Filled;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private readonly static Brush DefaultBackground = Colors.Transparent;
    private readonly static Color DefaultBackgroundColor = Colors.Transparent;
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBorderColor = Colors.Transparent;
    private readonly static CornerRadius DefaultCornerRadius = new(0);

    private readonly Dictionary<MaterialInputType, object> _backgroundColors = new()
    {
        { MaterialInputType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest } },
        { MaterialInputType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } }
    };

    private readonly Dictionary<MaterialInputType, object> _borderColors = new()
    {
        { MaterialInputType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant } },
        { MaterialInputType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } }
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
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialInputType), typeof(MaterialInputBase), defaultValue: DefaultInputType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.UpdateLayoutAfterTypeChanged(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Hint" /> bindable property.
    /// </summary>
    internal static readonly BindableProperty HintProperty = BindableProperty.Create(nameof(Hint), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialInputBase), propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.Hint = self.Label ?? self.Placeholder;
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputBase), propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.Hint = self.Label ?? self.Placeholder;
        }
    });

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
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTextColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetTextColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTintColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetTintColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TintColor" /> bindable property.
    /// </summary>
    internal static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTintColor);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialInputBase), defaultValue: DefaultBackground, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBorderColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBorderColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialInputBase), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconSourceProperty = BindableProperty.Create(nameof(LeadingIconSource), typeof(ImageSource), typeof(MaterialInputBase), propertyChanged: (bindable, oldValue, newValue) =>
    {
        //if (bindable is MaterialInputBase self)
        //{
        //    if ((newValue == null && oldValue != null) ||
        //        (newValue is ImageSource newImage && (oldValue == null || (oldValue is ImageSource oldImage && !oldImage.Equals(newImage)))))
        //    {
        //        self.UpdatePadding();
        //    }
        //}
    });

    /// <summary>
    /// The backing store for the <see cref="TrailingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconSourceProperty = BindableProperty.Create(nameof(TrailingIconSource), typeof(ImageSource), typeof(MaterialInputBase), propertyChanged: (bindable, oldValue, newValue) =>
    {
        //if (bindable is MaterialInputBase self)
        //{
        //    if ((newValue == null && oldValue != null) ||
        //        (newValue is ImageSource newImage && (oldValue == null || (oldValue is ImageSource oldImage && !oldImage.Equals(newImage)))))
        //    {
        //        self.UpdatePadding();
        //    }
        //}
    });

    /// <summary>
    /// The backing store for the <see cref="IsFocused"/> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

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
    /// Gets or sets a <see cref="Brush"/> that describes the background of the button. This is a bindable property.
    /// </summary>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the button. This is a bindable property.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the button. This is a bindable property.
    /// </summary>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.
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
    /// Allows you to display a bitmap image on the Button. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource LeadingIconSource
    {
        get => (ImageSource)GetValue(LeadingIconSourceProperty);
        set => SetValue(LeadingIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the Button. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource TrailingIconSource
    {
        get => (ImageSource)GetValue(TrailingIconSourceProperty);
        set => SetValue(TrailingIconSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the floating label of the input.
    /// Takes value from <see cref="Label"/> or <see cref="Placeholder"/>. This is a bindable property.
    /// </summary>
    public string Hint
    {
        get => (string)GetValue(HintProperty);
        internal set => SetValue(HintProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color? IconTintColor
    {
        get => (Color?)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color? TintColor
    {
        get => (Color?)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow effect cast by the element. This is a bindable property.
    /// </summary>
    public new bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set => SetValue(IsFocusedProperty, value);
    }

    #endregion Properties

    #region Layout

    //IMaterialInputBase _content;

    #endregion

    public MaterialInputBase()
    {
        InitializeComponent();

        CreateLayout();
        if (Type == DefaultInputType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(Content) && Content != null)
        {
            Content.Focused += ContentFocusChanged;
            Content.Unfocused += ContentFocusChanged;
        }
        else if (propertyName == nameof(Window) && Window == null)
        {
            // Window property is setted on null when the view is dissapearing
            // So we cleanup events/animations

            if (Content != null)
            {
                Content.Focused -= ContentFocusChanged;
                Content.Unfocused -= ContentFocusChanged;
            }
        }
    }

    private void CreateLayout()
    {
    }

    private void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        IsFocused = e.IsFocused;
        VisualStateManager.GoToState(this, IsFocused ? VisualStateManager.CommonStates.Focused : VisualStateManager.CommonStates.Normal);
    }

    //protected abstract View CreateView();

    private void UpdateLayoutAfterTypeChanged(MaterialInputType type)
    {
        SetTemplate(type);
        SetBackgroundColor(type);
        SetBorderColor(type);
        //SetTextColor(type);
        //SetTintColor(type);
        //SetShadow(type);
        SetCornerRadius(type);
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

    private void SetBackgroundColor(MaterialInputType type)
    {
        if (_backgroundColors.TryGetValue(type, out object background) && background != null)
        {
            // Default Material value according to Type
            if (background is Color backgroundColor)
            {
                BackgroundColor = backgroundColor;
            }
            else if (background is AppThemeBindingExtension theme)
            {
                BackgroundColor = theme.GetValueForCurrentTheme<Color>();
            }          
        }
    }

    private void SetBorderColor(MaterialInputType type)
    {
        if (_borderColors.TryGetValue(type, out object border) && border != null)
        {
            // Default Material value according to Type
            if (border is Color borderColor)
            {
                BorderColor = borderColor;
            }
            else if (border is AppThemeBindingExtension theme)
            {
                BorderColor = theme.GetValueForCurrentTheme<Color>();
            }
        }
    }

    private void SetCornerRadius(MaterialInputType type)
    {
        if (_cornerRadius.TryGetValue(type, out CornerRadius cornerRadius))
        {
            // Default Material value according to Type
            CornerRadius = cornerRadius;
        }
    }

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialInputBase.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabledState.Setters.Add(
            MaterialInputBase.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(
            MaterialInputBase.IconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(MaterialButton.ShadowProperty, null);

        disabledState.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        var focusedState = new VisualState { Name = ButtonCommonStates.Focused };
        focusedState.Setters.Add(
            MaterialInputBase.BackgroundColorProperty,
            Colors.Red);

        focusedState.Setters.Add(
            MaterialInputBase.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        focusedState.Setters.Add(MaterialInputBase.BorderWidthProperty, 2);
        focusedState.Setters.Add(MaterialInputBase.CornerRadiusProperty, new CornerRadius(16));

        commonStatesGroup.States.Add(new VisualState { Name = ButtonCommonStates.Normal });
        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(focusedState);

        var style = new Style(typeof(MaterialInputBase)) { ApplyToDerivedTypes = true };
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}