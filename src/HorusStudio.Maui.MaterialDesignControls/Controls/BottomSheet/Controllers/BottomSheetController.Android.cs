using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Google.Android.Material.Internal;
using Google.Android.Material.Color;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using AWindow = Android.Views.Window;
using Color = Microsoft.Maui.Graphics.Color;
using Insets = AndroidX.Core.Graphics.Insets;
using Paint = Microsoft.Maui.Graphics.Paint;
using Rect = Microsoft.Maui.Graphics.Rect;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// 
/// </summary>
/// <param name="mauiContext"></param>
/// <param name="showNextToAppBarLayout">If the sheet is to be shown "inside" the page, i.e. under any flyout page,
/// and vertically above the navigation bars, tab bars if any.
/// In such a case, we add it as a sibling of the <see cref="AppBarLayout"/>.
/// </param>
internal class BottomSheetController(IMauiContext mauiContext, MaterialBottomSheet sheet, bool showNextToAppBarLayout)
{
    #region Attributes

    /// <summary>
    /// If the sheet is to be shown "inside" the page, i.e. under any flyout page,
    /// and vertically above the navigation bars, tab bars if any.
    /// In such a case, we add it as a sibling of the <see cref="AppBarLayout"/>.
    /// </summary>
    //private readonly bool _showNextToAppBarLayout = showNextToAppBarLayout;

    private double _density;
    private bool _isShowInitializing;
    private bool? _isBackgroundLight;

    private BottomSheetContainerView? _containerView;
    private BottomSheetDragHandleView? _handleView;
    private BottomSheetBehavior? _behavior;

    private readonly Dictionary<Detent, int> _states = new();
    private Dictionary<Detent, double> _heights = new();
    private AWindow? _window;
    private double _heightsComputedForHeight;
    
    #endregion Attributes
    
    #region Constants
    
    private const int PlatformViewId = 245695;
    /// <summary>
    /// TODO: move that in the maui view (MaterialBottomSheet)
    /// </summary>
    private const bool UseNavigationBarArea = false;
    
    #endregion Constants
    
    /// <summary>
    /// The sheet's handler is null here
    /// </summary>
    public void CreateViews()
    {
        _density = DeviceDisplay.MainDisplayInfo.Density;
        EnsureWindowContainer();
        _handleView!.Visibility = ViewStates.Gone;
    }

    public void Show(AWindow? window, AView platformView, bool animated)
    {
        if(window?.DecorView is not ViewGroup parentView) return;

        _window = window;

        platformView.Id = PlatformViewId;
        _containerView!.BottomSheetView.RemoveAllViews();
        _containerView!.BottomSheetView.AddView(_handleView, new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent));
        _containerView!.BottomSheetView.AddView(platformView, new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent));
        
        if (showNextToAppBarLayout)
        {
            var firstAppBar = parentView.GetFirstChildOfType<AppBarLayout>();
            if (firstAppBar?.Parent is ViewGroup vg)
            {
                parentView = vg;
            }
        }

        //Start as hidden if animated. Otherwise, start in the default state.
        if (animated)
        {
            _behavior!.Hideable = true;
            _behavior.State = BottomSheetBehavior.StateHidden;
        }
        
        parentView.AddView(_containerView);
        _containerView!.BottomSheetView.Post(() => _containerView!.BottomSheetView.RequestLayout()); //Post required

        if (animated)
        {
            _containerView.Backdrop.AnimateIn();
        }
        
        _isShowInitializing = true;
        sheet.NotifyShowing();

        _containerView.Post(() =>
        {
            Layout();
            //Post required before changing state
            _behavior!.State = GetStateForDetent(sheet.SelectedDetent); //Triggers OnStateChanged

            //TODO: reverse that dependency !
            platformView.LayoutChange += OnLayoutChange;
        });
    }

    public void Dismiss(bool animated)
    {
        _isShowInitializing = false;
        _behavior!.Hideable = true;

        if (animated)
        {
            _containerView!.Backdrop.AnimateOut();
            _behavior.State = BottomSheetBehavior.StateHidden;
        }
        else
        {
            WhenDismissed();
        }
    }
    
    #region Updates from handlers's mappers
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// When a corner is set, the background of the bottom sheet is automatically set to a TopCornersRadiusDrawable.
    /// When the background color is set, the original drawable is updated.
    /// By combining both updates, we can ensure that the background color is always set on the correct drawable.
    /// </remarks>
    public void UpdateBackground(Brush? backgroundBrush, double cornerRadius)
    {
        var bottomSheetView = _containerView?.BottomSheetView;
        if (bottomSheetView is null) return;
        
        SetCornerRadius(bottomSheetView, cornerRadius);
        SetBackground(bottomSheetView, backgroundBrush);
    }

    public void UpdateHandle(Color? color, float? opacity)
    {
        if (_handleView is null) return;
        
        if (color is null)
        {
            _handleView.SetColorFilter(null);
        }
        else
        {
            _handleView.SetColorFilter(color.ToPlatform());    
        }
        _handleView.Alpha = opacity ?? 1f;
    }
    
    public void UpdateHasHandle(bool hasHandle) => _handleView!.Visibility = hasHandle ? ViewStates.Visible : ViewStates.Gone;

    public void UpdateSelectedDetent()
    {
        if (_behavior is null) return;
        var detent = GetDetentForState(_behavior.State);
        
        if (sheet.SelectedDetent == detent)
            sheet.SelectedDetent = null; //make sure to trigger a change
        
        sheet.SelectedDetent = detent;
    }

    public void UpdateStateFromDetent(Detent? detent)
    {
        if (_behavior is null) return;
        _behavior.State = GetStateForDetent(detent);
    }
    
    public void UpdateHasBackdrop(bool hasBackdrop) => _containerView?.SetBackdropVisibility(hasBackdrop);
    public void UpdateBackdropColor(Color? color) => _containerView?.SetBackdropColor(color);
    public void UpdateBackdropOpacity(float? opacity) => _containerView?.SetBackdropOpacity(opacity);
    
    #endregion Updates from handlers's mappers
    
    #region Helpers
    
    private void OnLayoutChange(object? sender, AView.LayoutChangeEventArgs e)
    {
        Layout();
    }
    
    private void Layout()
    {
        var maxHeight = GetAvailableHeight();
        if (maxHeight <= 0) return;
        
        CalculateHeights(maxHeight);
        CalculateStates();
        LayoutDetents();
        ResizeBottomSheetContentView();
    }

    private void WhenDismissed()
    {
        _containerView!.BottomSheetView.FindViewById(PlatformViewId)!.LayoutChange -= OnLayoutChange;
        _containerView.Backdrop.Click -= BackdropClicked;
        _containerView.BottomSheetView.RemoveAllViews();
        _containerView.RemoveFromParent();
        
        sheet.NotifyDismissed();
    }

    private void CalculateHeights(double maxSheetHeight)
    {
        _heights = sheet.GetEnabledDetents().ToDictionary(
            detent => detent,
            detent => detent.GetHeight(sheet, maxSheetHeight));
    }

    private void CalculateStates()
    {
        _states.Clear();
        
        var allHeights = _heights.OrderByDescending(kv => kv.Value).ToList();
        switch (allHeights.Count)
        {
            case 1:
                _states.Add(allHeights[0].Key, BottomSheetBehavior.StateCollapsed);
                break;
            case 2:
                _states.Add(allHeights[0].Key, BottomSheetBehavior.StateExpanded);
                _states.Add(allHeights[1].Key, BottomSheetBehavior.StateCollapsed);
                break;
            case 3:
                _states.Add(allHeights[0].Key, BottomSheetBehavior.StateExpanded);
                _states.Add(allHeights[1].Key, BottomSheetBehavior.StateHalfExpanded);
                _states.Add(allHeights[2].Key, BottomSheetBehavior.StateCollapsed);
                break;
        }
    }

    private int GetStateForDetent(Detent? detent)
    {
        var state = -1;
        if (detent is not null)
            _states.TryGetValue(detent, out state);
        
        if (state is -1)
            state = BottomSheetBehavior.StateCollapsed;
        if (state is BottomSheetBehavior.StateCollapsed && (_behavior.SkipCollapsed || !sheet.IsCancelable))
            state = BottomSheetBehavior.StateExpanded;

        return state;
    }
    
    private Detent GetDetentForState(int state)  => _states.FirstOrDefault(kv => kv.Value == state).Key;
    
    private void EnsureWindowContainer()
    {
        if (_containerView is not null) return;
        
        _handleView = new BottomSheetDragHandleView(mauiContext.Context!);
        _containerView = BottomSheetContainerView.Create(mauiContext.Context!);
        _containerView.Backdrop.Click += BackdropClicked;
        ViewCompat.SetOnApplyWindowInsetsListener(_containerView, new EdgeToEdgeListener(this));

        _behavior = BottomSheetBehavior.From(_containerView.BottomSheetView);
        _behavior.AddBottomSheetCallback(new BottomSheetCallback(OnStateChanged));
    }

    private void BackdropClicked(object? sender, EventArgs e)
    {
        if (sheet.IsCancelable)
        {
            Dismiss(true);
        }
    }
    
    private void OnStateChanged(int newState)
    {
        if (_isShowInitializing 
            && newState is BottomSheetBehavior.StateCollapsed or BottomSheetBehavior.StateHalfExpanded or BottomSheetBehavior.StateExpanded or BottomSheetBehavior.StateHidden)
        {
            _isShowInitializing = false;

            if (newState == BottomSheetBehavior.StateHidden)
            {
                _behavior.State = GetStateForDetent(sheet.SelectedDetent);
                return;
            }
            _behavior.Hideable = sheet.IsCancelable;
            sheet.NotifyShown();
        }
        
        if (newState is BottomSheetBehavior.StateHidden
            && _containerView.Parent is not null)
        {
            WhenDismissed();
            return;
        }
        
        UpdateSelectedDetent();
    }

    private WindowInsetsCompat GetWindowInsets()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(23) && _containerView.RootWindowInsets is not null)
            return WindowInsetsCompat.ToWindowInsetsCompat(_containerView.RootWindowInsets);

        return WindowInsetsCompat.Consumed;
    }

    private int BottomInset() => UseNavigationBarArea ? 0 : GetInsets().Bottom;

    private Insets GetInsets()
    {
        var insets = GetWindowInsets();
        if (OperatingSystem.IsAndroidVersionAtLeast(30))
            return insets.GetInsetsIgnoringVisibility(Android.Views.WindowInsets.Type.SystemBars());
        
#pragma warning disable CS0618 // Type or member is obsolete
        return Insets.Of(insets.StableInsetLeft, insets.StableInsetTop, insets.StableInsetRight, insets.StableInsetBottom);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    private int KeyboardHeight() => GetWindowInsets().GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
    
    private int TopInset() => GetInsets().Top;

    /// <returns>Height in maui points</returns>
    private double GetAvailableHeight() 
        => (_containerView.Height - TopInset() - BottomInset() - KeyboardHeight()) / _density;

    /// <summary>
    /// Sets behavior properties
    /// </summary>
    private void LayoutDetents()
    {
        var maxSheetHeight = GetAvailableHeight();

        // Android supports the following detents:
        // - expanded (top of screen - offset)
        // - half expanded (using ratio of expanded - peekHeight)
        // - collapsed (using peekHeight)

        var sortedHeights = _heights
            .OrderByDescending(i => i.Value)
            .ToList();

        var keyboardHeight = KeyboardHeight();

        var top = sortedHeights[0].Value;
        var bottomInset = BottomInset();

        // Configure the sheet to handle up to 3 detents

        if (sortedHeights.Count == 1)
        { 
            // Only way to have one detent on Android is to use fitToContent. Use that
            _behavior.FitToContents = true;
            _behavior.SkipCollapsed = true;
        }
        else if (sortedHeights.Count == 2)
        { 
            // We can handle a second detent by adding a collapsed state. Use peek height
            _behavior.FitToContents = true;
            _behavior.SkipCollapsed = false;

            var bottom = sortedHeights[1].Value;
            _behavior.PeekHeight = (int)(bottom * _density) + bottomInset + keyboardHeight;
        }
        else if (sortedHeights.Count == 3)
        { 
            // 3 detents can be done using the peek height AND disabling fitToContent
            // Doing so uses a property called halfExpandedRatio, giving us
            // Expanded: Use ExpandedOffset to offset from the top
            // HalfExpanded: Use HalfExpandedRatio
            // Collapsed: Use PeekHeight

            _behavior.FitToContents = false;
            _behavior.SkipCollapsed = false;

            var midway = sortedHeights[1].Value;
            var bottom = sortedHeights[2].Value;

            // Set the top detent by offsetting the requested height from the maxHeight
            var topOffset = (maxSheetHeight - top) * _density;
            _behavior.ExpandedOffset = Math.Max(0, (int)topOffset);

            // Set the midway detent by calculating the ratio using the top detent info
            //var availableHeight = BottomSheetView.LayoutParameters.Height; //not computed yet
            var availableHeight = maxSheetHeight * _density;
            var ratio = ((midway * _density) + keyboardHeight + bottomInset) / availableHeight; 
            _behavior.HalfExpandedRatio = MathF.Min(0.99f, MathF.Max(0.01f, (float)ratio));

            // Set the bottom detent using the peekHeight
            _behavior.PeekHeight = (int)(bottom * _density) + bottomInset + keyboardHeight;
        }
    }

    /// <param name="maxHeight">in maui points</param>
    /// <returns>height in maui points</returns>
    private double CalculateTallestDetent(double maxHeight)
    {
        if (!maxHeight.Equals(_heightsComputedForHeight))
        {
            CalculateHeights(maxHeight);
            _heightsComputedForHeight = maxHeight;
        }

        return _heights.Values.Max();
    }

    private void ResizeBottomSheetContentView()
    {
        var maxHeight = GetAvailableHeight();
        if (maxHeight <= 0) return;

        var height = CalculateTallestDetent(maxHeight);
        var platformHeight = (int)Math.Round(height * _density) + BottomInset() + KeyboardHeight();

        //If extended to max, add top inset
        if (height >= maxHeight)
        {
            platformHeight += TopInset();
            height = maxHeight;
        }

        //Resize virtual view
        if (sheet.Handler?.PlatformView is ContentViewGroup bottomSheetContentView)
        {
            var newBounds = new Rect(0, 0, _containerView!.BottomSheetView.Width / _density, height);
            bottomSheetContentView.CrossPlatformLayout?.CrossPlatformArrange(newBounds);

            //Required, otherwise the bottomSheet is full screen. Why ? No idea. WrapContent seems to be ignored.
            var lp = (FrameLayout.LayoutParams)bottomSheetContentView.LayoutParameters!;
            if (lp.Height != platformHeight)
                lp.Height = platformHeight;
        }
    }
    
    private void SetCornerRadius(AView view, double cornerRadius)
    {
        if (cornerRadius < 0) return;
        
        TopCornerRadiusDrawable drawable;
        if (view.Background is not TopCornerRadiusDrawable sheetRadiusDrawable)
        {
            drawable = new TopCornerRadiusDrawable();
            view.Background = drawable;
        }
        else
            drawable = sheetRadiusDrawable;
        
        drawable.SetTopCornerRadius(Convert.ToInt32(view.Context.ToPixels(cornerRadius)));
    }
    
    private void SetBackground(AView view, Brush? backgroundBrush)
    {
        Paint paint = backgroundBrush;
        if (paint?.ToColor() is not { } color) return;
        
        var platformColor = color.ToPlatform();
        if (view.Background is TopCornerRadiusDrawable sheetDrawable)
        {
            sheetDrawable.SetColor(platformColor);
        }
        else
        {
            view.SetBackgroundColor(platformColor);
        }

        CheckBackgroundColorForStatusBar(view);
    }

    private void CheckBackgroundColorForStatusBar(AView view)
    {
        // Try to find the background color to automatically change the status bar icons so they will
        // still be visible when the bottomsheet slides underneath the status bar.
        var backgroundTint = ViewCompat.GetBackgroundTintList(view);
            
        // First check for a tint
        if (backgroundTint != null)
        {
            _isBackgroundLight = MaterialColors.IsColorLight(backgroundTint.DefaultColor);
        }
        // Then check for the background color
        else if (view.Background is ColorDrawable drawable)
        {
            _isBackgroundLight = MaterialColors.IsColorLight(drawable.Color);
        }
        // Otherwise don't change the status bar color
        else
        {
            _isBackgroundLight = null;
        }
    }

    #endregion Helpers

    #region Listeners and callbacks
        
    private class EdgeToEdgeListener(BottomSheetController controller) : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        private EdgeToEdgeCallback? _edgeToEdgeCallback;

        public WindowInsetsCompat? OnApplyWindowInsets(AView? view, WindowInsetsCompat? insets)
        {
            if (_edgeToEdgeCallback != null && controller._behavior != null)
            {
                controller._behavior.RemoveBottomSheetCallback(_edgeToEdgeCallback);
                _edgeToEdgeCallback = null;
            }

            if (insets != null && controller._behavior != null)
            {
                _edgeToEdgeCallback = new EdgeToEdgeCallback(controller, insets);
                controller._behavior.AddBottomSheetCallback(_edgeToEdgeCallback);
                controller.Layout();
            }

            return ViewCompat.OnApplyWindowInsets(view, insets);
        }


        private class EdgeToEdgeCallback : BottomSheetBehavior.BottomSheetCallback
        {
            private readonly BottomSheetController _controller;
            private readonly WindowInsetsCompat _insetsCompat;
            private AWindow? Window => _controller._window;
            private readonly bool _isStatusBarLight;

            public EdgeToEdgeCallback(BottomSheetController controller, WindowInsetsCompat insetsCompat)
            {
                _controller = controller;
                _insetsCompat = insetsCompat;

                SetPaddingForPosition(controller._containerView!.BottomSheetView);

                if (Window == null) return;
                var insetsController = WindowCompat.GetInsetsController(Window, Window.DecorView);
                _isStatusBarLight = insetsController?.AppearanceLightStatusBars ?? true;
            }

            public override void OnStateChanged(AView bottomSheet, int newState) => SetPaddingForPosition(bottomSheet);
            public override void OnSlide(AView bottomSheet, float percent) => SetPaddingForPosition(bottomSheet);

            private int TopInset()
            {
                if (OperatingSystem.IsAndroidVersionAtLeast(30))
                    return _insetsCompat.GetInsetsIgnoringVisibility(WindowInsets.Type.SystemBars())?.Top ?? 0;

#pragma warning disable CS0618
                return _insetsCompat.StableInsetTop;
#pragma warning restore CS0618
            }

            /// <summary>
            /// TODO: replace obsolete EdgeToEdgeUtils with androidx.compose.foundation
            /// </summary>
            /// <param name="bottomSheet"></param>
            private void SetPaddingForPosition(AView bottomSheet)
            {
                var keyboardHeight = _controller.KeyboardHeight();
                var topInset = TopInset();

                if (bottomSheet.Top < topInset)
                {
                    // If the bottomsheet is light, we should set light status bar so the icons are visible since the bottomsheet is now under the status bar.
                    if (Window != null)
                        EdgeToEdgeUtils.SetLightStatusBar(Window, _controller._isBackgroundLight ?? _isStatusBarLight);

                    // Smooth transition into status bar when drawing edge to edge.
                    bottomSheet.SetPadding(bottomSheet.PaddingLeft, topInset - bottomSheet.Top, bottomSheet.PaddingRight, keyboardHeight);
                }
                else if (bottomSheet.Top != 0)
                {
                    // Reset the status bar icons to the original color because the bottomsheet is not under the status bar.
                    if (Window != null)
                        EdgeToEdgeUtils.SetLightStatusBar(Window, _isStatusBarLight);

                    bottomSheet.SetPadding(bottomSheet.PaddingLeft, 0, bottomSheet.PaddingRight, keyboardHeight);
                }
            }
        }
    }
    
    private class BottomSheetCallback(Action<int> stateChanged) : BottomSheetBehavior.BottomSheetCallback
    {
        public override void OnStateChanged(AView view, int newState) => stateChanged(newState);

        public override void OnSlide(AView bottomSheet, float newState) {}
    }
    
    #endregion Listeners and callbacks
}
