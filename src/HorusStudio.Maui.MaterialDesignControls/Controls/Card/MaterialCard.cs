using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialCardType
    {
        /// <summary>Elevated</summary>
        Elevated, 
        /// <summary>Filled</summary>
        Filled, 
        /// <summary>Outlined</summary>
        Outlined, 
        /// <summary>Custom</summary>
        Custom
    }

    /// <summary>
    /// Cards display content and actions about a single subject, and follow Material Design Guidelines. <see href="https://m3.material.io/components/cards/overview">See more</see>.
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialCard.jpg</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt;material:MaterialCard
    ///     Type="Elevated"&gt;
    ///     &lt;VerticalStackLayout
    ///         Spacing="8"&gt;
    ///         &lt;material:MaterialLabel
    ///             Type="TitleMedium"
    ///             Text="Elevated type"/&gt;
    ///         &lt;material:MaterialLabel
    ///             Text="Elevated cards provide separation from a patterned background."/&gt;
    ///     &lt;/VerticalStackLayout&gt;
    /// &lt;/material:MaterialCard/&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var label = new MaterialLabel()
    /// {
    ///     Text = "This a card."
    /// };
    /// 
    /// var vStack = new VerticalStackLayout()
    /// {
    ///     label
    /// };
    ///     
    /// var card = new MaterialCard()
    /// {
    ///     Type = MaterialCardType.Elevated,
    ///     Content = vStack
    /// };
    ///</code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/CardPage.xaml)
    ///
    /// </example>
    /// <todoList>
    ///  * Disable color styles looks a bit weird with the opacities that the guideline specifies, we have to review them.
    /// </todoList>
    public class MaterialCard : Border, ITouchableView
    {
        #region Attributes
        
        private const MaterialCardType DefaultCardType = MaterialCardType.Filled;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;
        private static readonly Color DefaultShadowColor = Color.FromRgba(1,1,1,.01);
        private static readonly Color DefaultBackgroundColor = Color.FromRgba(1,1,1,.01);
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(12);
        private const float DefaultBorderWidth = -1f;
        private static readonly Color DefaultBorderColor = Color.FromRgba(1,1,1,.01);
        private static readonly Shadow DefaultShadow = null!;
        private static readonly Thickness DefaultPadding = new Thickness(16);

        private readonly Dictionary<MaterialCardType, object> _backgroundColors = new()
        {
            { MaterialCardType.Elevated, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow } },
            { MaterialCardType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest } },
            { MaterialCardType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Surface, Dark = MaterialDarkTheme.Surface } },
            { MaterialCardType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest } }
        };

        private readonly Dictionary<MaterialCardType, Shadow> _shadows = new()
        {
            { MaterialCardType.Elevated, MaterialElevation.Level1 },
            { MaterialCardType.Custom, MaterialElevation.Level1 }
        };

        private readonly Dictionary<MaterialCardType, object> _borderColors = new()
        {
            { MaterialCardType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant } },
            { MaterialCardType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant } }
        };

        private readonly Dictionary<MaterialCardType, float> _borderWidths = new()
        {
            { MaterialCardType.Outlined, 1f },
            { MaterialCardType.Custom, 1f }
        };

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Type">Type</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialCardType), typeof(MaterialCard), defaultValue: DefaultCardType, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (bindable is MaterialCard self)
            {
                if (Enum.IsDefined(typeof(MaterialCardType), oldValue) &&
                    Enum.IsDefined(typeof(MaterialCardType), newValue) &&
                    (MaterialCardType)oldValue != (MaterialCardType)newValue)
                {
                    self.UpdateLayoutAfterTypeChanged((MaterialCardType)newValue);
                }
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Command">Command</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialCard), defaultValue: null, propertyChanged:
            (bindable, _, _) =>
            {
                var self = (MaterialCard)bindable;
                self.UpdateTouchBehavior();
            });

        /// <summary>
        /// The backing store for the <see cref="CommandParameter">CommandParameter</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialCard), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialCard), defaultValueCreator: DefaultTouchAnimationType);

        /// <summary>
        /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialCard), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="ShadowColor">ShadowColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultShadowColor, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetShadowColor();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBackgroundColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BorderColor">BorderColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultBorderColor, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBorderColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialCard), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetCornerRadius();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(MaterialCard), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBorderWidth(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Shadow">Shadow</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialCard), defaultValue: DefaultShadow, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetShadow(self.Type);
            }
        });

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the card <see cref="MaterialCardType">type</see>.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialCardType.Filled">MaterialCardType.Filled</see>
        /// </default>
        public MaterialCardType Type
        {
            get => (MaterialCardType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the card is clicked. This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a card. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</remarks>
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
        /// Gets or sets an animation to be executed when card is clicked.
        /// This is a bindable property.
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
        /// Gets or sets a custom animation to be executed when card is clicked.
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
        /// Gets or sets a color that describes the shadow color of the card.
        /// This is a bindable property.
        /// </summary>
        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
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
        /// Gets or sets a color that describes the border stroke color of the card.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</remarks>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius for the card, in device-independent units.
        /// This is a bindable property.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the border, in device-independent units.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
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

        private EventHandler? _clicked;
        private EventHandler? _pressed;
        private EventHandler? _released;
        private EventHandler<TouchEventArgs>? _touch;
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
                    UpdateTouchBehavior();
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _clicked -= value;
                    UpdateTouchBehavior();
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
                    UpdateTouchBehavior();
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _pressed -= value;
                    UpdateTouchBehavior();
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
                    UpdateTouchBehavior();
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _released -= value;
                    UpdateTouchBehavior();
                }
            }
        }
        
        /// <summary>
        /// Occurs when the card is touched.
        /// </summary>
        public event EventHandler<TouchEventArgs>? Touch
        {
            add
            {
                lock (_objectLock)
                {
                    _touch += value;
                    UpdateTouchBehavior();
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _touch -= value;
                    UpdateTouchBehavior();
                }
            }
        }

        #endregion Events

        #region Constructors

        public MaterialCard()
        {
            Padding = DefaultPadding;

            if (Type == DefaultCardType)
            {
                UpdateLayoutAfterTypeChanged(Type);
            }
        }

        #endregion Constructors

        #region Methods

        private void UpdateLayoutAfterTypeChanged(MaterialCardType type)
        {
            SetBackgroundColor(type);
            SetBorderColor(type);
            SetBorderWidth(type);
            SetCornerRadius();
            SetShadowColor();
            SetShadow(type);
        }
        
        private void SetBackgroundColor(MaterialCardType type)
        {
            if (_backgroundColors.TryGetValue(type, out object background) && background != null)
            {
                if (BackgroundColor is null && DefaultBackgroundColor is null || BackgroundColor!.Equals(DefaultBackgroundColor))
                {
                    // Default Material value according to Type
                    if (background is Color backgroundColor)
                    {
                        base.BackgroundColor = backgroundColor;
                    }
                    else if (background is AppThemeBindingExtension theme)
                    {
                        this.SetAppTheme(Border.BackgroundColorProperty, theme.Light, theme.Dark);
                    }
                }
                else
                {
                    // Set by user
                    base.BackgroundColor = BackgroundColor;
                }
            }
            else
            {
                // Unsupported for current card type, ignore
                base.BackgroundColor = DefaultBackgroundColor;
            }
        }

        private void SetBorderColor(MaterialCardType type)
        {
            if (_borderColors.TryGetValue(type, out object border) && border != null)
            {
                if ((BorderColor == null && DefaultBorderColor != null) || BorderColor.Equals(DefaultBorderColor))
                {
                    // Default Material value according to Type
                    if (border is Color borderColor)
                    {
                        Stroke = new SolidColorBrush(borderColor);
                    }
                    else if (border is AppThemeBindingExtension theme)
                    {
                        this.SetAppTheme(StrokeProperty, new SolidColorBrush((Color)theme.Light), new SolidColorBrush((Color)theme.Dark));
                    }
                }
                else
                {
                    // Set by user
                    Stroke = new SolidColorBrush(BorderColor);
                }
            }
            else
            {
                // Unsupported for current card type, ignore
                Stroke = new SolidColorBrush(DefaultBorderColor);
            }
        }

        private void SetBorderWidth(MaterialCardType type)
        {
            if (_borderWidths.TryGetValue(type, out float width))
            {
                if (BorderWidth.Equals(DefaultBorderWidth))
                {
                    // Default Material value according to Type
                    StrokeThickness = width;
                }
                else
                {
                    // Set by user
                    StrokeThickness = BorderWidth;
                }
            }
            else
            {
                // Unsupported for current card type, ignore
                StrokeThickness = DefaultBorderWidth;
            }
        }

        private void SetCornerRadius()
        {
            StrokeShape = new RoundRectangle
            {
                CornerRadius = CornerRadius
            };
        }

        private void SetShadowColor()
        {
            if (base.Shadow != null && !ShadowColor.Equals(DefaultShadowColor))
            {
                base.Shadow = new Shadow
                {
                    Brush = ShadowColor,
                    Radius = base.Shadow.Radius,
                    Opacity = ShadowColor != Colors.Transparent ? base.Shadow.Opacity : 0,
                    Offset = base.Shadow.Offset
                };
            }
        }

        private void SetShadow(MaterialCardType type)
        {
            if (_shadows.TryGetValue(type, out Shadow shadow))
            {
                if (base.Shadow == null && ((Shadow == null && DefaultShadow == null) || Shadow.Equals(DefaultShadow)))
                {
                    if (!ShadowColor.Equals(DefaultShadowColor))
                    {
                        shadow = new Shadow
                        {
                            Brush = ShadowColor,
                            Radius = shadow.Radius,
                            Opacity = shadow.Opacity,
                            Offset = shadow.Offset
                        };
                    }

                    // Default Material value according to Type
                    base.Shadow = shadow;
                }
                else
                {
                    // Set by user
                    base.Shadow = Shadow;
                }
            }
            else
            {
                // Unsupported for current card type, ignore
                base.Shadow = DefaultShadow;
            }
        }

        private void UpdateTouchBehavior()
        {
            var touchBehavior = Behaviors.FirstOrDefault(b => b is TouchBehavior) as TouchBehavior;
            
            if (Command != null || _clicked != null || _pressed != null || _released != null || _touch != null)
            {
                if (touchBehavior == null)
                {
                    Behaviors.Add(new TouchBehavior());
                }
            }
            else if (touchBehavior != null)
            {
                Behaviors.Remove(touchBehavior);
            }
        }

        #endregion Methods

        #region ITouchable

        public async void OnTouch(TouchEventType gestureType)
        {
            Utils.Logger.Debug($"Gesture: {gestureType}");

            if (!IsEnabled) return;
            await TouchAnimationManager.AnimateAsync(this, gestureType);
            
            _touch?.Invoke(this, new TouchEventArgs(gestureType));
            
            switch (gestureType)
            {
                case TouchEventType.Pressed:
                    _pressed?.Invoke(this, EventArgs.Empty);
                    VisualStateManager.GoToState(this, ButtonCommonStates.Pressed);
                    break;

                case TouchEventType.Released:
                    if (Command != null && Command.CanExecute(CommandParameter))
                    {
                        Command.Execute(CommandParameter);
                    }
                    if (_released != null)
                    {
                        _released.Invoke(this, EventArgs.Empty);
                    }
                    else if (_clicked != null)
                    {
                        _clicked.Invoke(this, EventArgs.Empty);
                    }

                    VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
                    break;
                default:
                    VisualStateManager.GoToState(this, ButtonCommonStates.Normal);
                    break;
            }
        }

        #endregion ITouchable

        #region Styles

        internal static IEnumerable<Style> GetStyles()
        {
            var resourceDictionary = new MaterialCardStyles();
            return resourceDictionary.Values.OfType<Style>();
        }

        #endregion Styles
    }
}