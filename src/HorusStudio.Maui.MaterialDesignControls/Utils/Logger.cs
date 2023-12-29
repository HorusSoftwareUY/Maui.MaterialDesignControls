using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace HorusStudio.Maui.MaterialDesignControls.Utils
{
	static class Logger
	{
		private const string Tag = "HorusStudio.Maui.MaterialDesignControls";

		public static void Debug(string message, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
		{
			var builder = new StringBuilder();
			builder.Append($"[{Tag} > {GetCallerNameFromFilePath(callerFilePath) ?? "N/A"} > {callerMemberName ?? "N/A"}]: ");
			builder.Append(message);

			System.Diagnostics.Debug.WriteLine(builder.ToString());
		}

		public static void Log(Exception exception, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null) => Debug(exception.ToString(), callerFilePath, callerMemberName);

        #region Helpers

        private static string GetCallerNameFromFilePath(string filePath)
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

