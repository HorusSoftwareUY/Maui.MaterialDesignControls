using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialViewGroup">view group</see> selection types.
/// </summary>
public enum SelectionType
{
	/// <summary>Single selection</summary>
	Single,
	/// <summary>Multiple selection</summary>
	Multiple
}

/// <summary>
/// View groups are logical groups of <see cref="IGroupableView">IGroupableView</see> views. 
/// This class provides the core logic for handling views that share a common parent layout and are grouped together.
/// </summary>
/// <remarks>We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs">See here.</see></remarks>
/// <todoList>
/// * The SelectedValues property only supports binding to properties of type <see cref="IList{object}">IList{object}</see> or to classes that inherit from it.
/// </todoList>
public static class MaterialViewGroup
{
    static readonly BindableProperty GroupControllerProperty =
		BindableProperty.CreateAttached("GroupController", typeof(MaterialViewGroupController), typeof(Element), null,
		propertyChanged: (b, o, n) =>
		{
			if (n is MaterialViewGroupController newController)
			{
				newController.GroupName = GetGroupName(b);
				newController.SelectedValue = GetSelectedValue(b);
				newController.SelectedValues = GetSelectedValues(b);
				newController.SelectedValueChangedCommand = GetSelectedValueChangedCommand(b);
				newController.SelectionType = GetSelectionType(b);
			}
		});
    
	static MaterialViewGroupController? GetGroupController(BindableObject b)
	{
		return (MaterialViewGroupController?)b.GetValue(GroupControllerProperty);
	}
	
	/// <summary>
	/// Sets the group controller for the set of views that will be grouped together.
	/// </summary>
	static void SetGroupController(BindableObject bindable, MaterialViewGroupController groupController)
	{
		bindable.SetValue(GroupControllerProperty, groupController);
	}

	/// <summary>
	/// The backing store for the <see cref="GroupName">GroupName</see> bindable property.
	/// </summary>
	public static readonly BindableProperty GroupNameProperty =
		BindableProperty.Create("GroupName", typeof(string), typeof(Element), null,
		propertyChanged: (b, o, n) =>
		{
			if (n is not string groupName || string.IsNullOrEmpty(groupName))
			{
				return;
			}
			
			if (b is Layout layout)
			{
				var groupController = GetGroupController(layout);
				
				if (groupController == null)
				{
					groupController = new MaterialViewGroupController(layout);
					groupController.SelectedValue = GetSelectedValue(layout);
					groupController.SelectedValues = GetSelectedValues(layout);
					groupController.SelectedValueChangedCommand = GetSelectedValueChangedCommand(layout);
					groupController.SelectionType = GetSelectionType(layout);
					SetGroupController(layout, groupController);
				}
				
				groupController.GroupName = groupName;
				groupController.Init();
			}
			else if (b is IGroupableView)
			{
				var groupController = MaterialViewGroupController.GetGroupController(groupName);
				if (groupController == null)
				{
					var parentLayout = ((Element)b).GetParent<Layout>();
					if (parentLayout == null)
					{
						((Element)b).ParentChanged += GroupableViewParentChanged;
					}
					else
					{
						SetPropagateGroupName(parentLayout, false);
						SetGroupName(parentLayout, groupName);
					}
				}
			}
		});
	
	private static void GroupableViewParentChanged(object? sender, EventArgs e)
	{
		if (sender != null && sender is Element element)
		{
			element.ParentChanged -= GroupableViewParentChanged;
			
			if (sender is IGroupableView)
			{
				var parentLayout = element.GetParent<Layout>();
				if (parentLayout != null)
				{
					var groupName = GetGroupName(element);
					var groupController = MaterialViewGroupController.GetGroupController(groupName);
					if (groupController == null)
					{
						SetPropagateGroupName(parentLayout, false);
						SetGroupName(parentLayout, groupName);
					}
				}
			}
		}
	}

	/// <summary>
	/// Returns the group name for the set of views that will be grouped together.
	/// </summary>
	public static string GetGroupName(BindableObject b)
	{
		return (string)b.GetValue(GroupNameProperty);
	}

	/// <summary>
	/// Sets the group name for the set of views that will be grouped together.
	/// </summary>
	public static void SetGroupName(BindableObject bindable, string groupName)
	{
		bindable.SetValue(GroupNameProperty, groupName);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectedValue">SelectedValue</see> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueProperty =
		BindableProperty.Create("SelectedValue", typeof(object), typeof(Element), null,
		defaultBindingMode: BindingMode.TwoWay,
		propertyChanged: (b, o, n) =>
		{
			var groupController = GetGroupController(b);
			if (groupController != null)
			{
				groupController.SelectedValue = n;
			}
		});

	/// <summary>
	/// Returns the selected value for the group of views that have been grouped together.
	/// </summary>
	public static object GetSelectedValue(BindableObject bindableObject)
	{
		return bindableObject.GetValue(SelectedValueProperty);
	}

	/// <summary>
	/// Sets the selected value for the group of views that have been grouped together.
	/// </summary>
	public static void SetSelectedValue(BindableObject bindable, object selectedValue)
	{
		bindable.SetValue(SelectedValueProperty, selectedValue);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectedValues">SelectedValues</see> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValuesProperty =
		BindableProperty.Create("SelectedValues", typeof(IList<object>), typeof(Element), null,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (b, o, n) =>
			{
				var groupController = GetGroupController(b);
				if (groupController != null)
				{
					groupController.SelectedValues = (IList<object>)n;
				}
			});

	/// <summary>
	/// Returns the collection of selected values for the group of views that have been grouped together.
	/// </summary>
	public static IList<object> GetSelectedValues(BindableObject bindableObject)
	{
		return (IList<object>)bindableObject.GetValue(SelectedValuesProperty);
	}

	/// <summary>
	/// Sets the collection of selected values for the group of views that have been grouped together.
	/// </summary>
	public static void SetSelectedValues(BindableObject bindable, IList<object> selectedValues)
	{
		bindable.SetValue(SelectedValuesProperty, selectedValues);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectedValueChangedCommand">SelectedValueChangedCommand</see> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueChangedCommandProperty =
		BindableProperty.Create("SelectedValueChangedCommand", typeof(ICommand), typeof(Element), null,
			propertyChanged: (b, o, n) =>
			{
				var groupController = GetGroupController(b);
				if (groupController != null)
				{
					groupController.SelectedValueChangedCommand = (ICommand)n;
				}
			});
	
	/// <summary>
	/// Returns the command to be executed when the selected value changes for the group of views that are grouped together.
	/// </summary>
	public static ICommand GetSelectedValueChangedCommand(BindableObject b)
	{
		return (ICommand)b.GetValue(SelectedValueChangedCommandProperty);
	}

	/// <summary>
	/// Sets the command to be executed when the selected value changes for the group of views that are grouped together.
	/// </summary>
	public static void SetSelectedValueChangedCommand(BindableObject bindable, ICommand groupName)
	{
		bindable.SetValue(SelectedValueChangedCommandProperty, groupName);
	}
	
	/// <summary>
	/// The backing store for the <see cref="SelectionType">SelectionType</see> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectionTypeProperty =
		BindableProperty.Create("SelectionType", typeof(SelectionType), typeof(Element), SelectionType.Single,
			propertyChanged: (b, o, n) =>
			{
				var groupController = GetGroupController(b);
				if (groupController != null)
				{
					groupController.SelectionType = (SelectionType)n;
				}
			});

	/// <summary>
	/// Returns the selection type for the group of views that are grouped together.
	/// </summary>
	public static SelectionType GetSelectionType(BindableObject b)
	{
		return (SelectionType)b.GetValue(SelectionTypeProperty);
	}

	/// <summary>
	/// Sets the selection type for the group of views that are grouped together.
	/// </summary>
	public static void SetSelectionType(BindableObject bindable, SelectionType selectionType)
	{
		bindable.SetValue(SelectionTypeProperty, selectionType);
	}
	
	/// <summary>
	/// The backing store for the <see cref="PropagateGroupName">PropagateGroupName</see> bindable property.
	/// </summary>
	internal static readonly BindableProperty PropagateGroupNameProperty =
		BindableProperty.Create("PropagateGroupName", typeof(bool), typeof(Element), true);

	/// <summary>
	/// Returns if the group controller should propagate the group name to all <see cref="IGroupableView">child</see> instances.
	/// </summary>
	internal static bool GetPropagateGroupName(BindableObject b)
	{
		return (bool)b.GetValue(PropagateGroupNameProperty);
	}

	/// <summary>
	/// Sets if the group controller should propagate the group name to all <see cref="IGroupableView">child</see> instances.
	/// </summary>
	internal static void SetPropagateGroupName(BindableObject bindable, bool propagateGroupName)
	{
		bindable.SetValue(PropagateGroupNameProperty, propagateGroupName);
	}
}