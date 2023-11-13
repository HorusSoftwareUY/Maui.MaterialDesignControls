using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Platforms.iOS.GestureRecognizers
{
    internal class TouchAndPressGestureRecognizer : UIGestureRecognizer
    {
        private readonly ITouchAndPressBehaviorConsumer _touchAndPressBehaviorConsumer;

        public TouchAndPressGestureRecognizer(ITouchAndPressBehaviorConsumer touchAndPressBehaviorConsumer)
        {
            _touchAndPressBehaviorConsumer = touchAndPressBehaviorConsumer;
        }

        public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesBegan(presses, evt);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Pressing);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Pressing);
        }

        public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesEnded(presses, evt);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Released);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Released);
        }

        public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesCancelled(presses, evt);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Cancelled);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Cancelled);
        }

        public override void IgnoreTouch(UITouch touch, UIEvent forEvent)
        {
            base.IgnoreTouch(touch, forEvent);
            _touchAndPressBehaviorConsumer.ConsumeEvent(EventType.Ignored);
        }
    }
}