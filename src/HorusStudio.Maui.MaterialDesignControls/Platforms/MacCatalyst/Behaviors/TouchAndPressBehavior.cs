using HorusStudio.Maui.MaterialDesignControls.GestureRecognizers;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchAndPressBehavior : PlatformBehavior<View>
    {
        #region Attributes

        private UIView _view;
        private TouchAndPressGestureRecognizer _touchAndPressGestureRecognizer;

        #endregion Attributes

        /// <inheritdoc/>
        protected override void OnAttachedTo(View bindable, UIView platformView)
        {
            base.OnAttachedTo(bindable, platformView);

            _view = platformView as UIView;
            if (_view != null && bindable is ITouchAndPressBehaviorConsumer touchAndPressBehaviorConsumer)
            {
                _view.UserInteractionEnabled = true;
                _touchAndPressGestureRecognizer = new TouchAndPressGestureRecognizer(touchAndPressBehaviorConsumer);
                _view.AddGestureRecognizer(_touchAndPressGestureRecognizer);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachedFrom(View bindable, UIView platformView)
        {
            base.OnDetachedFrom(bindable, platformView);

            if (_view != null && _touchAndPressGestureRecognizer != null)
                _view.RemoveGestureRecognizer(_touchAndPressGestureRecognizer);
        }
    }
}