using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroupController.cs">See here.</see>
/// This class acts like a controller to set group name and selected value.
/// This help to use as attachable properties that properties.
/// </summary>
internal class MaterialViewGroupController
{
	// TODO: Change Dictionary to ConditionalWeakTable when available in .NET 10.
    private static readonly Dictionary<IGroupableView, MaterialViewGroupController> GroupControllers = new();
    
	private readonly Element _layout;
	private string _groupName;
	private object? _selectedValue;
	private ICommand? _selectedValueChangedCommand;
	private bool _allowEmptySelection = true;

	public string GroupName { get => _groupName; set => SetGroupName(value); }
	public object? SelectedValue { get => _selectedValue; set => SetSelectedValue(value); }
	public ICommand? SelectedValueChangedCommand { get => _selectedValueChangedCommand; set => _selectedValueChangedCommand = value; }
	public bool AllowEmptySelection { get => _allowEmptySelection; set => SetAllowEmptySelection(value); }

	public MaterialViewGroupController(Element layout)
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

	internal static MaterialViewGroupController? GetGroupController(IGroupableView? groupableView)
	{
		if (groupableView is not null && GroupControllers.TryGetValue(groupableView, out var controller))
		{
			return controller;
		}
		
		return null;
	}

	internal void HandleMaterialViewGroupSelectionChanged(IGroupableView groupableView)
	{
		if (groupableView.GroupName != _groupName)
		{
			return;
		}

		if (groupableView.IsSelected)
		{
			_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
		}
		else
		{
			_layout.SetValue(MaterialViewGroup.SelectedValueProperty, null);
		}
		
		var commandParameter = groupableView.IsSelected ? groupableView.Value : null;
		if (SelectedValueChangedCommand != null && SelectedValueChangedCommand.CanExecute(commandParameter))
		{
			SelectedValueChangedCommand.Execute(commandParameter);
		}
	}

	void ChildAdded(object sender, ElementEventArgs e)
	{
		if (string.IsNullOrEmpty(_groupName))
		{
			return;
		}

		if (e.Element is IGroupableView groupableView)
		{
			AddGroupableView(groupableView);
		}
		else
		{
			foreach (var element in e.Element.GetDescendants())
			{
				if (element is IGroupableView groupableView1)
				{
					AddGroupableView(groupableView1);
				}
			}
		}
	}

	void ChildRemoved(object sender, ElementEventArgs e)
	{
		if (e.Element is IGroupableView groupableView)
		{
			if (GroupControllers.TryGetValue(groupableView, out _))
			{
				GroupControllers.Remove(groupableView);
			}
		}
		else
		{
			foreach (var element in e.Element.GetDescendants())
			{
				if (element is IGroupableView groupableView1)
				{
					if (GroupControllers.TryGetValue(groupableView1, out _))
					{
						GroupControllers.Remove(groupableView1);
					}
				}
			}
		}
	}

	internal void HandleMaterialViewValueChanged(IGroupableView groupableView)
	{
		if (groupableView?.GroupName != _groupName)
		{
			return;
		}

		_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
	}

	internal void HandleMaterialViewGroupNameChanged(string oldGroupName)
	{
		if (oldGroupName != _groupName)
		{
			return;
		}

		_layout.ClearValue(MaterialViewGroup.SelectedValueProperty);
	}

	void AddGroupableView(IGroupableView groupableView)
	{
		UpdateGroupName(groupableView, _groupName);

		if (groupableView.IsSelected)
		{
			_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
		}

		if (object.Equals(groupableView.Value, this.SelectedValue))
		{
			groupableView.IsSelected = true;
		}

		groupableView.AllowEmptySelection = _allowEmptySelection;
	}

	void UpdateGroupName(IGroupableView groupableView, string name, string? oldName = null)
	{
		var currentName = groupableView.GroupName;

		if (string.IsNullOrEmpty(currentName) || currentName == oldName)
		{
			groupableView.GroupName = name;
		}

		if (!GroupControllers.TryGetValue(groupableView, out _))
		{
			GroupControllers.Add(groupableView, this);
		}
	}

	void UpdateGroupNames(Element element, string name, string? oldName = null)
	{
		foreach (Element descendant in element.GetDescendants())
		{
			if (descendant is IGroupableView groupableView)
			{
				UpdateGroupName(groupableView, name, oldName);
			}
		}
	}

	void SetSelectedValue(object? groupableViewValue)
	{
		_selectedValue = groupableViewValue;

		if (groupableViewValue != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				if (child is IGroupableView groupableView && groupableView.GroupName == _groupName && groupableView.Value is not null && groupableView.Value.Equals(groupableViewValue))
				{
					groupableView.IsSelected = true;
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
	
	void SetAllowEmptySelection(bool allowEmptySelection)
	{
		_allowEmptySelection = allowEmptySelection;
		
		foreach (var child in _layout.GetDescendants())
		{
			if (child is IGroupableView groupableView && groupableView.GroupName == _groupName)
			{
				groupableView.AllowEmptySelection = _allowEmptySelection;
			}
		}
	}
}
