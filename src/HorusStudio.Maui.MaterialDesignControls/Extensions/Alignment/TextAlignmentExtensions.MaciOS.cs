using UIKit;

namespace Microsoft.Maui;

static class TextAlignmentExtensions
{
	public static UITextAlignment ToUIKit(this TextAlignment textAlignment)
	{
		return textAlignment switch
		{
			TextAlignment.Start => UITextAlignment.Left,
			TextAlignment.Center => UITextAlignment.Center,
			_ => UITextAlignment.Right
		};
	}
}