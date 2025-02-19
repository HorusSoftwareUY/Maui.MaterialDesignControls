namespace HorusStudio.Maui.MaterialDesignControls;

public interface IMaterialSnackbar
{
    IDisposable Show(string message, TimeSpan? duration = null, MaterialSnackbarConfig.ActionConfig? action = null, MaterialSnackbarConfig.IconConfig? leadingIcon = null, MaterialSnackbarConfig.IconConfig? trailingIcon = null);
    IDisposable Show(MaterialSnackbarConfig config);
    
    Task ShowAsync(string message, TimeSpan? duration = null, MaterialSnackbarConfig.ActionConfig? action = null, MaterialSnackbarConfig.IconConfig? leadingIcon = null, MaterialSnackbarConfig.IconConfig? trailingIcon = null, CancellationToken cancellationToken = default);
    Task ShowAsync(MaterialSnackbarConfig config, CancellationToken cancellationToken = default);
}