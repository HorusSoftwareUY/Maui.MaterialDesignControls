using System.Collections;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs">See here.</see>
/// </summary>
public static class MaterialRadioButtonGroup
{
    static readonly BindableProperty MaterialRadioButtonGroupControllerProperty =
		BindableProperty.CreateAttached("MaterialRadioButtonGroupController", typeof(MaterialRadioButtonGroupController), typeof(Element), default(MaterialRadioButtonGroupController),
		defaultValueCreator: (b) => new MaterialRadioButtonGroupController(b as Element),
		propertyChanged: (b, o, n) => OnControllerChanged(b, (MaterialRadioButtonGroupController)o, (MaterialRadioButtonGroupController)n));
	
	static MaterialRadioButtonGroupController GetMaterialRadioButtonGroupController(BindableObject b)
	{
		return (MaterialRadioButtonGroupController)b.GetValue(MaterialRadioButtonGroupControllerProperty);
	}

	/// <summary>
	/// The backing store for the <see cref="GroupName" /> bindable property.
	/// </summary>
	public static readonly BindableProperty GroupNameProperty =
		BindableProperty.Create("GroupName", typeof(string), typeof(Element), null,
		propertyChanged: (b, o, n) => { GetMaterialRadioButtonGroupController(b).GroupName = (string)n; });

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
		propertyChanged: (b, o, n) => { GetMaterialRadioButtonGroupController(b).SelectedValue = n; });

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

	internal static void UpdateMaterialRadioButtonGroup(MaterialRadioButton radioButton)
	{
		UncheckOtherMaterialRadioButtonsInScope(radioButton);
		radioButton.OnGroupSelectionChanged(radioButton);
	}

	internal static void UncheckOtherMaterialRadioButtonsInScope(MaterialRadioButton radioButton)
	{
		if (!string.IsNullOrEmpty(radioButton.GroupName))
		{
			var root = GetVisualRoot(radioButton) ?? radioButton.Parent;
			if (root is not IElementController rootController)
			{
				return;
			}

			foreach (var child in rootController.LogicalChildren)
			{
				UncheckMatchingDescendants(child, radioButton.GroupName, radioButton);
			}
		}
		else
		{
			if (radioButton.Parent is not IElementController parentController)
			{
				return;
			}

			foreach (var child in parentController.LogicalChildren)
			{
				if (child is MaterialRadioButton rb && string.IsNullOrEmpty(rb.GroupName))
				{
					UncheckMaterialRadioButtonIfChecked(rb, radioButton);
				}
			}
		}
	}

	static void UncheckMaterialRadioButtonIfChecked(MaterialRadioButton child, MaterialRadioButton radioButton)
	{
		if (child != radioButton && child.IsChecked)
		{
			child.SetValue(MaterialRadioButton.IsCheckedProperty, false);
		}
	}

	static void UncheckMatchingDescendants(Element element, string groupName, MaterialRadioButton radioButton)
	{
		if (element is MaterialRadioButton rb && rb.GroupName == groupName)
		{
			UncheckMaterialRadioButtonIfChecked(rb, radioButton);
		}

		if (element is IElementController controller)
		{
			foreach (var child in controller.LogicalChildren)
			{
				UncheckMatchingDescendants(child, groupName, radioButton);
			}
		}
	}

	static void OnControllerChanged(BindableObject bindableObject, MaterialRadioButtonGroupController oldController,
		MaterialRadioButtonGroupController? newController)
	{
		if (newController == null)
		{
			return;
		}

		newController.GroupName = GetGroupName(bindableObject);
		newController.SelectedValue = GetSelectedValue(bindableObject);
	}

	internal static Element? GetVisualRoot(Element element)
	{
		Element parent = element.Parent;
		while (parent != null && !(parent is Page))
		{
			parent = parent.Parent;
		}
		return parent;
	}
}