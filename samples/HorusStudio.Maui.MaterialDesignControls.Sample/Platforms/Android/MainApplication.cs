using Android.App;
using Android.Runtime;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
#if ENABLE_STARTUP_PROFILING
            // Touching StartupProfiler for the first time starts its Stopwatch (T0).
            // This mark will read ~0 ms and confirms where the timeline begins.
            StartupProfiler.Mark("MainApplication.ctor");
#endif
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}