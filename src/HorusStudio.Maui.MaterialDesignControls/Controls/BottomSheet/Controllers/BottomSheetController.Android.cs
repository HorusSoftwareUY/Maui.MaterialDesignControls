using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using Google.Android.Material.BottomSheet;
using Google.Android.Material.Color;
using Google.Android.Material.Internal;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using AWindow = Android.Views.Window;
using Insets = AndroidX.Core.Graphics.Insets;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
internal class BottomSheetController(IMauiContext windowMauiContext, MaterialBottomSheet sheet)
{
    #region Attributes

    private static StayOnFrontView? _stayOnFront;
    private IDictionary<Detent, int>? _states;
    private IDictionary<Detent, double>? _heights;
    private bool _isDuringShowingAnimation = false;
    private BottomSheetBehavior? _behavior;
    private ViewGroup? _frame;
    private BottomSheetContainer? _windowContainer;
    private BottomSheetDragHandleView? _handle;
    private bool? _isBackgroundLight;
    private readonly IMauiContext? _mauiContext = windowMauiContext;
    private readonly MaterialBottomSheet? _sheet = sheet;

    #endregion Attributes
    
    #region Properties
    
    public bool UseNavigationBarArea { get; set; } = false;
    
    private WindowInsetsCompat WindowInsets
    {
        get
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(23) && _windowContainer?.RootWindowInsets is not null)
            {
                return WindowInsetsCompat.ToWindowInsetsCompat(_windowContainer.RootWindowInsets);
            }

            return WindowInsetsCompat.Consumed;
        }
    }

    private Insets Insets
    {
        get
        {
            var insets = WindowInsets;
            if (OperatingSystem.IsAndroidVersionAtLeast(30))
            {
                return insets.GetInsetsIgnoringVisibility(Android.Views.WindowInsets.Type.SystemBars());
            }
#pragma warning disable CS0618
            return Insets.Of(insets.StableInsetLeft, insets.StableInsetTop, insets.StableInsetRight, insets.StableInsetBottom);
#pragma warning restore CS0618
        }
    }

    private int KeyboardHeight => WindowInsets.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
    private int TopInset => Insets.Top;
    private int BottomInset => UseNavigationBarArea ? 0 : Insets.Bottom;
    
    #endregion Properties

    #region Methods
    
    private void CalculateHeights(double maxSheetHeight)
    {
        var detents = _sheet?.GetEnabledDetents().ToList();
        if (detents is null) return;
        
        _heights = new Dictionary<Detent, double>();
        foreach (var detent in detents)
        {
            _heights.Add(detent, detent.GetHeight(_sheet!, maxSheetHeight));
        }
    }

    private void CalculateStates()
    {
        var heights = _heights?.OrderByDescending(kv => kv.Value).ToList();
        if (heights is null) return;
        
        _states = new Dictionary<Detent, int>();
        switch (heights.Count)
        {
            case 1:
                _states.Add(heights[0].Key, BottomSheetBehavior.StateCollapsed);
                break;
            case 2:
                _states.Add(heights[0].Key, BottomSheetBehavior.StateExpanded);
                _states.Add(heights[1].Key, BottomSheetBehavior.StateCollapsed);
                break;
            case 3:
                _states.Add(heights[0].Key, BottomSheetBehavior.StateExpanded);
                _states.Add(heights[1].Key, BottomSheetBehavior.StateHalfExpanded);
                _states.Add(heights[2].Key, BottomSheetBehavior.StateCollapsed);
                break;
        }
    }

    private int GetStateForDetent(Detent? detent)
    {
        if (detent is null || _states?.ContainsKey(detent) == false) return -1;
        return _states![detent];
    }
    
    private Detent? GetDetentForState(int state)
    {
        return _states?.FirstOrDefault(kv => kv.Value == state).Key;
    }

    internal void Dismiss(bool animated)
    {
        if (animated)
        {
            _windowContainer?.Backdrop.AnimateOut();
            _behavior!.Hideable = true;
            _behavior.State = BottomSheetBehavior.StateHidden;    
        }
        else
        {
            Dispose();
            _sheet?.NotifyDismissed();
        }
    }

    private void Dispose()
    {
        _frame!.LayoutChange -= OnLayoutChange;
        _windowContainer!.RemoveFromParent();
    }

    private void Layout() => LayoutDetents(_heights!, GetAvailableHeight());

    internal void UpdateBackground()
    {
        Paint paint = _sheet!.BackgroundBrush;
        if (_frame is not null)
        {
            if (_sheet.CornerRadius >= 0)
            {
                var radiusDrawable = _frame.Background as SheetRadiusDrawable ?? new SheetRadiusDrawable();
                radiusDrawable.SetCornerRadius(_frame.Context.ToPixels(_sheet.CornerRadius));
                _frame.Background = radiusDrawable;
            }
            if (paint is not null)
            {
                var platformColor = paint.ToColor()!.ToPlatform();
                if (_frame.Background is GradientDrawable drawable)
                {
                    drawable.SetColor(platformColor);
                }
                else
                {
                    _frame.BackgroundTintList = ColorStateList.ValueOf(platformColor);
                }
            }
        }
        // Try to find the background color to automatically change the status bar icons so they will
        // still be visible when the BottomSheet slides underneath the status bar.
        var backgroundTint = ViewCompat.GetBackgroundTintList(_frame!);
        if (backgroundTint != null)
        {
            // First check for a tint
            _isBackgroundLight = MaterialColors.IsColorLight(backgroundTint.DefaultColor);
        }
        else if (_frame?.Background is ColorDrawable colorDrawable)
        {
            // Then check for the background color
            _isBackgroundLight = MaterialColors.IsColorLight(colorDrawable.Color);
        }
        else
        {
            // Otherwise don't change the status bar color
            _isBackgroundLight = null;
        }
    }

    internal void UpdateHandleColor()
    {
        if (_handle is not null && _sheet?.HandleColor is not null)
        {
            _handle.SetColorFilter(_sheet.HandleColor.ToPlatform());
        }
    }

    private static void EnsureStayOnFrontView(Context context)
    {
        if (_stayOnFront is not null && _stayOnFront.IsAttachedToWindow) return;
        
        _stayOnFront = new StayOnFrontView(context);
        if (context is AppCompatActivity { Window.DecorView: ViewGroup parentView })
        {
            parentView.AddView(_stayOnFront);
        }
    }

    private void EnsureWindowContainer(Context context)
    {
        EnsureStayOnFrontView(context);
        if (_windowContainer is not null) return;
        if (AView.Inflate(context, Resource.Layout.bottom_sheet_design, null) is not FrameLayout container) return;
            
        //container.ViewAttachedToWindow += ContainerAttachedToWindow;
        //container.ViewDetachedFromWindow += ContainerDetachedFromWindow;

        _windowContainer = new BottomSheetContainer(context, container);
        _windowContainer.Backdrop.Click += BackdropClicked;
        ViewCompat.SetOnApplyWindowInsetsListener(_windowContainer, new EdgeToEdgeListener(this));
            
        _frame = (FrameLayout)container.FindViewById(Resource.Id.design_bottom_sheet)!;
        _frame.OutlineProvider = ViewOutlineProvider.Background;
        _frame.ClipToOutline = true;
        ViewCompat.SetWindowInsetsAnimationCallback(_frame, new BottomSheetInsetsAnimationCallback(this));
            
        var callback = new BottomSheetCallback(_sheet!);
        callback.StateChanged += Callback_StateChanged;
        _behavior = BottomSheetBehavior.From(_frame);
        _behavior.AddBottomSheetCallback(callback);
    }

    private void BackdropClicked(object? sender, EventArgs e)
    {
        if (_sheet?.IsCancelable == true)
        {
            Dismiss(true);
        }
    }

    private double GetAvailableHeight()
    {
        var density = DeviceDisplay.MainDisplayInfo.Density;

        return (_windowContainer!.Height - TopInset - BottomInset - KeyboardHeight) / density;
    }

    private void LayoutDetents(IDictionary<Detent, double> heights, double maxSheetHeight)
    {
        // Android supports the following detents:
        // - expanded (top of screen - offset)
        // - half expanded (using ratio of expanded - peekHeight)
        // - collapsed (using peekHeight)

        var sortedHeights = heights
            .OrderByDescending(i => i.Value)
            .ToList();
        var density = DeviceDisplay.MainDisplayInfo.Density;

        var keyboardHeight = KeyboardHeight;

        var top = sortedHeights[0].Value;

        // Configure the sheet to handle up to 3 detents

        if (sortedHeights.Count == 1)
        { // Only way to have one detent on Android is to use fitToContent. Use that
            _behavior!.FitToContents = true;
            _behavior.SkipCollapsed = true;
        }
        else if (sortedHeights.Count == 2)
        { // We can handle a second detent by adding a collapsed state. Use peek height
            _behavior!.FitToContents = true;
            _behavior.SkipCollapsed = false;

            var bottom = sortedHeights[1].Value;

            _behavior.PeekHeight = (int)(bottom * density) + BottomInset + keyboardHeight;
        }
        else if (sortedHeights.Count == 3)
        { // 3 detents can be done using the peek height AND disabling fitToContent
          // Doing so uses a property called halfExpandedRatio, giving us
          // Expanded: Use ExpandedOffset to offset from the top
          // HalfExpanded: Use HalfExpandedRatio
          // Collapsed: Use PeekHeight

            _behavior!.FitToContents = false;
            _behavior.SkipCollapsed = false;

            var midway = sortedHeights[1].Value;
            var bottom = sortedHeights[2].Value;

            // Set the top detent by offsetting the requested height from the maxHeight
            var topOffset = (maxSheetHeight - top) * density;
            _behavior.ExpandedOffset = Math.Max(0, (int)topOffset);

            // Set the midway detent by calculating the ratio using the top detent info
            var ratio = ((midway * density) + keyboardHeight + BottomInset) / _frame!.LayoutParameters!.Height;
            _behavior.HalfExpandedRatio = (float)ratio;

            // Set the bottom detent using the peekHeight
            _behavior.PeekHeight = (int)(bottom * density) + BottomInset + keyboardHeight;
        }
    }

    private double CalculateTallestDetent(double heightConstraint)
    {
        if (_heights is null)
        {
            CalculateHeights(heightConstraint);
        }
        return _heights!.Values.Max();
    }

    private void ResizeVirtualView()
    {
        if (_sheet?.Handler?.PlatformView is not ContentViewGroup pv) return;
        
        var maxHeight = GetAvailableHeight();
        var height = CalculateTallestDetent(maxHeight);
        var density = DeviceDisplay.MainDisplayInfo.Density;
        var platformHeight = (int)Math.Round(height * density);

        pv.LayoutParameters = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            platformHeight
        );

        var layoutParams = _frame!.LayoutParameters;
        if (layoutParams is not null)
        {
            layoutParams.Height = platformHeight + BottomInset + KeyboardHeight;
            if (height.Equals(maxHeight))
            {
                layoutParams.Height += TopInset;
            }    
        }
        
        _sheet.Arrange(new Rect(0, 0, _frame.Width / density, height));
    }

    internal void Show(bool animated)
    {
        var context = _mauiContext!.Context!;
        _isDuringShowingAnimation = true;

        EnsureWindowContainer(context);
        _stayOnFront!.AddView(_windowContainer);
        _frame!.RemoveAllViews();

        // The Android view for the page could already have a ContainerView as a parent if it was shown as a bottom sheet before
        if (_sheet!.Handler?.PlatformView is ContentViewGroup contentViewGroup) contentViewGroup.RemoveFromParent();
        
        var c = new FrameLayout(context);

        if (_sheet.HasHandle)
        {
            _handle = new BottomSheetDragHandleView(context);
            c.AddView(_handle, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent));
        }

        var containerView = _sheet.ToPlatform(_mauiContext);
        c.AddView(containerView);

        _frame.AddView(c);

        UpdateHasBackdrop();
        UpdateHandleColor();

        if (animated)
        {
            _windowContainer?.Backdrop.AnimateIn();
            _behavior!.State = BottomSheetBehavior.StateHidden;
        }

        _sheet.Dispatcher.Dispatch(() =>
        {
            ResizeVirtualView();

            CalculateHeights(GetAvailableHeight());
            CalculateStates();
            Layout();
            UpdateBackground();

            var state = GetStateForDetent(_sheet.SelectedDetent);

            var defaultDetent = _sheet.GetDefaultDetent();
            if (state is -1)
            {
                state = _behavior!.SkipCollapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
            }
            _behavior!.State = state;

            containerView.LayoutChange += OnLayoutChange;
            _sheet.NotifyShowing();
        });
    }

    private void OnLayoutChange(object? sender, AView.LayoutChangeEventArgs e)
    {
        _sheet?.Dispatcher.Dispatch(() =>
        {
            CalculateHeights(GetAvailableHeight());
            CalculateStates();
            ResizeVirtualView();
            Layout();
        });
    }

    private void Callback_StateChanged(object? sender, EventArgs e)
    {
        if (_isDuringShowingAnimation && (
            _behavior!.State == BottomSheetBehavior.StateCollapsed
            || _behavior.State == BottomSheetBehavior.StateHalfExpanded
            || _behavior.State == BottomSheetBehavior.StateExpanded
            ))
        {
            _isDuringShowingAnimation = false;
            _behavior.Hideable = _sheet!.IsCancelable;
            _sheet.NotifyShown();
        }
        if (_behavior!.State == BottomSheetBehavior.StateHidden)
        {
            Dispose();
            _sheet!.NotifyDismissed();
        }
        ((BottomSheetHandler)_sheet!.Handler!).UpdateSelectedDetent(_sheet);
    }

    internal void UpdateSelectedDetent()
    {
        var detent = GetDetentForState(_behavior!.State);
        if (detent is not null)
        {
            _sheet!.SelectedDetent = detent;
        }
    }

    internal void UpdateStateFromDetent()
    {
        if (_sheet?.SelectedDetent is null || _behavior is null || _states is null)
        {
            return;
        }
        _behavior.State = GetStateForDetent(_sheet.SelectedDetent);
    }

    internal void UpdateHasBackdrop()
    {
        _windowContainer?.SetBackdropVisibility(_sheet?.HasBackdrop ?? false);
    }
    
    #endregion Methods
    
    #region Helper classes
    
    private class EdgeToEdgeCallback : BottomSheetBehavior.BottomSheetCallback
    {
        private readonly BottomSheetController _controller;
        private readonly WindowInsetsCompat _insetsCompat;

        private AWindow? _window;
        private bool _isStatusBarLight;

        public EdgeToEdgeCallback(BottomSheetController controller, WindowInsetsCompat insetsCompat)
        {
            _controller = controller;
            _insetsCompat = insetsCompat;
            SetPaddingForPosition(_controller._frame);
        }

        public override void OnStateChanged(AView bottomSheet, int p1)
        {
            SetPaddingForPosition(bottomSheet);
        }

        public override void OnSlide(AView bottomSheet, float newState)
        {
            SetPaddingForPosition(bottomSheet);
        }

        internal void SetWindow(AWindow? window)
        {
            if (_window == window) return;
            _window = window;
            
            if (window != null)
            {
                WindowInsetsControllerCompat insetsController = WindowCompat.GetInsetsController(window, window.DecorView);
                _isStatusBarLight = insetsController.AppearanceLightStatusBars;
            }
        }

        private int TopInset
        {
            get
            {
                if (OperatingSystem.IsAndroidVersionAtLeast(30))
                {
                    return _insetsCompat.GetInsetsIgnoringVisibility(Android.Views.WindowInsets.Type.SystemBars()).Top;
                }
#pragma warning disable CS0618
                return _insetsCompat.StableInsetTop;
#pragma warning restore CS0618
            }
        }

        private void SetPaddingForPosition(AView bottomSheet)
        {
            var keyboardHeight = _insetsCompat.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
            if (bottomSheet.Top < TopInset)
            {
                // If the bottomsheet is light, we should set light status bar so the icons are visible
                // since the bottomsheet is now under the status bar.
                if (_window != null)
                {
                    EdgeToEdgeUtils.SetLightStatusBar(
                        _window, !_controller._isBackgroundLight.HasValue ? _isStatusBarLight : _controller._isBackgroundLight.Value);
                }
                // Smooth transition into status bar when drawing edge to edge.
                bottomSheet.SetPadding(
                    bottomSheet.PaddingLeft,
                    TopInset - bottomSheet.Top,
                    bottomSheet.PaddingRight,
                    keyboardHeight);
            }
            else if (bottomSheet.Top != 0)
            {
                // Reset the status bar icons to the original color because the bottomsheet is not under the
                // status bar.
                if (_window != null)
                {
                    EdgeToEdgeUtils.SetLightStatusBar(_window, _isStatusBarLight);
                }
                bottomSheet.SetPadding(
                    bottomSheet.PaddingLeft,
                    0,
                    bottomSheet.PaddingRight,
                    keyboardHeight);
            }
        }
    }
    
    private class EdgeToEdgeListener(BottomSheetController controller) : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        private EdgeToEdgeCallback? _edgeToEdgeCallback;

        public WindowInsetsCompat OnApplyWindowInsets(AView v, WindowInsetsCompat? insets)
        {
            if (_edgeToEdgeCallback is not null)
            {
                controller._behavior.RemoveBottomSheetCallback(_edgeToEdgeCallback);
            }

            if (insets != null)
            {
                _edgeToEdgeCallback = new EdgeToEdgeCallback(controller, insets);
                _edgeToEdgeCallback.SetWindow((controller._mauiContext.Context as AppCompatActivity)?.Window);
                controller._behavior.AddBottomSheetCallback(_edgeToEdgeCallback);
                controller.CalculateHeights(controller.GetAvailableHeight());
                controller.ResizeVirtualView();
                controller.Layout();
            }


            return ViewCompat.OnApplyWindowInsets(v, insets);
        }
    }
    
    private class BottomSheetInsetsAnimationCallback(BottomSheetController controller)
        : WindowInsetsAnimationCompat.Callback(DispatchModeStop)
    {
        private int _startHeight;
        private int _endHeight;

        public override WindowInsetsAnimationCompat.BoundsCompat OnStart(WindowInsetsAnimationCompat animation, WindowInsetsAnimationCompat.BoundsCompat bounds)
        {
            _endHeight = controller.WindowInsets.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
            controller._frame.TranslationY = _endHeight - _startHeight;
            return bounds;
        }

        public override void OnPrepare(WindowInsetsAnimationCompat animation)
        {
            _startHeight = controller.WindowInsets.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
            base.OnPrepare(animation);
        }

        public override WindowInsetsCompat OnProgress(WindowInsetsCompat insets, IList<WindowInsetsAnimationCompat> runningAnimations)
        {
            WindowInsetsAnimationCompat? imeAnimation = null;
            foreach (var animation in runningAnimations)
            {
                if ((animation.TypeMask & WindowInsetsCompat.Type.Ime()) != 0)
                {
                    imeAnimation = animation;
                    break;
                }
            }
            if (imeAnimation != null)
            {
                controller._frame.TranslationY = (_endHeight - _startHeight) * (1 - imeAnimation.InterpolatedFraction);
            }
            return insets;
        }
    }
    
    private class SheetRadiusDrawable: GradientDrawable
    {
        public void SetCornerRadius(int radius)
        {
            SetCornerRadii([radius, radius, radius, radius, 0, 0, 0, 0]);
        }
    }
    
    private class StayOnFrontView(Context context) : FrameLayout(context)
    {
        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (Parent is not ViewGroup vg) return;
            vg.ChildViewAdded += ParentChildViewAdded;
            vg.ChildViewRemoved += ParentChildViewRemoved;
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            if (Parent is not ViewGroup vg) return;
            vg.ChildViewAdded -= ParentChildViewAdded;
            vg.ChildViewRemoved -= ParentChildViewRemoved;
        }
        
        private void ParentChildViewAdded(object? sender, ChildViewAddedEventArgs e) => BringToFront();

        private void ParentChildViewRemoved(object? sender, ChildViewRemovedEventArgs e) => BringToFront();

        public void SetContentView(AView view)
        {
            RemoveAllViews();
            AddView(view);
        }
    }
    
    private sealed class BottomSheetBackdrop : AView
    {
        private const string AlphaProperty = "alpha";
        private const float AlphaVisible = .5f;
        private const float AlphaInvisible = 0f;
        private static readonly Android.Graphics.Color BackgroundColor = Android.Graphics.Color.Black; 
        private readonly int _animationDuration;
        
        // TODO: Configure backdrop color and animation duration
        public BottomSheetBackdrop(Context context) : base(context)
        {
            _animationDuration = context.Resources!.GetInteger(Resource.Integer.bottom_sheet_slide_duration);
            
            Clickable = true;
            Background = new ColorDrawable(BackgroundColor);
            Alpha = AlphaVisible;
        }

        public void AnimateIn()
        {
            var alphaAnimator = ObjectAnimator.OfFloat(this, AlphaProperty, AlphaInvisible, AlphaVisible);
            if (alphaAnimator == null) return;
            
            alphaAnimator.SetDuration(_animationDuration);
            alphaAnimator.Start();
        }

        public void AnimateOut()
        {
            var alphaAnimator = ObjectAnimator.OfFloat(this, AlphaProperty, AlphaVisible, AlphaInvisible);
            if (alphaAnimator == null) return;
            
            alphaAnimator.SetDuration(_animationDuration);
            alphaAnimator.Start();
        }
    }
    
    private sealed class BottomSheetContainer : FrameLayout
    {
        public BottomSheetBackdrop Backdrop { get; }

        public BottomSheetContainer(Context context, AView contentView) : base(context)
        {
            Backdrop = new BottomSheetBackdrop(context);
            AddView(Backdrop);
            AddView(contentView);
        }

        public void SetBackdropVisibility(bool hasBackdrop)
        {
            Backdrop.Visibility = hasBackdrop ? ViewStates.Visible : ViewStates.Gone;
        }
    }
    
    private class BottomSheetCallback(MaterialBottomSheet page) : BottomSheetBehavior.BottomSheetCallback
    {
        public event EventHandler? StateChanged;

        public override void OnSlide(AView bottomSheet, float newState)
        {
        }

        public override void OnStateChanged(AView view, int newState)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    #endregion Helper classes
}
