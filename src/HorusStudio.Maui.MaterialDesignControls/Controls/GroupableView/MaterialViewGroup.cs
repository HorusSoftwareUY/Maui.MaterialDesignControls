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
/// <todoList>
/// * The SelectedValues property only supports binding to properties of type <see cref="IList{object}"/> or to classes that inherit from it.
/// </todoList>
public static class MaterialViewGroup
{
    static readonly BindableProperty GroupControllerProperty =
		BindableProperty.CreateAttached("GroupController", typeof(MaterialViewGroupController), typeof(Element), default(MaterialViewGroupController),
		defaultValueCreator: (b) => new MaterialViewGroupController(b as Element),
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
    
	static MaterialViewGroupController GetGroupController(BindableObject b)
	{
		return (MaterialViewGroupController)b.GetValue(GroupControllerProperty);
	}

	/// <summary>
	/// The backing store for the <see cref="GroupName" /> bindable property.
	/// </summary>
	public static readonly BindableProperty GroupNameProperty =
		BindableProperty.Create("GroupName", typeof(string), typeof(Element), null,
		propertyChanged: (b, o, n) =>
		{
			GetGroupController(b).GroupName = (string)n;
		});

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
	/// The backing store for the <see cref="InternalGroupName" /> bindable property.
	/// </summary>
	internal static readonly BindableProperty InternalGroupNameProperty =
		BindableProperty.Create("InternalGroupName", typeof(string), typeof(Element), null,
			propertyChanged: (b, o, n) =>
			{
				
			});

	/// <summary>
	/// Returns the group name for the set of views that will be grouped together.
	/// </summary>
	internal static string GetInternalGroupName(BindableObject b)
	{
		return (string)b.GetValue(InternalGroupNameProperty);
	}

	/// <summary>
	/// Sets the group name for the set of views that will be grouped together.
	/// </summary>
	internal static void SetInternalGroupName(BindableObject bindable, string groupName)
	{
		bindable.SetValue(InternalGroupNameProperty, groupName);
	}

	/// <summary>
	/// The backing store for the <see cref="SelectedValue" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueProperty =
		BindableProperty.Create("SelectedValue", typeof(object), typeof(Element), null,
		defaultBindingMode: BindingMode.TwoWay,
		propertyChanged: (b, o, n) =>
		{
			GetGroupController(b).SelectedValue = n;
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
	/// The backing store for the <see cref="SelectedValues" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValuesProperty =
		BindableProperty.Create("SelectedValues", typeof(IList<object>), typeof(Element), null,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (b, o, n) =>
			{
				GetGroupController(b).SelectedValues = (IList<object>)n;
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
	/// The backing store for the <see cref="SelectedValueChangedCommand" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectedValueChangedCommandProperty =
		BindableProperty.Create("SelectedValueChangedCommand", typeof(ICommand), typeof(Element), null,
			propertyChanged: (b, o, n) =>
			{
				GetGroupController(b).SelectedValueChangedCommand = (ICommand)n;
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
	/// The backing store for the <see cref="SelectionType" /> bindable property.
	/// </summary>
	public static readonly BindableProperty SelectionTypeProperty =
		BindableProperty.Create("SelectionType", typeof(SelectionType), typeof(Element), SelectionType.Single,
			propertyChanged: (b, o, n) =>
			{
				GetGroupController(b).SelectionType = (SelectionType)n;
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
}