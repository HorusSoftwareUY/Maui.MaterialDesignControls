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
        private readonly static Color DefaultIndicatorColor = MaterialLightTheme.Primary;
        private readonly static Color DefaultTrackColor = MaterialLightTheme.SurfaceContainerHighest;

        private const string LinearAnimationName = "LinearAnimation";

        private const string CircularAnimationName = "CircularAnimation";

        private const int CircleAnimationMinimumProgress = 12;

        private const int CircleAnimationMaximumProgress = 96;

        private bool _rendered = false;

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

        #endregion Properties

        #region Layout

        private BoxView _progressBar;
        private ActivityIndicator _activityIndicator;
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
            if (propertyName == "Renderer")
            {
                if (!_rendered)
                {
                    _rendered = true;
                }
                else
                {
                    // This property is setted on the view appearing and in the view dissapearing
                    // So we abort the linear or circular animation here
                    if (Type == MaterialProgressIndicatorType.Circular
                        && _customActivityIndicator != null)
                    {
                        this.AbortAnimation(CircularAnimationName + Id);
                    }
                    else if (Type == MaterialProgressIndicatorType.Linear)
                    {
                        this.AbortAnimation(LinearAnimationName + Id);
                    }
                }
            }
        }

        public void UpdateLayoutAfterTypeChanged(MaterialProgressIndicatorType type)
        {
            switch (type)
            {
                case MaterialProgressIndicatorType.Linear:
                    HeightRequest = 4;
                    WidthRequest = -1;
                    _progressBar = new BoxView()
                    {
                        Color = IndicatorColor,
                        IsEnabled = this.IsEnabled,
                        Margin = new Thickness(0),
                    };
                    this.Content = _progressBar;
                    this.BackgroundColor = TrackColor;
                    StartLinearAnimation();
                    break;
                case MaterialProgressIndicatorType.Circular:
                    HeightRequest = 48;
                    WidthRequest = 48;
                    this.BackgroundColor = Colors.Transparent;

                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        _activityIndicator = new ActivityIndicator()
                        {
                            Color = IndicatorColor,
                            IsRunning = true
                        };
                        this.Content = _activityIndicator;
                    }
                    else
                    {
                        _customActivityIndicator = new CustomActivityIndicator()
                        {
                            IndicatorColor = IndicatorColor,
                            TrackColor = Colors.Transparent,
                            Size = 48,
                            Thickness = 4
                        };
                        this.Content = _customActivityIndicator;
                        StartCustomCircleAnimation();
                    }
                    break;
            }
        }

        private void SetIndicatorColor(MaterialProgressIndicatorType type)
        {
            if (type == MaterialProgressIndicatorType.Circular)
            {
                if (_activityIndicator != null)
                {
                    _activityIndicator.Color = IndicatorColor;
                }
                else if (_customActivityIndicator != null)
                {
                    _customActivityIndicator.IndicatorColor = IndicatorColor;
                }
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
                this.BackgroundColor = TrackColor;
            }
        }

        private void SetIsVisible(MaterialProgressIndicatorType type)
        {
            base.IsVisible = IsVisible;

            if (type == MaterialProgressIndicatorType.Circular)
            {
                if (_activityIndicator != null)
                {
                    _activityIndicator.IsRunning = IsVisible;
                }
                else if (_customActivityIndicator != null)
                {
                    // Handle circular animation
                    if (IsVisible)
                    {
                        StartCustomCircleAnimation();
                    }
                    else
                    {
                        this.AbortAnimation(CircularAnimationName + Id);
                    }
                }
            }
            else
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

        private void StartCustomCircleAnimation()
        {
            _customActivityIndicator.Progress = CircleAnimationMinimumProgress;
            CustomCircleAnimationA();
        }

        private void CustomCircleAnimationA()
        {
            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                _customActivityIndicator.Rotation = v;
            }, 0, 360, Easing.SinIn));
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                _customActivityIndicator.Progress = (int)v;
            }, CircleAnimationMinimumProgress, CircleAnimationMaximumProgress, Easing.SinIn));
            mainAnimation.Commit(this, CircularAnimationName + Id, 16, 1000, Easing.Linear,
            (v, c) =>
            {
                if (Type == MaterialProgressIndicatorType.Circular && IsVisible)
                {
                    CustomCircleAnimationB();
                }
            },
            () => false);
        }

        private void CustomCircleAnimationB()
        {
            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                _customActivityIndicator.Rotation = v;
            }, 0, 360, Easing.SinOut));
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                _customActivityIndicator.Progress = (int)v;
            }, CircleAnimationMaximumProgress, CircleAnimationMinimumProgress, Easing.SinOut));
            mainAnimation.Commit(this, CircularAnimationName + Id, 16, 1000, Easing.Linear,
            (v, c) =>
            {
                if (Type == MaterialProgressIndicatorType.Circular && IsVisible)
                {
                    CustomCircleAnimationA();
                }
            },
            () => false);
        }

        #endregion Methods
    }
}