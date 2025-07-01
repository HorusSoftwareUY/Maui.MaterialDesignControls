namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that simulates a "sink" or "sunken" effect by scaling the target element.
    /// </summary>
	public class ScaleAnimation : ITouchAnimation
    {
        /// <summary>
        /// Gets or sets the target scale of the animation.
        /// </summary>
        public double Scale { get; set; } = 0.95;

        /// <summary>
        /// Gets or sets the duration of the animation in milliseconds.
        /// </summary>
        public int Duration { get; set; } = 100;

        /// <summary>
        /// Run the press animation on the target element
        /// </summary>
        /// <param name="view">Target element</param>
        public Task BeginPressAnimation(View view)
        {
            if (view == null)
            {
                throw new NullReferenceException("The view parameter is required.");
            }

            return Task.Run(() =>
            {
                MainThreadExtensions.SafeRunOnUiThreadAsync(async () =>
                {
                    await view.ScaleTo(Scale, (uint)Duration);
                });
            });
        }

        /// <summary>
        /// Run the release animation on the target element
        /// </summary>
        /// <param name="view">Target element</param>
        public Task BeginReleaseAnimation(View view)
        {
            if (view == null)
            {
                throw new NullReferenceException("The view parameter is required.");
            }

            return Task.Run(() =>
            {
                MainThreadExtensions.SafeRunOnUiThreadAsync(async () =>
                {
                    await view.ScaleTo(1, (uint)Duration);
                });
            });
        }
    }
}