namespace HorusStudio.Maui.MaterialDesignControls
{
	class CustomActivityIndicator : ContentView
	{
        #region Attributes and Properties

        private CustomActivityIndicatorDrawable _circularProgressBarDrawable;

        private GraphicsView _graphicsView;

        private ActivityIndicator _activityIndicator;

        private const string CircularAnimationName = "CircularAnimation";

        private const int CircleAnimationMinimumProgress = 12;

        private const int CircleAnimationMaximumProgress = 96;

        #endregion Attributes and Properties

        #region Bindable properties

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is int newProgress && (o == null || (o is int oldProgress && !oldProgress.Equals(newProgress))))
                {
                    if (self._circularProgressBarDrawable != null)
                    {
                        self._circularProgressBarDrawable.Progress = newProgress;
                        self._graphicsView?.Invalidate();
                    }
                }
            }
        });

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is int newThickness && (o == null || (o is int oldThickness && !oldThickness.Equals(newThickness))))
                {
                    if (self._circularProgressBarDrawable != null)
                    {
                        self._circularProgressBarDrawable.Thickness = newThickness;
                        self._graphicsView?.Invalidate();
                    }
                }
            }
        });

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is Color newIndicatorColor && (o == null || (o is Color oldIndicatorColor && !oldIndicatorColor.Equals(newIndicatorColor))))
                {
                    if (self._activityIndicator != null)
                    {
                        self._activityIndicator.Color = newIndicatorColor;
                    }
                    else if (self._circularProgressBarDrawable != null)
                    {
                        self._circularProgressBarDrawable.IndicatorColor = newIndicatorColor;
                        self._graphicsView?.Invalidate();
                    }
                }
            }
        });

        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is Color newTrackColor && (o == null || (o is Color oldTrackColor && !oldTrackColor.Equals(newTrackColor))))
                {
                    if (self._circularProgressBarDrawable != null)
                    {
                        self._circularProgressBarDrawable.TrackColor = newTrackColor;
                        self._graphicsView?.Invalidate();
                    }
                }
            }
        });

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is int newSize && (o == null || (o is int oldSize && !oldSize.Equals(newSize))))
                {
                    self.HeightRequest = newSize;
                    self.WidthRequest = newSize;

                    if (self._circularProgressBarDrawable != null)
                    {
                        self._circularProgressBarDrawable.Size = newSize;
                        self._graphicsView?.Invalidate();
                    }
                }
            }
        });

        public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is bool newIsRunning && (o == null || (o is bool oldIsRunning && !oldIsRunning.Equals(newIsRunning))))
                {
                    if (self._activityIndicator != null)
                    {
                        self._activityIndicator.IsRunning = newIsRunning;
                    }
                    else if (self._circularProgressBarDrawable != null)
                    {
                        // Handle custom circular animation
                        if (newIsRunning)
                        {
                            self.StartCustomCircleAnimation();
                        }
                        else
                        {
                            self.AbortAnimation(CircularAnimationName + self.Id);
                        }
                    }
                }
            }
        });

        #endregion Bindable properties

        #region Properties

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        public Color TrackColor
        {
            get { return (Color)GetValue(TrackColorProperty); }
            set { SetValue(TrackColorProperty, value); }
        }

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        #endregion Properties

        #region Constructors

        public CustomActivityIndicator()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _activityIndicator = new ActivityIndicator()
                {
                    Color = IndicatorColor,
                    IsRunning = true
                };
                Content = _activityIndicator;
            }
            else
            {
                _circularProgressBarDrawable = new CustomActivityIndicatorDrawable
                {
                    Progress = Progress,
                    Size = Size,
                    Thickness = Thickness,
                    IndicatorColor = IndicatorColor,
                    TrackColor = TrackColor
                };
                _graphicsView = new GraphicsView
                {
                    Drawable = _circularProgressBarDrawable
                };
                Content = _graphicsView;
            }
        }

        #endregion Constructors

        #region Methods

        private void StartCustomCircleAnimation()
        {
            Progress = CircleAnimationMinimumProgress;
            CustomCircleAnimationA();
        }

        private void CustomCircleAnimationA()
        {
            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                Rotation = v;
            }, 0, 360, Easing.SinIn));
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                Progress = (int)v;
            }, CircleAnimationMinimumProgress, CircleAnimationMaximumProgress, Easing.SinIn));
            mainAnimation.Commit(this, CircularAnimationName + Id, 16, 1000, Easing.Linear,
            (v, c) =>
            {
                if (IsRunning)
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
                Rotation = v;
            }, 0, 360, Easing.SinOut));
            mainAnimation.Add(0, 1, new Animation(v =>
            {
                Progress = (int)v;
            }, CircleAnimationMaximumProgress, CircleAnimationMinimumProgress, Easing.SinOut));
            mainAnimation.Commit(this, CircularAnimationName + Id, 16, 1000, Easing.Linear,
            (v, c) =>
            {
                if (IsRunning)
                {
                    CustomCircleAnimationA();
                }
            },
            () => false);
        }

        #endregion Methods
    }
}