using System.Runtime.CompilerServices;
using System.Text;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace Microsoft.Maui.Controls;

static class VisualElementExtensions
{
	public static void PrintVisualStates(this VisualElement visualElement, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerName = null)
	{
        var builder = new StringBuilder();
        builder.AppendLine();
        builder.AppendLine("-------------------   VisualStates   --------------------");
        builder.AppendLine();
        if (visualElement.HasVisualStateGroups())
        {
            var visualStateGroups = VisualStateManager.GetVisualStateGroups(visualElement);
            if (visualStateGroups?.Any() ?? false)
            {
                foreach (var group in visualStateGroups)
                {
                    builder.AppendLine($"{visualElement.GetType()} -> Group: {group.Name}, TargetType: {group.TargetType ?? visualElement.GetType()}, CurrentState: {group.CurrentState?.Name}");
                    foreach (var state in group.States)
                    {
                        builder.AppendLine($"      [{state.Name}, {string.Join(", ", state.Setters.Select(s => $"({s.Property.PropertyName}, {s.TargetName}, {s.Value})"))}]");
                    }
                }
            }
        }
        else
        {
            builder.AppendLine($"{visualElement.GetType()} -> Undefined");
        }
        builder.AppendLine();
        builder.AppendLine("---------------------------------------------------------");

        Logger.Debug(builder.ToString(), callerFilePath, callerName);
    }
    
    public static void DebugTreeView(this IElement element, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerName = null)
    {
        var parentElements = element.GetParentElements();
        var tree = parentElements.Select(e => e.GetType().Name).Reverse();
        Logger.Debug($"TreeElements: {string.Join(" > ", tree)}", callerFilePath, callerName);
    }
    
    public static IEnumerable<IElement> GetParentElements(this IElement element)
    {
        var parentElements = new List<IElement>();
        element.GetParentElements(parentElements);
        return parentElements;
    }

    public static T? GetParent<T>(this IElement element) where T : IElement
    {
        return element?.Parent switch
        {
            null => default,
            T parent => parent,
            _ => element.Parent.GetParent<T>()
        };
    }

    private static void GetParentElements(this IElement element, List<IElement> parentElements)
    {
        if (element?.Parent is null) return;

        parentElements.Add(element.Parent);
        element.Parent.GetParentElements(parentElements);
    }
}
