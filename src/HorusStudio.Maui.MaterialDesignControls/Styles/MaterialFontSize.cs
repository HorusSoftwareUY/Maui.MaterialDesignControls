namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialFontSize
    {
        public static double DisplayLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 80 : 57;

        public static double DisplayMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 62 : 45;

        public static double DisplaySmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 50 : 36;

        public static double HeadlineLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 44 : 32;

        public static double HeadlineMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 38 : 28;

        public static double HeadlineSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 32 : 24;

        public static double TitleLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 26 : 22;

        public static double TitleMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 19 : 16;

        public static double TitleSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        public static double BodyLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 19 : 16;

        public static double BodyMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        public static double BodySmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 15 : 12;

        public static double LabelLarge { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 17 : 14;

        public static double LabelMedium { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 15 : 12;

        public static double LabelSmall { get; set; } = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 14 : 11;
    }
}