using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Utils
{
	public static class TextAlignmentHelper
	{
		public static UITextAlignment Convert(TextAlignment textAlignment)
		{
			switch (textAlignment)
			{
				case TextAlignment.Start:
					return UITextAlignment.Left;
				case TextAlignment.Center:
					return UITextAlignment.Center;
				default:
					return UITextAlignment.Right;
			}
		}
	}
}
