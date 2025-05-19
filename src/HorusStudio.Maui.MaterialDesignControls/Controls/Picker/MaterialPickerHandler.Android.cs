using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialPickerHandler
{
    private Android.Views.Window.ICallback _windowCallback;
    
    public static void MapBorder(IPickerHandler handler, IPicker picker)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    public new static void MapHorizontalTextAlignment(IPickerHandler handler, IPicker picker)
    {
        if (picker is CustomPicker customPicker)
        {
            handler.PlatformView.Gravity = customPicker.HorizontalTextAlignment.ToGravityFlags();
        }
    }

    protected override void ConnectHandler(MauiPicker platformView)
    {
        base.ConnectHandler(platformView);
        
        var editText = platformView as EditText;
        if (editText != null)
        {
            editText.FocusChange += (sender, e) =>
            {
                if (e.HasFocus)
                {
                    
                    
                    _windowCallback = Platform.CurrentActivity.Window.Callback;
                    Platform.CurrentActivity.Window.Callback = new CustomWindowCallback(_windowCallback, () =>
                    {
                        VirtualView.Unfocus();
                        RestoreWindowCallback();
                    });
                }
                else
                {
                    RestoreWindowCallback();
                }
            };
        }
    }
    
    protected override void DisconnectHandler(MauiPicker platformView)
    {
        base.DisconnectHandler(platformView);
        RestoreWindowCallback();
    }

    private void RestoreWindowCallback()
    {
        if (_windowCallback != null)
            Platform.CurrentActivity.Window.Callback = _windowCallback;

        _windowCallback = null;
    }
}

class CustomWindowCallback : Java.Lang.Object, Android.Views.Window.ICallback
{
    private readonly Android.Views.Window.ICallback _originalCallback;
    private readonly System.Action _pickerUnfocus;

    public CustomWindowCallback(Android.Views.Window.ICallback originalCallback, System.Action pickerUnfocus)
    {
        _originalCallback = originalCallback;
        _pickerUnfocus = pickerUnfocus;
    }

    public void OnWindowFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            // View/page receives the focus, so we invoke the unfocus on the picker control
            _pickerUnfocus?.Invoke();
        }
        
        _originalCallback?.OnWindowFocusChanged(hasFocus);
    }

    public ActionMode? OnWindowStartingActionMode(ActionMode.ICallback? callback, ActionModeType type) => _originalCallback?.OnWindowStartingActionMode(callback, type) ?? null;
    public void OnAttachedToWindow() => _originalCallback?.OnAttachedToWindow();
    public void OnContentChanged() => _originalCallback?.OnContentChanged();
    public bool OnCreatePanelMenu(int featureId, IMenu menu) => _originalCallback?.OnCreatePanelMenu(featureId, menu) ?? false;
    public Android.Views.View? OnCreatePanelView(int featureId) => _originalCallback?.OnCreatePanelView(featureId) ?? null;
    public void OnDetachedFromWindow() => _originalCallback?.OnDetachedFromWindow();
    public bool OnMenuItemSelected(int featureId, IMenuItem item) => _originalCallback?.OnMenuItemSelected(featureId, item) ?? false;
    public bool OnMenuOpened(int featureId, IMenu menu) => _originalCallback?.OnMenuOpened(featureId, menu) ?? false;
    public void OnPanelClosed(int featureId, IMenu menu) => _originalCallback?.OnPanelClosed(featureId, menu);
    public bool OnPreparePanel(int featureId, Android.Views.View? view, IMenu menu) => _originalCallback?.OnPreparePanel(featureId, view, menu) ?? false;
    public bool OnSearchRequested() => _originalCallback?.OnSearchRequested() ?? false;
    public bool OnSearchRequested(SearchEvent? searchEvent) => _originalCallback?.OnSearchRequested(searchEvent) ?? false;
    public void OnWindowAttributesChanged(WindowManagerLayoutParams attrs) => _originalCallback?.OnWindowAttributesChanged(attrs);
    public bool DispatchKeyEvent(KeyEvent e) => _originalCallback?.DispatchKeyEvent(e) ?? false;
    public bool DispatchKeyShortcutEvent(KeyEvent e) => _originalCallback?.DispatchKeyShortcutEvent(e) ?? false;
    public bool DispatchPopulateAccessibilityEvent(AccessibilityEvent? e) => _originalCallback?.DispatchPopulateAccessibilityEvent(e) ?? false;
    public bool DispatchTouchEvent(MotionEvent e) => _originalCallback?.DispatchTouchEvent(e) ?? false;
    public bool DispatchTrackballEvent(MotionEvent e) => _originalCallback?.DispatchTrackballEvent(e) ?? false;
    public bool DispatchGenericMotionEvent(MotionEvent e) => _originalCallback?.DispatchGenericMotionEvent(e) ?? false;
    public ActionMode OnWindowStartingActionMode(ActionMode.ICallback callback) => _originalCallback?.OnWindowStartingActionMode(callback);
    public ActionMode OnWindowStartingActionMode(ActionMode.ICallback callback, int type) => _originalCallback?.OnWindowStartingActionMode(callback, (ActionModeType)type);
    public void OnActionModeStarted(ActionMode mode) => _originalCallback?.OnActionModeStarted(mode);
    public void OnActionModeFinished(ActionMode mode) => _originalCallback?.OnActionModeFinished(mode);
}