using Android.Views;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchBehavior
	{
        #region Attributes

        private Android.Views.View? _view;
        private ITouchableView? _touchableElement;
        private float? _firstX;
        private float? _firstY;
        private bool _ignored;

        #endregion Attributes

        /// <inheritdoc/>
        protected override void OnAttachedTo(Microsoft.Maui.Controls.View bindable, Android.Views.View platformView)
        {
            base.OnAttachedTo(bindable, platformView);

            _view = platformView as Android.Views.View;
            if (_view != null && bindable is ITouchableView touchAndPressBehaviorConsumer)
            {
                _view.Touch += OnViewOnTouch;
                _touchableElement = touchAndPressBehaviorConsumer;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachedFrom(Microsoft.Maui.Controls.View bindable, Android.Views.View platformView)
        {
            base.OnDetachedFrom(bindable, platformView);

            if (_view != null)
                _view.Touch -= OnViewOnTouch;
        }

        private void OnViewOnTouch(object? sender, Android.Views.View.TouchEventArgs e)
        {
            switch (e?.Event?.ActionMasked)
            {
                case MotionEventActions.ButtonPress:
                case MotionEventActions.Down:
                case MotionEventActions.Pointer1Down:
                    _touchableElement?.OnTouch(TouchEventType.Pressed);
                    break;
                case MotionEventActions.Cancel:
                    _touchableElement?.OnTouch(TouchEventType.Cancelled);
                    break;
                case MotionEventActions.Move:
                    var motionEvent = e.Event;
                    if (motionEvent != null)
                    {
                        var x = motionEvent.GetX();
                        var y = motionEvent.GetY();

                        if (!_firstX.HasValue || !_firstY.HasValue)
                        {
                            _firstX = x;
                            _firstY = y;
                        }

                        var maxDelta = 10;
                        var deltaX = Math.Abs(x - _firstX.Value);
                        var deltaY = Math.Abs(y - _firstY.Value);
                        if (!_ignored && (deltaX > maxDelta || deltaY > maxDelta))
                        {
                            _ignored = true;
                            _touchableElement?.OnTouch(TouchEventType.Ignored);
                        }
                    }
                    break;
                case MotionEventActions.ButtonRelease:
                case MotionEventActions.Pointer1Up:
                case MotionEventActions.Up:
                    _touchableElement?.OnTouch(TouchEventType.Released);
                    break;
                case MotionEventActions.HoverEnter:
                case MotionEventActions.HoverExit:
                case MotionEventActions.HoverMove:
                case MotionEventActions.Mask:
                case MotionEventActions.Outside:
                case MotionEventActions.Pointer2Down:
                case MotionEventActions.Pointer2Up:
                case MotionEventActions.Pointer3Down:
                case MotionEventActions.Pointer3Up:
                case MotionEventActions.PointerIdMask:
                case MotionEventActions.PointerIdShift:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e.Event.ActionMasked), e.Event.ActionMasked, null);
            }

            if (e.Event.ActionMasked != MotionEventActions.Move)
            {
                _ignored = false;
                _firstX = null;
                _firstY = null;
            }
        }
    }
}