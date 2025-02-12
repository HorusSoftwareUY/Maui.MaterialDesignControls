using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// A switch <see cref="View" /> that allows the selection of an item on or off, and follows Material Design Guidelines <see href="https://m3.material.io/components/switch/overview" />.
    /// </summary>
    public class MaterialSwitch : ContentView
    {
        // TODO: Track color animation: change from on-track color to off-track color within the toggle animation
        // TODO: [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)

        #region Attributes

        private static readonly bool DefaultIsToggled = false;
        private static readonly bool DefaultIsEnabled = true;
        private static readonly Color DefaultTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();

#if IOS || MACCATALYST
        private static readonly double DefaultTrackWidthRequest = 52;
        private static readonly double DefaultTrackHeightRequest = 32;
#elif ANDROID
        // Sizes recommended by Material Design are increased by 4 points due to how the border is rendered in Android
        private static readonly double DefaultTrackWidthRequest = 56;
        private static readonly double DefaultTrackHeightRequest = 36;
#endif
        
        private static readonly double DefaultBorderWidth = 2;
        private static readonly Color DefaultThumbColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private static readonly Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
        private static readonly Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
        private static readonly double DefaultFontSize = MaterialFontSize.BodyLarge;
        private static readonly string DefaultFontFamily = MaterialFontFamily.Default;
        private static readonly FontAttributes DefaultFontAttributes = FontAttributes.None;
        private static readonly TextAlignment DefaultHorizontalTextAlignment = TextAlignment.Start;
        private static readonly TextSide DefaultTextSide = TextSide.Left;
        private static readonly Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
        private static readonly double DefaultSupportingFontSize = MaterialFontSize.BodySmall;
        private static readonly string DefaultSupportingFontFamily = MaterialFontFamily.Default;
        private static readonly FontAttributes DefaultSupportingFontAttributes = FontAttributes.None;
        private static readonly double DefaultSpacing = 16.0;
        private static readonly double DefaultTextSpacing = 4.0;

        private bool _isOnToggledState;
        private double _xReference;

        private readonly uint _toggleAnimationDuration = 150;

        private readonly double _thumbTrackSizeDifference = 8;
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
        /// The backing store for the <see cref="TrackWidthRequest" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackWidthRequestProperty = BindableProperty.Create(nameof(TrackWidthRequest), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTrackWidthRequest, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetTrackAndThumbSizes();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrackHeightRequest" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackHeightRequestProperty = BindableProperty.Create(nameof(TrackHeightRequest), typeof(double), typeof(MaterialSwitch), defaultValue: DefaultTrackHeightRequest, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetTrackAndThumbSizes();
            }
        });

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
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSwitch), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.SetText();
            }
        });

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
        public static readonly BindableProperty TextSideProperty = BindableProperty.Create(nameof(TextSide), typeof(TextSide), typeof(MaterialSwitch), defaultValue: DefaultTextSide, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialSwitch self)
            {
                self.UpdateLayoutAfterTextSideChanged((TextSide)n);
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
        /// Gets or sets the desired width of the track. This is a bindable property.
        /// </summary>
        /// <remarks>The default and minimum value is 52.</remarks>
        public double TrackWidthRequest
        {
            get => (double)GetValue(TrackWidthRequestProperty);
            set => SetValue(TrackWidthRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets the desired height of the track. This is a bindable property.
        /// </summary>
        /// <remarks>The default and minimum value is 32.</remarks>
        public double TrackHeightRequest
        {
            get => (double)GetValue(TrackHeightRequestProperty);
            set => SetValue(TrackHeightRequestProperty, value);
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
            get => (ImageSource)GetValue(SelectedIconProperty);
            set => SetValue(SelectedIconProperty, value);
        }

        /// <summary>
        /// Allows you to display a image on the switch's thumb when it is on the OFF state. This is a bindable property.
        /// </summary>
        public ImageSource UnselectedIcon
        {
            get => (ImageSource)GetValue(UnselectedIconProperty);
            set => SetValue(UnselectedIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the text displayed next to the switch.
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the text of the switch. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the text of this switch. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the text of this switch. This is a bindable property.
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
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string SupportingText
        {
            get => (string)GetValue(SupportingTextProperty);
            set => SetValue(SupportingTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the supporting text of the switch. This is a bindable property.
        /// </summary>
        public Color SupportingTextColor
        {
            get => (Color)GetValue(SupportingTextColorProperty);
            set => SetValue(SupportingTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the supporting text of this switch. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double SupportingFontSize
        {
            get => (double)GetValue(SupportingFontSizeProperty);
            set => SetValue(SupportingFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the supporting text of this switch. This is a bindable property.
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
        /// The default value is <see langword="true"/>.
        /// This is a bindable property.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
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

            _thumb.SetBinding(Microsoft.Maui.Controls.Frame.BackgroundColorProperty, new Binding(nameof(ThumbColor), source: this));

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

            Content = null;
            _mainContainer.Children.Add(_switch);
            _mainContainer.Children.Add(_textLabel);
            _mainContainer.Children.Add(_supportingTextLabel);

            UpdateLayoutAfterTextSideChanged(TextSide);

            Content = _mainContainer;
        }

        private void UpdateLayoutAfterTextSideChanged(TextSide textSide)
        {
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
            var trackWidth = TrackWidthRequest >= DefaultTrackWidthRequest ? TrackWidthRequest : DefaultTrackWidthRequest;
            var trackHeight = TrackHeightRequest >= DefaultTrackHeightRequest ? TrackHeightRequest : DefaultTrackHeightRequest;
            var thumbSelectedSize = trackHeight - _thumbTrackSizeDifference;

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

                _textLabel.Text = Text;
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

                _supportingTextLabel.Text = SupportingText;
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
            if (animate && Math.Abs(_thumb.TranslationX + _xReference) > 0.0)
            {
                this.AbortAnimation(SwitchAnimationName);
                
                var animationManager = Application.Current?.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Animations.IAnimationManager>();
                if (animationManager is null) return;
                
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
                
                var animationManager = Application.Current?.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Animations.IAnimationManager>();
                if (animationManager is null) return;
                
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
                .WithAlpha(0.12f));
            offDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

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
                Colors.Transparent);
            onDisabledState.Setters.Add(
                MaterialSwitch.ThumbColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.Surface,
                    Dark = MaterialDarkTheme.Surface
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