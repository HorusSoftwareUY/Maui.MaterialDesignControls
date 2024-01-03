using Foundation;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

class LongTouchGestureRecognizer : UIGestureRecognizer
{
    #region Attributes

    private readonly ITouchable _touchableElement;

    #endregion

    public LongTouchGestureRecognizer(ITouchable touchableElement)
    {
        _touchableElement = touchableElement;
    }

    public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        base.PressesBegan(presses, evt);
        _touchableElement.OnTouch(TouchType.Pressed);
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        _touchableElement.OnTouch(TouchType.Pressed);
    }

    public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        base.PressesEnded(presses, evt);
        _touchableElement.OnTouch(TouchType.Released);
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);
        _touchableElement.OnTouch(TouchType.Released);
    }

    public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        base.PressesCancelled(presses, evt);
        _touchableElement.OnTouch(TouchType.Cancelled);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);
        _touchableElement.OnTouch(TouchType.Cancelled);
    }

    public override void IgnoreTouch(UITouch touch, UIEvent forEvent)
    {
        base.IgnoreTouch(touch, forEvent);
        _touchableElement.OnTouch(TouchType.Ignored);
    }
}