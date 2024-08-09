﻿namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Typography helps make writing legible and beautiful <see href="https://m3.material.io/styles/typography/type-scale-tokens">see here.</see>
    /// </summary>
    public static class MaterialFontSize
    {
        /// <default> Tablet = 80 / Phone = 57 </default>
        public static double DisplayLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 80 : 57;

        /// <default> Tablet = 62 / Phone = 45 </default>
        public static double DisplayMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 62 : 45;

        /// <default> Tablet = 50 / Phone = 36 </default>
        public static double DisplaySmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 50 : 36;

        /// <default> Tablet = 44 / Phone = 32 </default>
        public static double HeadlineLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 44 : 32;

        /// <default> Tablet = 38 / Phone = 28 </default>
        public static double HeadlineMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 38 : 28;

        /// <default> Tablet = 32 / Phone = 24 </default>
        public static double HeadlineSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 32 : 24;

        /// <default> Tablet = 26 / Phone = 22 </default>
        public static double TitleLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 26 : 22;

        /// <default> Tablet = 19 / Phone = 16 </default>
        public static double TitleMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 19 : 16;

        /// <default> Tablet = 17 / Phone = 14 </default>
        public static double TitleSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        /// <default> Tablet = 19 / Phone = 16 </default>
        public static double BodyLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 19 : 16;

        /// <default> Tablet = 17 / Phone = 14 </default>
        public static double BodyMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        /// <default> Tablet = 15 / Phone = 12 </default>
        public static double BodySmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 15 : 12;

        /// <default> Tablet = 17 / Phone = 14 </default>
        public static double LabelLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        /// <default> Tablet = 15 / Phone = 12 </default>
        public static double LabelMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 15 : 12;

        /// <default> Tablet = 14 / Phone = 11 </default>
        public static double LabelSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 14 : 11;
    }
}