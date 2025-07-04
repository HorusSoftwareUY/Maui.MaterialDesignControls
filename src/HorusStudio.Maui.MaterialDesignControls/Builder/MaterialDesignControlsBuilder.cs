using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Material Design Controls builder.
/// </summary>
/// <param name="AppBuilder">MAUI application builder.</param>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed record MaterialDesignControlsBuilder(MauiAppBuilder AppBuilder);

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public static class MaterialDesignControlsBuilderExtensions
{
    private static Action<MaterialHandlerOptions>? _configureHandlers;

    private static List<(Type, Type, Type)> _controlHandlerMappings = new List<(Type, Type, Type)>
    {
        (typeof(MaterialButton), typeof(CustomButton), typeof(MaterialButtonHandler )),
        (typeof(MaterialRadioButton), typeof(CustomRadioButton), typeof(MaterialRadioButtonHandler)),
        (typeof(MaterialTextField), typeof(CustomEntry), typeof(MaterialTextFieldHandler)),
        (typeof(MaterialTimePicker), typeof(CustomTimePicker), typeof(MaterialTimePickerHandler)),
        (typeof(MaterialDatePicker), typeof(CustomDatePicker), typeof(MaterialDatePickerHandler)),
        (typeof(MaterialPicker), typeof(CustomPicker), typeof(MaterialPickerHandler)),
        (typeof(MaterialMultilineTextField), typeof(CustomEditor), typeof(MaterialMultilineTextFieldHandler)),
        (typeof(MaterialCheckBox), typeof(CustomCheckBox), typeof(MaterialCheckBoxHandler)),
        (typeof(MaterialSlider), typeof(CustomSlider), typeof(MaterialSliderHandler)),
    };

    /// <summary>
    /// Register Material Design Controls on application builder.
    /// </summary>
    /// <param name="appBuilder">MAUI application builder.</param>
    /// <param name="configureDelegate">Configuration delegate. Optional.</param>
    /// <param name="configureHandlers">Configuration handlers. Optional.</param>
    /// <returns>Updated MAUI application builder</returns>
    public static MauiAppBuilder UseMaterialDesignControls(this MauiAppBuilder appBuilder,
        Action<MaterialDesignControlsBuilder>? configureDelegate = null, Action<MaterialHandlerOptions>? configureHandlers = null)
    {   
        Logger.Debug("Configuring Material Design Controls");
        try
        {
            _configureHandlers = configureHandlers;

            appBuilder
                .ConfigureMauiHandlers(ConfigureHandlers)
                .ConfigureLifecycleEvents(ConfigureLifeCycleEvents);

            appBuilder.Services
                .ConfigureServices();

            if (configureDelegate != null)
            {
                var builder = new MaterialDesignControlsBuilder(appBuilder);
                configureDelegate.Invoke(builder);
            }
        }
        catch (Exception ex)
        {
            Logger.Debug("ERROR configuring Material Design Controls");
            if (!ex.Data.Contains(Logger.LoggedKey)) Logger.LogException(ex);
        }
        Logger.Debug("Material Design Controls configuration COMPLETED");
        
        return appBuilder;
    }

    /// <summary>
    /// Enables debug logging.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder EnableDebug(this MaterialDesignControlsBuilder builder)
    {
        Logger.DebugMode = true;
        return builder;
    }
    
    /// <summary>
    /// Registers a delegate to be invoked in case of Exception.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="configureDelegate">Custom defined delegate.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder OnException(this MaterialDesignControlsBuilder builder,
        Action<object?, Exception> configureDelegate)
    {
        Logger.OnException += configureDelegate.Invoke;
        return builder;
    }
    
    /// <summary>
    /// Registers custom fonts on MAUI Application and Material Design Controls library.
    /// It also indicates MDC which font to use for Regular, Medium and Default.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="configureDelegate">Custom defined delegate for font collection. Optional.</param>
    /// <param name="options">Set Regular, Medium and Default fonts to be used by MDC.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureFonts(this MaterialDesignControlsBuilder builder, Action<IFontCollection>? configureDelegate, MaterialFontOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        
        ConfigurationWithLogger(() =>
            {
                builder.AppBuilder.ConfigureFonts(fonts =>
                {
                    configureDelegate?.Invoke(fonts);
                    MaterialFontFamily.Configure(fonts, options);
                });
            });

        return builder;
    }
    
    /// <summary>
    /// Overrides, partially or fully, default color palettes for one or both Light and Dark themes.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="lightTheme"><see cref="MaterialTheme" /> configuration for Light theme. Optional.</param>
    /// <param name="darkTheme"><see cref="MaterialTheme" /> configuration for Dark theme. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureThemes(this MaterialDesignControlsBuilder builder, MaterialTheme? lightTheme, MaterialTheme? darkTheme)
    {
        if (lightTheme is not null)
        {
            Logger.Debug("Configuring Light Theme");
            MaterialLightTheme.Configure(lightTheme);
            Logger.Debug("Light Theme configuration COMPLETED");
        }
        if (darkTheme is not null)
        {
            Logger.Debug("Configuring Dark Theme");
            MaterialDarkTheme.Configure(darkTheme);
            Logger.Debug("Dark Theme configuration COMPLETED");
        }
        
        return builder;
    }
    
    /// <summary>
    /// Overrides, partially or fully, default color palettes for one or both Light and Dark themes from Resources.
    /// MDC will match resource keys with <see cref="MaterialTheme" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="lightThemeResourcePrefix">Prefix for Light theme colors. Optional.</param>
    /// <param name="darkThemeResourcePrefix">Prefix for Dark theme colors. Optional.</param>
    /// <returns></returns>
    public static MaterialDesignControlsBuilder ConfigureThemesFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? lightThemeResourcePrefix = null,
        string? darkThemeResourcePrefix = null)
    {
        Logger.Debug("Enqueuing Themes loading task from resources");
        
        MaterialDesignControls.EnqueueAction(
            resourceDictionaryName,
            resources =>
            {
                Logger.DebugWithCallerInfo($"Configuring Themes from Resources source: {resources.Source}", nameof(MaterialDesignControlsBuilder), nameof(ConfigureThemesFromResources));
                
                builder.ConfigureThemes(
                    resources.FromResources<MaterialTheme>(lightThemeResourcePrefix), 
                    resources.FromResources<MaterialTheme>(darkThemeResourcePrefix));
                
                Logger.DebugWithCallerInfo("Themes configuration from resources COMPLETED", nameof(MaterialDesignControlsBuilder), nameof(ConfigureThemesFromResources));
            });
        
        return builder;
    }
    
    /// <summary>
    /// Overrides, partially or fully, default font sizes for texts.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialSizeOptions" /> configuration for font sizes.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureFontSize(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFontSize.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default font sizes for texts from Resources.
    /// MDC will match resource keys with <see cref="MaterialSizeOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureFontSizeFromResources(
        this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialSizeOptions>(
            ConfigureFontSize, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureFontSizeFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default font tracking sizes for texts.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialSizeOptions" /> configuration for font tracking sizes.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureFontTracking(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFontTracking.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default font tracking sizes for texts from Resources.
    /// MDC will match resource keys with <see cref="MaterialSizeOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureFontTrackingFromResources(
        this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialSizeOptions>(
            ConfigureFontTracking, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureFontTrackingFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default <see cref="Shadow"/> configuration for elevation levels.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialElevationOptions" /> configuration for elevation levels.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureElevation(this MaterialDesignControlsBuilder builder, MaterialElevationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialElevation.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default <see cref="Shadow"/> configuration for elevation levels from Resources.
    /// MDC will match resource keys with <see cref="MaterialElevationOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureElevationFromResources(this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialElevationOptions>(
            ConfigureElevation, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureElevationFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default string formats used by Material Design Controls.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialFormatOptions" /> configuration for string formats.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureStringFormat(this MaterialDesignControlsBuilder builder, MaterialFormatOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFormat.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default string formats from Resources.
    /// MDC will match resource keys with <see cref="MaterialFormatOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureStringFormatFromResources(
        this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialFormatOptions>(
            ConfigureStringFormat, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureStringFormatFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default icons used by Material Design Controls.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialIconOptions" /> configuration for icons.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureIcons(this MaterialDesignControlsBuilder builder, MaterialIconOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialIcon.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default icons used by Material Design Controls from Resources.
    /// MDC will match resource keys with <see cref="MaterialIconOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureIconsFromResources(this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialIconOptions>(
            ConfigureIcons, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureIconsFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default animations used by Material Design Controls.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialAnimationOptions" /> configuration for animations.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureAnimations(this MaterialDesignControlsBuilder builder, MaterialAnimationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialAnimation.Configure(options));
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default animations used by Material Design Controls from Resources.
    /// MDC will match resource keys with <see cref="MaterialAnimationOptions" /> property names.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="resourceDictionaryName"><see cref="ResourceDictionary"/> name. Optional. If not provided, MDC will scan for every Resource registered on Application.</param>
    /// <param name="resourcePrefix">Prefix for resources. Optional.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureAnimationsFromResources(
        this MaterialDesignControlsBuilder builder,
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
    {
        builder.ConfigureFromResources<MaterialAnimationOptions>(
            ConfigureAnimations, 
            resourceDictionaryName, 
            resourcePrefix, 
            nameof(MaterialDesignControlsBuilder), 
            nameof(ConfigureAnimationsFromResources));
        
        return builder;
    }

    /// <summary>
    /// Overrides, partially or fully, default <see cref="MaterialSnackbar"/> configuration globally.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialSnackbarOptions" /> configuration for <see cref="MaterialSnackbar"/>.</param>
    /// <returns>Updated Material Design Controls builder.</returns>
    public static MaterialDesignControlsBuilder ConfigureSnackbar(this MaterialDesignControlsBuilder builder, MaterialSnackbarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialSnackbarConfig.Configure(options));
        return builder;
    } 
    
    /// <summary>
    /// Registers a custom defined implementation for <see cref="IMaterialSnackbar"/> and set global default configuration.
    /// </summary>
    /// <param name="builder">Material Design Controls builder.</param>
    /// <param name="options"><see cref="MaterialSnackbarOptions" /> configuration for <see cref="MaterialSnackbar"/>.</param>
    /// <typeparam name="T">User-defined implementation for <see cref="IMaterialSnackbar"/>.</typeparam>
    /// <returns></returns>
    public static MaterialDesignControlsBuilder ConfigureSnackbar<T>(this MaterialDesignControlsBuilder builder, MaterialSnackbarOptions? options = null) where T : IMaterialSnackbar
    {
        ConfigurationWithLogger(() =>
        {
            var sd = builder.AppBuilder.Services.FirstOrDefault(s => s.ServiceType == typeof(IMaterialSnackbar));
            if (sd != null) builder.AppBuilder.Services.Remove(sd);
        
            builder.AppBuilder.Services.AddSingleton(typeof(IMaterialSnackbar), typeof(T));
            if (options != null)
            {
                MaterialSnackbarConfig.Configure(options);
            }
        });
        
        return builder;
    }

    #region MauiAppBuilder

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        ConfigurationWithLogger(() =>
        {
            services.AddSingleton<IMaterialSnackbar, MaterialSnackbar>();
        });
        return services;
    }

    private static void ConfigureHandlers(IMauiHandlersCollection handlers)
    {
        ConfigurationWithLogger(() =>
        {
            handlers
                .AddHandler(typeof(CustomButton), typeof(MaterialButtonHandler ))
                .AddHandler(typeof(CustomRadioButton), typeof(MaterialRadioButtonHandler))
                .AddHandler(typeof(CustomEntry), typeof(MaterialTextFieldHandler))
                .AddHandler(typeof(CustomTimePicker), typeof(MaterialTimePickerHandler))
                .AddHandler(typeof(CustomDatePicker), typeof(MaterialDatePickerHandler))
                .AddHandler(typeof(CustomPicker), typeof(MaterialPickerHandler))
                .AddHandler(typeof(CustomEditor), typeof(MaterialMultilineTextFieldHandler))
                .AddHandler(typeof(CustomCheckBox), typeof(MaterialCheckBoxHandler))
                .AddHandler(typeof(CustomSlider), typeof(MaterialSliderHandler))
                .AddHandler(typeof(MaterialBottomSheet), typeof(BottomSheetHandler));

            if (_configureHandlers is not null)
            {
                var customHandlers = new MaterialHandlerOptions();
                _configureHandlers.Invoke(customHandlers);

                if (customHandlers is not null && customHandlers.Any())
                {
                    foreach (var customHandler in customHandlers)
                    {
                        if (!_controlHandlerMappings.Any(x => x.Item1 == customHandler.Key))
                        {
                            throw new ArgumentException("One of the configured handlers uses a ViewType that is not recognized as a customizable control in Material Design");
                        }

                        var controlHandlerMapping = _controlHandlerMappings.FirstOrDefault(x => x.Item1 == customHandler.Key);
                        if (customHandler.Value != null && customHandler.Value.IsSubclassOf(controlHandlerMapping.Item3))
                        {
                            handlers.AddHandler(controlHandlerMapping.Item2, customHandler.Value);
                            Logger.Debug($"The {customHandler.Value.Name} handler has been configured for the {controlHandlerMapping.Item1.Name} control");
                        }
                        else
                        {
                            throw new ArgumentException($"The handler configured for the {controlHandlerMapping.Item1.Name} control must inherit from {controlHandlerMapping.Item3.Name}");
                        }
                    }
                }
                else
                {
                    Logger.Debug("No custom handlers have been set for configuration");
                }
            }
            else
            {
                Logger.Debug("No custom handlers have been set for configuration");
            }

        }, @throw: true);
    }

    private static void ConfigureLifeCycleEvents(ILifecycleBuilder appLifeCycle)
    {
#if ANDROID
        appLifeCycle.AddAndroid(android => android
            .OnApplicationCreate(_ =>
            {
                RegisterDefaultStyles();
            }));
#elif IOS || MACCATALYST
        appLifeCycle.AddiOS(ios => ios
            .FinishedLaunching((_, _) =>
            {
                RegisterDefaultStyles();
                return true;
            }));
#elif WINDOWS
        appLifeCycle.AddWindows(windows => windows
            .OnLaunched((_, _) =>
            {
                RegisterDefaultStyles();
            }));
#endif
    }
    
    #endregion MauiAppBuilder
    
    #region Styles & Resources
    
    private static void RegisterDefaultStyles()
    {
        Logger.Debug("Start registering components default styles");
        if (Application.Current == null)
        {
            Logger.Debug("Error initializing Material Design Controls: MAUI Application is null.");
            return;
        }

        Application.Current.Resources
            .AddStyles(MaterialButton.GetStyles())
            .AddStyles(MaterialIconButton.GetStyles())
            .AddStyles(MaterialSwitch.GetStyles())
            .AddStyles(MaterialCard.GetStyles())
            .AddStyles(MaterialRadioButton.GetStyles())
            .AddStyles(MaterialCheckBox.GetStyles())
            .AddStyles(MaterialTextField.GetStyles())
            .AddStyles(MaterialChips.GetStyles())
            .AddStyles(MaterialRating.GetStyles())
            .AddStyles(MaterialSelection.GetStyles())
            .AddStyles(MaterialTimePicker.GetStyles())
            .AddStyles(MaterialDatePicker.GetStyles())
            .AddStyles(MaterialPicker.GetStyles())
            .AddStyles(MaterialMultilineTextField.GetStyles())
            .AddStyles(MaterialSlider.GetStyles())
            .AddStyles(MaterialFloatingButton.GetStyles())
            .AddStyles(MaterialSegmentedButton.GetStyles());

        Logger.Debug("Components styles registration COMPLETED");
    }
    
    private static ResourceDictionary AddStyles(this ResourceDictionary resources, IEnumerable<Style> styles)
    {
        foreach (var style in styles)
        {
            Logger.Debug($"Registering default style for {style.TargetType.Name}");
            
            var implicitStyleName = style.TargetType.FullName!;
            if (resources.TryGetValue(implicitStyleName, out var resource))
            {
                if (resource is Style currentStyle)
                {
                    Logger.Debug($"Applying Developer's overrides to default style for {style.TargetType.Name}");
                    foreach (var setter in currentStyle.Setters)
                    {
                        style.Setters.Add(setter);
                    }    
                }
                resources[implicitStyleName] = style;
            }
            else
            {
                resources.Add(style);    
            }
        }    
    
        return resources;
    }

    private static MaterialDesignControlsBuilder ConfigureFromResources<T>(this MaterialDesignControlsBuilder builder, 
        Func<MaterialDesignControlsBuilder, T, MaterialDesignControlsBuilder> func, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null,
        string? callerFilePath = null,
        string? callerMemberName = null) where T : new()
    {
        var methodName = callerMemberName?
            .Replace("Configure", string.Empty)
            .Replace("FromResources", string.Empty);
        
        Logger.DebugWithCallerInfo($"Enqueuing {methodName} loading task from resources", callerFilePath, callerMemberName);
        
        MaterialDesignControls.EnqueueAction(
            resourceDictionaryName,
            resources =>
            {
                Logger.DebugWithCallerInfo($"Configuring {methodName} from Resources source: {resources.Source}", callerFilePath, callerMemberName);
                
                var opt = resources.FromResources<T>(resourcePrefix);
                func(builder, opt);    
                
                Logger.DebugWithCallerInfo($"{methodName} configuration from resources COMPLETED", callerFilePath, callerMemberName);
            });
        
        return builder;
    }
    
    private static T FromResources<T>(this ResourceDictionary dictionary, string? prefix = null) where T : new()
    {
        var resSource = dictionary.Source?.ToString();
        resSource = string.IsNullOrEmpty(resSource) ? "ALL MERGED" : resSource;
        
        Logger.Debug($"Loading resources from {resSource} into {typeof(T).Name}");
        T result = new T();

        var propertiesSet = 0;
        foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var resourceKey = $"{prefix ?? string.Empty}{propInfo.Name}";
            if (dictionary.TryGetValue(resourceKey, out object? resourceValue))
            {
                propInfo.SetValue(result, resourceValue);
                propertiesSet++;
            }
        }
        Logger.Debug($"Properties loaded from resources: {propertiesSet}");
        Logger.Debug($"Loaded configuration: {JsonSerializer.Serialize(result, JsonSerializationUtils.Instance.SerializerOptions)}");
        
        return result;
    }
    
    #endregion Styles & Resources
    
    #region Logging
    
    private static void ConfigurationWithLogger(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        Logger.DebugWithCallerInfo("Init configuration", callerFilePath, callerMemberName);
        Logger.LogOnExceptionWithCallerInfo(onTry,
            ex =>
            {
                Logger.DebugWithCallerInfo("Configuration completed with ERROR", callerFilePath, callerMemberName);
                onCatch?.Invoke(ex);
            }, @throw, callerFilePath, callerMemberName);
        Logger.DebugWithCallerInfo("Configuration COMPLETED", callerFilePath, callerMemberName);
    }
    
    private static void ConfigurationWithLoggerAndCaller(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, string? callerFilePath = null, string? callerMemberName = null)
    {
        Logger.DebugWithCallerInfo("Init configuration", callerFilePath, callerMemberName);
        Logger.LogOnExceptionWithCallerInfo(onTry,
            ex =>
            {
                Logger.DebugWithCallerInfo("Configuration completed with ERROR", callerFilePath, callerMemberName);
                onCatch?.Invoke(ex);
            }, @throw, callerFilePath, callerMemberName);
        Logger.DebugWithCallerInfo("Configuration COMPLETED", callerFilePath, callerMemberName);
    }
    
    #endregion Logging
}
