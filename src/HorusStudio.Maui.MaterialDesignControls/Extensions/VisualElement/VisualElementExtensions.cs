using System.Runtime.CompilerServices;
using System.Text;
using HorusStudio.Maui.MaterialDesignControls.Utils;
using static Microsoft.Maui.Controls.VisualStateManager;

namespace Microsoft.Maui.Controls
{
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
    }
}
