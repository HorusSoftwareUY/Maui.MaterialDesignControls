using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors.Touch
{
    /// <summary>
    /// This implementation is based on the following internal implementation of MAUI:
    /// https://github.com/dotnet/maui/blob/f65f1eaad38b7e41d352383479f54b410d637c75/src/Controls/src/Core/Platform/GestureManager/GesturePlatformManager.iOS.cs#L735
    /// </summary>
	internal class ShouldReceiveTouchProxy
	{
        #region Attributes

        private View _virtualView;
        private UIView _platformView;
        private static double _lastTouchTimestamp;

        #endregion Attributes

        public ShouldReceiveTouchProxy(View virtualView, UIView platformView)
        {
            _virtualView = virtualView;
            _platformView = platformView;
        }

        public bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
        {
            if (_virtualView == null || _platformView == null)
            {
                return false;
            }

            if (_virtualView.InputTransparent)
            {
                return false;
            }

            if (!_virtualView.IsEnabled)
            {
                return false;
            }

            if (touch.View == _platformView)
            {
                _lastTouchTimestamp = touch.Timestamp;
                return true;
            }

            // If the touch is coming from the UIView our handler is wrapping (e.g., if it's  
            // wrapping a UIView which already has a gesture recognizer), then we should let it through
            // (This goes for children of that control as well)
            var isDescendantOfView = touch.View.IsDescendantOfView(_platformView);
            var touchViewGestureRecognizers = touch.View.GestureRecognizers != null ? touch.View.GestureRecognizers.Length : 0;
            var platformViewGestureRecognizers = _platformView.GestureRecognizers != null ? _platformView.GestureRecognizers.Length : 0;

            if (isDescendantOfView &&
                (touchViewGestureRecognizers > 0 || platformViewGestureRecognizers > 0))
            {
                if (touch.View is UIButton)
                {
                    return false;
                }
                else if (_lastTouchTimestamp == touch.Timestamp)
                {
                    return false;
                }
                else
                {
                    _lastTouchTimestamp = touch.Timestamp;
                    return true;
                }
            }

            return false;
        }
    }
}