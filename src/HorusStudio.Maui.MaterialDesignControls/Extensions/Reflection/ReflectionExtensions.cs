namespace System;

static class ReflectionExtensions
{
    public static object? GetPropertyValue(this object obj, string propertyName)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        return prop?.GetValue(obj);
    }
    
    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
        var prop = obj.GetPropertyValue(propertyName);
        if (prop is null || !prop.GetType().IsAssignableTo(typeof(T))) return default;
        return (T)prop;
    }
}