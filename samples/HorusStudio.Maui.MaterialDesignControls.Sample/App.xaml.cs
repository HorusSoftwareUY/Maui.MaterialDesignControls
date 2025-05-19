namespace HorusStudio.Maui.MaterialDesignControls.Sample;

public partial class App
{
    public App()
    {
        InitializeComponent();
        MaterialDesignControls.InitializeComponents();
        MainPage = new AppShell();
    }
}