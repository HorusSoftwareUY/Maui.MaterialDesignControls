using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HorusStudio.Maui.MaterialDesignControls;

public sealed record MaterialDesignControlsBuilder(MauiAppBuilder AppBuilder);

public static class MaterialDesignControlsBuilderExtensions
{   
    public static MauiAppBuilder UseMaterialDesignControls(this MauiAppBuilder appBuilder,
        Action<MaterialDesignControlsBuilder>? configureDelegate = null)
    {   
        Logger.Debug("Configuring Material Design Controls");
        try
        {
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

    public static MaterialDesignControlsBuilder EnableDebug(this MaterialDesignControlsBuilder builder)
    {
        Logger.DebugMode = true;
        return builder;
    }

    private static void ConfigurationWithLogger(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        Logger.DebugWithCaller("Init configuration", callerFilePath, callerMemberName);
        Logger.LogOnExceptionWithCaller(onTry,
            ex =>
            {
                Logger.DebugWithCaller("Configuration completed with ERROR", callerFilePath, callerMemberName);
                onCatch?.Invoke(ex);
            }, @throw, callerFilePath, callerMemberName);
        Logger.DebugWithCaller("Configuration COMPLETED", callerFilePath, callerMemberName);
    }
    
    private static void ConfigurationWithLoggerAndCaller(Action onTry, Action<Exception>? onCatch = null, bool @throw = false, string? callerFilePath = null, string? callerMemberName = null)
    {
        Logger.DebugWithCaller("Init configuration", callerFilePath, callerMemberName);
        Logger.LogOnExceptionWithCaller(onTry,
            ex =>
            {
                Logger.DebugWithCaller("Configuration completed with ERROR", callerFilePath, callerMemberName);
                onCatch?.Invoke(ex);
            }, @throw, callerFilePath, callerMemberName);
        Logger.DebugWithCaller("Configuration COMPLETED", callerFilePath, callerMemberName);
    }
    
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
                Logger.DebugWithCaller($"Configuring Themes from Resources source: {resources.Source}", nameof(MaterialDesignControlsBuilder), nameof(ConfigureThemesFromResources));
                
                builder.ConfigureThemes(
                    resources.FromResources<MaterialTheme>(lightThemeResourcePrefix), 
                    resources.FromResources<MaterialTheme>(darkThemeResourcePrefix));
                
                Logger.DebugWithCaller("Themes configuration from resources COMPLETED", nameof(MaterialDesignControlsBuilder), nameof(ConfigureThemesFromResources));
            });
        
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureFontSize(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFontSize.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureFontTracking(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFontTracking.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureElevation(this MaterialDesignControlsBuilder builder, MaterialElevationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialElevation.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureStringFormat(this MaterialDesignControlsBuilder builder, MaterialFormatOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialFormat.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureIcons(this MaterialDesignControlsBuilder builder, MaterialIconOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialIcon.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureAnimations(this MaterialDesignControlsBuilder builder, MaterialAnimationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialAnimation.Configure(options));
        return builder;
    }

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

    public static MaterialDesignControlsBuilder ConfigureSnackbar(this MaterialDesignControlsBuilder builder, MaterialSnackbarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ConfigurationWithLogger(() => MaterialSnackbarConfig.Configure(options));
        return builder;
    } 
    
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
        
        Logger.DebugWithCaller($"Enqueuing {methodName} loading task from resources", callerFilePath, callerMemberName);
        
        MaterialDesignControls.EnqueueAction(
            resourceDictionaryName,
            resources =>
            {
                Logger.DebugWithCaller($"Configuring {methodName} from Resources source: {resources.Source}", callerFilePath, callerMemberName);
                
                var opt = resources.FromResources<T>(resourcePrefix);
                func(builder, opt);    
                
                Logger.DebugWithCaller($"{methodName} configuration from resources COMPLETED", callerFilePath, callerMemberName);
            });
        
        return builder;
    }
    
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
                .AddHandler(typeof(CustomButton), typeof(CustomButtonHandler))
                .AddHandler(typeof(CustomRadioButton), typeof(CustomRadioButtonHandler))
                .AddHandler(typeof(BorderlessEntry), typeof(BorderlessEntryHandler))
                .AddHandler(typeof(CustomTimePicker), typeof(CustomTimePickerHandler))
                .AddHandler(typeof(CustomDatePicker), typeof(CustomDatePickerHandler))
                .AddHandler(typeof(CustomPicker), typeof(CustomPickerHandler))
                .AddHandler(typeof(CustomEditor), typeof(CustomEditorHandler))
                .AddHandler(typeof(CustomCheckBox), typeof(CustomCheckboxHandler))
                .AddHandler(typeof(CustomSlider), typeof(CustomSliderHandler));
        });
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
            .AddStyles(MaterialFloatingButton.GetStyles());
        
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
}
