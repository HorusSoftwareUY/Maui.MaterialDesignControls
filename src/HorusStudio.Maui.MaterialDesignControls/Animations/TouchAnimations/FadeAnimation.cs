namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that simulates a "fade" effect by changing the opacity over the target element.
    /// </summary>
	public class FadeAnimation : ITouchAnimation
	{
        /// <summary>
        /// Gets or sets the target opacity of the animation.
        /// </summary>
        public double Opacity { get; set; } = 0.6;

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
                    await view.FadeTo(Opacity, (uint)Duration);
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
                    await view.FadeTo(1, (uint)Duration);
                });
            });
        }
    }
}