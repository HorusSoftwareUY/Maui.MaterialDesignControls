using Microsoft.Maui.Controls.Platform;

#if ANDROID
using Android.Views;
using View = Android.Views.View;
#endif

namespace HorusStudio.Maui.MaterialDesignControls
{
    public class TouchReleaseEffect : RoutingEffect
    {
        public Action OnRelease { get; set; }

        public TouchReleaseEffect(Action onRelease)
        {
            OnRelease = onRelease;
        }
    }

#if ANDROID
    internal class TouchReleasePlatformEffect : PlatformEffect
    {
        private View _view;
        private Action _onRelease;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            
            if (_view != null)
            {
                var touchReleaseEffect = (MaterialDesignControls.TouchReleaseEffect)Element.Effects.FirstOrDefault(x => x is MaterialDesignControls.TouchReleaseEffect);
                if (touchReleaseEffect != null && touchReleaseEffect.OnRelease != null)
                {
                    _onRelease = touchReleaseEffect.OnRelease;
                    _view.Touch += OnViewOnTouch;
                }
            }
        }

        protected override void OnDetached()
        {
            if (_view != null)
                _view.Touch -= OnViewOnTouch;
        }

        private void OnViewOnTouch(object sender, View.TouchEventArgs e)
        {
            e.Handled = false;

            if (e.Event.ActionMasked == MotionEventActions.Up)
            {
                System.Diagnostics.Debug.WriteLine(e.Event.ActionMasked);
                _onRelease.Invoke();
            }
        }
    }
#endif
}