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

        /// <default><see langword="True"></see></default>
        public static bool AnimateOnError { get; set; } = true;

        internal static void Configure(MaterialAnimationOptions options)
        {
            if (options.AnimateOnError != null) AnimateOnError = options.AnimateOnError.Value;
            if (options.Type != null) Type = options.Type.Value;
            if (options.Parameter != null) Parameter = options.Parameter.Value;
        }
    }
}