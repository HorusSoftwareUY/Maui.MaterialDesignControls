namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class LoggerHelper
    {
        private const string Prefix = "[HorusStudio.Maui.MaterialDesignControls]";

        public static void Log(Exception ex)
        {
            Console.WriteLine($"{Prefix} - {ex.Message} - {ex.StackTrace}");
        }
    }
}