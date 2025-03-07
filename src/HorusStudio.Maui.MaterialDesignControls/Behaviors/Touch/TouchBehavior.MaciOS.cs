using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchBehavior
    {
        #region Attributes

        private UIView? _view;
        private TouchGestureRecognizer? _touchGestureRecognizer;

        #endregion Attributes

        /// <inheritdoc/>
        protected override void OnAttachedTo(View bindable, UIView platformView)
        {
            base.OnAttachedTo(bindable, platformView);

            _view = platformView as UIView;
            if (_view != null && bindable is ITouchable touchableElement)
            {
                _view.UserInteractionEnabled = true;
                _touchGestureRecognizer = new TouchGestureRecognizer(touchableElement);
                _view.AddGestureRecognizer(_touchGestureRecognizer);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachedFrom(View bindable, UIView platformView)
        {
            base.OnDetachedFrom(bindable, platformView);

            if (_view != null && _touchGestureRecognizer != null)
                _view.RemoveGestureRecognizer(_touchGestureRecognizer);
        }
    }
}