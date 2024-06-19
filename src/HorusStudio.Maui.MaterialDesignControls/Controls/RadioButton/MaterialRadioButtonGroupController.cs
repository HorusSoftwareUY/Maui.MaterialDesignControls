namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButtonGroupController.cs">See here.</see>
/// This class acts like a controller to set group name and selected value.
/// This help to use as attachable properties that properties.
/// </summary>
internal class MaterialRadioButtonGroupController
{
    #region Attributes

    readonly Element _layout;
    string _groupName;
    private object _selectedValue;
    public string GroupName { get => _groupName; set => SetGroupName(value); }
    public object SelectedValue { get => _selectedValue; set => SetSelectedValue(value); }

    #endregion Attributes

    #region Constructor
    public MaterialRadioButtonGroupController(Microsoft.Maui.ILayout layout)
    {
        if (layout is null)
        {
            throw new ArgumentNullException(nameof(layout));
        }

        _layout = (Element)layout;
        _layout.ChildAdded += ChildAdded;

        if (!string.IsNullOrEmpty(_groupName))
        {
            UpdateGroupNames(_layout, _groupName);
        }

#pragma warning disable CS0618 // TODO: Remove when we internalize/replace MessagingCenter
        MessagingCenter.Subscribe<MaterialRadioButton, MaterialRadioButtonGroupSelectionChanged>(this,
            MaterialRadioButtonGroup.GroupSelectionChangedMessage, HandleRadioButtonGroupSelectionChanged);
        MessagingCenter.Subscribe<MaterialRadioButton, MaterialRadioButtonGroupNameChanged>(this, MaterialRadioButton.GroupNameChangedMessage,
            HandleRadioButtonGroupNameChanged);
        MessagingCenter.Subscribe<MaterialRadioButton, MaterialRadioButtonValueChanged>(this, MaterialRadioButton.ValueChangedMessage,
            HandleRadioButtonValueChanged);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    #endregion Constructor

    #region Methods

    bool MatchesScope(MaterialRadioButtonScopeMessage message)
    {
        return MaterialRadioButtonGroup.GetVisualRoot(_layout) == message.Scope;
    }

    void HandleRadioButtonGroupSelectionChanged(MaterialRadioButton selected, MaterialRadioButtonGroupSelectionChanged args)
    {
        if (selected.GroupName != _groupName || !MatchesScope(args))
        {
            return;
        }

        _layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, selected.Value);
    }

    void HandleRadioButtonGroupNameChanged(MaterialRadioButton radioButton, MaterialRadioButtonGroupNameChanged args)
    {
        if (args.OldName != _groupName || !MatchesScope(args))
        {
            return;
        }

        _layout.ClearValue(MaterialRadioButtonGroup.SelectedValueProperty);
    }

    void HandleRadioButtonValueChanged(MaterialRadioButton radioButton, MaterialRadioButtonValueChanged args)
    {
        if (radioButton.GroupName != _groupName || !MatchesScope(args) || radioButton.Value is null || this.SelectedValue is null)
        {
            return;
        }

        if (object.Equals(radioButton.Value, this.SelectedValue))
        {
            radioButton.SetValue(MaterialRadioButton.IsCheckedProperty, true);
            _layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, radioButton.Value);
        }
    }

    void ChildAdded(object sender, ElementEventArgs e)
    {
        if (string.IsNullOrEmpty(_groupName))
        {
            return;
        }

        if (e.Element is MaterialRadioButton radioButton)
        {
            AddRadioButton(radioButton);
        }
        else
        {
            foreach (var element in e.Element.GetVisualTreeDescendants())
            {
                if (element is MaterialRadioButton radioButton1)
                {
                    AddRadioButton(radioButton1);
                }
            }
        }
    }

    void AddRadioButton(MaterialRadioButton radioButton)
    {
        UpdateGroupName(radioButton, _groupName);

        if (radioButton.IsChecked && radioButton.Value is not null)
        {
            _layout.SetValue(MaterialRadioButtonGroup.SelectedValueProperty, radioButton.Value);
        }

        if (radioButton.Value != null && this.SelectedValue != null && object.Equals(radioButton.Value, this.SelectedValue))
        {
            radioButton.SetValue(MaterialRadioButton.IsCheckedProperty, true);
        }
    }

    void UpdateGroupName(Element element, string name, string oldName = null)
    {
        if (!(element is MaterialRadioButton radioButton))
        {
            return;
        }

        var currentName = radioButton.GroupName;

        if (string.IsNullOrEmpty(currentName) || currentName == oldName || oldName is null)
        {
            radioButton.GroupName = name;
        }
    }

    void UpdateGroupNames(Element element, string name, string oldName = null)
    {
        foreach (Element descendant in element.GetVisualTreeDescendants())
        {
            UpdateGroupName(descendant, name, oldName);
        }
    }

    void SetSelectedValue(object radioButtonValue)
    {
        _selectedValue = radioButtonValue;

        if (radioButtonValue != null)
        {

#pragma warning disable CS0618 // TODO: Remove when we internalize/replace MessagingCenter
            MessagingCenter.Send<Element, MaterialRadioButtonGroupValueChanged>(_layout, MaterialRadioButtonGroup.GroupValueChangedMessage,
                new MaterialRadioButtonGroupValueChanged(_groupName, MaterialRadioButtonGroup.GetVisualRoot(_layout), radioButtonValue));
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }

    void SetGroupName(string groupName)
    {
        var oldName = _groupName;
        _groupName = groupName;
        UpdateGroupNames(_layout, _groupName, oldName);
    }

    #endregion Methods
}
