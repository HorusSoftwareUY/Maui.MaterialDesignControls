namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Types of animations available for touchable views
    /// </summary>
    public enum TouchAnimationTypes
    {
        /// <summary>None: no animation runs.</summary>
        None, 
        /// <summary>Fade: Represents an animation that simulates a "fade" effect by changing the opacity over the target element.</summary>
        Fade, 
        /// <summary>Scale: Represents an animation that simulates a "sink" or "sunken" effect by scaling the target element.</summary>
        Scale,
        /// <summary>Bounce: Represents an animation that simulates a "sink" or "sunken" effect with a "bounce" effect when the user releases the target element.</summary>
        Bounce,
    }
}