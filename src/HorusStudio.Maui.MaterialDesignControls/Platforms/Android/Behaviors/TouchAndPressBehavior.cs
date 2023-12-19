using Android.Views;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchAndPressBehavior : PlatformBehavior<Microsoft.Maui.Controls.View>
	{
        #region Attributes

        private Android.Views.View _view;
        private ITouchAndPressBehaviorConsumer _touchAndPressBehaviorConsumer;
        private float? _firstX;
        private float? _firstY;
        private bool _ignored;

        #endregion Attributes

        /// <inheritdoc/>
        protected override void OnAttachedTo(Microsoft.Maui.Controls.View bindable, Android.Views.View platformView)
        {
            base.OnAttachedTo(bindable, platformView);

            _view = platformView as Android.Views.View;
            if (_view != null && bindable is ITouchAndPressBehaviorConsumer touchAndPressBehaviorConsumer)
            {
                _view.Touch += OnViewOnTouch;
                _touchAndPressBehaviorConsumer = touchAndPressBehaviorConsumer;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachedFrom(Microsoft.Maui.Controls.View bindable, Android.Views.View platformView)
        {
            base.OnDetachedFrom(bindable, platformView);

            if (_view != null)
                _view.Touch -= OnViewOnTouch;
        }

        private void OnViewOnTouch(object sender, Android.Views.View.TouchEventArgs e)
        {
            switch (e.Event.ActionMasked)
            {
                case MotionEventActions.ButtonPress:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Pressing);
                    break;
                case MotionEventActions.ButtonRelease:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Released);
                    break;
                case MotionEventActions.Cancel:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Cancelled);
                    break;
                case MotionEventActions.Down:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Pressing);
                    break;
                case MotionEventActions.HoverEnter:
                    break;
                case MotionEventActions.HoverExit:
                    break;
                case MotionEventActions.HoverMove:
                    break;
                case MotionEventActions.Mask:
                    break;
                case MotionEventActions.Move:
                    var motionEvent = e.Event as MotionEvent;

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
                            _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Ignored);
                        }
                    }
                    break;
                case MotionEventActions.Outside:
                    break;
                case MotionEventActions.Pointer1Down:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Pressing);
                    break;
                case MotionEventActions.Pointer1Up:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Released);
                    break;
                case MotionEventActions.Pointer2Down:
                    break;
                case MotionEventActions.Pointer2Up:
                    break;
                case MotionEventActions.Pointer3Down:
                    break;
                case MotionEventActions.Pointer3Up:
                    break;
                case MotionEventActions.PointerIdMask:
                    break;
                case MotionEventActions.PointerIdShift:
                    break;
                case MotionEventActions.Up:
                    _touchAndPressBehaviorConsumer?.ConsumeEvent(EventType.Released);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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