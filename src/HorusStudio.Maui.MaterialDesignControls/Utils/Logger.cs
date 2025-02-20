using System.Runtime.CompilerServices;
using System.Text;

namespace HorusStudio.Maui.MaterialDesignControls.Utils
{
	static class Logger
	{
		#region Constants
		
		private const string Tag = "HorusStudio.Maui.MaterialDesignControls";
		internal const string LoggedKey = "MDC_ExceptionLogged";
		
		#endregion Constants
		
		#region Properties
		
		public static bool DebugMode { get; set; } = false;

		#endregion Properties
		
		public static void Debug(string message, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null) => InternalLog(message, callerFilePath, callerMemberName);
		
		public static void DebugWithCaller(string message, string? callerFilePath = null, string? callerMemberName = null) => InternalLog(message, callerFilePath, callerMemberName);
		
		public static void LogException(Exception exception, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null) => InternalLog(exception.ToString(), callerFilePath, callerMemberName);

		public static void LogExceptionWithCaller(Exception ex, string? callerFilePath = null, string? callerMemberName = null) => DebugWithCaller(ex.ToString(), callerFilePath, callerMemberName);
		
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
					InternalLog(ex.ToString(), callerFilePath, callerMemberName);
					ex.Data.Add(LoggedKey, true);	
				}
				if (@throw) throw;
			}
		}
		
		public static void LogOnExceptionWithCaller(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, string? callerFilePath = null, string? callerMemberName = null)
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
					InternalLog(ex.ToString(), callerFilePath, callerMemberName);
					ex.Data.Add(LoggedKey, true);	
				}
				if (@throw) throw;
			}
		}
		
        #region Helpers

        private static void InternalLog(string message, string? callerFilePath = null, string? callerMemberName = null)
        {
#if !DEBUG
			if (!DebugMode) return;
#endif
			
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
}

