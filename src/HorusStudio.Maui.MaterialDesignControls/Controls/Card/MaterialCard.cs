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
    /// A card <see cref="View" /> that display content and actions about a single subject, and follows Material Design Guidelines <see href="https://m3.material.io/components/cards/overview">See here</see>.
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
    /// <list type="list">
    ///         <item>
    ///             <term></term>
    ///             <description>Disable color styles looks a bit weird with the opacities that the guideline specifies, we have to review them</description>
    ///         </item>
    ///     </list>
    /// </todoList>
    public class MaterialCard : Border, ITouchable
    {
        #region Attributes

        private readonly static MaterialCardType DefaultCardType = MaterialCardType.Filled;
        private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
#nullable enable
        private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable
        private readonly static Color DefaultShadowColor = Colors.Transparent;
        private readonly static Color DefaultBackgroundColor = Colors.Transparent;
        private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(12);
        private readonly static float DefaultBorderWidth = 0f;
        private readonly static Color DefaultBorderColor = Colors.Transparent;
        private readonly static Shadow DefaultShadow = null;

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
        /// The backing store for the <see cref="Type" /> bindable property.
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
        /// The backing store for the <see cref="Command" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialCard), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="CommandParameter" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialCard), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="Animation" /> bindable property.
        /// </summary>
        public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialCard), defaultValue: DefaultAnimationType);

        /// <summary>
        /// The backing store for the <see cref="AnimationParameter" /> bindable property.
        /// </summary>
        public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialCard), defaultValue: DefaultAnimationParameter);

        /// <summary>
        /// The backing store for the <see cref="CustomAnimation" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialCard), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="ShadowColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultShadowColor, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetShadowColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BackgroundColor" /> bindable property.
        /// </summary>
        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBackgroundColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BorderColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialCard), defaultValue: DefaultBorderColor, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBorderColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="CornerRadius" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialCard), defaultValue: DefaultCornerRadius, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetCornerRadius(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BorderWidth" /> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(MaterialCard), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetBorderWidth(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Shadow" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialCard), defaultValue: DefaultShadow, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialCard self)
            {
                self.SetShadow(self.Type);
            }
        });

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the card type according to <see cref="MaterialCardType"/> enum.
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

        /// <summary>
        /// Gets or sets an animation to be executed when card is clicked.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="AnimationTypes.Fade">AnimationTypes.Fade</see>
        /// </default>
        public AnimationTypes Animation
        {
            get => (AnimationTypes)GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
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
        /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0.</remarks>
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

        public MaterialCard()
        {
            Padding = new Thickness(16);

            Behaviors.Add(new TouchBehavior());

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
            SetCornerRadius(type);
            SetShadowColor(type);
            SetShadow(type);
        }
        
        private void SetBackgroundColor(MaterialCardType type)
        {
            if (_backgroundColors.TryGetValue(type, out object background) && background != null)
            {
                if ((BackgroundColor == null && DefaultBackgroundColor == null) || BackgroundColor.Equals(DefaultBackgroundColor))
                {
                    // Default Material value according to Type
                    if (background is Color backgroundColor)
                    {
                        base.BackgroundColor = backgroundColor;
                    }
                    else if (background is AppThemeBindingExtension theme)
                    {
                        base.BackgroundColor = theme.GetValueForCurrentTheme<Color>();
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
                        Stroke = new SolidColorBrush(theme.GetValueForCurrentTheme<Color>());
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

        private void SetCornerRadius(MaterialCardType type)
        {
            StrokeShape = new RoundRectangle
            {
                CornerRadius = CornerRadius
            };
        }

        private void SetShadowColor(MaterialCardType type)
        {
            if (base.Shadow != null && !ShadowColor.Equals(DefaultShadowColor))
            {
                base.Shadow = new Shadow
                {
                    Brush = ShadowColor,
                    Radius = base.Shadow.Radius,
                    Opacity = base.Shadow.Opacity,
                    Offset = base.Shadow.Offset
                };
            }
        }

        private void SetShadow(MaterialCardType type)
        {
            if (_shadows.TryGetValue(type, out Shadow shadow))
            {
                if ((Shadow == null && DefaultShadow == null) || Shadow.Equals(DefaultShadow))
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

        #endregion Methods

        #region ITouchable

        public async void OnTouch(TouchType gestureType)
        {
            if (!IsEnabled || (Command == null && _pressed == null && _released == null && _clicked == null))
                return;

            await TouchAnimation.AnimateAsync(this, gestureType);

            switch (gestureType)
            {
                case TouchType.Pressed:
                    _pressed?.Invoke(this, null);
                    VisualStateManager.GoToState(this, ButtonCommonStates.Pressed);
                    break;

                case TouchType.Released:
                    if (Command != null && Command.CanExecute(CommandParameter))
                    {
                        Command.Execute(CommandParameter);
                    }
                    else if (_released != null)
                    {
                        _released.Invoke(this, null);
                    }
                    else if (_clicked != null)
                    {
                        _clicked.Invoke(this, null);
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
            var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

            var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
            disabledState.Setters.Add(
                MaterialCard.BackgroundColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Surface,
                    Dark = MaterialDarkTheme.Surface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            disabledState.Setters.Add(MaterialCard.ShadowProperty, null);

            disabledState.Setters.Add(
                MaterialCard.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Surface,
                    Dark = MaterialDarkTheme.Surface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            //disabledState.Setters.Add(MaterialCard.OpacityProperty, 0.38f);

            var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };
            //pressedState.Setters.Add(MaterialCard.OpacityProperty, 1f);

            var normalState = new VisualState { Name = ButtonCommonStates.Normal };
            //normalState.Setters.Add(MaterialCard.OpacityProperty, 1f);

            commonStatesGroup.States.Add(normalState);
            commonStatesGroup.States.Add(disabledState);
            commonStatesGroup.States.Add(pressedState);

            var style = new Style(typeof(MaterialCard));
            style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

            return new List<Style> { style };
        }

        #endregion Styles
    }
}