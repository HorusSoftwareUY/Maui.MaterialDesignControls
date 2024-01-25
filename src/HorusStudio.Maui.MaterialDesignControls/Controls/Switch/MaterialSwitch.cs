using System.Windows.Input;
using Maui.Graphics;
using static Microsoft.Maui.Controls.VisualStateManager;

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
        #region Attributes

        private readonly static bool DefaultIsToggled = false;
        private readonly static bool DefaultIsEnabled = true;
        private readonly static Color DefaultTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultThumbColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultBorderWidth = 2;
        private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
        private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
        private readonly static SwitchTextSide DefaultTextSide = SwitchTextSide.Left;
        private readonly static Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialDarkTheme.Error }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultSupportingSize = MaterialFontSize.BodySmall;
        private readonly static string DefaultSupportingFontFamily = MaterialFontFamily.Default;
        private readonly static Thickness DefaultSupportingMargin = new Thickness(14, 2, 14, 0);
        private readonly static bool DefaultAnimateError = MaterialAnimation.AnimateOnError;
        private readonly static LayoutOptions DefaultSwitchHorizontalOptions = LayoutOptions.End;
        private readonly static LayoutOptions DefaultTextHorizontalOptions = LayoutOptions.FillAndExpand;
        private readonly static double DefaultSpacing = 10.0;
        
        private bool _isOnToggledState;
        private double _xReference;

        private readonly uint _toggleAnimationDuration = 150;

        private readonly double _trackHeight = 32;
        private readonly double _trackWidth = 52;

        private readonly double _thumbSelectedSize = 24;
        private readonly double _thumbUnselectedWithoutIconSize = 16;

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
        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(nameof(SelectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="UnselectedIcon"/> bindable property.
        /// </summary>
        public static readonly BindableProperty UnselectedIconProperty = BindableProperty.Create(nameof(UnselectedIcon), typeof(ImageSource), typeof(MaterialSwitch), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="Text"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSwitch), defaultValue: string.Empty);

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
        /// The backing store for the <see cref="TextHorizontalOptions"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextHorizontalOptionsProperty = BindableProperty.Create(nameof(TextHorizontalOptions), typeof(LayoutOptions), typeof(MaterialSwitch), defaultValue: DefaultTextHorizontalOptions);

        /// <summary>
        /// The backing store for the <see cref="SupportingText"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetSupportingText();
            }
        }, validateValue: (bindable, v) =>
        {
            if (bindable is MaterialSwitch self)
            {
                // Used to animate the error when the assistive text doesn't change
                if (self.AnimateError && !string.IsNullOrEmpty(self.SupportingText) && self.SupportingText == (string)v)
                    _ = ShakeAnimation.AnimateAsync(self);
            }

            return true;
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
        /// The backing store for the <see cref="SupportingMargin"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SupportingMarginProperty = BindableProperty.Create(nameof(SupportingMargin), typeof(Thickness), typeof(MaterialSwitch), defaultValue: DefaultSupportingMargin);

        /// <summary>
        /// The backing store for the <see cref="AnimateError"/> bindable property.
        /// </summary>
        public static readonly BindableProperty AnimateErrorProperty = BindableProperty.Create(nameof(AnimateError), typeof(bool), typeof(MaterialSwitch), defaultValue: DefaultAnimateError);

        /// <summary>
        /// The backing store for the <see cref="SwitchHorizontalOptions"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SwitchHorizontalOptionsProperty = BindableProperty.Create(nameof(SwitchHorizontalOptions), typeof(LayoutOptions), typeof(MaterialSwitch), defaultValue: DefaultSwitchHorizontalOptions);

        /// <summary>
        /// The backing store for the <see cref="Spacing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultSpacing);

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

        public LayoutOptions TextHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(TextHorizontalOptionsProperty); }
            set { SetValue(TextHorizontalOptionsProperty, value); }
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

        public Thickness SupportingMargin
        {
            get { return (Thickness)GetValue(SupportingMarginProperty); }
            set { SetValue(SupportingMarginProperty, value); }
        }

        public bool AnimateError
        {
            get { return (bool)GetValue(AnimateErrorProperty); }
            set { SetValue(AnimateErrorProperty, value); }
        }

        public LayoutOptions SwitchHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(SwitchHorizontalOptionsProperty); }
            set { SetValue(SwitchHorizontalOptionsProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
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

        private StackLayout _container;
        private MaterialLabel _label;
        private Grid _switch;
        private Frame _track;
        private Frame _trackInner;
        private Frame _thumb;
        private Image _icon;
        private MaterialLabel _lblSupportingText;

        #endregion Layout

        #region Constructors

        public MaterialSwitch()
		{
            CreateLayout();

            if (!IsToggled)
            {
                GoToOffState();
            }
        }

        #endregion Constructors

        #region Methods

        private void CreateLayout()
        {
            var mainContainer = new StackLayout()
            {
                Spacing = 0,
                Margin = 0,
                Padding = 0
            };

            _container = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };

            _container.SetBinding(StackLayout.SpacingProperty, new Binding(nameof(Spacing), source: this));

            _switch = new Grid();

            _switch.SetBinding(Grid.HorizontalOptionsProperty, new Binding(nameof(SwitchHorizontalOptions), source: this));

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

            _label = new MaterialLabel
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
            _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            _label.SetBinding(MaterialLabel.HorizontalOptionsProperty, new Binding(nameof(TextHorizontalOptions), source: this));

            UpdateLayoutAfterTextSideChanged(TextSide);

            _lblSupportingText = new MaterialLabel()
            {
                IsVisible = false,
                LineBreakMode = LineBreakMode.NoWrap,
                HorizontalTextAlignment = TextAlignment.Start
            };
            _lblSupportingText.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SupportingTextColor), source: this));
            _lblSupportingText.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(SupportingFontFamily), source: this));
            _lblSupportingText.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SupportingSize), source: this));
            _lblSupportingText.SetBinding(MaterialLabel.MarginProperty, new Binding(nameof(SupportingMargin), source: this));

            mainContainer.Children.Add(_container);
            mainContainer.Children.Add(_lblSupportingText);

            _xReference = ((_track.WidthRequest - _thumb.WidthRequest) / 2) - 5;
            _thumb.TranslationX = !_isOnToggledState ? -_xReference : _xReference;

            Content = mainContainer;
        }

        private void UpdateLayoutAfterTextSideChanged(SwitchTextSide textSide)
        {
            if (_container != null)
            {
                _container.Children.Clear();
                if (textSide == SwitchTextSide.Left)
                {
                    _container.Children.Add(_label);
                    _container.Children.Add(_switch);
                }
                else
                {
                    _container.Children.Add(_switch);
                    _container.Children.Add(_label);
                }
            }
        }

        private void SetSupportingText()
        {
            _lblSupportingText.Text = SupportingText;
            _lblSupportingText.IsVisible = !string.IsNullOrEmpty(SupportingText);
            if (AnimateError && !string.IsNullOrEmpty(SupportingText))
                _ = ShakeAnimation.AnimateAsync(this);
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
                GoToOnState();
            }
            else if (!isToggled && _isOnToggledState)
            {
                GoToOffState();
            }
        }

        private void GoToOffState()
        {
            if (Math.Abs(_thumb.TranslationX + _xReference) > 0.0)
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
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, 1, 0.7));
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
                this.AbortAnimation(SwitchAnimationName);
                _isOnToggledState = false;
                IsToggled = false;
                SetVisualState();
            }

            if (_reduceThumbSize)
            {
                _icon.IsVisible = false;
            }
            else
            {
                _thumb.Scale = 1;
                SetUnselectedIconSource();
            }
        }

        private void GoToOnState()
        {
            if (Math.Abs(_thumb.TranslationX - _xReference) > 0.0)
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
                    animation.Add(0, 1, new Animation(v => _thumb.Scale = v, 0.7, 1));
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
                this.AbortAnimation(SwitchAnimationName);
                _isOnToggledState = true;
                IsToggled = true;
                SetVisualState();
            }

            _icon.IsVisible = true;
            _thumb.Scale = 1;
            SetSelectedIconSource();
        }

        private Animation GetChangeTrackColorAnimation(bool animationToOnSate)
        {
            string fromState;
            string toState;

            if (animationToOnSate)
            {
                fromState = IsEnabled ? SwitchCommonStates.Off : SwitchCommonStates.OffDisabled;
                toState = IsEnabled ? SwitchCommonStates.On : SwitchCommonStates.OnDisabled;
            }
            else
            {
                fromState = IsEnabled ? SwitchCommonStates.On : SwitchCommonStates.OnDisabled;
                toState = IsEnabled ? SwitchCommonStates.Off : SwitchCommonStates.OffDisabled;
            }

            var trackColorFromValue = this.GetVisualStatePropertyValue(nameof(CommonStates), fromState, TrackColorProperty.PropertyName);
            var trackColorToValue = this.GetVisualStatePropertyValue(nameof(CommonStates), toState, TrackColorProperty.PropertyName);

            if (trackColorFromValue is Color trackColorFrom && trackColorToValue is Color trackColorTo)
            {
                return new Animation(v =>
                {
                    TrackColor = trackColorFrom.AnimateTo(trackColorTo, v);
                }, 0, 1);
            }
            else
            {
                return null;
            }    
        }

        private void SetUnselectedIconSource()
        {
            _icon.Source = UnselectedIcon;
            _icon.IsVisible = !_reduceThumbSize && !IsToggled;
        }

        private void SetSelectedIconSource()
        {
            _icon.Source = SelectedIcon;
            _icon.IsVisible = IsToggled;
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
            offState.Setters.Add(
                MaterialSwitch.TextColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Text,
                    Dark = MaterialDarkTheme.Text
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
            onState.Setters.Add(
                MaterialSwitch.TextColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Text,
                    Dark = MaterialDarkTheme.Text
                }
                .GetValueForCurrentTheme<Color>());

            var offDisabledState = new VisualState { Name = SwitchCommonStates.OffDisabled };
            offDisabledState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.SurfaceContainerHighest,
                    Dark = MaterialDarkTheme.SurfaceContainerHighest
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
            offDisabledState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
            offDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            // TODO: IT SHOULD BE THE SAME DISABLE COLOR OF THE BASE INPUT LABEL COLOR
            offDisabledState.Setters.Add(
                MaterialSwitch.TextColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(1f));

            var onDisabledState = new VisualState { Name = SwitchCommonStates.OnDisabled };
            onDisabledState.Setters.Add(
                MaterialSwitch.TrackColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
            onDisabledState.Setters.Add(
                MaterialSwitch.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
            onDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Surface,
                    Dark = MaterialDarkTheme.Surface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            // TODO: IT SHOULD BE THE SAME DISABLE COLOR OF THE BASE INPUT LABEL COLOR
            onDisabledState.Setters.Add(
                MaterialSwitch.TextColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
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