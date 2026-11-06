using HorusStudio.Maui.MaterialDesignControls.Sample.Views;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();

            // Defer flyout content inflation (30-item drawer) out of the startup path.
            // Loads after the first frame settles, or immediately if the user opens
            // the flyout before that (so the menu is never seen empty).
            Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(400), () => FlyoutLazyContent.Load());
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(FlyoutIsPresented) && FlyoutIsPresented)
                    FlyoutLazyContent.Load();
            };
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