using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialProgressIndicatorType
    {
        /// <summary> Circular </summary>
        Circular, 
        /// <summary> Linear </summary>
        Linear
    }

    /// <summary>
    /// Progress indicators show the status of a process and follow Material Design Guidelines. <see href="https://m3.material.io/components/progress-indicators/overview">See more</see>.
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialProgressIndictor.gif</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt;material:MaterialProgressIndicator
    ///         Type="Linear"
    ///         IndicatorColor="DarkBlue"
    ///         TrackColor="LightBlue"/&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var progressIndicator = new MaterialProgressIndicator()
    /// {
    ///     Type = MaterialProgressIndicatorType.Linear,
    ///     IndicatorColor = Colors.Blue,
    ///     TrackColor = Colors.LightBlue
    /// };
    ///</code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ProgressIndicatorPage.xaml)
    /// 
    /// </example>
    public class MaterialProgressIndicator : ContentView
    {
        #region Attributes

        private const MaterialProgressIndicatorType DefaultProgressIndicatorType = MaterialProgressIndicatorType.Circular;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultIndicatorColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTrackColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
        private const double DefaultHeightRequest = -1;
        private const double DefaultWidthRequest = -1;
        private const int CircularThickness = 4;

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

        #endregion Attributes

        #region Bindable properties

        /// <summary>
        /// The backing store for the <see cref="Type">Type</see> bindable property.
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
        /// The backing store for the <see cref="IndicatorColor">IndicatorColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValueCreator: DefaultIndicatorColor, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetIndicatorColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrackColor">TrackColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValueCreator: DefaultTrackColor, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetTrackColor(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IsVisible">IsVisible</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(MaterialProgressIndicator), defaultValue: true, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialProgressIndicator self)
            {
                self.SetIsVisible(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="HeightRequest">HeightRequest</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialProgressIndicator), defaultValue: DefaultHeightRequest, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialProgressIndicator self
                && n is double newHeight && (o == null || (o is double oldHeight && !oldHeight.Equals(newHeight))))
            {
                self.SetHeightRequest(self.Type);
            }
        });

        /// <summary>
        /// The backing store for the <see cref="WidthRequest">WidthRequest</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(MaterialProgressIndicator), defaultValue: DefaultWidthRequest, propertyChanged: (bindable, o, n) =>
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
        /// Gets or sets the progress indicator <see cref="MaterialProgressIndicatorType">type</see>.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialProgressIndicatorType.Circular">MaterialProgressIndicatorType.Circular</see>
        /// </default>
        public MaterialProgressIndicatorType Type
        {
            get => (MaterialProgressIndicatorType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color">color</see> for the active indicator of the progress indicator.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
        /// </default>
        public Color IndicatorColor
        {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the track <see cref="Color">color</see> of the progress indicator.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light: <see cref="MaterialLightTheme.SurfaceContainerHighest">MaterialLightTheme.SurfaceContainerHighest</see> - Dark: <see cref="MaterialDarkTheme.SurfaceContainerHighest">MaterialDarkTheme.SurfaceContainerHighest</see>
        /// </default>
        /// <remarks>This property will not have an effect unless <see cref="MaterialProgressIndicator.Type">MaterialProgressIndicator.Type</see> is set to <see cref="MaterialProgressIndicatorType.Linear">MaterialProgressIndicatorType.Linear</see>.</remarks>
        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        /// <summary>
        /// Gets or sets if progress indicator is visible.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="true">True</see>
        /// </default>
        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets the height of the progress indicator.
        /// This is a bindable property.
        /// </summary>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the progress indicator.
        /// This is a bindable property.
        /// </summary>
        public new double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        #endregion Properties

        #region Layout

        private BoxView? _progressBar = null!;
        private CustomActivityIndicator? _customActivityIndicator = null!;

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

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
            var height = HeightRequest != DefaultHeightRequest ?
                    HeightRequest :
                    _controlDefaultHeights[type];

            base.HeightRequest = height;

            if (_customActivityIndicator != null && type == MaterialProgressIndicatorType.Circular)
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

            if (_customActivityIndicator != null && type == MaterialProgressIndicatorType.Circular)
            {
                _customActivityIndicator.Size = (int)width;
            }
        }

        private void StartLinearAnimation()
        {
            var animationManager = Application.Current?.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Animations.IAnimationManager>();
            if (animationManager is null || _progressBar is null)
            {
                return;
            }
            
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
            mainAnimation.Commit(this, LinearAnimationName + Id, 16, 1500, Easing.Linear, (_, _) => ++index,
            () => Type == MaterialProgressIndicatorType.Linear && IsVisible);
        }

        #endregion Methods
    }
}