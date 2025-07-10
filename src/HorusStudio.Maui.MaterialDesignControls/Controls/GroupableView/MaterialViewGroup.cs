using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialViewGroup" /> selection types
/// </summary>
public enum SelectionType
{
	/// <summary>Single selection</summary>
	Single,
	/// <summary>Multiple selection</summary>
	Multiple
}

/// <summary>
/// Manages a logical group of <see cref="IGroupableView"/> controls. 
/// This class provides the core logic for handling views that share a common parent layout and are grouped together.
/// </summary>
/// <remarks>We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs">See here.</see></remarks>
public static class MaterialViewGroup
{
    static readonly BindableProperty GroupControllerProperty =
		BindableProperty.CreateAttached("GroupController", typeof(MaterialViewGroupController), typeof(Element), default(MaterialViewGroupController),
		defaultValueCreator: (b) => new MaterialViewGroupController(b as Element),
		propertyChanged: (b, o, n) => OnControllerChanged(b, (MaterialViewGroupController)n));
	
	static MaterialViewGroupController GetGroupController(BindableObject b)
	{
		return (MaterialViewGroupController)b.GetValue(GroupControllerProperty);
	}

	/// <summary>
	/// The backing store for the <see cref="GroupName" /> bindable property.
	/// </summary>
	public static readonly BindableProperty GroupNameProperty =
		BindableProperty.Create("GroupName", typeof(string), typeof(Element), null,
		propertyChanged: (b, o, n) => { GetGroupController(b).GroupName = (string)n; });

	/// <summary>
	/// Returns the bindableObject's group name
	/// </summary>
	public static string GetGroupName(BindableObject b)
	{
		return (string)b.GetValue(GroupNameProperty);
	}

	/// <summary>
	/// Sets the bindableObject's group name
	/// </summary>
	public static void SetGroupName(BindableObject bindable, string groupName)
	{
		bindable.SetValue(GroupNameProperty, groupName);
	}

	/// <summary>
	/// The backing store for the <see cref="SelectedValue" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueProperty =
		BindableProperty.Create("SelectedValue", typeof(object), typeof(Element), null,
		defaultBindingMode: BindingMode.TwoWay,
		propertyChanged: (b, o, n) => { GetGroupController(b).SelectedValue = n; });

	/// <summary>
	/// Returns the bindableObject's selected value
	/// </summary>
	public static object GetSelectedValue(BindableObject bindableObject)
	{
		return bindableObject.GetValue(SelectedValueProperty);
	}

	/// <summary>
	/// Sets the bindableObject's selected value
	/// </summary>
	public static void SetSelectedValue(BindableObject bindable, object selectedValue)
	{
		bindable.SetValue(SelectedValueProperty, selectedValue);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectedValueChangedCommand" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueChangedCommandProperty =
		BindableProperty.Create("SelectedValueChangedCommand", typeof(ICommand), typeof(Element), null,
			propertyChanged: (b, o, n) => { GetGroupController(b).SelectedValueChangedCommand = (ICommand)n; });
	
	/// <summary>
	/// Returns the bindableObject's selected value changed command
	/// </summary>
	public static ICommand GetSelectedValueChangedCommand(BindableObject b)
	{
		return (ICommand)b.GetValue(SelectedValueChangedCommandProperty);
	}

	/// <summary>
	/// Sets the bindableObject's selected value changed command
	/// </summary>
	public static void SetSelectedValueChangedCommand(BindableObject bindable, ICommand groupName)
	{
		bindable.SetValue(SelectedValueChangedCommandProperty, groupName);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectionType" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectionTypeProperty =
		BindableProperty.Create("SelectionType", typeof(SelectionType), typeof(Element), SelectionType.Single,
			propertyChanged: (b, o, n) => { GetGroupController(b).SelectionType = (SelectionType)n; });

	/// <summary>
	/// Returns the bindableObject's selection type
	/// </summary>
	public static SelectionType GetSelectionType(BindableObject b)
	{
		return (SelectionType)b.GetValue(SelectionTypeProperty);
	}

	/// <summary>
	/// Sets the bindableObject's selection type
	/// </summary>
	public static void SetSelectionType(BindableObject bindable, SelectionType selectionType)
	{
		bindable.SetValue(SelectionTypeProperty, selectionType);
	}
	
    internal static void UncheckOtherMaterialGroupableViewInScope<T>(T groupableView) 
        where T : IGroupableView
    {
        if (groupableView is not Element element)
        {
            return;
        }
        
        if (!string.IsNullOrEmpty(groupableView.GroupName))
        {
            var root = element.GetVisualRoot() ?? element.Parent;
            if (root is not IElementController rootController)
            {
                return;
            }

            foreach (var child in rootController.LogicalChildren)
            {
                UncheckMatchingDescendants(child, groupableView.GroupName, groupableView);
            }
        }
        else
        {
            if (element.Parent is not IElementController parentController)
            {
                return;
            }

            foreach (var child in parentController.LogicalChildren)
            {
                if (child is T groupableViewChild && string.IsNullOrEmpty(groupableViewChild.GroupName))
                {
                    UncheckMaterialGroupableViewIfChecked(groupableViewChild, groupableView);
                }
            }
        }
    }
    
    private static void UncheckMaterialGroupableViewIfChecked<T>(T child, T groupableView)
        where T : IGroupableView
    {
        if (!child.Equals(groupableView) && child.IsSelected)
        {
            child.IsSelected = false;
        }
    }

    private static void UncheckMatchingDescendants<T>(Element element, string groupName, T groupableView)
        where T : IGroupableView
    {
        if (element is T groupableViewChild && groupableViewChild.GroupName == groupName)
        {
            UncheckMaterialGroupableViewIfChecked(groupableViewChild, groupableView);
        }

        if (element is IElementController controller)
        {
            foreach (var child in controller.LogicalChildren)
            {
                UncheckMatchingDescendants(child, groupName, groupableView);
            }
        }
    }
    
    private static void OnControllerChanged(BindableObject bindableObject, MaterialViewGroupController? newController)
    {
        if (newController == null)
        {
            return;
        }
        
        newController.GroupName = GetGroupName(bindableObject);
        newController.SelectedValue = GetSelectedValue(bindableObject);
    }
}