namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// The IValidableView interface serves as a base for validable views.
    /// </summary>
	public interface IValidableView
	{
        /// <summary>
        /// Gets or sets the animation type to be executed when the control has an error.
        /// </summary>
        ErrorAnimationTypes ErrorAnimationType { get; set; }

        /// <summary>
        /// Gets or sets a custom animation to be executed when the control has an error.
        /// </summary>
        IErrorAnimation ErrorAnimation { get; set; }
    }
}