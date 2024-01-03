using UIKit;
using PlatformView = UIKit.UIView;
using VirtualView = Microsoft.Maui.Controls.View;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

public partial class LongTouchBehavior : PlatformBehavior<VirtualView>
{
    #region Attributes

    private PlatformView _view;
    private UIGestureRecognizer _gestureRecognizer;

    #endregion Attributes

    /// <inheritdoc/>
    protected override void OnAttachedTo(VirtualView bindable, PlatformView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        _view = platformView;
        if (_view != null)
        {
            _view.UserInteractionEnabled = true;

            _gestureRecognizer = new UILongPressGestureRecognizer(OnViewLongClick);
            _view.AddGestureRecognizer(_gestureRecognizer);
        }
    }

    /// <inheritdoc/>
    protected override void OnDetachedFrom(VirtualView bindable, PlatformView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        if (_view != null && _gestureRecognizer != null)
            _view.RemoveGestureRecognizer(_gestureRecognizer);
    }
}