namespace Microsoft.Maui;

static class IElementExtensions
{
    public static void DebugTreeView(this IElement element)
    {
        var parentElements = element.GetParentElements();
        var tree = parentElements.Select(e => e.GetType().Name).Reverse();
        System.Diagnostics.Debug.WriteLine($"TreeElements: {string.Join(" > ", tree)}");
    }
    
    public static IEnumerable<IElement> GetParentElements(this IElement element)
	{
        var parentElements = new List<IElement>();
        element.GetParentElements(parentElements);
        return parentElements;
	}

    public static T GetParent<T>(this IElement element) where T : IElement
    {
        if (element?.Parent is null) return default;
        if (element.Parent is T parent) return parent;

        return element.Parent.GetParent<T>();
    }

    private static void GetParentElements(this IElement element, List<IElement> parentElements)
    {
        if (element?.Parent is null) return;

        parentElements.Add(element.Parent);
        element.Parent.GetParentElements(parentElements);
    }
}

