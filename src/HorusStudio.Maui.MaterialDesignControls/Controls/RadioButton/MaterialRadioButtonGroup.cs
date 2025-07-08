using System.Collections;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs">See here.</see>
/// </summary>
public static class MaterialRadioButtonGroup
{
    static readonly BindableProperty GroupControllerProperty =
		BindableProperty.CreateAttached("GroupController", typeof(MaterialViewGroupController), typeof(Element), default(MaterialViewGroupController),
		defaultValueCreator: (b) => new MaterialViewGroupController(b as Element, MaterialRadioButton.IsCheckedProperty, SelectedValueProperty),
		propertyChanged: (b, o, n) => MaterialViewGroup.OnControllerChanged(b, GetGroupName, GetSelectedValue, (MaterialViewGroupController)n));
	
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

	internal static void UpdateMaterialRadioButtonGroup(MaterialRadioButton radioButton)
	{
		MaterialViewGroup.UncheckOtherMaterialGroupableViewInScope(radioButton, MaterialRadioButton.IsCheckedProperty);
		radioButton.OnGroupSelectionChanged(radioButton);
	}
}