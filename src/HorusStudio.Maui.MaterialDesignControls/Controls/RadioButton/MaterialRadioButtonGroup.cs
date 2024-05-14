using System.Collections;

namespace HorusStudio.Maui.MaterialDesignControls;

//We reuse some thigs from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs
public static class MaterialRadioButtonGroup
{
    internal const string GroupSelectionChangedMessage = "MaterialRadioButtonGroupSelectionChanged";
    internal const string GroupValueChangedMessage = "MaterialRadioButtonGroupValueChanged";

    static readonly BindableProperty RadioButtonGroupControllerProperty =
        BindableProperty.CreateAttached("MaterialRadioButtonGroupController", typeof(MaterialRadioButtonGroupController), typeof(Microsoft.Maui.ILayout), default(MaterialRadioButtonGroupController),
        defaultValueCreator: (b) => {

            return new MaterialRadioButtonGroupController(b as Microsoft.Maui.ILayout);
            
            },
        propertyChanged: (b, o, n) => OnControllerChanged(b, (MaterialRadioButtonGroupController)o, (MaterialRadioButtonGroupController)n));

    static MaterialRadioButtonGroupController GetRadioButtonGroupController(BindableObject b)
    {
        return (MaterialRadioButtonGroupController)b.GetValue(RadioButtonGroupControllerProperty);
    }

    public static readonly BindableProperty GroupNameProperty =
        BindableProperty.CreateAttached("MaterialGroupName", typeof(string), typeof(Microsoft.Maui.ILayout), null,
        propertyChanged: (b, o, n) => { GetRadioButtonGroupController(b).GroupName = (string)n; });

    public static string GetGroupName(BindableObject b)
    {
        return (string)b.GetValue(GroupNameProperty);
    }

    public static void SetGroupName(BindableObject bindable, string groupName)
    {
        bindable.SetValue(GroupNameProperty, groupName);
    }

    /// <summary>Bindable property for attached property <c>SelectedValue</c>.</summary>
    public static readonly BindableProperty SelectedValueProperty =
        BindableProperty.Create("MaterialSelectedValue", typeof(object), typeof(Microsoft.Maui.ILayout), null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (b, o, n) => { GetRadioButtonGroupController(b).SelectedValue = n; });

    public static object GetSelectedValue(BindableObject bindableObject)
    {
        return bindableObject.GetValue(SelectedValueProperty);
    }

    public static void SetSelectedValue(BindableObject bindable, object selectedValue)
    {
        bindable.SetValue(SelectedValueProperty, selectedValue);
    }

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
            IEnumerator itor = children.GetEnumerator();
            while (itor.MoveNext())
            {
                var rb = itor.Current as MaterialRadioButton;
                if (rb != null && rb != radioButton && string.IsNullOrEmpty(rb.GroupName) && (rb.IsChecked == true))
                    rb.SetValueFromRenderer(MaterialRadioButton.IsCheckedProperty, false);
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


    static void OnControllerChanged(BindableObject bindableObject, MaterialRadioButtonGroupController oldController,
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
