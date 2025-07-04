namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// The IErrorAnimation interface serves as a base for error animations.
    /// </summary>
	public interface IErrorAnimation
	{
        /// <summary>
        /// Gets or sets the duration of the animation in milliseconds.
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// Run the animation on the target element
        /// </summary>
        /// <param name="validableView">Target element</param>
        Task BeginAnimation(IValidableView validableView);
    }
}