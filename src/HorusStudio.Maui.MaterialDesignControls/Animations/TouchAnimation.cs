using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class TouchAnimation
    {
        public static async Task AnimateAsync(View view, TouchType gestureType)
        {
            switch (gestureType)
            {
                case TouchType.Pressed:
                    await SetAnimationAsync(view);
                    break;
                case TouchType.Cancelled:
                case TouchType.Released:
                case TouchType.Ignored:
                    await RestoreAnimationAsync(view);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gestureType), gestureType, null);
            }
        }

        private static async Task SetAnimationAsync(View view)
        {
            var touchableElement = view as ITouchable;

            if (touchableElement.Animation != AnimationTypes.None && touchableElement.IsEnabled)
            {
                if (touchableElement.Animation == AnimationTypes.Fade)
                    await view.FadeTo(touchableElement.AnimationParameter ?? 0.6, 100);
                else if (touchableElement.Animation == AnimationTypes.Scale)
                    await view.ScaleTo(touchableElement.AnimationParameter ?? 0.95, 100);
                else if (touchableElement.Animation == AnimationTypes.Custom && touchableElement.CustomAnimation != null)
                    await touchableElement.CustomAnimation.SetAnimationAsync(view);
            }
        }

        private static async Task RestoreAnimationAsync(View view)
        {
            var touchableElement = view as ITouchable;

            if (touchableElement.Animation != AnimationTypes.None)
            {
                if (touchableElement.Animation == AnimationTypes.Fade)
                    await view.FadeTo(1, 100);
                else if (touchableElement.Animation == AnimationTypes.Scale)
                    await view.ScaleTo(1, 100);
                else if (touchableElement.Animation == AnimationTypes.Custom && touchableElement.CustomAnimation != null)
                    await touchableElement.CustomAnimation.RestoreAnimationAsync(view);
            }
        }
    }
}