namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Abstraction to handle Material Snackbar component that follows <see href="https://m3.material.io/components/snackbar/overview">Material Design Guidelines</see>.
/// </summary>
/// <example>
/// <h3>C# sample</h3>
/// <code>
/// private readonly IMaterialSnackbar _snackbar;
/// 
/// public SnackbarViewModel(IMaterialSnackbar snackbar)
/// {
///     _snackbar = snackbar;
///     Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
/// }
///
/// private async void DefaultSnackbar()
/// {
///     _snackbar.Show(new MaterialSnackbarConfig("Default snackbar with custom action")
///     {
///         Action = new MaterialSnackbarConfig.ActionConfig("Close action", SnackbarAction)
///     });
/// }
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SnackbarPage.xaml)
/// </example>
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