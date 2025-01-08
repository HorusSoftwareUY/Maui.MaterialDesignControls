using HorusStudio.Maui.MaterialDesignControls.Sample.Pages;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using Microsoft.Extensions.Logging;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    public static class MauiProgram
    {
        private const string FontRegular = "FontRegular";
        private const string FontMedium = "FontMedium";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Roboto-Regular.ttf", FontRegular);
                    fonts.AddFont("Roboto-Medium.ttf", FontMedium);
                    // Workaround for Android error
                    fonts.AddFont("Roboto-Medium.ttf", "sans-serif-medium");
                })
                .ConfigureMaterialDesignControls()
                .UseSnackbar(true);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            builder.Services
                .ConfigureMaterial()
                .AutoConfigureViewModelsAndPages();

            return builder.Build();
        }

        static IServiceCollection ConfigureMaterial(this IServiceCollection services)
        {
            MaterialFontFamily.Medium = FontMedium;
            MaterialFontFamily.Regular = FontRegular;
            MaterialFontFamily.Default = MaterialFontFamily.Regular;

            return services;
        }

        static IServiceCollection AutoConfigureViewModelsAndPages(this IServiceCollection services)
        {
            var vmTypes = GetViewModelsToRegister();
            foreach (var vm in vmTypes)
            {
                services.AddTransient(vm);
            }

            var pageTypes = GetPagesToRegister(vmTypes);
            foreach (var page in pageTypes)
            {
                services.AddTransient(page);
            }

            return services;
        }

        public static IEnumerable<Type> GetViewModelsToRegister()
        {
            // Get ViewModel types that satisfy MyViewModel:BaseViewModel
            var currentNs = typeof(MauiProgram).Namespace;
            var types = typeof(MauiProgram).Assembly.GetTypes();
            var viewModels = types.Where(t => t.Namespace == $"{currentNs}.ViewModels" && t.BaseType == typeof(BaseViewModel));

            return viewModels;
        }

        public static IEnumerable<Type> GetPagesToRegister(IEnumerable<Type> viewModelTypes)
        {
            // Get ContentPage types that satisfy MyPage:BaseContentPage<MyPage>
            var currentNs = typeof(MauiProgram).Namespace;
            var types = typeof(MauiProgram).Assembly.GetTypes();
            var pages = types.Where(t => t.Namespace == $"{currentNs}.Pages" &&
                t.BaseType.Name.StartsWith(typeof(BaseContentPage<>).Name) &&
                viewModelTypes.Any(vm => t.BaseType.FullName.Contains(vm.FullName)));

            return pages;
        }
    }
}