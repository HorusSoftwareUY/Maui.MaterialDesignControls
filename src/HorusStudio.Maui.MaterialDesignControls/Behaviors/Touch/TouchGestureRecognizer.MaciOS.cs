using Foundation;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
    class TouchGestureRecognizer : UIGestureRecognizer
    {
        #region Attributes

        private readonly ITouchableView _touchableElement;

        #endregion

        public TouchGestureRecognizer(ITouchableView touchableElement)
        {
            _touchableElement = touchableElement;
        }

        public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesBegan(presses, evt);
            _touchableElement.OnTouch(TouchEventType.Pressed);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            _touchableElement.OnTouch(TouchEventType.Pressed);
        }

        public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesEnded(presses, evt);
            _touchableElement.OnTouch(TouchEventType.Released);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            _touchableElement.OnTouch(TouchEventType.Released);
        }

        public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesCancelled(presses, evt);
            _touchableElement.OnTouch(TouchEventType.Cancelled);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            _touchableElement.OnTouch(TouchEventType.Cancelled);
        }

        public override void IgnoreTouch(UITouch touch, UIEvent forEvent)
        {
            base.IgnoreTouch(touch, forEvent);
            _touchableElement.OnTouch(TouchEventType.Ignored);
        }
    }
}