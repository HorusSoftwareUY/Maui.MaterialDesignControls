using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;

namespace HorusStudio.Maui.MaterialDesignControls;

public sealed record MaterialDesignControlsBuilder(MauiAppBuilder AppBuilder);

public static class MaterialDesignControlsBuilderExtensions
{
    private static readonly HashSet<IntialAction> InitialActions = [];
    
    public static MauiAppBuilder UseMaterialDesignControls(this MauiAppBuilder appBuilder,
        Action<MaterialDesignControlsBuilder>? configureDelegate = null)
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
        
        return appBuilder;
    }

    public static MaterialDesignControlsBuilder ConfigureFonts(this MaterialDesignControlsBuilder builder, Action<IFontCollection>? configureDelegate, MaterialFontOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        
        builder.AppBuilder.ConfigureFonts(fonts => 
        {
            configureDelegate?.Invoke(fonts);
            MaterialFontFamily.Configure(fonts, options);    
        });

        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureThemes(this MaterialDesignControlsBuilder builder, MaterialTheme? lightTheme, MaterialTheme? darkTheme)
    {
        if (lightTheme is not null) MaterialLightTheme.Configure(lightTheme);
        if (darkTheme is not null) MaterialDarkTheme.Configure(darkTheme);
        
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureThemesFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? lightThemeResourcePrefix = null,
        string? darkThemeResourcePrefix = null)
    {
        InitialActions.Add(new IntialAction
        {
            ResourceDictionaryName = resourceDictionaryName,
            Action = resources =>
            {
                builder.ConfigureThemes(
                    resources?.FromResources<MaterialTheme>(lightThemeResourcePrefix), 
                    resources?.FromResources<MaterialTheme>(darkThemeResourcePrefix));
            } 
        });
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureFontSize(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialFontSize.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureFontSizeFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null) 
        => builder.ConfigureFromResources<MaterialSizeOptions>(ConfigureFontSize, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureFontTracking(this MaterialDesignControlsBuilder builder, MaterialSizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialFontTracking.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureFontTrackingFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
        => builder.ConfigureFromResources<MaterialSizeOptions>(ConfigureFontTracking, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureElevation(this MaterialDesignControlsBuilder builder, MaterialElevationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialElevation.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureElevation(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
        => builder.ConfigureFromResources<MaterialElevationOptions>(ConfigureElevation, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureStringFormat(this MaterialDesignControlsBuilder builder, MaterialFormatOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialFormat.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureStringFormatFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
        => builder.ConfigureFromResources<MaterialFormatOptions>(ConfigureStringFormat, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureIcons(this MaterialDesignControlsBuilder builder, MaterialIconOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialIcon.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureIconsFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
        => builder.ConfigureFromResources<MaterialIconOptions>(ConfigureIcons, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureAnimations(this MaterialDesignControlsBuilder builder, MaterialAnimationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        MaterialAnimation.Configure(options);
        return builder;
    }
    
    public static MaterialDesignControlsBuilder ConfigureAnimationsFromResources(this MaterialDesignControlsBuilder builder, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null)
        => builder.ConfigureFromResources<MaterialAnimationOptions>(ConfigureAnimations, resourceDictionaryName, resourcePrefix);
    
    public static MaterialDesignControlsBuilder ConfigureSnackbar(this MaterialDesignControlsBuilder builder, MaterialSnackbarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        SnackbarConfig.Configure(options);
        return builder;
    } 
    
    public static MaterialDesignControlsBuilder ConfigureSnackbar<T>(this MaterialDesignControlsBuilder builder, MaterialSnackbarOptions? options = null) where T : IMaterialSnackbar
    {
        var sd = builder.AppBuilder.Services.FirstOrDefault(s => s.ServiceType == typeof(IMaterialSnackbar));
        if (sd != null) builder.AppBuilder.Services.Remove(sd);
        
        builder.AppBuilder.Services.AddSingleton(typeof(IMaterialSnackbar), typeof(T));
        if (options != null)
        {
            SnackbarConfig.Configure(options);
        }

        return builder;
    }

    private static MaterialDesignControlsBuilder ConfigureFromResources<T>(this MaterialDesignControlsBuilder builder, 
        Func<MaterialDesignControlsBuilder, T, MaterialDesignControlsBuilder> func, 
        string? resourceDictionaryName = null,
        string? resourcePrefix = null) where T : new()
    {
        InitialActions.Add(new IntialAction
        {
            ResourceDictionaryName = resourceDictionaryName,
            Action = resources =>
            {
                var opt = resources.FromResources<T>(resourcePrefix);
                func(builder, opt);
            }
        });
        return builder;
    }
    
    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services.AddSingleton<IMaterialSnackbar, MaterialSnackbar>();
    }
    
    private static void ConfigureHandlers(IMauiHandlersCollection handlers)
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
     
        var resources = Application.Current.Resources;
        if (InitialActions.Count > 0)
        {
            ResourceDictionary? allResources = null;
            foreach (var initialAction in InitialActions)
            {
                var res = GetResources(resources, initialAction.ResourceDictionaryName, ref allResources);
                initialAction.Action(res);
            }
            InitialActions.Clear();
        }
        
        RegisterDefaultStyles(resources);
    }

    private static ResourceDictionary GetResources(ResourceDictionary rootResources, string? resourcesName, ref ResourceDictionary? allResources)
    {
        if (resourcesName != null && 
            rootResources.MergedDictionaries.FirstOrDefault(d =>
                d.Source != null && d.Source.ToString().Contains(resourcesName)) is { } rd) return rd;
            
        allResources ??= rootResources.Flatten();
        return allResources;
    } 
    
    private static void RegisterDefaultStyles(ResourceDictionary resources)
    {
        resources
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
    }

    private static ResourceDictionary AddStyles(this ResourceDictionary resources, IEnumerable<Style> styles)
    {
        foreach (var style in styles)
        {
            resources.Add(style);
        }

        return resources;
    }

    private static T FromResources<T>(this ResourceDictionary dictionary, string? prefix = null) where T : new()
    {
        T result = new T();
        
        foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var resourceKey = $"{prefix ?? string.Empty}{propInfo.Name}";
            if (dictionary.TryGetValue(resourceKey, out object? resourceValue))
            {
                propInfo.SetValue(result, resourceValue);
            }
        }

        return result;
    }

    private static ResourceDictionary Flatten(this ResourceDictionary resources)
    {
        var result = new ResourceDictionary();
        
        resources.CopyTo(result);
        foreach (var mergedDictionary in resources.MergedDictionaries)
        {
            mergedDictionary.CopyTo(result);
        }
        
        return result;
    }

    private static void CopyTo(this ResourceDictionary source, ResourceDictionary destination)
    {
        foreach (var key in source.Keys)
        {
            if (!destination.ContainsKey(key))
            {
                destination.Add(key, source[key]);    
            }
        }
    }

    private class IntialAction
    {
        public required Action<ResourceDictionary> Action { get; set; }
        public string? ResourceDictionaryName { get; set; }
    }
}