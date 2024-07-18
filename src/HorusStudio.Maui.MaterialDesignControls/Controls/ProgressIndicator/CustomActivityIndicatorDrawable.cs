namespace HorusStudio.Maui.MaterialDesignControls
{
    class CustomActivityIndicatorDrawable : BindableObject, IDrawable
    {
        #region Bindable properties

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(CustomActivityIndicatorDrawable));

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomActivityIndicatorDrawable));

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CustomActivityIndicatorDrawable));

        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(CustomActivityIndicatorDrawable));

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomActivityIndicatorDrawable));

        #endregion Bindable properties

        #region Properties

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public int Thickness
        {
            get => (int)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public Color IndicatorColor
        {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        public int Size
        {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        #endregion Properties

        #region Methods

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var thickness = Thickness;
            if (thickness > 1 && Size <= 36 && Size > 24)
                thickness -= 1;
            else if (thickness > 2 && Size <= 24 && Size > 0)
                thickness -= 2;

            float effectiveSize = Size - thickness;
            float x = thickness / 2;
            float y = thickness / 2;

            if (Progress < 0)
            {
                Progress = 0;
            }
            else if (Progress > 100)
            {
                Progress = 100;
            }

            if (Progress < 100)
            {
                float angle = GetAngle(Progress);

                canvas.StrokeColor = TrackColor;
                canvas.StrokeSize = thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);

                // Draw arc
                canvas.StrokeColor = IndicatorColor;
                canvas.StrokeSize = thickness;
                canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
            }
            else
            {
                // Draw circle
                canvas.StrokeColor = IndicatorColor;
                canvas.StrokeSize = thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);
            }
        }

        private float GetAngle(int progress)
        {
            float factor = 90f / 25f;
            if (progress > 75)
            {
                return -180 - ((progress - 75) * factor);
            }
            else if (progress > 50)
            {
                return -90 - ((progress - 50) * factor);
            }
            else if (progress > 25)
            {
                return 0 - ((progress - 25) * factor);
            }
            else
            {
                return 90 - (progress * factor);
            }
        }

        #endregion Methods
    }
}