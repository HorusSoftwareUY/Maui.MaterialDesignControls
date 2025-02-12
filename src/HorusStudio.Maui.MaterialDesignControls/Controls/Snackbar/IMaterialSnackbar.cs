namespace HorusStudio.Maui.MaterialDesignControls;

public interface IMaterialSnackbar
{
    IDisposable Show(string message, TimeSpan? duration = null, SnackbarConfig.ActionConfig? action = null, SnackbarConfig.IconConfig? leadingIcon = null, SnackbarConfig.IconConfig? trailingIcon = null);
    IDisposable Show(SnackbarConfig config);
    
    Task ShowAsync(string message, TimeSpan? duration = null, SnackbarConfig.ActionConfig? action = null, SnackbarConfig.IconConfig? leadingIcon = null, SnackbarConfig.IconConfig? trailingIcon = null, CancellationToken cancellationToken = default);
    Task ShowAsync(SnackbarConfig config, CancellationToken cancellationToken = default);
}