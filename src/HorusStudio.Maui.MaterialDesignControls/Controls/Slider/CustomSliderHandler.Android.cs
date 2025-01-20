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
using Paint = Android.Graphics.Paint;
using Color = Android.Graphics.Color;
using Android.Content;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomSliderHandler
{
    public static async void MapDesignProperties(ISliderHandler handler, ISlider slider)
    {
        if (slider is CustomSlider customSlider && handler.PlatformView is SeekBar control)
        {
            var activeTrackColor = customSlider.MinimumTrackColor.ToPlatform();
            var inactiveTrackColor = customSlider.MaximumTrackColor.ToPlatform();

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

            if (customSlider.ThumbImageSource is null && customSlider.ThumbColor is not null)
            {
                var thumbDrawable = new VerticalBarDrawable(customSlider.ThumbColor.ToPlatform(), customSlider.ThumbWidth, customSlider.ThumbHeight, customSlider.TrackCornerRadius, customSlider.TrackHeight, customSlider.ThumbBackgroundColor?.ToPlatform() ?? Color.Transparent);
                control.SetThumb(thumbDrawable);
                control.Thumb.SetColorFilter(new PorterDuffColorFilter(customSlider.ThumbColor.ToPlatform(), PorterDuff.Mode.SrcIn));
            }
            else
            {
                var drawable = await GetDrawableAsync(customSlider.ThumbImageSource, customSlider.ThumbWidth, customSlider.ThumbHeight, handler.MauiContext, MauiApplication.Current.ApplicationContext);
                if (drawable is not null)
                {
                    control.SetThumb(drawable);
                }
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
        }
    }

    private static async Task<Drawable> GetDrawableAsync(ImageSource imageSource, int width, int height, IMauiContext context, Context androidContext)
    {
        if (imageSource == null)
            return null;

        var serviceProvider = MauiApplication.Current.Services;
        var provider = serviceProvider.GetRequiredService<IImageSourceServiceProvider>();
        var service = provider.GetRequiredImageSourceService(imageSource);

        var result = await service.GetPlatformImageAsync(imageSource, context);

        if (result == null)
            return null;

        var drawable = result.Value;

        if (drawable is BitmapDrawable bitmapDrawable)
        {
            var bitmap = Bitmap.CreateScaledBitmap(bitmapDrawable.Bitmap, width * 2, height * 2, true);
            return new BitmapDrawable(androidContext.Resources, bitmap);
        }

        return drawable;
    }
}

class VerticalBarDrawable : Drawable
{
    private readonly Paint _paint;
    private readonly int _width;
    private readonly int _height;
    private readonly int _cornerRadius;
    private readonly int _trackHeight;
    private readonly Paint _backgroundPaint;

    public VerticalBarDrawable(Color color, int width, int height, int cornerRadius, int trackHeight, Color backgroundColor)
    {
        _paint = new Paint { Color = color, AntiAlias = true };
        _backgroundPaint = new Paint { Color = backgroundColor, AntiAlias = true };
        _width = width;
        _height = height;
        _trackHeight = trackHeight * 2;
        _cornerRadius = cornerRadius;
    }

    public override void Draw(Canvas canvas)
    {
        Android.Graphics.Rect bounds = Bounds;
        int centerX = bounds.CenterX();
        int centerY = bounds.CenterY();
        int _padding = 15;

        Android.Graphics.RectF backgroundRect = new Android.Graphics.RectF(
            centerX - _width / 2 - _padding,
            centerY - _trackHeight,
            centerX + _width / 2 + _padding,
            centerY + _trackHeight);

        canvas.DrawRoundRect(backgroundRect, _cornerRadius, _cornerRadius, _backgroundPaint);

        Android.Graphics.RectF rect = new Android.Graphics.RectF(centerX - _width, centerY - _height, centerX + _width, centerY + _height);

        canvas.DrawRoundRect(rect, _cornerRadius, _cornerRadius, _paint);
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
