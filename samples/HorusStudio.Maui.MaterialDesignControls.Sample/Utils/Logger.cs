namespace HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

public static class Logger
{
    public static void Log(string message)
    {
#if DEBUG
        System.Diagnostics.Debug.WriteLine(message);
#else
		Console.WriteLine(message);
#endif
    }
}