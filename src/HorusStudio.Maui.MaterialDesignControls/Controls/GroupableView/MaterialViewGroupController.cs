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
	private IList<object>? _selectedValues;
	private ICommand? _selectedValueChangedCommand;
	private SelectionType _selectionType = SelectionType.Single;

	public string GroupName { get => _groupName; set => SetGroupName(value); }
	public object? SelectedValue { get => _selectedValue; set => SetSelectedValue(value); }
	public IList<object>? SelectedValues { get => _selectedValues; set => SetSelectedValues(value); }
	public ICommand? SelectedValueChangedCommand { get => _selectedValueChangedCommand; set => _selectedValueChangedCommand = value; }
	public SelectionType SelectionType { get => _selectionType; set => SetSelectionType(value); }

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
		
		if (_selectionType == SelectionType.Single)
		{
			if (groupableView.IsSelected)
			{
				_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
			}
			else
			{
				_layout.SetValue(MaterialViewGroup.SelectedValueProperty, null);
				HandleSelectedValueChangedCommand(groupableView);
			}
		}
		else
		{
			if (_selectedValues is null)
			{
				_selectedValues = new List<object>();
			}
			
			if (groupableView.IsSelected)
			{
				if (groupableView.Value is not null && !_selectedValues.Contains(groupableView.Value))
				{
					_selectedValues.Add(groupableView.Value);
				}
			}
			else
			{
				if (groupableView.Value is not null && _selectedValues.Contains(groupableView.Value))
				{
					_selectedValues.Remove(groupableView.Value);
				}
			}
			
			_layout.SetValue(MaterialViewGroup.SelectedValuesProperty, _selectedValues);
			
			HandleSelectedValueChangedCommand(groupableView);
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
		_layout.ClearValue(MaterialViewGroup.SelectedValuesProperty);
	}

	void AddGroupableView(IGroupableView groupableView)
	{
		UpdateGroupName(groupableView, _groupName);

		if (groupableView.IsSelected)
		{
			if (_selectionType == SelectionType.Single)
			{
				_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
			}
			else
			{
				if (_selectedValues is null)
				{
					_selectedValues = new List<object>();
				}

				if (groupableView.Value is not null && !_selectedValues.Contains(groupableView.Value))
				{
					_selectedValues.Add(groupableView.Value);
				}

				_layout.SetValue(MaterialViewGroup.SelectedValuesProperty, _selectedValues);
			}
		}

		if (object.Equals(groupableView.Value, this.SelectedValue))
		{
			groupableView.IsSelected = true;
		}
		
		groupableView.SelectionType = _selectionType;

		if (groupableView is MaterialRadioButton materialRadioButton)
		{
			ApplyWorkaroundForInternalRadioButtonGroup(materialRadioButton);
		}
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

		if (_selectedValue != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				if (child is IGroupableView groupableView 
				    && groupableView.GroupName == _groupName 
				    && groupableView.Value is not null 
				    && groupableView.Value.Equals(_selectedValue))
				{
					groupableView.IsSelected = true;
					
					if (!string.IsNullOrEmpty(groupableView.GroupName) && _selectionType == SelectionType.Single)
					{
						MaterialViewGroup.UncheckOtherMaterialGroupableViewInScope(groupableView);
					}
					
					HandleSelectedValueChangedCommand(groupableView);
				}
			}
		}
	}
	
	void SetSelectedValues(IList<object>? groupableViewValues)
	{
		_selectedValues = groupableViewValues;

		if (_selectedValues != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				if (child is IGroupableView groupableView 
				    && groupableView.GroupName == _groupName 
				    && groupableView.Value is not null 
				    && _selectedValues is not null
				    && _selectedValues.Contains(groupableView.Value))
				{
					groupableView.IsSelected = true;
					HandleSelectedValueChangedCommand(groupableView);
				}
			}
		}
	}

	void HandleSelectedValueChangedCommand(IGroupableView groupableView)
	{
		var commandParameter = groupableView.IsSelected ? groupableView.Value : null;
		if (SelectedValueChangedCommand != null && SelectedValueChangedCommand.CanExecute(commandParameter))
		{
			SelectedValueChangedCommand.Execute(commandParameter);
		}
	}
	
	void SetGroupName(string groupName)
	{
		var oldName = _groupName;
		_groupName = groupName;
		UpdateGroupNames(_layout, _groupName, oldName);
	}
	
	void SetSelectionType(SelectionType selectionType)
	{
		_selectionType = selectionType;
		
		foreach (var child in _layout.GetDescendants())
		{
			if (child is IGroupableView groupableView && groupableView.GroupName == _groupName)
			{
				groupableView.SelectionType = _selectionType;
				
				if (groupableView is MaterialRadioButton materialRadioButton)
				{
					ApplyWorkaroundForInternalRadioButtonGroup(materialRadioButton);
				}
			}
		}
	}

	void ApplyWorkaroundForInternalRadioButtonGroup(MaterialRadioButton materialRadioButton)
	{
		if (_selectionType == SelectionType.Multiple)
		{
			materialRadioButton.InternalRadioButton.GroupName = Guid.NewGuid().ToString();
		}
		else
		{
			materialRadioButton.InternalRadioButton.GroupName = null;
		}
	}
}
