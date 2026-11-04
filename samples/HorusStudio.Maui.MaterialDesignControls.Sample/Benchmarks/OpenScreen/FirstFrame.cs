namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.OpenScreen;

/// <summary>
/// Calls back once, on the UI thread, when the platform first draws a frame
/// that contains <paramref name="view"/> with a real size — the moment the user
/// can see it.
///
/// Android: a ViewTreeObserver draw listener on the view's native peer, so the
/// end point is the native draw pass itself, not a MAUI event (Loaded fires
/// before anything is on screen). Other platforms fall back to the first
/// SizeChanged plus one dispatch, which is good enough for a sample but is not
/// what the numbers in the docs are measured with.
/// </summary>
public static class FirstFrame
{
    public static void Await(View view, Action onFirstFrame)
    {
#if ANDROID
        void Attach()
        {
            if (view.Handler?.PlatformView is not global::Android.Views.View native)
                return;

            var listener = new DrawListener(native, onFirstFrame);
            native.ViewTreeObserver?.AddOnDrawListener(listener);
            native.Invalidate();
        }

        if (view.Handler is not null)
            Attach();
        else
            view.HandlerChanged += OnHandlerChanged;

        void OnHandlerChanged(object? sender, EventArgs e)
        {
            view.HandlerChanged -= OnHandlerChanged;
            Attach();
        }
#else
        void OnSizeChanged(object? sender, EventArgs e)
        {
            if (view.Width <= 0) return;
            view.SizeChanged -= OnSizeChanged;
            view.Dispatcher.Dispatch(onFirstFrame);
        }
        view.SizeChanged += OnSizeChanged;
#endif
    }

#if ANDROID
    private sealed class DrawListener : Java.Lang.Object, global::Android.Views.ViewTreeObserver.IOnDrawListener
    {
        private readonly global::Android.Views.View _native;
        private Action? _callback;

        public DrawListener(global::Android.Views.View native, Action callback)
        {
            _native = native;
            _callback = callback;
        }

        public void OnDraw()
        {
            // Wait for the draw pass in which the view is laid out with a size.
            if (_callback is null || !_native.IsLaidOut || _native.Width == 0 || _native.Height == 0)
                return;

            var callback = _callback;
            _callback = null;
            callback();

            // A listener can't be removed from inside its own OnDraw.
            _native.Post(() => _native.ViewTreeObserver?.RemoveOnDrawListener(this));
        }
    }
#endif
}
