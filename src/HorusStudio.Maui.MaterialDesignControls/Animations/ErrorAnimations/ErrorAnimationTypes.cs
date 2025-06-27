namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Types of animations available for error scenarios
    /// </summary>
	public enum ErrorAnimationTypes
	{
        /// <summary>None: no animation runs.</summary>
        None,
        /// <summary>Shake: Represents an animation that simulates a "shake" effect by moving the target element back and forth along the X-axis.</summary>
        Shake,
        /// <summary>Breath: represents an animation that performs a "heartbeat" effect by scaling the target element in a pulsating manner.</summary>
        Heart,
        /// <summary>Jump: represents an animation that creates a "jump" effect by translating the target element along the Y-axis.</summary>
        Jump,
    }
}