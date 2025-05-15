namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Typography helps make writing legible and beautiful <see href="https://m3.material.io/styles/typography/type-scale-tokens">see here.</see>
    /// </summary>
    public static class MaterialFontTracking
	{
        /// <default>-0.25</default>
        public static double DisplayLarge { get; set; } = -0.25;

        /// <default>0</default>
        public static double DisplayMedium { get; set; } = 0;

        /// <default>0</default>
        public static double DisplaySmall { get; set; } = 0;

        /// <default>0</default>
        public static double HeadlineLarge { get; set; } = 0;

        /// <default>0</default>
        public static double HeadlineMedium { get; set; } = 0;

        /// <default>0</default>
        public static double HeadlineSmall { get; set; } = 0;

        /// <default>0</default>
        public static double TitleLarge { get; set; } = 0;

        /// <default>0.15</default>
        public static double TitleMedium { get; set; } = 0.15;

        /// <default>0.1</default>
        public static double TitleSmall { get; set; } = 0.1;

        /// <default>0.5</default>
        public static double BodyLarge { get; set; } = 0.5;

        /// <default>0.25</default>
        public static double BodyMedium { get; set; } = 0.25;

        /// <default>0.4</default>
        public static double BodySmall { get; set; } = 0.4;

        /// <default>0.1</default>
        public static double LabelLarge { get; set; } = 0.1;

        /// <default>0.5</default>
        public static double LabelMedium { get; set; } = 0.5;

        /// <default>0.5</default>
        public static double LabelSmall { get; set; } = 0.5;

        internal static void Configure(MaterialSizeOptions options)
        {
            if (options.DisplayLarge != null) DisplayLarge = options.DisplayLarge.Value;
            if (options.DisplayMedium != null) DisplayMedium = options.DisplayMedium.Value;
            if (options.DisplaySmall != null) DisplaySmall = options.DisplaySmall.Value;
            if (options.HeadlineLarge != null) HeadlineLarge = options.HeadlineLarge.Value;
            if (options.HeadlineMedium != null) HeadlineMedium = options.HeadlineMedium.Value;
            if (options.HeadlineSmall != null) HeadlineSmall = options.HeadlineSmall.Value;
            if (options.TitleLarge != null) TitleLarge = options.TitleLarge.Value;
            if (options.TitleMedium != null) TitleMedium = options.TitleMedium.Value;
            if (options.TitleSmall != null) TitleSmall = options.TitleSmall.Value;
            if (options.BodyLarge != null) BodyLarge = options.BodyLarge.Value;
            if (options.BodyMedium != null) BodyMedium = options.BodyMedium.Value;
            if (options.BodySmall != null) BodySmall = options.BodySmall.Value;
            if (options.LabelLarge != null) LabelLarge = options.LabelLarge.Value;
            if (options.LabelMedium != null) LabelMedium = options.LabelMedium.Value;
            if (options.LabelSmall != null) LabelSmall = options.LabelSmall.Value;
        }
    }
}