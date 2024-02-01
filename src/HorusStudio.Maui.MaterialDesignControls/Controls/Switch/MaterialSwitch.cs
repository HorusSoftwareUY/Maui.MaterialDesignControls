using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum SwitchTextSide
    {
        Right, Left
    }

    /// <summary>
    /// A switch <see cref="View" /> that allows the selection of an item on or off, and follows Material Design Guidelines <see href="https://m3.material.io/components/switch/overview" />.
    /// </summary>
    public class MaterialSwitch : ContentView
    {
        // TODO: Track color animation: change from on-track color to off-track color within the toggle animation
        // TODO: Disable color styles looks a bit weird with the opacities that the guideline specifies, we have to review them
        // TODO: FontAttributes and SupportingFontAttributes don't work

        #region Attributes

        private readonly static bool DefaultIsToggled = false;
        private readonly static bool DefaultIsEnabled = true;
        private readonly static Color DefaultTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultThumbColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();

#if IOS
        // A border width 4 on iOS looks the same as the border width 2 recommended by Material Design
        private readonly static double DefaultBorderWidth = 4;
#else
        private readonly static double DefaultBorderWidth = 2;
#endif

        private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
        private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
        private readonly static FontAttributes DefaultFontAttributes = FontAttributes.None;
        private readonly static TextAlignment DefaultHorizontalTextAlignment = TextAlignment.Start;
        private readonly static SwitchTextSide DefaultTextSide = SwitchTextSide.Left;
        private readonly static Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultSupportingFontSize = MaterialFontSize.BodySmall;
        private readonly static string DefaultSupportingFontFamily = MaterialFontFamily.Default;
        private readonly static FontAttributes DefaultSupportingFontAttributes = FontAttributes.None;
        private readonly static double DefaultSpacing = 16.0;
        private readonly static double DefaultTextSpacing = 4.0;

        private bool _isOnToggledState;
        private double _xReference;

        private readonly uint _toggleAnimationDuration = 150;

        private readonly double _trackHeight = 32;
        private readonly double _trackWidth = 52;

        private readonly double _thumbSelectedSize = 24;
        private readonly double _thumbUnselectedWithoutIconScale = 0.7;

        private bool _reduceThumbSize => UnselectedIcon == null;

        private const string SwitchAnimationName = "SwitchAnimation";

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="TrackColor"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialSwitch), DefaultTrackColor);

        /// <summary>
        /// The backing store for the <see cref="BorderColor"/> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSwitch), DefaultBorderColor);

        /// <summary>
        /// The backing store for the <see cref="BorderWidth"/> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialSwitch), DefaultBorderWidth, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetBorderWidth();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IsToggled"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(MaterialSwitch), DefaultIsToggled, BindingMode.TwoWay, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIsToggled((bool)n);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="ToggledCommand"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ToggledCommandProperty = BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(MaterialSwitch));

        /// <summary>
        /// The backing store for the <see cref="ThumbColor"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(MaterialSwitch), DefaultThumbColor);

        /// <summary>
        /// The backing store for the <see cref="SelectedIcon"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(nameof(SelectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIconSource();
                self.SetThumbSize();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="UnselectedIcon"/> bindable property.
        /// </summary>
        public static readonly BindableProperty UnselectedIconProperty = BindableProperty.Create(nameof(UnselectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetIconSource();
                self.SetThumbSize();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Text"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSwitch), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="TextColor"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialSwitch), defaultValue: DefaultTextColor);

        /// <summary>
        /// The backing store for the <see cref="FontSize"/> bindable property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultFontSize);

        /// <summary>
        /// The backing store for the <see cref="FontFamily"/> bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSwitch), defaultValue: DefaultFontFamily);

        /// <summary>
        /// The backing store for the <see cref="FontAttributes" /> bindable property.
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialSwitch), defaultValue: DefaultFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="HorizontalTextAlignment" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(MaterialSwitch), defaultValue: DefaultHorizontalTextAlignment);

        /// <summary>
        /// The backing store for the <see cref="TextSide"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(SwitchTextSide), typeof(MaterialSwitch), defaultValue: DefaultTextSide, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.UpdateLayoutAfterTextSideChanged((SwitchTextSide)n);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="SupportingText"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetSupportingText();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="SupportingTextColor"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(MaterialSwitch), defaultValue: DefaultSupportingTextColor);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontSize"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontSizeProperty = BindableProperty.Create(nameof(SupportingFontSize), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultSupportingFontSize);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontFamily"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontFamilyProperty = BindableProperty.Create(nameof(SupportingFontFamily), typeof(string), typeof(MaterialSwitch), defaultValue: DefaultSupportingFontFamily);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontAttributes" /> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontAttributesProperty = BindableProperty.Create(nameof(SupportingFontAttributes), typeof(FontAttributes), typeof(MaterialSwitch), defaultValue: DefaultSupportingFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="Spacing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultSpacing);

        /// <summary>
        /// The backing store for the <see cref="TextSpacing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextSpacingProperty = BindableProperty.Create(nameof(TextSpacing), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTextSpacing);

        /// <summary>
        /// The backing store for the <see cref="IsEnabled"/> bindable property.
        /// </summary>
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(VisualElement), defaultValue: DefaultIsEnabled, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetVisualState();
            }
        });

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets a color that describes the track color of the switch. This is a bindable property.
        /// </summary>
        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a color that describes the border stroke color of the switch. This is a bindable property.
        /// </summary>
        /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0.</remarks>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the border. This is a bindable property.
        /// </summary>
        /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets if switch is on 'On' state or on 'Off'.
        /// The default value is <see langword="false"/>.
        /// This is a bindable property.
        /// </summary>
        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the switch's IsToggled property changes. This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a switch. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.</remarks>
        public ICommand ToggledCommand
        {
            get => (ICommand)GetValue(ToggledCommandProperty);
            set => SetValue(ToggledCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets a color that describes the thumb color of the switch. This is a bindable property.
        /// </summary>
        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        /// <summary>
        /// Allows you to display a image on the switch's thumb when it is on the ON state. This is a bindable property.
        /// </summary>
        public ImageSource SelectedIcon
        {
            get { return (ImageSource)GetValue(SelectedIconProperty); }
            set { SetValue(SelectedIconProperty, value); }
        }

        /// <summary>
        /// Allows you to display a image on the switch's thumb when it is on the OFF state. This is a bindable property.
        /// </summary>
        public ImageSource UnselectedIcon
        {
            get { return (ImageSource)GetValue(UnselectedIconProperty); }
            set { SetValue(UnselectedIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text displayed next to the switch.
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the text of the switch. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font for the text of this switch. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the text of this switch. This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
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
        public SwitchTextSide TextSide
        {
            get { return (SwitchTextSide)GetValue(TextSideProperty); }
            set { SetValue(TextSideProperty, value); }
        }

        /// <summary>
        /// Gets or sets the supporting text displayed next to the switch and under the Text.
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string SupportingText
        {
            get { return (string)GetValue(SupportingTextProperty); }
            set { SetValue(SupportingTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the supporting text of the switch. This is a bindable property.
        /// </summary>
        public Color SupportingTextColor
        {
            get { return (Color)GetValue(SupportingTextColorProperty); }
            set { SetValue(SupportingTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font for the supporting text of this switch. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double SupportingFontSize
        {
            get { return (double)GetValue(SupportingFontSizeProperty); }
            set { SetValue(SupportingFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the supporting text of this switch. This is a bindable property.
        /// </summary>
        public string SupportingFontFamily
        {
            get { return (string)GetValue(SupportingFontFamilyProperty); }
            set { SetValue(SupportingFontFamilyProperty, value); }
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
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the spacing between the Text and SupportingText.
        /// This is a bindable property.
        /// </summary>
        public double TextSpacing
        {
            get { return (double)GetValue(TextSpacingProperty); }
            set { SetValue(TextSpacingProperty, value); }
        }

        /// <summary>
        /// Gets or sets if switch is on 'OnDisabled' state or 'OffDisabled'.
        /// The default value is <see langword="true"/>.
        /// This is a bindable property.
        /// </summary>
        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        #endregion Properties

        #region Events

        public event EventHandler<ToggledEventArgs> Toggled;

        #endregion Events

        #region Layout

        private Grid _mainContainer;
        private ColumnDefinition _leftColumnMainContainer;
        private ColumnDefinition _rightColumnMainContainer;
        private MaterialLabel _textLabel;
        private Grid _switch;
        private Border _track;
        private Border _thumb;
        private Image _icon;
        private MaterialLabel _supportingTextLabel;

        #endregion Layout

        #region Constructors

        public MaterialSwitch()
        {
            CreateLayout();

            if (!IsToggled)
            {
                GoToOffState(animate: false);
            }
        }

        #endregion Constructors

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
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

        private void CreateLayout()
        {
            _mainContainer = new Grid
            {
                RowSpacing = 0
            };
            _mainContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            _mainContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

            _leftColumnMainContainer = new ColumnDefinition(GridLength.Star);
            _rightColumnMainContainer = new ColumnDefinition(_trackWidth);
            _mainContainer.ColumnDefinitions.Add(_leftColumnMainContainer);
            _mainContainer.ColumnDefinitions.Add(_rightColumnMainContainer);

            _mainContainer.SetBinding(Grid.ColumnSpacingProperty, new Binding(nameof(Spacing), source: this));
            _mainContainer.SetBinding(Grid.RowSpacingProperty, new Binding(nameof(TextSpacing), source: this));

            _switch = new Grid
            {
                VerticalOptions = LayoutOptions.Center
            };

            _track = new Border
            {
                Padding = 0,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius((float)(_trackHeight / 2))
                },
                HeightRequest = _trackHeight,
                WidthRequest = _trackWidth,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _track.SetBinding(Border.BackgroundColorProperty, new Binding(nameof(TrackColor), source: this));
            _track.SetBinding(Border.StrokeProperty, new Binding(nameof(BorderColor), source: this));

            SetBorderWidth();

            _thumb = new Border
            {
                Padding = 0,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius((float)(_thumbSelectedSize / 2))
                },
                HeightRequest = _thumbSelectedSize,
                WidthRequest = _thumbSelectedSize,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _thumb.SetBinding(Microsoft.Maui.Controls.Frame.BackgroundColorProperty, new Binding(nameof(ThumbColor), source: this));

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

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                if (IsEnabled)
                {
                    IsToggled = !IsToggled;
                    Toggled?.Invoke(this, new ToggledEventArgs(IsToggled));
                    ToggledCommand?.Execute(IsToggled);
                }
            };

            var contentViewGesture = new ContentView();
            contentViewGesture.GestureRecognizers.Add(tapGestureRecognizer);
            _switch.Children.Add(contentViewGesture);

            _textLabel = new MaterialLabel
            {
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            _textLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
            _textLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _textLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _textLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            _textLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
            _textLabel.SetBinding(MaterialLabel.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));

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

            _mainContainer.Children.Add(_switch);
            _mainContainer.Children.Add(_textLabel);
            _mainContainer.Children.Add(_supportingTextLabel);

            UpdateLayoutAfterTextSideChanged(TextSide);

            _xReference = ((_track.WidthRequest - _thumb.WidthRequest) / 2) - 5;
            _thumb.TranslationX = !_isOnToggledState ? -_xReference : _xReference;

            Content = _mainContainer;
        }

        private void UpdateLayoutAfterTextSideChanged(SwitchTextSide textSide)
        {
            if (textSide == SwitchTextSide.Left)
            {
                _leftColumnMainContainer.Width = GridLength.Star;
                _rightColumnMainContainer.Width = _trackWidth;

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
                _leftColumnMainContainer.Width = _trackWidth;
                _rightColumnMainContainer.Width = GridLength.Star;

                Grid.SetRow(_textLabel, 0);
                Grid.SetColumn(_textLabel, 1);

                Grid.SetRow(_supportingTextLabel, 1);
                Grid.SetColumn(_supportingTextLabel, 1);

                Grid.SetRow(_switch, 0);
                Grid.SetColumn(_switch, 0);
                Grid.SetRowSpan(_switch, 2);
            }
        }

        private void SetSupportingText()
        {
            _supportingTextLabel.Text = SupportingText;
            _supportingTextLabel.IsVisible = !string.IsNullOrEmpty(SupportingText);
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
            if (animate && Math.Abs(_thumb.TranslationX + _xReference) > 0.0)
            {
                this.AbortAnimation(SwitchAnimationName);

                var animation = new Animation
                {
                    {0, 1, new Animation(v => _thumb.TranslationX = v, _thumb.TranslationX, -_xReference)}
                };

                if (_reduceThumbSize)
                {
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, 1, _thumbUnselectedWithoutIconScale));
                }

                animation.Commit(this, SwitchAnimationName, 16, _toggleAnimationDuration, null, (_, __) =>
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
            if (animate && Math.Abs(_thumb.TranslationX - _xReference) > 0.0)
            {
                this.AbortAnimation(SwitchAnimationName);

                IsToggled = true;
                var animation = new Animation
                {
                    {0, 1, new Animation(v => _thumb.TranslationX = v, _thumb.TranslationX, _xReference)}
                };

                if (_reduceThumbSize)
                {
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, _thumbUnselectedWithoutIconScale, 1));
                }

                animation.Commit(this, SwitchAnimationName, 16, _toggleAnimationDuration, null, (_, __) =>
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
            if (IsToggled)
            {
                _icon.Source = SelectedIcon;
                _icon.IsVisible = SelectedIcon != null;
            }
            else
            {
                _icon.Source = UnselectedIcon;
                _icon.IsVisible = !_reduceThumbSize;
            }
        }

        private void SetThumbSize()
        {
            if (!IsToggled && _reduceThumbSize)
            {
                _thumb.Scale = _thumbUnselectedWithoutIconScale;
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

        #region Styles

        internal static IEnumerable<Style> GetStyles()
        {
            var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

            var offState = new VisualState { Name = SwitchCommonStates.Off };
            offState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.SurfaceContainerHighest,
                    Dark = MaterialDarkTheme.SurfaceContainerHighest
                }
                .GetValueForCurrentTheme<Color>());
            offState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>());
            offState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>());

            var onState = new VisualState { Name = SwitchCommonStates.On };
            onState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Primary,
                    Dark = MaterialDarkTheme.Primary
                }
                .GetValueForCurrentTheme<Color>());
            onState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Primary,
                    Dark = MaterialDarkTheme.Primary
                }
                .GetValueForCurrentTheme<Color>());
            onState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnPrimary,
                    Dark = MaterialDarkTheme.OnPrimary
                }
                .GetValueForCurrentTheme<Color>());

            //var offDisabledState = new VisualState { Name = SwitchCommonStates.OffDisabled };
            //offDisabledState.Setters.Add(
            //    MaterialSwitch.TrackColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.SurfaceContainerHighest,
            //        Dark = MaterialDarkTheme.SurfaceContainerHighest
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.12f));
            //offDisabledState.Setters.Add(
            //    MaterialSwitch.BorderColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.OnSurface,
            //        Dark = MaterialDarkTheme.OnSurface
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.38f));
            //offDisabledState.Setters.Add(
            //    MaterialSwitch.ThumbColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.OnSurface,
            //        Dark = MaterialDarkTheme.OnSurface
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.38f));

            var offDisabledState = new VisualState { Name = SwitchCommonStates.OffDisabled };
            offDisabledState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.SurfaceContainerHighest,
                    Dark = MaterialDarkTheme.SurfaceContainerHighest
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(1f));
            offDisabledState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
            offDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            //var onDisabledState = new VisualState { Name = SwitchCommonStates.OnDisabled };
            //onDisabledState.Setters.Add(
            //    MaterialSwitch.TrackColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.OnSurface,
            //        Dark = MaterialDarkTheme.OnSurface
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.12f));
            //onDisabledState.Setters.Add(
            //    MaterialSwitch.BorderColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.OnSurface,
            //        Dark = MaterialDarkTheme.OnSurface
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.12f));
            //onDisabledState.Setters.Add(
            //    MaterialSwitch.ThumbColorProperty,
            //    new AppThemeBindingExtension
            //    {
            //        Light = MaterialLightTheme.Surface,
            //        Dark = MaterialDarkTheme.Surface
            //    }
            //    .GetValueForCurrentTheme<Color>()
            //    .WithAlpha(0.38f));

            var onDisabledState = new VisualState { Name = SwitchCommonStates.OnDisabled };
            onDisabledState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
            onDisabledState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Outline,
                    Dark = MaterialDarkTheme.Outline
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
            onDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.SurfaceContainerHighest,
                    Dark = MaterialDarkTheme.SurfaceContainerHighest
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(1f));

            commonStatesGroup.States.Add(new VisualState { Name = SwitchCommonStates.Normal });
            commonStatesGroup.States.Add(offState);
            commonStatesGroup.States.Add(onState);
            commonStatesGroup.States.Add(offDisabledState);
            commonStatesGroup.States.Add(onDisabledState);

            var style = new Style(typeof(MaterialSwitch));
            style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

            return new List<Style> { style };
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