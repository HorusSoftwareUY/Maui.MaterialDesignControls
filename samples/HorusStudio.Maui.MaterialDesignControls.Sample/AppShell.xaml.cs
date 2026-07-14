using HorusStudio.Maui.MaterialDesignControls.Sample.Views;

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

        /// <summary>
        /// Attaches the nav timing overlay to the current page.
        /// Called from BaseContentPage.OnAppearing so the overlay floats above each page.
        /// Idempotent — wraps the page content only once.
        /// </summary>
        internal static void EnsureNavTimingOverlay(ContentPage page)
        {
#if ENABLE_NAV_TIMING
            // Already wrapped — just make sure overlay is subscribed
            if (page.Content is Grid grid && grid.Children.OfType<NavTimingOverlay>().Any())
                return;

            var overlay = new NavTimingOverlay
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End,
                ZIndex = 999,
            };

            var original = page.Content;
            var wrapper = new Grid();
            if (original is not null)
                wrapper.Children.Add(original);
            wrapper.Children.Add(overlay);
            page.Content = wrapper;
#endif
        }
    }
}