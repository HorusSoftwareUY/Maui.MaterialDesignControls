using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialProgressIndicatorType
    {
        Circular, Linear
    }

    public class MaterialProgressIndicator : ContentView
    {
        #region Attributes and Properties

        private const string LinearAnimationName = "LinearAnimation";

        private const string CircularAnimationName = "CircularAnimation";

        private const int CircleAnimationMinimumProgress = 12;

        private const int CircleAnimationMaximumProgress = 96;

        private bool _initialized = false;

        private bool _rendered = false;

        private BoxView _progressBar;

        private ActivityIndicator _activityIndicator;

        private CustomActivityIndicator _customActivityIndicator;

        #endregion Attributes and Properties

        #region Bindable properties

        public static readonly BindableProperty TypeProperty =
            BindableProperty.Create(nameof(Type), typeof(MaterialProgressIndicatorType), typeof(MaterialProgressIndicator), defaultValue: MaterialProgressIndicatorType.Circular);

        public MaterialProgressIndicatorType Type
        {
            get { return (MaterialProgressIndicatorType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly BindableProperty IndicatorColorProperty =
            BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValue: MaterialColor.Primary);

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        public static readonly BindableProperty TrackColorProperty =
            BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialProgressIndicator), defaultValue: MaterialColor.SurfaceContainerHighest);

        public Color TrackColor
        {
            get { return (Color)GetValue(TrackColorProperty); }
            set { SetValue(TrackColorProperty, value); }
        }

        #endregion Bindable properties

        #region Constructors

        public MaterialProgressIndicator()
        {
            if (!_initialized)
                Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _initialized = true;

            Padding = 0;

            SetProgressIndicatorType();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!_initialized)
                Initialize();

            switch (propertyName)
            {
                case nameof(Type):
                    SetProgressIndicatorType();
                    break;
                case nameof(TrackColor):
                    if (Type == MaterialProgressIndicatorType.Linear)
                        this.BackgroundColor = TrackColor;
                    break;
                case nameof(IndicatorColor):
                    if (Type == MaterialProgressIndicatorType.Circular)
                    {
                        if (_activityIndicator != null)
                            _activityIndicator.Color = IndicatorColor;
                        else if (_customActivityIndicator != null)
                            _customActivityIndicator.IndicatorColor = IndicatorColor;
                    }
                    else if (Type == MaterialProgressIndicatorType.Linear && _progressBar != null)
                        _progressBar.Color = IndicatorColor;
                    break;
                case nameof(IsVisible):
                    base.OnPropertyChanged(propertyName);

                    if (Type == MaterialProgressIndicatorType.Circular)
                    {
                        if (_activityIndicator != null)
                            _activityIndicator.IsRunning = IsVisible;
                        else if (_customActivityIndicator != null)
                        {
                            // Handle circular animation
                            if (IsVisible)
                                StartCustomCircleAnimation();
                            else
                                this.AbortAnimation(CircularAnimationName + Id);
                        }
                    }
                    else
                    {
                        // Handle linear animation
                        if (IsVisible)
                            StartLinearAnimation();
                        else
                            this.AbortAnimation(LinearAnimationName + Id);
                    }
                    break;
                case "Renderer":
                    if (!_rendered)
                        _rendered = true;
                    else
                    {
                        // This property is setted on the view appearing and in the view dissapearing
                        // So we abort the linear or circular animation here
                        if (Type == MaterialProgressIndicatorType.Circular
                            && _customActivityIndicator != null)
                            this.AbortAnimation(CircularAnimationName + Id);
                        else if (Type == MaterialProgressIndicatorType.Linear)
                            this.AbortAnimation(LinearAnimationName + Id);
                    }
                    break;
            }
        }

        public void SetProgressIndicatorType()
        {
            switch (Type)
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

        private void StartLinearAnimation()
        {
            var index = 1;
            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                if (index % 2 != 0)
                    _progressBar.Margin = new Thickness(0, 0, Width - (Width * v), 0); // Expanding boxview
                else
                    _progressBar.Margin = new Thickness(Width * v, 0, 0, 0); // Collapsing boxview
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
                    CustomCircleAnimationB();
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
                    CustomCircleAnimationA();
            },
            () => false);
        }

        #endregion Methods
    }
}