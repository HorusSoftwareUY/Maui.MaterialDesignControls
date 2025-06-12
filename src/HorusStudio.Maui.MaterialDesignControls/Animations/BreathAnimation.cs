namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that performs a "heartbeat" effect by scaling the target element in a pulsating manner.
    /// </summary>
    public static class BreathAnimation
    {
        /// <summary>
        /// Run the animation on the target element
        /// </summary>
        /// <param name="view">Target element</param>
        /// <param name="duration">Animation duration in milliseconds</param>
        public static Task BeginAnimation(View view, double duration)
        {
            if (view == null)
            {
                throw new NullReferenceException("The view parameter is required.");
            }

            return Task.Run(() =>
            {
                // TODO: Use the new MainThread extension
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    view.Animate(nameof(BreathAnimation), GetAnimation(view), 16, Convert.ToUInt32(duration));
                });
            });
        }

        private static Animation GetAnimation(View view)
        {
            var animation = new Animation();

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale, view.Scale,
               Easing.Linear, 0, 0.1);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale, view.Scale * 1.1,
               Easing.Linear, 0.1, 0.4);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale * 1.1, view.Scale,
               Easing.Linear, 0.4, 0.5);

            animation.WithConcurrent(
                (f) => view.Scale = f,
                view.Scale, view.Scale * 1.1,
                Easing.Linear, 0.5, 0.8);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale * 1.1, view.Scale,
               Easing.Linear, 0.8, 1);

            return animation;
        }
    }
}