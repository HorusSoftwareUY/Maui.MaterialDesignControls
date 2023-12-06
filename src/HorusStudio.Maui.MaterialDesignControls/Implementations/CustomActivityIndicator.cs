namespace HorusStudio.Maui.MaterialDesignControls
{
	public class CustomActivityIndicator : ContentView
	{
        private CustomActivityIndicatorDrawable _circularProgressBarDrawable;

        private GraphicsView _graphicsView;

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(int), typeof(CustomActivityIndicator));

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomActivityIndicator));

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly BindableProperty IndicatorColorProperty =
            BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CustomActivityIndicator));

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        public static readonly BindableProperty TrackColorProperty =
            BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(CustomActivityIndicator));

        public Color TrackColor
        {
            get { return (Color)GetValue(TrackColorProperty); }
            set { SetValue(TrackColorProperty, value); }
        }

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomActivityIndicator));

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

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

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SizeProperty.PropertyName)
            {
                HeightRequest = Size;
                WidthRequest = Size;
                _circularProgressBarDrawable.Size = Size;
                _graphicsView?.Invalidate();
            }
            else if (propertyName == ProgressProperty.PropertyName)
            {
                _circularProgressBarDrawable.Progress = Progress;
                _graphicsView?.Invalidate();
            }
            else if (propertyName == ThicknessProperty.PropertyName)
            {
                _circularProgressBarDrawable.Thickness = Thickness;
                _graphicsView?.Invalidate();
            }
            else if (propertyName == IndicatorColorProperty.PropertyName)
            {
                _circularProgressBarDrawable.IndicatorColor = IndicatorColor;
                _graphicsView?.Invalidate();
            }
            else if (propertyName == TrackColorProperty.PropertyName)
            {
                _circularProgressBarDrawable.TrackColor = TrackColor;
                _graphicsView?.Invalidate();
            }
        }
    }
}