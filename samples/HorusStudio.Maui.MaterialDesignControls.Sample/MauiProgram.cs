using HorusStudio.Maui.MaterialDesignControls.Sample.Pages;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    public static class MauiProgram
    {
        private const string FontRegular = "FontRegular";
        private const string FontMedium = "FontMedium";
        private const string FontBold = "FontBold";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMaterialDesignControls(options =>
                {
                    options.EnableDebug();
                    /*
                    options.OnException((sender, exception) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"EXCEPTION ON LIBRARY: {sender} - {exception}");
                    });
                    */
                    options.ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("Roboto-Regular.ttf", FontRegular);
                        fonts.AddFont("Roboto-Medium.ttf", FontMedium);
                        fonts.AddFont("Roboto-Bold.ttf", FontBold);
                    }, new(FontRegular, FontMedium, FontRegular));
                    /*
                    // Plugin configuration using C#
                    options.ConfigureThemes(
                        lightTheme: new MaterialTheme
                        {
                            Primary = Colors.Blue,
                            OnPrimary = Colors.LightBlue
                        },
                        darkTheme: new MaterialTheme
                        {
                            Primary = Colors.SkyBlue,
                            OnPrimary = Colors.DarkBlue
                        });

                    options.ConfigureFontSize(new MaterialSizeOptions
                    {
                        BodyMedium = 25,
                        LabelLarge = 20
                    });

                    options.ConfigureFontTracking(new MaterialSizeOptions
                       {
                           BodyMedium = 0.35,
                           LabelLarge = 0.2
                       });

                    options.ConfigureIcons(new MaterialIconOptions
                    {
                        Picker = "arrow_right.png",
                        Error = "info.png",
                        DatePicker = "ic_date.png"
                    });

                    options.ConfigureStringFormat(new MaterialFormatOptions
                    {
                        DateFormat = "dd/MM/yyyy"
                    });

                    options.ConfigureElevation(new MaterialElevationOptions
                    {
                        Level1 = new Shadow
                        {
                            Brush = MaterialLightTheme.Shadow,
                            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 5 : 1.5f,
                            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .3f : .35f,
                            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-0.5, 2) : new Point(0, 1.5)
                        }
                    });

                    options.ConfigureAnimations(new MaterialAnimationOptions
                    {
                        Parameter = 0.1,
                        Type = AnimationTypes.Scale
                    });

                    options.ConfigureSnackbar(new MaterialSnackbarOptions
                    {
                        DefaultBackgroundColor = Colors.LightGoldenrodYellow,
                        DefaultTextColor = Colors.Black,
                        DefaultActionColor = Colors.Brown,
                        DefaultIconColor = Colors.Brown
                    });
                    */

                    /*
                    // Plugin configuration using App Resources (include MaterialCustomizations dictionary on App.xaml)
                    options
                        .ConfigureThemesFromResources("MaterialCustomizations.xaml", "MaterialLight", "MaterialDark")
                        .ConfigureFontSizeFromResources("MaterialCustomizations.xaml","MaterialFont")
                        .ConfigureFontTrackingFromResources("MaterialCustomizations.xaml")
                        .ConfigureIconsFromResources("MaterialCustomizations.xaml","MaterialIcon")
                        .ConfigureStringFormatFromResources("MaterialCustomizations.xaml");
                    */
                }, configureHandler: handlers =>
                {
                    handlers.AddHandler(typeof(MaterialTextField), typeof(Handlers.CustomMaterialTextFieldHandler));
                });
         
            builder.Services
                .AutoConfigureViewModelsAndPages();

            return builder.Build();
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