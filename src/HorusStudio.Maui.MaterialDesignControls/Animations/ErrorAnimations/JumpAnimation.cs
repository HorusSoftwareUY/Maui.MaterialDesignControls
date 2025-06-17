namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that creates a "jump" effect by translating the target element along the Y-axis.
    /// </summary>
    /// <todoList>
    /// * [iOS] Jump animation doesn't work
    /// </todoList>
    public class JumpAnimation : IErrorAnimation
    {
        /// <summary>
        /// Gets or sets the Y-axis movement of the animation.
        /// </summary>
        public double Movement { get; set; } = -25;

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
                MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (validableView is View view)
                    {
                        view.Animate(nameof(JumpAnimation), GetAnimation(view), 16, Convert.ToUInt32(Duration));
                    }
                });
            });
        }

        private Animation GetAnimation(View view)
        {
            var animation = new Animation();

            animation.WithConcurrent(
              (f) => view.TranslationY = f,
              view.TranslationY, view.TranslationX,
              Easing.Linear, 0, 0.2);

            animation.WithConcurrent(
              (f) => view.TranslationY = f,
              view.TranslationY + Movement, view.TranslationX,
              Easing.Linear, 0.2, 0.4);

            animation.WithConcurrent(
             (f) => view.TranslationY = f,
             view.TranslationY, view.TranslationX,
             Easing.Linear, 0.5, 1.0);

            return animation;
        }
    }
}