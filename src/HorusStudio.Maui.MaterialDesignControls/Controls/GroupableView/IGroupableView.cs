namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// The IGroupableView interface serves as a base for groupable views.
/// </summary>
public interface IGroupableView
{
    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    string GroupName { get; set; }
    
    /// <summary>
    /// Gets the selected value of the view.
    /// </summary>
    object? Value { get; }
    
    /// <summary>
    /// Gets if the view is selected or not.
    /// </summary>
    bool IsSelected { get; }
    
    /// <summary>
    /// Sets the value of the specified bindable property.
    /// </summary>
    /// <param name="property">The bindable property on which to assign a value.</param>
    /// <param name="value">The value to set.</param>
    void SetValue(BindableProperty property, object value);
}