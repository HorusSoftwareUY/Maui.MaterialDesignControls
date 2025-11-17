using CommunityToolkit.Maui;
using HorusStudio.Maui.MaterialDesignControls.Sample.Pages;
using HorusStudio.Maui.MaterialDesignControls.Sample.Services;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using Microsoft.Maui.LifecycleEvents;

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
#if RELEASE
                .InitFirebase()
#endif
                .UseMauiCommunityToolkit()
                .UseMaterialDesignControls(options =>
                {
                    options.EnableDebug();
                    options.OnException((sender, exception) =>
                    {
                        Logger.LogException(exception);
                    });
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
                }, configureHandlers: handlers =>
                {
                    handlers.AddHandler(typeof(MaterialTextField), typeof(Handlers.CustomMaterialTextFieldHandler));
                });
         
            builder.Services
                .AutoConfigureViewModelsAndPages()
                .RegisterServices();
            
            var app = builder.Build();
            App.ServiceProvider = app.Services;
            
#if RELEASE
            var crashlyticsService = App.ServiceProvider.GetService<ICrashlyticsService>();
            crashlyticsService?.InitCrashDetection();
#endif
            
            return app;
        }
        
        private static MauiAppBuilder InitFirebase(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => 
            {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((_, __) =>
            {
                var coreType = typeof(Firebase.Core.App);
                var installationsType = typeof(Firebase.Installations.Installations);
                var analyticsType = typeof(Firebase.Analytics.Analytics);

                Firebase.Core.App.Configure();

                Firebase.Crashlytics.Crashlytics.SharedInstance.Init();
                Firebase.Crashlytics.Crashlytics.SharedInstance.SetCrashlyticsCollectionEnabled(true);
                Firebase.Crashlytics.Crashlytics.SharedInstance.SendUnsentReports();

                var installation = Firebase.Installations.Installations.DefaultInstance;
                installation.GetAuthToken(completion: (token, error) => {
                    if (error != null)
                    {
                        Logger.LogInfo($"Error getting firebase authentication token: {error.Description}");
                    }
                });

                return false;
            }));
#elif ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                {
                    Firebase.FirebaseApp.InitializeApp(activity);
                    Firebase.Crashlytics.FirebaseCrashlytics.Instance.SetCrashlyticsCollectionEnabled(Java.Lang.Boolean.True);
                    Firebase.Crashlytics.FirebaseCrashlytics.Instance.SendUnsentReports();
                }));
#endif
            });

            return builder;
        }
        
        static IServiceCollection RegisterServices(this IServiceCollection services)
        {
#if IOS
            services.AddSingleton<IAnalyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.iOS.Services.AnalyticsService>();
            services.AddSingleton<ICrashlyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.iOS.Services.CrashlyticsService>();
#elif ANDROID
            services.AddSingleton<IAnalyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.Android.Services.AnalyticsService>();
            services.AddSingleton<ICrashlyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.Android.Services.CrashlyticsService>();
#elif MACCATALYST
            services.AddSingleton<IAnalyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.MacCatalyst.Services.AnalyticsService>();
            services.AddSingleton<ICrashlyticsService, HorusStudio.Maui.MaterialDesignControls.Sample.MacCatalyst.Services.CrashlyticsService>();
#endif

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