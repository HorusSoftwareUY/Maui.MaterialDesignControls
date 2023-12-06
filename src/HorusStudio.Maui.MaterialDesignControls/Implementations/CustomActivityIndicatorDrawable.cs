namespace HorusStudio.Maui.MaterialDesignControls
{
    public class CustomActivityIndicatorDrawable : BindableObject, IDrawable
    {
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(int), typeof(CustomActivityIndicatorDrawable));

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomActivityIndicatorDrawable));

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly BindableProperty IndicatorColorProperty =
            BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CustomActivityIndicatorDrawable));

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        public static readonly BindableProperty TrackColorProperty =
            BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(CustomActivityIndicatorDrawable));

        public Color TrackColor
        {
            get { return (Color)GetValue(TrackColorProperty); }
            set { SetValue(TrackColorProperty, value); }
        }

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomActivityIndicatorDrawable));

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float effectiveSize = Size - Thickness;
            float x = Thickness / 2;
            float y = Thickness / 2;

            if (Progress < 0)
                Progress = 0;
            else if (Progress > 100)
                Progress = 100;

            if (Progress < 100)
            {
                float angle = GetAngle(Progress);

                canvas.StrokeColor = TrackColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);

                // Draw arc
                canvas.StrokeColor = IndicatorColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
            }
            else
            {
                // Draw circle
                canvas.StrokeColor = IndicatorColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);
            }
        }

        private float GetAngle(int progress)
        {
            float factor = 90f / 25f;
            if (progress > 75)
                return -180 - ((progress - 75) * factor);
            else if (progress > 50)
                return -90 - ((progress - 50) * factor);
            else if (progress > 25)
                return 0 - ((progress - 25) * factor);
            else
                return 90 - (progress * factor);
        }
    }
}