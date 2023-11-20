using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is MainViewModel vm)
            {
                foreach (var mi in vm.MenuItems)
                {
                    var item = new MenuItem();
                    item.SetBinding(MenuItem.TextProperty, nameof(MenuItemViewModel.Title));
                    item.SetBinding(MenuItem.IconImageSourceProperty, nameof(MenuItemViewModel.Icon));
                    item.SetBinding(MenuItem.CommandParameterProperty, nameof(MenuItemViewModel.ViewModel));
                    item.BindingContext = mi;
                    item.Command = vm.GoToCommand;

                    Items.Add(item);
                }
            }
        }
    }
}