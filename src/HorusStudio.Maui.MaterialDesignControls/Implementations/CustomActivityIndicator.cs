namespace HorusStudio.Maui.MaterialDesignControls
{
	internal class CustomActivityIndicator : ContentView
	{
        #region Attributes and Properties

        private CustomActivityIndicatorDrawable _circularProgressBarDrawable;

        private GraphicsView _graphicsView;

        #endregion Attributes and Properties

        #region Bindable properties

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is int newProgress && (o == null || (o is int oldProgress && !oldProgress.Equals(newProgress))))
                {
                    self._circularProgressBarDrawable.Progress = newProgress;
                    self._graphicsView?.Invalidate();
                }
            }
        });

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is int newThickness && (o == null || (o is int oldThickness && !oldThickness.Equals(newThickness))))
                {
                    self._circularProgressBarDrawable.Thickness = newThickness;
                    self._graphicsView?.Invalidate();
                }
            }
        });

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is Color newIndicatorColor && (o == null || (o is Color oldIndicatorColor && !oldIndicatorColor.Equals(newIndicatorColor))))
                {
                    self._circularProgressBarDrawable.IndicatorColor = newIndicatorColor;
                    self._graphicsView?.Invalidate();
                }
            }
        });

        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(CustomActivityIndicator), propertyChanged: (bindable, o, n) =>
        {
            if (bindable is CustomActivityIndicator self)
            {
                if (n is Color newTrackColor && (o == null || (o is Color oldTrackColor && !oldTrackColor.Equals(newTrackColor))))
                {
                    self._circularProgressBarDrawable.TrackColor = newTrackColor;
                    self._graphicsView?.Invalidate();
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
                    self._circularProgressBarDrawable.Size = newSize;
                    self._graphicsView?.Invalidate();
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

        #endregion Properties

        #region Constructors

        public CustomActivityIndicator()
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

        #endregion Constructors
    }
}