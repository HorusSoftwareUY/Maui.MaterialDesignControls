
namespace HorusStudio.Maui.MaterialDesignControls.Interface;

public interface ISnackbar : IBaseFloatView
{
    /// <summary>
    /// Action to invoke on action button click
    /// </summary>
    Action? ActionLeading { get; }
    
    /// <summary>
    /// Action to invoke on action button click
    /// </summary>
    Action? ActionTrailing { get; }
    
    /// <summary>
    /// Action to invoke on action button click
    /// </summary>
    Action? ActionText { get; }

    /// <summary>
    /// Snackbar anchor. Snackbar appears near this view
    /// </summary>
    IView? Anchor { get; }

    /// <summary>
    /// Snackbar duration
    /// </summary>
    TimeSpan Duration { get; }

    /// <summary>
    /// Snackbar visual options
    /// </summary>
    SnackbarOptions SnackbarOptions { get; }
}