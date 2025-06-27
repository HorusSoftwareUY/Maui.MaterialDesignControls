namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that simulates a "shake" effect by moving the target element back and forth along the X-axis.
    /// </summary>
    public class ShakeAnimation : IErrorAnimation
    {
        /// <summary>
        /// Gets or sets the X-axis movement of the animation.
        /// </summary>
        public double Movement { get; set; } = 5;

        /// <summary>
        /// Gets or sets the duration of the animation in milliseconds.
        /// </summary>
        public int Duration { get; set; } = 1000;

        /// <summary>
        /// Run the animation on the target element
        /// </summary>
        /// <param name="validableView">Target element</param>
        public Task BeginAnimation(IValidableView validableView)
        {
            if (validableView == null)
            {
                throw new NullReferenceException("The view parameter is required.");
            }

            return Task.Run(() =>
            {
                // TODO: Use the new MainThread extension
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (validableView is View view)
                    {
                        view.Animate(nameof(ShakeAnimation), GetAnimation(view), 16, Convert.ToUInt32(Duration));
                    }
                });
            });
        }

        private Animation GetAnimation(View view)
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