namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// MaterialAnimation allows for configuring animations used in the interactions and visual reactions of controls, enhancing the smoothness and appeal of the user interface.
    /// </summary>
    public static class MaterialAnimation
    {
        /// <default><see cref="TouchAnimationTypes.Fade">TouchAnimationTypes.Fade</see></default>
        public static TouchAnimationTypes TouchAnimationType { get; set; } = TouchAnimationTypes.Fade;

        /// <default><see cref="ErrorAnimationTypes.Shake">ErrorAnimationTypes.Shake</see></default>
        public static ErrorAnimationTypes ErrorAnimationType { get; set; } = ErrorAnimationTypes.Shake;

        internal static void Configure(MaterialAnimationOptions options)
        {
            if (options.ErrorAnimationType != null) ErrorAnimationType = options.ErrorAnimationType.Value;
            if (options.TouchAnimationType != null) TouchAnimationType = options.TouchAnimationType.Value;
        }
    }
}