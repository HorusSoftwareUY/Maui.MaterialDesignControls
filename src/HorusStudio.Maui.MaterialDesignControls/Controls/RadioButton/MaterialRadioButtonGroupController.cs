using Microsoft.Maui.Controls;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroupController.cs">See here.</see>
/// This class acts like a controller to set group name and selected value.
/// This help to use as attachable properties that properties.
/// </summary>
internal class MaterialRadioButtonGroupController
{
    static readonly Dictionary<MaterialRadioButton, MaterialRadioButtonGroupController> groupControllers = new();
	readonly Element _layout;
	string _groupName;
	private object? _selectedValue;

	public string GroupName { get => _groupName; set => SetGroupName(value); }
	public object? SelectedValue { get => _selectedValue; set => SetSelectedValue(value); }

	public MaterialRadioButtonGroupController(Element layout)
	{
		if (layout is null)
		{
			throw new ArgumentNullException(nameof(layout));
		}

		_layout = layout;
		_layout.ChildAdded += ChildAdded;
		_layout.ChildRemoved += ChildRemoved;

		if (!string.IsNullOrEmpty(_groupName))
		{
			UpdateGroupNames(_layout, _groupName);
		}
	}

	internal static MaterialRadioButtonGroupController? GetGroupController(MaterialRadioButton? radioButton)
	{
		if (radioButton is not null && groupControllers.TryGetValue(radioButton, out var controller))
		{
			return controller;
		}
		
		return null;
	}

	internal void HandleMaterialRadioButtonGroupSelectionChanged(MaterialRadioButton radioButton)
	{
		if (radioButton.GroupName != _groupName)
		{
			return;
		}

		_layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, radioButton.Value);
	}

	void ChildAdded(object sender, ElementEventArgs e)
	{
		if (string.IsNullOrEmpty(_groupName))
		{
			return;
		}

		if (e.Element is MaterialRadioButton radioButton)
		{
			AddMaterialRadioButton(radioButton);
		}
		else
		{
			foreach (var element in e.Element.GetDescendants())
			{
				if (element is MaterialRadioButton childMaterialRadioButton)
				{
					AddMaterialRadioButton(childMaterialRadioButton);
				}
			}
		}
	}

	void ChildRemoved(object sender, ElementEventArgs e)
	{
		if (e.Element is MaterialRadioButton radioButton)
		{
			if (groupControllers.TryGetValue(radioButton, out _))
			{
				groupControllers.Remove(radioButton);
			}
		}
		else
		{
			foreach (var element in e.Element.GetDescendants())
			{
				if (element is MaterialRadioButton radioButton1)
				{
					if (groupControllers.TryGetValue(radioButton1, out _))
					{
						groupControllers.Remove(radioButton1);
					}
				}
			}
		}
	}

	internal void HandleMaterialRadioButtonValueChanged(MaterialRadioButton radioButton)
	{
		if (radioButton?.GroupName != _groupName)
		{
			return;
		}

		_layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, radioButton.Value);
	}

	internal void HandleMaterialRadioButtonGroupNameChanged(string oldGroupName)
	{
		if (oldGroupName != _groupName)
		{
			return;
		}

		_layout.ClearValue(MaterialRadioButtonGroup.SelectedValueProperty);
	}

	void AddMaterialRadioButton(MaterialRadioButton radioButton)
	{
		UpdateGroupName(radioButton, _groupName);

		if (radioButton.IsChecked)
		{
			_layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, radioButton.Value);
		}

		if (object.Equals(radioButton.Value, this.SelectedValue))
		{
			radioButton.SetValue(MaterialRadioButton.IsCheckedProperty, true);
		}
	}

	void UpdateGroupName(Element element, string name, string? oldName = null)
	{
		if (!(element is MaterialRadioButton radioButton))
		{
			return;
		}

		var currentName = radioButton.GroupName;

		if (string.IsNullOrEmpty(currentName) || currentName == oldName)
		{
			radioButton.GroupName = name;
		}

		if (!groupControllers.TryGetValue(radioButton, out _))
		{
			groupControllers.Add(radioButton, this);
		}
	}

	void UpdateGroupNames(Element element, string name, string? oldName = null)
	{
		foreach (Element descendant in element.GetDescendants())
		{
			UpdateGroupName(descendant, name, oldName);
		}
	}

	void SetSelectedValue(object? radioButtonValue)
	{
		_selectedValue = radioButtonValue;

		if (radioButtonValue != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				if (child is MaterialRadioButton radioButton && radioButton.GroupName == _groupName && radioButton.Value is not null && radioButton.Value.Equals(radioButtonValue))
				{
					radioButton.SetValue(MaterialRadioButton.IsCheckedProperty, true);
				}
			}
		}
	}
	
	void SetGroupName(string groupName)
	{
		var oldName = _groupName;
		_groupName = groupName;
		UpdateGroupNames(_layout, _groupName, oldName);
	}
}
