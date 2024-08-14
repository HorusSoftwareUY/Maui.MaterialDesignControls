namespace HorusStudio.Maui.MaterialDesignControls.Interface;

public interface IBaseFloatView
{
    /// <summary>
    /// Dismiss float view
    /// </summary>
    Task Dismiss(CancellationToken token = default);

    /// <summary>
    /// Show float view
    /// </summary>
    Task Show(CancellationToken token = default);
    
}