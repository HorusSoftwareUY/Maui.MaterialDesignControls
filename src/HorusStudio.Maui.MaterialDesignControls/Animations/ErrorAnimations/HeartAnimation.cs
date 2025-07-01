namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that performs a "heartbeat" effect by scaling the target element in a pulsating manner.
    /// </summary>
    public class HeartAnimation : IErrorAnimation
    {
        /// <summary>
        /// Gets or sets the scale of the animation.
        /// </summary>
        public double Scale { get; set; } = 1.1;

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
                MainThreadExtensions.SafeRunOnUiThreadAsync(async () =>
                {
                    if (validableView is View view)
                    {
                        view.Animate(nameof(HeartAnimation), GetAnimation(view), 16, Convert.ToUInt32(Duration));
                    }
                });
            });
        }

        private Animation GetAnimation(View view)
        {
            var animation = new Animation();

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale, view.Scale,
               Easing.Linear, 0, 0.1);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale, view.Scale * Scale,
               Easing.Linear, 0.1, 0.4);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale * Scale, view.Scale,
               Easing.Linear, 0.4, 0.5);

            animation.WithConcurrent(
                (f) => view.Scale = f,
                view.Scale, view.Scale * Scale,
                Easing.Linear, 0.5, 0.8);

            animation.WithConcurrent(
               (f) => view.Scale = f,
               view.Scale * Scale, view.Scale,
               Easing.Linear, 0.8, 1);

            return animation;
        }
    }
}