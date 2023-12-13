using System;
using System.Diagnostics;

namespace Microsoft.Maui.Controls
{
	public static class VisualElementExtensions
	{
		public static void PrintVisualStates(this VisualElement visualElement)
		{
            Debug.WriteLine("-------------------   VisualStates   --------------------");
            Debug.WriteLine(string.Empty);
            if (visualElement.HasVisualStateGroups())
            {
                var visualStateGroups = VisualStateManager.GetVisualStateGroups(visualElement);
                if (visualStateGroups?.Any() ?? false)
                {
                    foreach (var group in visualStateGroups)
                    {
                        Debug.WriteLine($"{visualElement.GetType()} -> Group: {group.Name}, TargetType: {group.TargetType ?? visualElement.GetType()}, CurrentState: {group.CurrentState?.Name}");
                        foreach (var state in group.States)
                        {
                            Debug.WriteLine($"      [{state.Name}, {string.Join(", ", state.Setters.Select(s => $"({s.Property.PropertyName}, {s.TargetName}, {s.Value})"))}]");
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine($"{visualElement.GetType()} -> Undefined");
            }
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("---------------------------------------------------------");
        }
	}
}
