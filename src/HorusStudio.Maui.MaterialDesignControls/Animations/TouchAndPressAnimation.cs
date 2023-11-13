using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class TouchAndPressAnimation
    {
        public static void Animate(View view, EventType gestureType)
        {
            var touchAndPressBehaviorConsumer = view as ITouchAndPressBehaviorConsumer;

            switch (gestureType)
            {
                case EventType.Pressing:
                    SetAnimation(view, touchAndPressBehaviorConsumer);
                    break;
                case EventType.Cancelled:
                case EventType.Released:
                    touchAndPressBehaviorConsumer.ExecuteAction();
                    RestoreAnimation(view, touchAndPressBehaviorConsumer);
                    break;
                case EventType.Ignored:
                    RestoreAnimation(view, touchAndPressBehaviorConsumer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gestureType), gestureType, null);
            }
        }

        private static void SetAnimation(View view, ITouchAndPressBehaviorConsumer touchAndPressBehaviorConsumer)
        {
            if (touchAndPressBehaviorConsumer.Animation != AnimationTypes.None && touchAndPressBehaviorConsumer.IsEnabled)
            {
                Task.Run(async () =>
                {
                    if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Fade)
                        await view.FadeTo(touchAndPressBehaviorConsumer.AnimationParameter.HasValue ? touchAndPressBehaviorConsumer.AnimationParameter.Value : 0.6, 100);
                    else if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Scale)
                        await view.ScaleTo(touchAndPressBehaviorConsumer.AnimationParameter.HasValue ? touchAndPressBehaviorConsumer.AnimationParameter.Value : 0.95, 100);
                    else if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Custom && touchAndPressBehaviorConsumer.CustomAnimation != null)
                        await touchAndPressBehaviorConsumer.CustomAnimation.SetAnimation(view);
                });
            }
        }

        private static void RestoreAnimation(View view, ITouchAndPressBehaviorConsumer touchAndPressBehaviorConsumer)
        {
            if (touchAndPressBehaviorConsumer.Animation != AnimationTypes.None)
            {
                Task.Run(async () =>
                {
                    if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Fade)
                        await view.FadeTo(1, 100);
                    else if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Scale)
                        await view.ScaleTo(1, 100);
                    else if (touchAndPressBehaviorConsumer.Animation == AnimationTypes.Custom && touchAndPressBehaviorConsumer.CustomAnimation != null)
                        await touchAndPressBehaviorConsumer.CustomAnimation.RestoreAnimation(view);
                });
            }
        }
    }
}