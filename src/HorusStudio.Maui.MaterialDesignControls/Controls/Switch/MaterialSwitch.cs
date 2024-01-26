using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        // TODO: Track color animation is commented on because it produces an issue on track color when you change the IsEnabled and IsToggled values
        // TODO: Disable color styles looks a bit weird with the opacities that the guideline specifies, we have to review them

        #region Attributes

        private readonly static bool DefaultIsToggled = false;
        private readonly static bool DefaultIsEnabled = true;
        private readonly static Color DefaultTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultThumbColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultBorderWidth = 2;
        private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
        private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
        private readonly static SwitchTextSide DefaultTextSide = SwitchTextSide.Left;
        private readonly static Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultSupportingSize = MaterialFontSize.BodySmall;
        private readonly static string DefaultSupportingFontFamily = MaterialFontFamily.Default;
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
        /// The backing store for the <see cref="SupportingSize"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultSupportingSize);

        /// <summary>
        /// The backing store for the <see cref="SupportingFontFamily"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingFontFamilyProperty = BindableProperty.Create(nameof(SupportingFontFamily), typeof(string), typeof(MaterialSwitch), defaultValue: DefaultSupportingFontFamily);

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

        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        public ICommand ToggledCommand
        {
            get => (ICommand)GetValue(ToggledCommandProperty);
            set => SetValue(ToggledCommandProperty, value);
        }

        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        public ImageSource SelectedIcon
        {
            get { return (ImageSource)GetValue(SelectedIconProperty); }
            set { SetValue(SelectedIconProperty, value); }
        }

        public ImageSource UnselectedIcon
        {
            get { return (ImageSource)GetValue(UnselectedIconProperty); }
            set { SetValue(UnselectedIconProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public SwitchTextSide TextSide
        {
            get { return (SwitchTextSide)GetValue(TextSideProperty); }
            set { SetValue(TextSideProperty, value); }
        }

        public string SupportingText
        {
            get { return (string)GetValue(SupportingTextProperty); }
            set { SetValue(SupportingTextProperty, value); }
        }

        public Color SupportingTextColor
        {
            get { return (Color)GetValue(SupportingTextColorProperty); }
            set { SetValue(SupportingTextColorProperty, value); }
        }

        public double SupportingSize
        {
            get { return (double)GetValue(SupportingSizeProperty); }
            set { SetValue(SupportingSizeProperty, value); }
        }

        public string SupportingFontFamily
        {
            get { return (string)GetValue(SupportingFontFamilyProperty); }
            set { SetValue(SupportingFontFamilyProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public double TextSpacing
        {
            get { return (double)GetValue(TextSpacingProperty); }
            set { SetValue(TextSpacingProperty, value); }
        }

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
        private Frame _track;
        private Frame _trackInner;
        private Frame _thumb;
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

            _trackInner = new Frame
            {
                Padding = 0,
                HasShadow = false,
                BorderColor = Colors.Transparent,
                IsClippedToBounds = true
            };

            _trackInner.SetBinding(Microsoft.Maui.Controls.Frame.BackgroundColorProperty, new Binding(nameof(TrackColor), source: this));

            SetBorderWidth();

            _track = new Frame
            {
                Padding = 0,
                CornerRadius = (float)(_trackHeight / 2),
                HeightRequest = _trackHeight,
                WidthRequest = _trackWidth,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HasShadow = false,
                BorderColor = Colors.Transparent,
                IsClippedToBounds = true,
                Content = _trackInner
            };

            _track.SetBinding(Microsoft.Maui.Controls.Frame.BackgroundColorProperty, new Binding(nameof(BorderColor), source: this));

            _thumb = new Frame
            {
                Padding = 0,
                CornerRadius = (float)(_thumbSelectedSize / 2),
                HeightRequest = _thumbSelectedSize,
                WidthRequest = _thumbSelectedSize,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HasShadow = false,
                IsClippedToBounds = true
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

            _supportingTextLabel = new MaterialLabel()
            {
                IsVisible = false,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            _supportingTextLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SupportingTextColor), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(SupportingFontFamily), source: this));
            _supportingTextLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SupportingSize), source: this));

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
            if (_trackInner != null)
            {
                _trackInner.CornerRadius = (float)(_trackHeight / 2 - BorderWidth);
                _trackInner.HeightRequest = _trackHeight - 2 * BorderWidth;
                _trackInner.WidthRequest = _trackWidth - 2 * BorderWidth;
                _trackInner.Margin = BorderWidth;
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

                var changeTrackColorAnimation = GetChangeTrackColorAnimation(false);
                if (changeTrackColorAnimation != null)
                {
                    animation.Add(0, 1, changeTrackColorAnimation);
                }

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

                var changeTrackColorAnimation = GetChangeTrackColorAnimation(true);
                if (changeTrackColorAnimation != null)
                {
                    animation.Add(0, 1, changeTrackColorAnimation);
                }

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

        private Animation GetChangeTrackColorAnimation(bool animationToOnSate)
        {
            //string fromState;
            //string toState;

            //if (animationToOnSate)
            //{
            //    fromState = IsEnabled ? SwitchCommonStates.Off : SwitchCommonStates.OffDisabled;
            //    toState = IsEnabled ? SwitchCommonStates.On : SwitchCommonStates.OnDisabled;
            //}
            //else
            //{
            //    fromState = IsEnabled ? SwitchCommonStates.On : SwitchCommonStates.OnDisabled;
            //    toState = IsEnabled ? SwitchCommonStates.Off : SwitchCommonStates.OffDisabled;
            //}

            //var trackColorFromValue = this.GetVisualStatePropertyValue(nameof(CommonStates), fromState, TrackColorProperty.PropertyName);
            //var trackColorToValue = this.GetVisualStatePropertyValue(nameof(CommonStates), toState, TrackColorProperty.PropertyName);

            //if (trackColorFromValue is Color trackColorFrom && trackColorToValue is Color trackColorTo)
            //{
            //    return new Animation(v =>
            //    {
            //        TrackColor = trackColorFrom.AnimateTo(trackColorTo, v);
            //    }, 0, 0.1);
            //}
            //else
            //{
            //    return null;
            //}

            return null;
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