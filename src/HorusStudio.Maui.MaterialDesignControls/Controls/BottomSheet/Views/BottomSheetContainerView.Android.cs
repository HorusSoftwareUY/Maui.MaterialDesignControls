#define USE_MATERIAL3

using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.View;
using Google.Android.Material.BottomSheet;

namespace HorusStudio.Maui.MaterialDesignControls;

internal class BottomSheetContainerView : CoordinatorLayout
{
    #region Properties

    /// <summary>
    /// Optional clickable backdrop
    /// </summary>
    public BottomSheetBackdropView Backdrop { get; private init; }

    /// <summary>
    /// Container for custom content
    /// </summary>
    public FrameLayout BottomSheetView { get; private init; }

    #endregion Properties

    private BottomSheetContainerView(Context context) : base(context)
    {
    }
    
    public static BottomSheetContainerView Create(Context context)
    {
#if USE_MATERIAL3
        var frameStyle = Resource.Style.Widget_Material3_BottomSheet_Modal;
#else
        var frameStyle = Resource.Style.Widget_MaterialComponents_BottomSheet_Modal;
#endif

        var bsc = new BottomSheetContainerView(context)
        {
            Focusable = false,
            ImportantForAccessibility = ImportantForAccessibility.No,
            SoundEffectsEnabled = false,
            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent),
            Backdrop = BottomSheetBackdropView.Create(context),
            BottomSheetView = new FrameLayout(context, null, 0, frameStyle)
            {
                LayoutParameters =  new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = (int)(GravityFlags.CenterHorizontal | GravityFlags.Bottom),
                    Behavior = new BottomSheetBehavior(),
                },
                ClipToOutline = true,
                OutlineProvider = ViewOutlineProvider.Background
            }
        };

        bsc.AddView(bsc.Backdrop, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        bsc.AddView(bsc.BottomSheetView);
        ViewCompat.SetWindowInsetsAnimationCallback(bsc.BottomSheetView, new BottomSheetInsetsAnimationCallback(bsc));

        return bsc;
    }

    public void SetBackdropVisibility(bool hasBackdrop) => Backdrop.Visibility = hasBackdrop ? ViewStates.Visible : ViewStates.Gone;
    public void SetBackdropColor(Color? color) => Backdrop.SetBackgroundColor(color);
    public void SetBackdropOpacity(float? opacity) => Backdrop.SetOpacity(opacity);

    private WindowInsetsCompat? GetWindowInsets()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(23) && RootWindowInsets is not null)
            return WindowInsetsCompat.ToWindowInsetsCompat(RootWindowInsets);

        return WindowInsetsCompat.Consumed;
    }

    #region Listeners and callbacks
    
    private class BottomSheetInsetsAnimationCallback(BottomSheetContainerView ownerView) : WindowInsetsAnimationCompat.Callback(DispatchModeStop)
    {
        private int _startHeight;
        private int _endHeight;

        public override void OnPrepare(WindowInsetsAnimationCompat? animation)
        {
            _startHeight = ownerView.GetWindowInsets()?.GetInsets(WindowInsetsCompat.Type.Ime())?.Bottom ?? 0;
            base.OnPrepare(animation);
        }

        public override WindowInsetsAnimationCompat.BoundsCompat? OnStart(WindowInsetsAnimationCompat? animation, WindowInsetsAnimationCompat.BoundsCompat? bounds)
        {
            _endHeight = ownerView.GetWindowInsets()?.GetInsets(WindowInsetsCompat.Type.Ime())?.Bottom ?? 0;
            ownerView.BottomSheetView.TranslationY = _endHeight - _startHeight;
            return bounds;
        }

        public override WindowInsetsCompat? OnProgress(WindowInsetsCompat? insets, IList<WindowInsetsAnimationCompat>? runningAnimations)
        {
            var imeAnimation = runningAnimations?.FirstOrDefault(animation => (animation.TypeMask & WindowInsetsCompat.Type.Ime()) != 0);
            if (imeAnimation != null)
                ownerView.BottomSheetView.TranslationY = (_endHeight - _startHeight) * (1 - imeAnimation.InterpolatedFraction);

            return insets;
        }
    }
    
    #endregion Listeners and callbacks
}