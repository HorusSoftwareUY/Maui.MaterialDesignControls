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

        public static object GetVisualStatePropertyValue(this VisualElement visualElement, string visualStateGroupName, string visualStateName, string propertyName)
        {
            var visualStateGroups = VisualStateManager.GetVisualStateGroups(visualElement);
            if (visualStateGroups != null)
            {
                var commonStates = visualStateGroups.Where(x => x.Name == visualStateGroupName);
                if (commonStates != null)
                {
                    foreach (var commonState in commonStates)
                    {
                        var onVisualStates = commonState.States.Where(x => x.Name == visualStateName);
                        if (onVisualStates != null)
                        {
                            foreach (var onVisualState in onVisualStates)
                            {
                                var onTrackColorSetter = onVisualState.Setters.FirstOrDefault(x => x.Property.PropertyName == propertyName);
                                if (onTrackColorSetter != null)
                                {
                                    return onTrackColorSetter.Value;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
