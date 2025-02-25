using System.Runtime.CompilerServices;
using System.Text;

namespace HorusStudio.Maui.MaterialDesignControls.Utils;

static class Logger
{
	#region Constants
	
	private const string Tag = "HorusStudio.Maui.MaterialDesignControls";
	internal const string LoggedKey = "MDC_ExceptionLogged";
	
	#endregion Constants
	
	#region Properties
	
	public static bool DebugMode { get; set; } = false;
	public static EventHandler<Exception>? OnException;

	#endregion Properties
	
	public static void Debug(string message, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null) => InternalDebug(message, callerFilePath, callerMemberName);
	
	public static void DebugWithCallerInfo(string message, string callerName, string callerMethodName) => InternalDebug(message, callerName, callerMethodName);

	public static void LogException(Exception exception, object? sender = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null) => InternalException(exception, sender, callerFilePath, callerMemberName);
	
	public static void LogException(string message, Exception exception, object? sender = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null) => InternalException(exception, sender, message, callerFilePath, callerMemberName);

	public static void LogExceptionWithCallerInfo(Exception exception, string callerName, string callerMethodName, object? sender = null) => InternalException(exception, sender, callerName, callerMethodName);
	
	public static void LogExceptionWithCallerInfo(string message, Exception exception, object? sender = null, string? callerFilePath = null, string? callerMemberName = null) => InternalException(exception, sender, message, callerFilePath, callerMemberName);
	
	public static void LogOnException(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
	{
		try
		{
			onTry.Invoke();
		}
		catch (Exception ex)
		{
			onCatch?.Invoke(ex);
			if (!ex.Data.Contains(LoggedKey))
			{
				InternalException(ex, callerFilePath, callerMemberName);
				ex.Data.Add(LoggedKey, true);	
			}
			if (@throw) throw;
		}
	}
	
	public static void LogOnExceptionWithCallerInfo(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, string? callerName = null, string? callerMethodName = null)
	{
		try
		{
			onTry.Invoke();
		}
		catch (Exception ex)
		{
			onCatch?.Invoke(ex);
			if (!ex.Data.Contains(LoggedKey))
			{
				InternalException(ex, callerName, callerMethodName);
				ex.Data.Add(LoggedKey, true);	
			}
			if (@throw) throw;
		}
	}
	
    #region Helpers

    private static void InternalLog(string message, string? callerFilePath = null, string? callerMemberName = null)
    {
        var builder = new StringBuilder();
        builder.Append($"[{Tag} > {GetCallerNameFromFilePath(callerFilePath) ?? "N/A"} > {callerMemberName ?? "N/A"}]: ");
        builder.Append(message);
		
        var msg = builder.ToString();
#if DEBUG
        System.Diagnostics.Debug.Indent();
        System.Diagnostics.Debug.WriteLine(msg);
        System.Diagnostics.Debug.Unindent();
#else
		Console.WriteLine(msg);
#endif
    }

    private static void InternalException(Exception ex, object? sender, string? message = null, string? callerFilePath = null, string? callerMemberName = null)
    {
	    var logInfo = string.IsNullOrEmpty(message) ? ex.ToString() : $"{message}. EXCEPTION: {ex}";

	    InternalLog(logInfo, callerFilePath, callerMemberName);
	    OnException?.Invoke(sender ?? GetCallerNameFromFilePath(callerFilePath), ex);
    }
    
    private static void InternalDebug(string message, string? callerFilePath = null, string? callerMemberName = null)
    {
		if (!DebugMode) return;
        InternalLog(message, callerFilePath, callerMemberName);
    }
    
    private static string? GetCallerNameFromFilePath(string? filePath)
	{
		if (string.IsNullOrEmpty(filePath)) return null;

		var separator = "/";
		if (!filePath.Contains(separator))
		{
			separator = "\\";
        }

		return filePath
			.Split(separator, StringSplitOptions.RemoveEmptyEntries)
			.LastOrDefault()?
			.Split(".", StringSplitOptions.RemoveEmptyEntries)?
			.FirstOrDefault();
	}

    #endregion Helpers
}
