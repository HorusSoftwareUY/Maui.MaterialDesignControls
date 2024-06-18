using Android.Content.Res;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Android.Widget;
using ShapeDrawable = Android.Graphics.Drawables.ShapeDrawable;
using HorusStudio.Maui.MaterialDesignControls.Utils;
using Paint = Android.Graphics.Paint;
using Color = Android.Graphics.Color;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomSliderHandler
{
    public static void MapDesignProperties(ISliderHandler handler, ISlider slider)
    {
        if (slider is CustomSlider customSlider && handler.PlatformView is SeekBar control)
        {
            var activeTrackColor = customSlider.MinimumTrackColor.ToPlatform();
            var inactiveTrackColor = customSlider.MaximumTrackColor.ToPlatform();

            control.SetPadding(0, 0, control.Thumb.IntrinsicWidth, 0);

            BuildVersionCodes androidVersion = Build.VERSION.SdkInt;
            if (androidVersion >= BuildVersionCodes.M)
            {
                var trackCornerRadius = customSlider.TrackCornerRadius.DpToPixels(control.Context);
                var trackHeight = customSlider.TrackHeight.DpToPixels(control.Context);

                var radius = new float[] { trackCornerRadius, trackCornerRadius, trackCornerRadius, trackCornerRadius, trackCornerRadius, trackCornerRadius, trackCornerRadius, trackCornerRadius };
                var trackShape = new RoundRectShape(radius, new Android.Graphics.RectF(), radius);

                var background = new ShapeDrawable(trackShape);
                background.SetColorFilter(new PorterDuffColorFilter(inactiveTrackColor, PorterDuff.Mode.SrcIn));

                var progress = new ShapeDrawable(trackShape);
                progress.SetColorFilter(new PorterDuffColorFilter(activeTrackColor, PorterDuff.Mode.SrcIn));
                var clippedProgress = new ClipDrawable(progress, GravityFlags.Start, ClipDrawableOrientation.Horizontal);

                var progressDrawable = new LayerDrawable(new Drawable[] { background, clippedProgress });
                progressDrawable.SetLayerHeight(0, trackHeight);
                progressDrawable.SetLayerGravity(0, GravityFlags.CenterVertical);
                progressDrawable.SetLayerHeight(1, trackHeight);
                progressDrawable.SetLayerGravity(1, GravityFlags.CenterVertical);
                control.ProgressDrawable = progressDrawable;

                var progressToLevel = ((double)control.Progress / (double)control.Max) * 10000;
                control.ProgressDrawable.SetLevel(Convert.ToInt32(progressToLevel));
            }
            else
            {
                control.ProgressTintList = ColorStateList.ValueOf(activeTrackColor);
                control.ProgressTintMode = PorterDuff.Mode.SrcIn;

                control.ProgressBackgroundTintList = ColorStateList.ValueOf(inactiveTrackColor);
                control.ProgressBackgroundTintMode = PorterDuff.Mode.SrcIn;
            }

            if (customSlider.ThumbImageSource == null && customSlider.ThumbColor != null)
            {
                control.Thumb.SetColorFilter(new PorterDuffColorFilter(customSlider.ThumbColor.ToPlatform(), PorterDuff.Mode.SrcIn));
            }

            if (customSlider.UserInteractionEnabled)
            {
                control.SplitTrack = true;
                control.Thumb.Mutate().SetAlpha(255);
            }
            else
            {
                control.SplitTrack = false;
                control.Thumb.Mutate().SetAlpha(0);
                control.Enabled = false;
            }
           
            if(customSlider.ThumbImageSource is null)
            {
                int thumbWidth = 4;
                int thumbHeight = 44;
                var thumbDrawable = new VerticalBarDrawable(customSlider.ThumbColor.ToPlatform(), thumbWidth, thumbHeight);
                control.SetThumb(thumbDrawable);
            }
            else
            {
                Drawable thumb = control.Thumb;
                int thumbTop = (control.Height / 2 - thumb.IntrinsicHeight / 2);
                thumb.SetBounds(thumb.Bounds.Left, thumbTop, thumb.Bounds.Left + thumb.IntrinsicWidth, thumbTop + thumb.IntrinsicHeight);
                control.SetPadding(thumb.IntrinsicWidth / 2, 0, thumb.IntrinsicWidth / 2, 0);
            }
        }
    }
}

public class VerticalBarDrawable : Drawable
{
    private readonly Paint _paint;
    private readonly int _width;
    private readonly int _height;

    public VerticalBarDrawable(Color color, int width, int height)
    {
        _paint = new Paint { Color = color };
        _width = width * 2;
        _height = height;
    }

    public override void Draw(Canvas canvas)
    {
        Android.Graphics.Rect bounds = Bounds;
        int centerX = bounds.CenterX();
        int centerY = bounds.CenterY();

        canvas.DrawRect(centerX - _width + 20, centerY - _height, centerX + _width - 10, centerY + _height, _paint);
    }

    public override void SetAlpha(int alpha)
    {
        _paint.Alpha = alpha;
    }

    public override void SetColorFilter(ColorFilter colorFilter)
    {
        _paint.SetColorFilter(colorFilter);
    }

    public override int Opacity => (int)Format.Opaque;
}
