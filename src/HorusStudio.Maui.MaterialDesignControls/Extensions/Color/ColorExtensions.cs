namespace Maui.Graphics
{
	public static class ColorExtensions
	{
		public static Color Default => new(0, 0, 0, 0);

        public static string ToRgbaString(this Color color)
            => $"rgba({color.Red}, {color.Green}, {color.Blue}, {color.Alpha})";

        public static Color AnimateTo(this Color fromColor, Color toColor, double t)
        {
            return Color.FromRgba(fromColor.Red + (t * (toColor.Red - fromColor.Red)),
                fromColor.Green + (t * (toColor.Green - fromColor.Green)),
                fromColor.Blue + (t * (toColor.Blue - fromColor.Blue)),
                fromColor.Alpha + (t * (toColor.Alpha - fromColor.Alpha)));
        }
    }
}