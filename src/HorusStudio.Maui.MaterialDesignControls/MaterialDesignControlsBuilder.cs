using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.LifecycleEvents;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        public static MauiAppBuilder ConfigureMaterialDesignControls(this MauiAppBuilder builder)
        {
            builder
                .ConfigureMauiHandlers(ConfigureHandlers)
                .ConfigureLifecycleEvents(ConfigureLifeCycleEvents);
            
            return builder;
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
            // Radio Button
            application.Resources.AddStyles(MaterialRadioButton.GetStyles());
            // Checkbox
            application.Resources.AddStyles(MaterialCheckBox.GetStyles());
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