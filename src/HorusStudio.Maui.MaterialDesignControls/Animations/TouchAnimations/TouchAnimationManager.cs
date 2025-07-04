using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls
{
    internal static class TouchAnimationManager
    {
        internal static async Task AnimateAsync(View view, TouchEventType gestureType)
        {
            if (view == null)
            {
                return;
            }

            switch (gestureType)
            {
                case TouchEventType.Pressed:
                    await BeginPressAnimation(view);
                    break;
                case TouchEventType.Cancelled:
                case TouchEventType.Released:
                case TouchEventType.Ignored:
                    await BeginReleaseAnimation(view);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gestureType), gestureType, null);
            }
        }

        private static async Task BeginPressAnimation(View view)
        {
            var touchableElement = view as ITouchableView;

            if (touchableElement != null
                && touchableElement.IsEnabled)
            {
                if (touchableElement.TouchAnimation != null)
                {
                    await touchableElement.TouchAnimation.BeginPressAnimation(view);
                }
                else if (touchableElement.TouchAnimationType != TouchAnimationTypes.None)
                {
                    switch (touchableElement.TouchAnimationType)
                    {
                        case TouchAnimationTypes.Fade:
                            var fadeAnimation = new FadeAnimation();
                            await fadeAnimation.BeginPressAnimation(view);
                            break;
                        case TouchAnimationTypes.Scale:
                            var scaleAnimation = new ScaleAnimation();
                            await scaleAnimation.BeginPressAnimation(view);
                            break;
                        case TouchAnimationTypes.Bounce:
                            var bounceAnimation = new BounceAnimation();
                            await bounceAnimation.BeginPressAnimation(view);
                            break;
                    }
                }
            }
        }

        private static async Task BeginReleaseAnimation(View view)
        {
            var touchableElement = view as ITouchableView;

            if (touchableElement != null)
            {
                if (touchableElement.TouchAnimation != null)
                {
                    await touchableElement.TouchAnimation.BeginReleaseAnimation(view);
                }
                else if (touchableElement.TouchAnimationType != TouchAnimationTypes.None)
                {
                    switch (touchableElement.TouchAnimationType)
                    {
                        case TouchAnimationTypes.Fade:
                            var fadeAnimation = new FadeAnimation();
                            await fadeAnimation.BeginReleaseAnimation(view);
                            break;
                        case TouchAnimationTypes.Scale:
                            var scaleAnimation = new ScaleAnimation();
                            await scaleAnimation.BeginReleaseAnimation(view);
                            break;
                        case TouchAnimationTypes.Bounce:
                            var bounceAnimation = new BounceAnimation();
                            await bounceAnimation.BeginReleaseAnimation(view);
                            break;
                    }
                }
            }
        }
    }
}