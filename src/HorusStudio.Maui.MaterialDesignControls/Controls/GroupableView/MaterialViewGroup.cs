namespace HorusStudio.Maui.MaterialDesignControls;

internal static class MaterialViewGroup
{
    internal static void UncheckOtherMaterialGroupableViewInScope<T>(T groupableView, BindableProperty isControlSelectedBindableProperty) 
        where T : Element, IGroupableView
    {
        if (!string.IsNullOrEmpty(groupableView.GroupName))
        {
            var root = groupableView.GetVisualRoot() ?? groupableView.Parent;
            if (root is not IElementController rootController)
            {
                return;
            }

            foreach (var child in rootController.LogicalChildren)
            {
                UncheckMatchingDescendants(child, groupableView.GroupName, groupableView, isControlSelectedBindableProperty);
            }
        }
        else
        {
            if (groupableView.Parent is not IElementController parentController)
            {
                return;
            }

            foreach (var child in parentController.LogicalChildren)
            {
                if (child is T groupableViewChild && string.IsNullOrEmpty(groupableViewChild.GroupName))
                {
                    UncheckMaterialGroupableViewIfChecked(groupableViewChild, groupableView, isControlSelectedBindableProperty);
                }
            }
        }
    }
    
    private static void UncheckMaterialGroupableViewIfChecked<T>(T child, T groupableView, BindableProperty isControlSelectedBindableProperty)
        where T : Element, IGroupableView
    {
        if (child != groupableView && child.IsSelected)
        {
            child.SetValue(isControlSelectedBindableProperty, false);
        }
    }

    private static void UncheckMatchingDescendants<T>(Element element, string groupName, T groupableView, BindableProperty isControlSelectedBindableProperty)
        where T : Element, IGroupableView
    {
        if (element is T groupableViewChild && groupableViewChild.GroupName == groupName)
        {
            UncheckMaterialGroupableViewIfChecked(groupableViewChild, groupableView, isControlSelectedBindableProperty);
        }

        if (element is IElementController controller)
        {
            foreach (var child in controller.LogicalChildren)
            {
                UncheckMatchingDescendants(child, groupName, groupableView, isControlSelectedBindableProperty);
            }
        }
    }
    
    internal static void OnControllerChanged(BindableObject bindableObject, 
        Func<BindableObject, string> GetGroupName, 
        Func<BindableObject, object?> GetSelectedValue,
        MaterialViewGroupController? newController)
    {
        if (newController == null)
        {
            return;
        }

        newController.GroupName = GetGroupName.Invoke(bindableObject);
        newController.SelectedValue = GetSelectedValue.Invoke(bindableObject);
    }
}