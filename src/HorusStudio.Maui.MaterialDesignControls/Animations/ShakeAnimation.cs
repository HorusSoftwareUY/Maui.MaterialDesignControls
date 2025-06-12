namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that simulates a "shake" effect by moving the target element back and forth along the X-axis.
    /// </summary>
    public static class ShakeAnimation
    {
        private const int Movement = 5;

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
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    view.Animate(nameof(ShakeAnimation), GetAnimation(view), 16, Convert.ToUInt32(duration));
                });
            });
        }

        private static Animation GetAnimation(View view)
        {
            var animation = new Animation();

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX + Movement, view.TranslationX,
                Easing.Linear, 0, 0.1);

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX - Movement, view.TranslationX,
                Easing.Linear, 0.1, 0.2);

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX + Movement, view.TranslationX,
                Easing.Linear, 0.2, 0.3);

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX - Movement, view.TranslationX,
                Easing.Linear, 0.3, 0.4);

            animation.WithConcurrent(
                 (f) => view.TranslationX = f,
                 view.TranslationX + Movement, view.TranslationX,
                 Easing.Linear, 0.4, 0.5);

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX - Movement, view.TranslationX,
                Easing.Linear, 0.5, 0.6);

            animation.WithConcurrent(
                 (f) => view.TranslationX = f,
                 view.TranslationX + Movement, view.TranslationX,
                 Easing.Linear, 0.6, 0.7);

            animation.WithConcurrent(
                (f) => view.TranslationX = f,
                view.TranslationX - Movement, view.TranslationX,
                Easing.Linear, 0.7, 0.8);

            return animation;
        }
    }
}