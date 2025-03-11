using System.Collections;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// We reuse some code from MAUI official repository: <see href="https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs">See here.</see>
/// </summary>
public static class MaterialRadioButtonGroup
{
    #region Attributes

    internal const string GroupSelectionChangedMessage = "MaterialRadioButtonGroupSelectionChanged";
    internal const string GroupValueChangedMessage = "MaterialRadioButtonGroupValueChanged";

    #endregion Attributes

    #region Properties

    /// <summary>
    /// The backing store for the <see cref="MaterialRadioButtonGroupController" /> bindable property.
    /// </summary>
    static readonly BindableProperty MaterialRadioButtonGroupControllerProperty =
        BindableProperty.CreateAttached("MaterialRadioButtonGroupController", typeof(MaterialRadioButtonGroupController), typeof(Microsoft.Maui.ILayout), default(MaterialRadioButtonGroupController),
        defaultValueCreator: (b) => {

            return new MaterialRadioButtonGroupController((Microsoft.Maui.ILayout)b);

        },
        propertyChanged: (b, o, n) => OnControllerChanged(b, (MaterialRadioButtonGroupController)o, (MaterialRadioButtonGroupController)n));

    /// <summary>
    /// Returns the bindableObject's <see cref="MaterialRadioButtonGroupController" />
    /// </summary>
    /// <param name="bindableObject"></param>
    /// <returns></returns>
    static MaterialRadioButtonGroupController GetMaterialRadioButtonGroupController(BindableObject bindableObject)
    {
        return (MaterialRadioButtonGroupController)bindableObject.GetValue(MaterialRadioButtonGroupControllerProperty);
    }

    /// <summary>
    /// The backing store for the <see cref="MaterialGroupName" /> bindable property.
    /// </summary>
    public static readonly BindableProperty GroupNameProperty =
        BindableProperty.CreateAttached("MaterialGroupName", typeof(string), typeof(Microsoft.Maui.ILayout), null,
        propertyChanged: (b, _, n) => { GetMaterialRadioButtonGroupController(b).GroupName = (string)n; });

    /// <summary>
    /// Returns the bindableObject's group name
    /// </summary>
    public static string GetGroupName(BindableObject bindableObject)
    {
        return (string)bindableObject.GetValue(GroupNameProperty);
    }

    /// <summary>
    /// Sets the bindableObject's group name
    /// </summary>
    public static void SetGroupName(BindableObject bindableObject, string groupName)
    {
        bindableObject.SetValue(GroupNameProperty, groupName);
    }

    /// <summary>
    /// The backing store for the <see cref="MaterialSelectedValue" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedValueProperty =
        BindableProperty.Create("MaterialSelectedValue", typeof(object), typeof(Microsoft.Maui.ILayout),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (b, _, n) => { GetMaterialRadioButtonGroupController(b).SelectedValue = n; });

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
    public static void SetSelectedValue(BindableObject bindableObject, object selectedValue)
    {
        bindableObject.SetValue(SelectedValueProperty, selectedValue);
    }

    #endregion Properties

    internal static void UpdateRadioButtonGroup(MaterialRadioButton radioButton)
    {
        string groupName = radioButton.GroupName;

        Element scope = string.IsNullOrEmpty(groupName)
            ? GroupByParent(radioButton)
            : GetVisualRoot(radioButton);

#pragma warning disable CS0618 // TODO: Remove when we internalize/replace MessagingCenter
        MessagingCenter.Send(radioButton, GroupSelectionChangedMessage,
            new MaterialRadioButtonGroupSelectionChanged(scope));
#pragma warning restore CS0618 // Type or member is obsolete
    }

    internal static Element GroupByParent(MaterialRadioButton radioButton)
    {
        Element parent = radioButton.Parent;

        if (parent != null)
        {
            // Traverse logical children
            IEnumerable children = ((IElementController)parent).LogicalChildren;
            foreach (object child in children)
            {
                if (child is MaterialRadioButton rb && rb != radioButton && string.IsNullOrEmpty(rb.GroupName) && rb.IsChecked)
                {
                    rb.SetValueFromRenderer(MaterialRadioButton.IsCheckedProperty, false);
                }
            }
        }

        return parent;
    }

    internal static Element GetVisualRoot(Element element)
    {
        Element parent = element.Parent;
        while (parent != null && !(parent is Page))
            parent = parent.Parent;
        return parent;
    }

    static void OnControllerChanged(BindableObject bindableObject, MaterialRadioButtonGroupController _,
        MaterialRadioButtonGroupController newController)
    {
        if (newController == null)
        {
            return;
        }

        newController.GroupName = GetGroupName(bindableObject);
        newController.SelectedValue = GetSelectedValue(bindableObject);
    }
}

/// <summary>
/// This class is used to store, set and get the GroupName and Selected Value. 
/// </summary>
internal abstract class MaterialRadioButtonScopeMessage
{
    public Element Scope { get; }

    protected MaterialRadioButtonScopeMessage(Element scope) => Scope = scope;
}

internal class MaterialRadioButtonGroupSelectionChanged : MaterialRadioButtonScopeMessage
{
    public MaterialRadioButtonGroupSelectionChanged(Element scope) : base(scope) { }
}

internal class MaterialRadioButtonGroupNameChanged : MaterialRadioButtonScopeMessage
{
    public string OldName { get; }

    public MaterialRadioButtonGroupNameChanged(Element scope, string oldName) : base(scope)
    {
        OldName = oldName;
    }
}

internal class MaterialRadioButtonValueChanged : MaterialRadioButtonScopeMessage
{
    public MaterialRadioButtonValueChanged(Element scope) : base(scope) { }
}

internal class MaterialRadioButtonGroupValueChanged : MaterialRadioButtonScopeMessage
{
    public object Value { get; }
    public string GroupName { get; }

    public MaterialRadioButtonGroupValueChanged(string groupName, Element scope, object value) : base(scope)
    {
        GroupName = groupName;
        Value = value;
    }
}
