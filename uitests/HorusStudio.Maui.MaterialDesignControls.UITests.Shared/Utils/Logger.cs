namespace HorusStudio.Maui.MaterialDesignControls.UITests.Utils;

public static class Logger
{
    public static void LogInfo(string message)
    {
#if DEBUG
        System.Diagnostics.Debug.WriteLine(message);
#else
		Console.WriteLine(message);
#endif
    }
}