using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.LifecycleEvents;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        static ISnackbarUser _currentInstance;
        
        public static MauiAppBuilder ConfigureMaterialDesignControls(this MauiAppBuilder builder)
        {
            builder
                .ConfigureMauiHandlers(ConfigureHandlers)
                .ConfigureLifecycleEvents(ConfigureLifeCycleEvents);
            
            return builder;
        }
        
        public static MauiAppBuilder UseSnackbar(this MauiAppBuilder builder, bool registerInterface, Action configure = null)
        {
            Instance = new SnackbarImplemetation();

            configure?.Invoke();

            if (registerInterface)
            {
                builder.Services.AddTransient((s) => Instance);
            }

            return builder;
        }

        public static MauiAppBuilder UseSnackbar(this MauiAppBuilder builder, Action configure = null)
        {
            return UseSnackbar(builder, false, configure);
        }

        public static ISnackbarUser Instance
        {
            get
            {
                if (_currentInstance is null)
                    throw new ArgumentException("[HorusStudio.Maui.MaterialDesignControls] You must call UseSnackbar() in your MauiProgram for initialization");

                return _currentInstance;
            }
            set => _currentInstance = value;
        }

        private static void ConfigureHandlers(IMauiHandlersCollection handlers)
        {
            handlers.AddHandler(typeof(CustomButton), typeof(CustomButtonHandler));
            handlers.AddHandler(typeof(CustomRadioButton), typeof(CustomRadioButtonHandler));
            handlers.AddHandler(typeof(CheckBox), typeof(CustomCheckboxHandler));
        }

        private static void ConfigureLifeCycleEvents(ILifecycleBuilder appLifeCycle)
        {
#if ANDROID
            appLifeCycle.AddAndroid(android => android
                .OnApplicationCreate(_ =>
                {
                    InitializeComponents();
                }));
#elif IOS || MACCATALYST
            appLifeCycle.AddiOS(ios => ios
                .FinishedLaunching((_, _) =>
                {
                    InitializeComponents();
                    return true;
                }));
#elif WINDOWS
            appLifeCycle.AddWindows(windows => windows
                .OnLaunched((_, _) =>
                {
                    InitializeComponents();
                }));
#endif
        }

        private static void InitializeComponents()
        {
            if (Application.Current == null)
            {
                Logger.Debug("Error initializing Material Design Controls: MAUI Application is null.");
                return;
            }
            RegisterDefaultStyles(Application.Current);
        }

        private static void RegisterDefaultStyles(Application application)
        {
            // Button
            application.Resources.AddStyles(MaterialButton.GetStyles());
            // Icon Button
            application.Resources.AddStyles(MaterialIconButton.GetStyles());
            // Card
            application.Resources.AddStyles(MaterialCard.GetStyles());
            // Radio Button
            application.Resources.AddStyles(MaterialRadioButton.GetStyles());
            // Checkbox
            application.Resources.AddStyles(MaterialCheckBox.GetStyles());
            // Chips
            application.Resources.AddStyles(MaterialChips.GetStyles());
            // Rating
            application.Resources.AddStyles(MaterialRating.GetStyles());
        }

        private static void AddStyles(this ResourceDictionary resources, IEnumerable<Style> styles)
        {
            if (styles == null) return;

            foreach (var style in styles)
            {
                resources.Add(style);
            }
        }
    }
}