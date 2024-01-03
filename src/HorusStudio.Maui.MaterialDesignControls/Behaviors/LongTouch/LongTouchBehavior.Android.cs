using PlatformView = Android.Views.View;
using VirtualView = Microsoft.Maui.Controls.View;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

public partial class LongTouchBehavior : PlatformBehavior<VirtualView>
{
    #region Attributes

    private PlatformView _view;

    #endregion Attributes

    /// <inheritdoc/>
    protected override void OnAttachedTo(VirtualView bindable, PlatformView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        _view = platformView;
        if (_view != null)
        {
            _view.LongClickable = true;
            _view.LongClick += OnViewLongClick;
        }
    }

    /// <inheritdoc/>
    protected override void OnDetachedFrom(VirtualView bindable, PlatformView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        if (_view != null)
            _view.LongClick -= OnViewLongClick;
    }

    private void OnViewLongClick(object sender, PlatformView.LongClickEventArgs e)
    {
        OnViewLongClick();
    }
}