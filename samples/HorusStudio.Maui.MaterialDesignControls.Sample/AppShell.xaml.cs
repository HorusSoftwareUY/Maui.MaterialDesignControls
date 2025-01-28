namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Automatically register routes for each pair MyViewModel:BaseViewModel <-> MyPage:BaseContentPage<MyPage>
            var viewModels = MauiProgram.GetViewModelsToRegister();
            var pages = MauiProgram.GetPagesToRegister(viewModels);

            foreach (var vm in viewModels)
            {
                var page = pages.FirstOrDefault(p => p.BaseType.FullName.Contains(vm.FullName));
                if (page != null)
                {
                    Routing.RegisterRoute(vm.Name, page);
                }
            }
        }
    }
}