using System.Runtime.CompilerServices;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using HorusStudio.Maui.MaterialDesignControls.Converters;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Switches allow the selection of an item on or off, and follow Material Design Guidelines. <see href="https://m3.material.io/components/switch/overview">See more</see>.
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSwitch.jpg</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt;material:MaterialSwitch
    ///         IsToggled="True"/&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var switch = new MaterialSwitch()
    /// {
    ///     IsToggled = True
    /// };
    ///</code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SwitchPage.xaml)
    /// 
    /// </example>
    /// <todoList>
    /// * Track color animation: change from on-track color to off-track color within the toggle animation.
    /// * [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)
    /// * The Selected property in Appium is not supported when using the AutomationId of this control, just like with the native MAUI control.
    /// </todoList>
    public class MaterialSwitch : ContentView, ITouchableView
    {
        #region Attributes

        private const bool DefaultIsToggled = false;
        private const bool DefaultIsEnabled = true;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTrackColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();

#if IOS || MACCATALYST
        private const double DefaultTrackWidthRequest = 52;
        private const double DefaultTrackHeightRequest = 32;
#elif ANDROID
        // Sizes recommended by Material Design are increased by 4 points due to how the border is rendered in Android
        private const double DefaultTrackWidthRequest = 56;
        private const double DefaultTrackHeightRequest = 36;
#endif

        private const double DefaultBorderWidth = 2;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultThumbColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface };
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBorderColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
        private const FontAttributes DefaultFontAttributes = FontAttributes.None;
        private const TextAlignment DefaultHorizontalTextAlignment = TextAlignment.Start;
        private const TextSide DefaultTextSide = TextSide.Left;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant };
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingFontSize = _ => MaterialFontSize.BodySmall;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultSupportingFontFamily = _ => MaterialFontFamily.Default;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;
        private const FontAttributes DefaultSupportingFontAttributes = FontAttributes.None;
        private const double DefaultSpacing = 16.0;
        private const double DefaultTextSpacing = 4.0;
        private const uint ToggleAnimationDuration = 150;
        private const double ThumbTrackSizeDifference = 8;
        private const double ThumbUnselectedWithoutIconScale = 0.7;
        private const string SwitchAnimationName = "SwitchAnimation";

        private bool _isOnToggledState;
        private double _xReference;
        private bool ReduceThumbSize => UnselectedIcon == null;

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="TrackColor">TrackColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialSwitch), defaultValueCreator: DefaultTrackColor);

        /// <summary>
        /// The backing store for the <see cref="TrackWidthRequest">TrackWidthRequest</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackWidthRequestProperty = BindableProperty.Create(nameof(TrackWidthRequest), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTrackWidthRequest, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetTrackAndThumbSizes();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrackHeightRequest">TrackHeightRequest</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackHeightRequestProperty = BindableProperty.Create(nameof(TrackHeightRequest), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTrackHeightRequest, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetTrackAndThumbSizes();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BorderColor">BorderColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSwitch), defaultValueCreator: DefaultBorderColor);

        /// <summary>
        /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialSwitch), DefaultBorderWidth, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetBorderWidth();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IsToggled">IsToggled</see> bindable property.
        /// </summary>
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(MaterialSwitch), DefaultIsToggled, BindingMode.TwoWay, propertyChanged: (bindable, _, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIsToggled((bool)n);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="ToggledCommand">ToggledCommand</see> bindable property.
        /// </summary>
        public static readonly BindableProperty ToggledCommandProperty = BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(MaterialSwitch));

        /// <summary>
        /// The backing store for the <see cref="ThumbColor">ThumbColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(MaterialSwitch), defaultValueCreator: DefaultThumbColor);

        /// <summary>
        /// The backing store for the <see cref="SelectedIcon">SelectedIcon</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(nameof(SelectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIconSource();
                self.SetThumbSize();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="UnselectedIcon">UnselectedIcon</see> bindable property.
        /// </summary>
        public static readonly BindableProperty UnselectedIconProperty = BindableProperty.Create(nameof(UnselectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIconSource();
                self.SetThumbSize();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Text">Text</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetText();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TextColor">TextColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialSwitch), defaultValueCreator: DefaultTextColor);

        /// <summary>
        /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSwitch), defaultValueCreator: DefaultFontSize);

        /// <summary>
        /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSwitch), defaultValueCreator: DefaultFontFamily);

        /// <summary>
        /// The backing store for the <see cref="FontAttributes">FontAttributes</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialSwitch), defaultValue: DefaultFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="HorizontalTextAlignment">HorizontalTextAlignment</see> bindable property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MaterialSwitch), defaultValue: DefaultHorizontalTextAlignment);

        /// <summary>
        /// The backing store for the <see cref="TextSide">TextSide</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(TextSide), typeof(MaterialSwitch), defaultValue: DefaultTextSide, propertyChanged: (bindable, _, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.UpdateLayoutAfterTextSideChanged((TextSide)n);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="SupportingText">SupportingText</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetSupportingText();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="SupportingTextColor">SupportingTextColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(MaterialSwitch), defaultValueCreator: DefaultSupportingTextColor);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontSize">SupportingFontSize</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontSizeProperty = BindableProperty.Create(nameof(SupportingFontSize), typeof(double), typeof(MaterialSwitch), defaultValueCreator: DefaultSupportingFontSize);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontFamily">SupportingFontFamily</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontFamilyProperty = BindableProperty.Create(nameof(SupportingFontFamily), typeof(string), typeof(MaterialSwitch), defaultValueCreator: DefaultSupportingFontFamily);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontAttributes">SupportingFontAttributes</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontAttributesProperty = BindableProperty.Create(nameof(SupportingFontAttributes), typeof(FontAttributes), typeof(MaterialSwitch), defaultValue: DefaultSupportingFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="Spacing">Spacing</see> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultSpacing);

        /// <summary>
        /// The backing store for the <see cref="TextSpacing">TextSpacing</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextSpacingProperty = BindableProperty.Create(nameof(TextSpacing), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTextSpacing);

        /// <summary>
        /// The backing store for the <see cref="IsEnabled">IsEnabled</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(VisualElement), defaultValue: DefaultIsEnabled, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetVisualState();
            }
        });

        // <summary>
        /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialSwitch), defaultValueCreator: DefaultTouchAnimationType);

        /// <summary>
        /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialSwitch));

        /// <summary>
        /// The backing store for the <see cref="AutomationId">AutomationId</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty AutomationIdProperty = BindableProperty.Create(nameof(AutomationId), typeof(string), typeof(MaterialSwitch), null);
        
        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets a color that describes the track color of the switch.
        /// This is a bindable property.
        /// </summary>
        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the desired width of the track.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>The default and minimum value is 52.</remarks>
        public double TrackWidthRequest
        {
            get => (double)GetValue(TrackWidthRequestProperty);
            set => SetValue(TrackWidthRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets the desired height of the track.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>The default and minimum value is 32.</remarks>
        public double TrackHeightRequest
        {
            get => (double)GetValue(TrackHeightRequestProperty);
            set => SetValue(TrackHeightRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets a color that describes the border stroke color of the switch.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</remarks>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets if switch is on 'On' state or on 'Off'.
        /// The default value is <see langword="false">false</see>.
        /// This is a bindable property.
        /// </summary>
        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the switch's IsToggled property changes.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>
        /// This property is used to associate a command with an instance of a switch.
        /// This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
        /// The command parameter is of type <see cref="bool">bool</see> and corresponds to the value of the <see cref="IsToggled">IsToggled</see> property.
        /// </remarks>
        public ICommand ToggledCommand
        {
            get => (ICommand)GetValue(ToggledCommandProperty);
            set => SetValue(ToggledCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets a color that describes the thumb color of the switch.
        /// This is a bindable property.
        /// </summary>
        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        /// <summary>
        /// Allows you to display a image on the switch's thumb when it is on the ON state.
        /// This is a bindable property.
        /// </summary>
        public ImageSource SelectedIcon
        {
            get => (ImageSource)GetValue(SelectedIconProperty);
            set => SetValue(SelectedIconProperty, value);
        }

        /// <summary>
        /// Allows you to display a image on the switch's thumb when it is on the OFF state.
        /// This is a bindable property.
        /// </summary>
        public ImageSource UnselectedIcon
        {
            get => (ImageSource)GetValue(UnselectedIconProperty);
            set => SetValue(UnselectedIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the text displayed next to the switch.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null">Null</see>
        /// </default>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color">color</see> for the text of the switch.
        /// This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the text of this switch.
        /// This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the text of this switch.
        /// This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font for the text of this switch is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates the horizontal alignment of the text and supporting text.
        /// This is a bindable property.
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        /// <summary>
        /// Determines if the Text and SupportingText are displayed to the right or left of the switch.
        /// The default value is Left. This is a bindable property.
        /// </summary>
        public TextSide TextSide
        {
            get => (TextSide)GetValue(TextSideProperty);
            set => SetValue(TextSideProperty, value);
        }

        /// <summary>
        /// Gets or sets the supporting text displayed next to the switch and under the Text.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null">Null</see>
        /// </default>
        public string SupportingText
        {
            get => (string)GetValue(SupportingTextProperty);
            set => SetValue(SupportingTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color">color</see> for the supporting text of the switch.
        /// This is a bindable property.
        /// </summary>
        public Color SupportingTextColor
        {
            get => (Color)GetValue(SupportingTextColorProperty);
            set => SetValue(SupportingTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the supporting text of this switch.
        /// This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double SupportingFontSize
        {
            get => (double)GetValue(SupportingFontSizeProperty);
            set => SetValue(SupportingFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the supporting text of this switch.
        /// This is a bindable property.
        /// </summary>
        public string SupportingFontFamily
        {
            get => (string)GetValue(SupportingFontFamilyProperty);
            set => SetValue(SupportingFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font for the supporting text of this switch is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        public FontAttributes SupportingFontAttributes
        {
            get => (FontAttributes)GetValue(SupportingFontAttributesProperty);
            set => SetValue(SupportingFontAttributesProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the switch and the texts (Text and SupportingText).
        /// This is a bindable property.
        /// </summary>
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the Text and SupportingText.
        /// This is a bindable property.
        /// </summary>
        public double TextSpacing
        {
            get => (double)GetValue(TextSpacingProperty);
            set => SetValue(TextSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets if switch is on 'OnDisabled' state or 'OffDisabled'.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="true">True</see>
        /// </default>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets an animation to be executed when element is clicked.
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
        /// Gets or sets a custom animation to be executed when element is clicked.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Null
        /// </default>
        public ITouchAnimation TouchAnimation
        {
            get => (ITouchAnimation)GetValue(TouchAnimationProperty);
            set => SetValue(TouchAnimationProperty, value);
        }
        
        /// <summary>
        /// Gets or sets a value that allows the automation framework to find and interact with this element.
        /// </summary>
        /// <remarks>
        /// This value may only be set once on an element.
        /// 
        /// When set on this control, the <see cref="AutomationId">AutomationId</see> is also used as a base identifier for its internal elements:
        /// - The <see cref="Switch">Switch</see> control uses the same <see cref="AutomationId">AutomationId</see> value.
        /// - The switch's text label uses the identifier "{AutomationId}_Text".
        /// - The supporting text label uses the identifier "{AutomationId}_SupportingText".
        /// 
        /// This convention allows automated tests and accessibility tools to consistently locate all subelements of the control.
        /// </remarks>
        public new string AutomationId
        {
            get => (string)GetValue(AutomationIdProperty);
            set => SetValue(AutomationIdProperty, value);
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Occurs when the switch is toggled.
        /// </summary>
        public event EventHandler<ToggledEventArgs>? Toggled;

        /// <summary>
        /// Occurs when the switch is touched.
        /// </summary>
        public event EventHandler<TouchEventArgs>? Touch;

        #endregion Events

        #region Layout

        private Grid _mainContainer = null!;
        private ColumnDefinition? _leftColumnMainContainer = null!;
        private ColumnDefinition? _rightColumnMainContainer = null!;
        private MaterialLabel? _textLabel = null!;
        private Grid _switch = null!;
        private Border? _track = null!;
        private Border? _thumb = null!;
        private Image? _icon = null!;
        private MaterialLabel? _supportingTextLabel = null!;

        #endregion Layout

        #region Constructors

        public MaterialSwitch()
        {
            this.SetAppTheme(TextColorProperty, ((AppThemeBindingExtension)DefaultTextColor.Invoke(this)).Light, ((AppThemeBindingExtension)DefaultTextColor.Invoke(this)).Dark);
            this.SetAppTheme(SupportingTextColorProperty, ((AppThemeBindingExtension)DefaultSupportingTextColor.Invoke(this)).Light, ((AppThemeBindingExtension)DefaultSupportingTextColor.Invoke(this)).Dark);

            CreateLayout();

            if (!IsToggled)
            {
                GoToOffState(animate: false);
            }
        }

        #endregion Constructors

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Window property is setted with a value when the view is appearing
            // So we force the visual state setting to apply the initial state styles here
            if (propertyName == nameof(Window)
                && Window != null)
            {
                SetVisualState();
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Content is not null)
            {
                SetInheritedBindingContext(Content, BindingContext);

                if (ReferenceEquals(Content.Parent, this) is false)
                {
                    Content.Parent = null;
                    Content.Parent = this;
                }
            }
        }

        private void CreateLayout()
        {
            HorizontalOptions = LayoutOptions.Center;

            _switch = new Grid
            {
                VerticalOptions = LayoutOptions.Center
            };
            _switch.SetBinding(Grid.AutomationIdProperty, new Binding(nameof(AutomationId), source: this));

            _track = new Border
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _track.SetBinding(Border.BackgroundColorProperty, new Binding(nameof(TrackColor), source: this));
            _track.SetBinding(Border.StrokeProperty, new Binding(nameof(BorderColor), source: this));

            SetBorderWidth();

            _thumb = new Border
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _thumb.SetBinding(Border.BackgroundColorProperty, new Binding(nameof(ThumbColor), source: this));

            SetTrackAndThumbSizes();

            _icon = new Image
            {
                IsVisible = false,
                WidthRequest = 16,
                HeightRequest = 16,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            _thumb.Content = _icon;

            _switch.Children.Add(_track);
            _switch.Children.Add(_thumb);

            Behaviors.Add(new TouchBehavior());

            Content = _switch;
        }

        private void CreateLayoutWithTexts()
        {
            HorizontalOptions = LayoutOptions.Fill;

            _mainContainer = new Grid
            {
                RowSpacing = 0
            };
            _mainContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            _mainContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

            var trackWidth = TrackWidthRequest >= DefaultTrackWidthRequest ? TrackWidthRequest : DefaultTrackWidthRequest;

            _leftColumnMainContainer = new ColumnDefinition(GridLength.Star);
            _rightColumnMainContainer = new ColumnDefinition(trackWidth);
            _mainContainer.ColumnDefinitions.Add(_leftColumnMainContainer);
            _mainContainer.ColumnDefinitions.Add(_rightColumnMainContainer);

            _mainContainer.SetBinding(Grid.ColumnSpacingProperty, new Binding(nameof(Spacing), source: this));
            _mainContainer.SetBinding(Grid.RowSpacingProperty, new Binding(nameof(TextSpacing), source: this));

            _textLabel = new MaterialLabel
            {
                IsVisible = false,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            _textLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _textLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _textLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            _textLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
            _textLabel.SetBinding(MaterialLabel.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
            _textLabel.SetBinding(MaterialLabel.AutomationIdProperty, new Binding(nameof(AutomationId), source: this, converter: new AutomationIdConverter(), converterParameter: "Text"));

            _supportingTextLabel = new MaterialLabel()
            {
                IsVisible = false,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            _supportingTextLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SupportingTextColor), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(SupportingFontFamily), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SupportingFontSize), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(SupportingFontAttributes), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.AutomationIdProperty, new Binding(nameof(AutomationId), source: this, converter: new AutomationIdConverter(), converterParameter: "SupportingText"));

            Content = null;
            _mainContainer.Children.Add(_switch);
            _mainContainer.Children.Add(_textLabel);
            _mainContainer.Children.Add(_supportingTextLabel);

            UpdateLayoutAfterTextSideChanged(TextSide);

            Content = _mainContainer;
        }

        private void UpdateLayoutAfterTextSideChanged(TextSide textSide)
        {
            if (_leftColumnMainContainer == null || _rightColumnMainContainer == null)
            {
                return;
            }

            var trackWidth = TrackWidthRequest >= DefaultTrackWidthRequest ? TrackWidthRequest : DefaultTrackWidthRequest;

            if (textSide == TextSide.Left)
            {
                _leftColumnMainContainer.Width = GridLength.Star;
                _rightColumnMainContainer.Width = trackWidth;

                Grid.SetRow(_textLabel, 0);
                Grid.SetColumn(_textLabel, 0);

                Grid.SetRow(_supportingTextLabel, 1);
                Grid.SetColumn(_supportingTextLabel, 0);

                Grid.SetRow(_switch, 0);
                Grid.SetColumn(_switch, 1);
                Grid.SetRowSpan(_switch, 2);
            }
            else
            {
                _leftColumnMainContainer.Width = trackWidth;
                _rightColumnMainContainer.Width = GridLength.Star;

                Grid.SetRow(_textLabel, 0);
                Grid.SetColumn(_textLabel, 1);

                Grid.SetRow(_supportingTextLabel, 1);
                Grid.SetColumn(_supportingTextLabel, 1);

                Grid.SetRow(_switch, 0);
                Grid.SetColumn(_switch, 0);
                Grid.SetRowSpan(_switch, 2);
            }
            Grid.SetRowSpan(_textLabel, _supportingTextLabel?.IsVisible ?? false ? 1 : 2);
        }

        private void SetTrackAndThumbSizes()
        {
            if (_track == null || _thumb == null)
            {
                return;
            }

            var trackWidth = TrackWidthRequest >= DefaultTrackWidthRequest ? TrackWidthRequest : DefaultTrackWidthRequest;
            var trackHeight = TrackHeightRequest >= DefaultTrackHeightRequest ? TrackHeightRequest : DefaultTrackHeightRequest;
            var thumbSelectedSize = trackHeight - ThumbTrackSizeDifference;

            _track.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius((float)(trackHeight / 2))
            };
            _track.HeightRequest = trackHeight;
            _track.WidthRequest = trackWidth;

            _thumb.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius((float)(thumbSelectedSize / 2))
            };
            _thumb.WidthRequest = thumbSelectedSize;
            _thumb.HeightRequest = thumbSelectedSize;

            _xReference = ((_track.WidthRequest - _thumb.WidthRequest) / 2) - 5;
            _thumb.TranslationX = !_isOnToggledState ? -_xReference : _xReference;

            if (_rightColumnMainContainer != null)
            {
                if (TextSide == TextSide.Left)
                {
                    _leftColumnMainContainer.Width = GridLength.Star;
                    _rightColumnMainContainer.Width = trackWidth;
                }
                else
                {
                    _leftColumnMainContainer.Width = trackWidth;
                    _rightColumnMainContainer.Width = GridLength.Star;
                }
            }
        }

        private void SetText()
        {
            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(SupportingText))
            {
                Content = _switch;
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                if (_textLabel == null)
                {
                    CreateLayoutWithTexts();
                }
                else
                {
                    Content = _mainContainer;
                }

                _textLabel!.Text = Text;
                _textLabel.IsVisible = !string.IsNullOrEmpty(Text);
            }
        }

        private void SetSupportingText()
        {
            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(SupportingText))
            {
                Content = _switch;
            }
            else if (!string.IsNullOrEmpty(SupportingText))
            {
                if (_supportingTextLabel == null)
                {
                    CreateLayoutWithTexts();
                }
                else
                {
                    Content = _mainContainer;
                }

                _supportingTextLabel!.Text = SupportingText;
                _supportingTextLabel.IsVisible = !string.IsNullOrEmpty(SupportingText);
            }

            Grid.SetRowSpan(_textLabel, _supportingTextLabel?.IsVisible ?? false ? 1 : 2);
        }

        private void SetBorderWidth()
        {
            if (_track != null)
            {
                _track.StrokeThickness = BorderWidth;
            }
        }

        private void SetIsToggled(bool isToggled)
        {
            if (isToggled && !_isOnToggledState)
            {
                GoToOnState(animate: Window != null);
            }
            else if (!isToggled && _isOnToggledState)
            {
                GoToOffState(animate: Window != null);
            }
        }

        private void GoToOffState(bool animate)
        {
            if (_thumb == null)
            {
                return;
            }

            if (animate && Math.Abs(_thumb.TranslationX + _xReference) > 0.0)
            {
                this.AbortAnimation(SwitchAnimationName);

                var animationManager = Application.Current?.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Animations.IAnimationManager>();
                if (animationManager is null) return;

                var animation = new Animation
                {
                    {0, 1, new Animation(v => _thumb.TranslationX = v, _thumb.TranslationX, -_xReference)}
                };

                if (ReduceThumbSize)
                {
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, 1, ThumbUnselectedWithoutIconScale));
                }

                animation.Commit(this, SwitchAnimationName, 16, ToggleAnimationDuration, null, (_, _) =>
                {
                    this.AbortAnimation(SwitchAnimationName);
                    _isOnToggledState = false;
                    IsToggled = false;
                    SetVisualState();
                });
            }
            else
            {
                _thumb.TranslationX = -_xReference;
                this.AbortAnimation(SwitchAnimationName);
                _isOnToggledState = false;
                IsToggled = false;
                SetVisualState();
            }

            SetIconSource();
            SetThumbSize();
        }

        private void GoToOnState(bool animate)
        {
            if (_thumb == null)
            {
                return;
            }

            if (animate && Math.Abs(_thumb.TranslationX - _xReference) > 0.0)
            {
                this.AbortAnimation(SwitchAnimationName);
                IsToggled = true;

                var animationManager = Application.Current?.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Animations.IAnimationManager>();
                if (animationManager is null) return;

                var animation = new Animation
                {
                    {0, 1, new Animation(v => _thumb.TranslationX = v, _thumb.TranslationX, _xReference)}
                };

                if (ReduceThumbSize)
                {
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, ThumbUnselectedWithoutIconScale));
                }

                animation.Commit(this, SwitchAnimationName, 16, ToggleAnimationDuration, null, (_, _) =>
                {
                    this.AbortAnimation(SwitchAnimationName);
                    _isOnToggledState = true;
                    IsToggled = true;
                    SetVisualState();
                });
            }
            else
            {
                _thumb.TranslationX = _xReference;
                this.AbortAnimation(SwitchAnimationName);
                _isOnToggledState = true;
                IsToggled = true;
                SetVisualState();
            }

            SetIconSource();
            SetThumbSize();
        }

        private void SetIconSource()
        {
            if (_icon == null)
            {
                return;
            }

            if (IsToggled)
            {
                _icon.Source = SelectedIcon;
                _icon.IsVisible = SelectedIcon != null;
            }
            else
            {
                _icon.Source = UnselectedIcon;
                _icon.IsVisible = !ReduceThumbSize;
            }
        }

        private void SetThumbSize()
        {
            if (_thumb == null)
            {
                return;
            }

            if (!IsToggled && ReduceThumbSize)
            {
                _thumb.Scale = ThumbUnselectedWithoutIconScale;
            }
            else
            {
                _thumb.Scale = 1;
            }
        }

        private void SetVisualState()
        {
            if (IsEnabled)
            {
                if (IsToggled)
                {
                    VisualStateManager.GoToState(this, SwitchCommonStates.On);
                }
                else
                {
                    VisualStateManager.GoToState(this, SwitchCommonStates.Off);
                }
            }
            else
            {
                if (IsToggled)
                {
                    VisualStateManager.GoToState(this, SwitchCommonStates.OnDisabled);
                }
                else
                {
                    VisualStateManager.GoToState(this, SwitchCommonStates.OffDisabled);
                }
            }
        }

        #endregion Methods

        #region ITouchable

        public async void OnTouch(TouchEventType gestureType)
        {
            Utils.Logger.Debug($"Gesture: {gestureType}");

            if (!IsEnabled) return;
            await TouchAnimationManager.AnimateAsync(this, gestureType);

            Touch?.Invoke(this, new TouchEventArgs(gestureType));

            switch (gestureType)
            {
                case TouchEventType.Pressed:
                    VisualStateManager.GoToState(this, ButtonCommonStates.Pressed);
                    break;
                case TouchEventType.Released:
                    IsToggled = !IsToggled;
                    Toggled?.Invoke(this, new ToggledEventArgs(IsToggled));
                    if (ToggledCommand?.CanExecute(IsToggled) == true)
                    {
                        ToggledCommand?.Execute(IsToggled);
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
            var resourceDictionary = new MaterialSwitchStyles();
            return resourceDictionary.Values.OfType<Style>();
        }

        #endregion Styles
    }

    public class SwitchCommonStates : VisualStateManager.CommonStates
    {
        public const string On = "On";

        public const string Off = "Off";

        public const string OnDisabled = "OnDisabled";

        public const string OffDisabled = "OffDisabled";
    }
}