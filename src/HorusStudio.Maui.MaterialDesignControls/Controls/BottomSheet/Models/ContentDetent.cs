namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class ContentDetent : Detent
{
    public override double GetHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        if (page?.Content is null)
        {
            return maxSheetHeight;
        }

        // I left this code as comment because it might be useful for future reference, but it's not used
        //if (page.Content is ScrollView sv)
        //{
        //    var s = sv.Content.Measure(page.Window.Width - page.Padding.HorizontalThickness, maxSheetHeight);
        //}

        // HACK: from 'page.Window.Width' to 'page.Window?.Width ?? page.Width' 
        var r = page.Content.Measure(page.Window?.Width ?? page.Width - page.Padding.HorizontalThickness, maxSheetHeight);
        
        return Math.Min(maxSheetHeight, r.Height + page.Padding.VerticalThickness);
    }
}
