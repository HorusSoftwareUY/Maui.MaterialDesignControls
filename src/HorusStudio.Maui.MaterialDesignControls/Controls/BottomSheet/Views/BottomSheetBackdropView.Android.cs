using Android.Animation;
using Android.Content;
using Android.Graphics.Drawables;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace HorusStudio.Maui.MaterialDesignControls;

internal class BottomSheetBackdropView : AView
{
    private const float DefaultOpacity = .5f;
    private static Color DefaultColor { get; } = Colors.Black;
    private float _opacity = DefaultOpacity;
    private Color? _color;
    
    private BottomSheetBackdropView(Context context) : base(context)
    {
    }

    public static BottomSheetBackdropView Create(Context context, Color? color = null, float? opacity = null)
    {
        var bsb = new BottomSheetBackdropView(context);
        bsb.SetBackgroundColor(color);
        bsb.SetOpacity(opacity);
        return bsb;
    }
    
    public void SetBackgroundColor(Color? color)
    {
        _color = color ?? DefaultColor;
        Background = new ColorDrawable(_color.ToPlatform());    
    }
    
    public void SetOpacity(float? opacity)
    {
        _opacity = opacity ?? DefaultOpacity;
        Alpha = _opacity;
    }
    
    public void AnimateIn()
    {
        var alphaAnimator = ObjectAnimator.OfFloat(this, "alpha", 0f, _opacity);
        alphaAnimator!.SetDuration(Context!.Resources!.GetInteger(Resource.Integer.bottom_sheet_slide_duration));
        alphaAnimator.Start();
    }

    public void AnimateOut()
    {
        var alphaAnimator = ObjectAnimator.OfFloat(this, "alpha", _opacity, 0f);
        alphaAnimator!.SetDuration(Context!.Resources!.GetInteger(Resource.Integer.bottom_sheet_slide_duration));
        alphaAnimator.Start();
    }
}
