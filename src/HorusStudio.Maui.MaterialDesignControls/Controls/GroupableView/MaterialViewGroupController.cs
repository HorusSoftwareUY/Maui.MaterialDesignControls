using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using ILayout = Microsoft.Maui.ILayout;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroupController.cs">See here.</see>
/// This class acts like a controller to set group name and selected value.
/// This help to use as attachable properties that properties.
/// </summary>
internal class MaterialViewGroupController
{
	// TODO: Change Dictionary to ConditionalWeakTable when available in .NET 10.
    private static readonly Dictionary<string, MaterialViewGroupController> GroupControllers = new();
    
	private readonly VisualElement _layout;
	private string _groupName;
	private object? _selectedValue;
	private IList<object>? _selectedValues;
	private ICommand? _selectedValueChangedCommand;
	private SelectionType _selectionType = SelectionType.Single;

	internal string GroupName { get => _groupName; set => SetGroupName(value); }
	internal object? SelectedValue { get => _selectedValue; set => SetSelectedValue(value); }
	internal IList<object>? SelectedValues { get => _selectedValues; set => SetSelectedValues(value); }
	internal ICommand? SelectedValueChangedCommand { get => _selectedValueChangedCommand; set => _selectedValueChangedCommand = value; }
	internal SelectionType SelectionType { get => _selectionType; set => _selectionType = value; }
	
	internal MaterialViewGroupController(VisualElement layout)
	{
		if (layout is null)
		{
			throw new ArgumentNullException(nameof(layout));
		}
		
		_layout = layout;
		_layout.Unloaded += Unloaded;
		_layout.ChildAdded += ChildAdded;
		_layout.ChildRemoved += ChildRemoved;
	}

	private void Unloaded(object sender, EventArgs e)
	{
		_layout.Unloaded -= Unloaded;
		_layout.ChildAdded -= ChildAdded;
		_layout.ChildRemoved -= ChildRemoved;
		
		if (GroupControllers.TryGetValue(_groupName, out var controller) && controller == this)
		{
			GroupControllers.Remove(_groupName);
		}
	}
	
	internal void Init()
	{
		var propagateGroupName = MaterialViewGroup.GetPropagateGroupName(_layout);
		if (propagateGroupName && !string.IsNullOrEmpty(_groupName))
		{
			UpdateGroupNames(_layout, _groupName);
		}
		
		if (!GroupControllers.TryGetValue(_groupName, out _))
		{
			GroupControllers.Add(_groupName, this);
		}
	}
	
	internal static MaterialViewGroupController? GetGroupController(string? groupName)
	{
		if (groupName is not null && GroupControllers.TryGetValue(groupName, out var controller))
		{
			return controller;
		}
		
		return null;
	}
	
	internal void HandleMaterialViewGroupSelectionChanged(IGroupableView groupableView)
	{
		if (MaterialViewGroup.GetGroupName((Element)groupableView) != _groupName)
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

	private void ChildAdded(object sender, ElementEventArgs e)
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

	private void ChildRemoved(object sender, ElementEventArgs e)
	{
		if (e.Element is IGroupableView groupableView)
		{
			var groupName = MaterialViewGroup.GetGroupName(e.Element);
			if (GroupControllers.TryGetValue(groupName, out _))
			{
				GroupControllers.Remove(groupName);
			}
		}
		else
		{
			foreach (var element in e.Element.GetDescendants())
			{
				if (element is IGroupableView)
				{
					var childGroupName = MaterialViewGroup.GetGroupName(element);
					if (GroupControllers.TryGetValue(childGroupName, out _))
					{
						GroupControllers.Remove(childGroupName);
					}
				}
			}
		}
	}

	private void AddGroupableView(IGroupableView groupableView)
	{
		var propagateGroupName = MaterialViewGroup.GetPropagateGroupName(_layout);
		if (propagateGroupName)
		{
			UpdateGroupName(groupableView, _groupName);
		}
		
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
		
		groupableView.GroupableViewPropertyChanged -= HandleMaterialViewGroupSelectionChanged;
		var groupableViewGroupName = MaterialViewGroup.GetGroupName((Element)groupableView);
		if (!string.IsNullOrEmpty(groupableViewGroupName))
		{
			groupableView.GroupableViewPropertyChanged += HandleMaterialViewGroupSelectionChanged;
		}
	}

	private void UpdateGroupName(IGroupableView groupableView, string name, string? oldName = null)
	{
		var currentName = MaterialViewGroup.GetGroupName((Element)groupableView);

		if (string.IsNullOrEmpty(currentName) || currentName == oldName)
		{
			MaterialViewGroup.SetGroupName((Element)groupableView, name);
		}
	}

	private void UpdateGroupNames(Element element, string name, string? oldName = null)
	{
		foreach (Element descendant in element.GetDescendants())
		{
			if (descendant is IGroupableView groupableView)
			{
				UpdateGroupName(groupableView, name, oldName);
			}
		}
	}

	private void SetSelectedValue(object? groupableViewValue)
	{
		_selectedValue = groupableViewValue;

		if (_selectedValue != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				var groupableViewGroupName = MaterialViewGroup.GetGroupName(child);
				if (child is IGroupableView groupableView 
				    && groupableViewGroupName == _groupName 
				    && groupableView.Value is not null 
				    && groupableView.Value.Equals(_selectedValue))
				{
					groupableView.IsSelected = true;
					
					if (!string.IsNullOrEmpty(groupableViewGroupName) && _selectionType == SelectionType.Single)
					{
						UncheckOtherMaterialGroupableViewInScope(groupableView);
					}
					
					HandleSelectedValueChangedCommand(groupableView);
				}
			}
		}
	}
	
	private void SetSelectedValues(IList<object>? groupableViewValues)
	{
		_selectedValues = groupableViewValues;

		if (_selectedValues != null)
		{
			foreach (var child in _layout.GetDescendants())
			{
				if (child is IGroupableView groupableView 
				    && MaterialViewGroup.GetGroupName(child) == _groupName 
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
	
	private static void UncheckOtherMaterialGroupableViewInScope<T>(T groupableView) 
        where T : IGroupableView
    {
        if (groupableView is not Element element)
        {
            return;
        }
        
        var groupableViewGroupName = MaterialViewGroup.GetGroupName(element);
        if (!string.IsNullOrEmpty(groupableViewGroupName))
        {
            var root = element.GetVisualRoot() ?? element.Parent;
            if (root is not IElementController rootController)
            {
                return;
            }

            foreach (var child in rootController.LogicalChildren)
            {
                UncheckMatchingDescendants(child, groupableViewGroupName, groupableView);
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
	            var groupableViewChildGroupName = MaterialViewGroup.GetGroupName(child);
                if (child is T groupableViewChild && string.IsNullOrEmpty(groupableViewChildGroupName))
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
	    var groupableViewChildGroupName = MaterialViewGroup.GetGroupName(element);
        if (element is T groupableViewChild && groupableViewChildGroupName == groupName)
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
 
	private void HandleSelectedValueChangedCommand(IGroupableView groupableView)
	{
		var commandParameter = groupableView.IsSelected ? groupableView.Value : null;
		if (SelectedValueChangedCommand != null && SelectedValueChangedCommand.CanExecute(commandParameter))
		{
			SelectedValueChangedCommand.Execute(commandParameter);
		}
	}
	
	private void SetGroupName(string groupName)
	{
		var oldName = _groupName;
		_groupName = groupName;
		
		var propagateGroupName = MaterialViewGroup.GetPropagateGroupName(_layout);
		if (propagateGroupName)
		{
			UpdateGroupNames(_layout, _groupName, oldName);
		}
	}
	
	private void HandleMaterialViewGroupSelectionChanged(object? sender, GroupableViewPropertyChangedEventArgs e)
	{
		if (sender is IGroupableView groupableView)
		{
			if (e.PropertyName == nameof(IGroupableView.Value))
			{
				HandleMaterialViewValueChanged(groupableView);
			}
			else if (e.PropertyName == nameof(IGroupableView.IsSelected))
			{
				HandleMaterialViewIsSelectedChanged(groupableView);
			}
		}
	}
	
	private void HandleMaterialViewValueChanged(IGroupableView groupableView)
	{
		var groupName = MaterialViewGroup.GetGroupName((Element)groupableView);
		if (!groupableView.IsSelected 
		    || string.IsNullOrEmpty(groupName)
		    || groupName != _groupName)
		{
			return;
		}
		
		_layout.SetValue(MaterialViewGroup.SelectedValueProperty, groupableView.Value);
	}
	
	private void HandleMaterialViewIsSelectedChanged(IGroupableView groupableView)
	{
		if (SelectionType == SelectionType.Multiple || !groupableView.IsSelected)
		{
			groupableView.IsSelected = !groupableView.IsSelected;
			HandleMaterialViewGroupSelectionChanged(groupableView);
		}
	}
}
