using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Material Design Controls initializer class
/// </summary>
public static class MaterialDesignControls
{
    private static readonly HashSet<InitialTask> InitialTasks = [];
    
    /// <summary>
    /// Register resources and initialize Material Design Controls
    /// </summary>
    public static void InitializeComponents()
    {   
        Logger.Debug("Start Components initialization");
        if (Application.Current == null)
        {
            Logger.Debug("Error initializing Material Design Controls: MAUI Application is null.");
            return;
        }

        var resources = Application.Current.Resources;
        if (InitialTasks.Count > 0)
        {
            Logger.Debug($"Initialization from Resources tasks found: {InitialTasks.Count}");
            ResourceDictionary? allResources = null;
            foreach (var task in InitialTasks)
            {
                Logger.Debug("Getting resources for initialization task");
                var res = GetResources(resources, task.ResourceDictionaryName, ref allResources);
                task.Action(res);
            }
            InitialTasks.Clear();
            Logger.Debug("Initialization tasks COMPLETED");
        }
        else
        {
            Logger.Debug("No initialization from Resources tasks where found");
        }
        
        Logger.Debug("Components initialization COMPLETED");
    }

    internal static void EnqueueAction(string? resourceDictionaryName, Action<ResourceDictionary> action) =>
        InitialTasks.Add(new InitialTask { ResourceDictionaryName = resourceDictionaryName, Action = action });
    
    private static ResourceDictionary GetResources(ResourceDictionary rootResources, string? resourcesName, ref ResourceDictionary? allResources)
    {
        if (resourcesName != null &&
            rootResources.MergedDictionaries.FirstOrDefault(d =>
                d.Source != null && d.Source.ToString().Contains(resourcesName)) is { } rd)
        {
            Logger.Debug($"Specific ResourceDictionary {resourcesName} found");
            return rd;
        }
        Logger.Debug($"ResourceDictionary {resourcesName} was not found");
        
        allResources ??= rootResources.Flatten();
        return allResources;
    }
    
    private static ResourceDictionary Flatten(this ResourceDictionary resources)
    {
        var result = new ResourceDictionary();
        Logger.Debug("Getting all merged resources");
        
        resources.CopyTo(result);
        foreach (var mergedDictionary in resources.MergedDictionaries)
        {
            mergedDictionary.CopyTo(result);
        }
        
        Logger.Debug("Resources discovery COMPLETED");
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
    
    private class InitialTask
    {
        public required Action<ResourceDictionary> Action { get; init; }
        public string? ResourceDictionaryName { get; init; }
    }
}