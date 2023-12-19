namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MaterialDesignControls.Init(this);
            MainPage = new AppShell();
        }
    }
}