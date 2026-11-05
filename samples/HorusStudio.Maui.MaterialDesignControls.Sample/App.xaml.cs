namespace HorusStudio.Maui.MaterialDesignControls.Sample;

public partial class App
{
    public static IServiceProvider ServiceProvider { get; set; }
    
    public App()
    {
        InitializeComponent();
        MaterialDesignControls.InitializeComponents();
    }

    // Application.MainPage is obsolete in .NET 11: the window comes from CreateWindow.
    protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());
}