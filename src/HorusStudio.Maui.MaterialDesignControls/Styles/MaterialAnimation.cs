namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// MaterialAnimation allows for configuring animations used in the interactions and visual reactions of controls, enhancing the smoothness and appeal of the user interface.
    /// </summary>
    public static class MaterialAnimation
    {
        /// <default>0.7</default>
        public static double Parameter { get; set; } = 0.7;

        /// <default><see cref="AnimationTypes.Fade">AnimationTypes.Fade</see></default>
        public static AnimationTypes Type { get; set; } = AnimationTypes.Fade;

        /// <default><see cref="ErrorAnimationTypes.Shake">ErrorAnimationTypes.Shake</see></default>
        public static ErrorAnimationTypes ErrorAnimationType { get; set; } = ErrorAnimationTypes.Shake;

        internal static void Configure(MaterialAnimationOptions options)
        {
            if (options.ErrorAnimationType != null) ErrorAnimationType = options.ErrorAnimationType.Value;
            if (options.Type != null) Type = options.Type.Value;
            if (options.Parameter != null) Parameter = options.Parameter.Value;
        }
    }
}