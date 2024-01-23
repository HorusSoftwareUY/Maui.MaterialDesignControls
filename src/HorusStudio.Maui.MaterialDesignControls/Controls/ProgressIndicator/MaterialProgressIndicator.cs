using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialProgressIndicatorType
    {
        Circular, Linear
    }

    /// <summary>
    /// A progress indicator <see cref="View" /> that show the status of a process and follows Material Design Guidelines.
    /// </summary>
    public class MaterialProgressIndicator : ContentView
    {
        #region Attributes and Properties

        private readonly static MaterialProgressIndicatorType DefaultProgressIndicatorType = MaterialProgressIndicatorType.Circular;
        private readonly static Color DefaultIndicatorColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultTrackColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultHeightRequest = -1;
        private readonly static double DefaultWidthRequest = -1;
        private readonly static int CircularThickness = 4;

        private readonly Dictionary<MaterialProgressIndicatorType, double> _controlDefaultWidths = new()
        {
            { MaterialProgressIndicatorType.Circular, 48 },
            { MaterialProgressIndicatorType.Linear, -1 }
        };

        private readonly Dictionary<MaterialProgressIndicatorType, double> _controlDefaultHeights = new()
        {
            { MaterialProgressIndicatorType.Circular, 48 },
            { MaterialProgressIndicatorType.Linear, 4 }
        };

        private const string LinearAnimationName = "LinearAnimation";

        #endregion Attributes and Properties

        #region Bindable properties

        /// <summary>
        /// The backing store for the <see cref="Type" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialProgressIndicatorType), typeof(MaterialProgressIndicator), defaultValue: DefaultProgressIndicatorType, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                if (Enum.IsDefined(typeof(MaterialProgressIndicatorType), oldValue) &&
                    Enum.IsDefined(typeof(MaterialProgressIndicatorType), newValue) &&
                    (MaterialProgressIndicatorType)oldValue != (MaterialProgressIndicatorType)newValue)
                {
                    self.UpdateLayoutAfterTypeChanged((MaterialProgressIndicatorType)newValue);
                }
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IndicatorColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValue: DefaultIndicatorColor, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetIndicatorColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrackColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValue: DefaultTrackColor, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetTrackColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IsVisible" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(MaterialProgressIndicator), defaultValue: true, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetIsVisible(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="HeightRequest" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialProgressIndicator), defaultValue: DefaultHeightRequest, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self
                && n is double newHeight && (o == null || (o is double oldHeight && !oldHeight.Equals(newHeight))))
            {
                self.SetHeightRequest(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="WidthRequest" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(MaterialProgressIndicator), defaultValue: DefaultWidthRequest, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self
                && n is double newWidth && (o == null || (o is double oldWidth && !oldWidth.Equals(newWidth))))
            {
                self.SetWidthRequest(self.Type);
            }
        });

        #endregion Bindable properties

        #region Properties

        /// <summary>
        /// Gets or sets the progress indicator type according to <see cref="MaterialProgressIndicatorType"/> enum.
        /// The default value is <see cref="MaterialProgressIndicatorType.Circular"/>. This is a bindable property.
        /// </summary>
        public MaterialProgressIndicatorType Type
        {
            get { return (MaterialProgressIndicatorType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the active indicator of the progress indicator. This is a bindable property.
        /// </summary>
        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the track of the progress indicator. This is a bindable property.
        /// </summary>
        /// <remarks>This property will not have an effect unless <see cref="MaterialProgressIndicator.Type" /> is set to <see cref="MaterialProgressIndicatorType.Linear"/>.</remarks>
        public Color TrackColor
        {
            get { return (Color)GetValue(TrackColorProperty); }
            set { SetValue(TrackColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets if progress indicator is visible.
        /// The default value is <see langword="true"/>.
        /// This is a bindable property.
        /// </summary>
        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets height of the progress indicator. This is a bindable property.
        /// </summary>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets width of the progress indicator. This is a bindable property.
        /// </summary>
        public new double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        #endregion Properties

        #region Layout

        private BoxView _progressBar;
        private CustomActivityIndicator _customActivityIndicator;

        #endregion Layout

        #region Constructors

        public MaterialProgressIndicator()
        {
            Padding = 0;

            if (Type == DefaultProgressIndicatorType)
            {
                UpdateLayoutAfterTypeChanged(Type);
            }
        }

        #endregion Constructors

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(Window)
                && Window == null)
            {
                // Window property is setted on null when the view is dissapearing
                // So we abort the linear or circular animation here
                if (Type == MaterialProgressIndicatorType.Circular
                    && _customActivityIndicator != null)
                {
                    _customActivityIndicator.IsRunning = false;
                }
                else if (Type == MaterialProgressIndicatorType.Linear)
                {
                    this.AbortAnimation(LinearAnimationName + Id);
                }
            }
        }

        public void UpdateLayoutAfterTypeChanged(MaterialProgressIndicatorType type)
        {
            switch (type)
            {
                case MaterialProgressIndicatorType.Linear:
                    _progressBar = new BoxView
                    {
                        Color = IndicatorColor,
                        IsEnabled = this.IsEnabled,
                        Margin = new Thickness(0),
                    };
                    Content = _progressBar;
                    BackgroundColor = TrackColor;
                    StartLinearAnimation();
                    break;
                case MaterialProgressIndicatorType.Circular:
                    BackgroundColor = Colors.Transparent;
                    _customActivityIndicator = new CustomActivityIndicator
                    {
                        IndicatorColor = IndicatorColor,
                        TrackColor = Colors.Transparent,
                        Thickness = CircularThickness
                    };
                    Content = _customActivityIndicator;
                    _customActivityIndicator.IsRunning = true;
                    break;
            }

            SetWidthRequest(type);
            SetHeightRequest(type);
        }

        private void SetIndicatorColor(MaterialProgressIndicatorType type)
        {
            if (type == MaterialProgressIndicatorType.Circular && _customActivityIndicator != null)
            {
                _customActivityIndicator.IndicatorColor = IndicatorColor;
            }
            else if (type == MaterialProgressIndicatorType.Linear && _progressBar != null)
            {
                _progressBar.Color = IndicatorColor;
            }
        }

        private void SetTrackColor(MaterialProgressIndicatorType type)
        {
            if (type == MaterialProgressIndicatorType.Linear)
            {
                BackgroundColor = TrackColor;
            }
        }

        private void SetIsVisible(MaterialProgressIndicatorType type)
        {
            base.IsVisible = IsVisible;

            if (type == MaterialProgressIndicatorType.Circular
                && _customActivityIndicator != null)
            {
                _customActivityIndicator.IsRunning = IsVisible;
            }
            else if (type == MaterialProgressIndicatorType.Linear)
            {
                // Handle linear animation
                if (IsVisible)
                {
                    StartLinearAnimation();
                }
                else
                {
                    this.AbortAnimation(LinearAnimationName + Id);
                }
            }
        }

        private void SetHeightRequest(MaterialProgressIndicatorType type)
        {
            var height = this.HeightRequest != DefaultHeightRequest ?
                    this.HeightRequest :
                    _controlDefaultHeights[type];

            base.HeightRequest = height;

            if (type == MaterialProgressIndicatorType.Circular)
            {
                _customActivityIndicator.Size = (int)height;
            }
        }

        private void SetWidthRequest(MaterialProgressIndicatorType type)
        {
            var width = this.WidthRequest != DefaultWidthRequest ?
                    this.WidthRequest :
                    _controlDefaultWidths[type];

            base.WidthRequest = width;

            if (type == MaterialProgressIndicatorType.Circular)
            {
                _customActivityIndicator.Size = (int)width;
            }
        }

        private void StartLinearAnimation()
        {
            var index = 1;
            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                if (index % 2 != 0)
                {
                    // Expanding boxview
                    _progressBar.Margin = new Thickness(0, 0, Width - (Width * v), 0);
                }
                else
                {
                    // Collapsing boxview
                    _progressBar.Margin = new Thickness(Width * v, 0, 0, 0);
                }
            }, 0, 1, Easing.CubicOut));
            mainAnimation.Commit(this, LinearAnimationName + Id, 16, 1500, Easing.Linear, (v, c) => ++index,
            () => Type == MaterialProgressIndicatorType.Linear && IsVisible);
        }

        #endregion Methods
    }
}