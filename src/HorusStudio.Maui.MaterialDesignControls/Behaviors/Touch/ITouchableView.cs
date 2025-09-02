using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
    /// <summary>
    /// The ITouchableView interface serves as a base for touchable views.
    /// </summary>
	public interface ITouchableView
	{
        /// <summary>
        /// Method invoked when a touch event occurs on the view, updating the gesture state for that view.
        /// </summary>
        void OnTouch(TouchEventType touchEventType);

        /// <summary>
        /// Occurs when the view is touched.
        /// </summary>
        event EventHandler<TouchEventArgs>? Touch;

        /// <summary>
        /// Gets or sets the state when the view is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets an animation to be executed when view is clicked.
        /// </summary>
        TouchAnimationTypes TouchAnimationType { get; set; }

        /// <summary>
        /// Gets or sets a custom animation to be executed when view is clicked.
        /// </summary>
        ITouchAnimation TouchAnimation { get; set; }
    }

    /// <summary>
    /// Types of touch events
    /// </summary>
    public enum TouchEventType
    {
        /// <summary> Pressed </summary>
        Pressed,
        /// <summary> Released </summary>
        Released,
        /// <summary> Cancelled </summary>
        Cancelled,
        /// <summary> Ignored </summary>
        Ignored
    }
}