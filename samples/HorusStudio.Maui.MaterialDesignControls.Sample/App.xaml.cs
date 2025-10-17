namespace HorusStudio.Maui.MaterialDesignControls.Sample;

public partial class App
{
    public static IServiceProvider ServiceProvider { get; set; }
    
    public App()
    {
        InitializeComponent();
        MaterialDesignControls.InitializeComponents();
        MainPage = new AppShell();
    }
}