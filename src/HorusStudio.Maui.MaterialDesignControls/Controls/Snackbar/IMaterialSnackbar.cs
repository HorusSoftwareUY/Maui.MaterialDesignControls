namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Abstraction to handle <see cref="MaterialSnackbar" /> component
/// </summary>
public interface IMaterialSnackbar
{
    /// <summary>
    /// Show a message within a Snackbar component
    /// </summary>
    /// <param name="message">Text to be displayed</param>
    /// <param name="duration">Display time. Optional</param>
    /// <param name="action">Action to dismiss Snackbar. Optional</param>
    /// <param name="leadingIcon">Leading icon. Optional</param>
    /// <param name="trailingIcon">Trailing icon. Optional</param>
    /// <returns>Disposable instance</returns>
    IDisposable Show(string message, TimeSpan? duration = null, MaterialSnackbarConfig.ActionConfig? action = null, MaterialSnackbarConfig.IconConfig? leadingIcon = null, MaterialSnackbarConfig.IconConfig? trailingIcon = null);
    
    /// <summary>
    /// Show a message within a Snackbar component from custom configuration
    /// </summary>
    /// <param name="config">Configuration object containing information to be displayed on Snackbar</param>
    /// <returns>Disposable instance</returns>
    IDisposable Show(MaterialSnackbarConfig config);
    
    /// <summary>
    /// Show a message asynchronously within a Snackbar component
    /// </summary>
    /// <param name="message">Text to be displayed</param>
    /// <param name="duration">Display time. Optional</param>
    /// <param name="action">Action to dismiss Snackbar. Optional</param>
    /// <param name="leadingIcon">Leading icon. Optional</param>
    /// <param name="trailingIcon">Trailing icon. Optional</param>
    /// <param name="cancellationToken">Cancellation token for task. Optional</param>
    Task ShowAsync(string message, TimeSpan? duration = null, MaterialSnackbarConfig.ActionConfig? action = null, MaterialSnackbarConfig.IconConfig? leadingIcon = null, MaterialSnackbarConfig.IconConfig? trailingIcon = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Show a message asynchronously within a Snackbar component from custom configuration
    /// </summary>
    /// <param name="config">Configuration object containing information to be displayed on Snackbar</param>
    /// <param name="cancellationToken">Cancellation token for task. Optional</param>
    Task ShowAsync(MaterialSnackbarConfig config, CancellationToken cancellationToken = default);
}