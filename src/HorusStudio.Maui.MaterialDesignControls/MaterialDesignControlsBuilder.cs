using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;
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
            Instance = new SnackbarImplementation();

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
            handlers.AddHandler(typeof(BorderlessEntry), typeof(BorderlessEntryHandler));
            handlers.AddHandler(typeof(CustomTimePicker), typeof(CustomTimePickerHandler));
            handlers.AddHandler(typeof(CustomDatePicker), typeof(CustomDatePickerHandler));
            handlers.AddHandler(typeof(CustomPicker), typeof(CustomPickerHandler));
            handlers.AddHandler(typeof(CustomEditor), typeof(CustomEditorHandler));
            handlers.AddHandler(typeof(CustomCheckBox), typeof(CustomCheckboxHandler));
            handlers.AddHandler(typeof(CustomSlider), typeof(CustomSliderHandler));
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
            // Switch
            application.Resources.AddStyles(MaterialSwitch.GetStyles());
            // Card
            application.Resources.AddStyles(MaterialCard.GetStyles());
            // Radio Button
            application.Resources.AddStyles(MaterialRadioButton.GetStyles());
            // Checkbox
            application.Resources.AddStyles(MaterialCheckBox.GetStyles());
            // MaterialTextField
            application.Resources.AddStyles(MaterialTextField.GetStyles());
            // Chips
            application.Resources.AddStyles(MaterialChips.GetStyles());
            // Rating
            application.Resources.AddStyles(MaterialRating.GetStyles());
            // Selection
            application.Resources.AddStyles(MaterialSelection.GetStyles());
            // Time Picker
            application.Resources.AddStyles(MaterialTimePicker.GetStyles());
            // DatePicker
            application.Resources.AddStyles(MaterialDatePicker.GetStyles());
            // Picker
            application.Resources.AddStyles(MaterialPicker.GetStyles());
            // Multiline Text Field
            application.Resources.AddStyles(MaterialMultilineTextField.GetStyles());
            // Slider
            application.Resources.AddStyles(MaterialSlider.GetStyles());
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