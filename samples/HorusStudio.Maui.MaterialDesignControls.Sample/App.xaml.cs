using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample;

public partial class App
{
    public static IServiceProvider ServiceProvider { get; set; }
    
    public App()
    {
        InitializeComponent();
#if ENABLE_STARTUP_PROFILING
        StartupProfiler.Mark("App.InitializeComponent done");
#endif
        MaterialDesignControls.InitializeComponents();
#if ENABLE_STARTUP_PROFILING
        StartupProfiler.Mark("MaterialDesignControls.InitializeComponents done");
#endif
        MainPage = new AppShell();
#if ENABLE_STARTUP_PROFILING
        StartupProfiler.Mark("App.MainPage = new AppShell() done");
#endif
    }
}