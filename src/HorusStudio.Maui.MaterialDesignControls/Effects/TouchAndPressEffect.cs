using Microsoft.Maui.Controls.Platform;

#if ANDROID
using Android.Views;
using View = Android.Views.View;
#elif IOS
using Foundation;
using UIKit;
#endif

namespace HorusStudio.Maui.MaterialDesignControls
{
    public interface ITouchAndPressEffectConsumer
    {
        void ConsumeEvent(EventType gestureType);
        bool IsEnabled { get; set; }
        AnimationTypes Animation { get; set; }
        ICustomAnimation CustomAnimation { get; set; }
        double? AnimationParameter { get; set; }
        void ExecuteAction();
    }

    public enum EventType
    {
        Pressing,
        Released,
        Cancelled,
        Ignored
    }

    public class TouchAndPressEffect : RoutingEffect
    { }

#if ANDROID
    internal class TouchAndPressPlatformEffect : PlatformEffect
    {
        private View _view;
        private ITouchAndPressEffectConsumer _touchAndPressEffectConsumer;
        private float? firstX;
        private float? firstY;
        private bool ignored;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            
            if (_view != null && Element is ITouchAndPressEffectConsumer touchAndPressEffectConsumer)
            {
                _view.Touch += OnViewOnTouch;
                _touchAndPressEffectConsumer = touchAndPressEffectConsumer;
            }
        }

        protected override void OnDetached()
        {
            if (_view != null)
                _view.Touch -= OnViewOnTouch;
        }

        private void OnViewOnTouch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.ActionMasked)
            {
                case MotionEventActions.ButtonPress:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Pressing);
                    break;
                case MotionEventActions.ButtonRelease:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Released);
                    break;
                case MotionEventActions.Cancel:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Cancelled);
                    break;
                case MotionEventActions.Down:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Pressing);
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

                        if (!this.firstX.HasValue || !this.firstY.HasValue)
                        {
                            this.firstX = x;
                            this.firstY = y;
                        }

                        var maxDelta = 10;
                        var deltaX = Math.Abs(x - this.firstX.Value);
                        var deltaY = Math.Abs(y - this.firstY.Value);
                        if (!this.ignored && (deltaX > maxDelta || deltaY > maxDelta))
                        {
                            this.ignored = true;
                            _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Ignored);
                        }
                    }
                    break;
                case MotionEventActions.Outside:
                    break;
                case MotionEventActions.Pointer1Down:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Pressing);
                    break;
                case MotionEventActions.Pointer1Up:
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Released);
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
                    _touchAndPressEffectConsumer?.ConsumeEvent(EventType.Released);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (e.Event.ActionMasked != MotionEventActions.Move)
            {
                this.ignored = false;
                this.firstX = null;
                this.firstY = null;
            }
        }
    }
#elif IOS
    internal class TouchAndPressPlatformEffect : PlatformEffect
    {
        private UIView _view;
        private TouchAndPressGestureRecognizer _touchAndPressGestureRecognizer;
        
        protected override void OnAttached()
        {
            _view = Control ?? Container;
            
            if (Element is ITouchAndPressEffectConsumer touchAndPressEffectConsumer)
            {
                _view.UserInteractionEnabled = true;
                
                _touchAndPressGestureRecognizer = new TouchAndPressGestureRecognizer(touchAndPressEffectConsumer);
                _view.AddGestureRecognizer(_touchAndPressGestureRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (_view != null && _touchAndPressGestureRecognizer != null)
                _view.RemoveGestureRecognizer(_touchAndPressGestureRecognizer);
        }
    }
    
    internal class TouchAndPressGestureRecognizer : UIGestureRecognizer
    {
        private readonly ITouchAndPressEffectConsumer _touchAndPressEffectConsumer;

        public TouchAndPressGestureRecognizer(ITouchAndPressEffectConsumer touchAndPressEffectConsumer)
        {
            _touchAndPressEffectConsumer = touchAndPressEffectConsumer;
        }

        public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesBegan(presses, evt);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Pressing);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Pressing);
        }

        public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesEnded(presses, evt);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Released);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Released);
        }

        public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesCancelled(presses, evt);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Cancelled);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Cancelled);
        }

        public override void IgnoreTouch(UITouch touch, UIEvent forEvent)
        {
            base.IgnoreTouch(touch, forEvent);
            _touchAndPressEffectConsumer.ConsumeEvent(EventType.Ignored);
        }
    }
#endif
}