namespace HorusStudio.Maui.MaterialDesignControls;

public interface IMaterialSnackbar
{
    IDisposable ShowSnackbar(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, Action action = null, Action actionLeading = null, Action actionTrailing = null);
    IDisposable ShowSnackbar(SnackbarConfig config);
    
    Task ShowSnackbarAsync(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, CancellationToken? cancelToken = null);
    Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken = null);
}