namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents an animation that simulates a "sink" or "sunken" effect with a "bounce" effect when the user releases the target element.
    /// </summary>
	public class BounceAnimation : ITouchAnimation
    {
        /// <summary>
        /// Gets or sets the target scale of the animation.
        /// </summary>
        public double Scale { get; set; } = 0.95;

        /// <summary>
        /// Gets or sets the duration of the press animation in milliseconds.
        /// </summary>
        public int PressAnimationDuration { get; set; } = 100;

        /// <summary>
        /// Gets or sets the duration of the release animation in milliseconds.
        /// </summary>
        public int ReleaseAnimationDuration { get; set; } = 500;

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
                    await view.ScaleTo(Scale, (uint)PressAnimationDuration);
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
                    await view.ScaleTo(1, (uint)ReleaseAnimationDuration, Easing.BounceOut);
                });
            });
        }
    }
}