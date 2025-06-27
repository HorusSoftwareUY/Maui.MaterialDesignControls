namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// The ITouchAnimation interface serves as a base for touch animations.
    /// </summary>
    public interface ITouchAnimation
    {
        /// <summary>
        /// Run the animation on the target element when the user press the view.
        /// </summary>
        /// <param name="view">Target element</param>
        Task BeginPressAnimation(View view);

        /// <summary>
        /// Run the animation on the target element when the user release, ignore or cancel the touch gesture.
        /// </summary>
        /// <param name="view">Target element</param>
        Task BeginReleaseAnimation(View view);
    }
}