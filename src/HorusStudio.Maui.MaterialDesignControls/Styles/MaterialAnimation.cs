namespace HorusStudio.Maui.MaterialDesignControls
{
    
    /// <summary>
    /// Motion makes a UI expressive and easy to use <see href="https://m3.material.io/styles/motion/overview"></see>
    /// </summary>
    public static class MaterialAnimation
    {
        /// <default>0.7</default>
        public static double Parameter { get; set; } = 0.7;

        /// <default><see cref="AnimationTypes.Fade">AnimationTypes.Fade</see></default>
        public static AnimationTypes Type { get; set; } = AnimationTypes.Fade;

        /// <default><see langword="True"></see></default>
        public static bool AnimateOnError { get; set; } = true;
    }
}