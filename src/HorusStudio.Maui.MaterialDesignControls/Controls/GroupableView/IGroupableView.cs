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
    /// Gets or sets if the view is selected or not.
    /// </summary>
    bool IsSelected { get; set; }

    // TODO: REVIEW IF WE CAN AVOID HAVE THIS METHOD IN THE INTERFACE
    void OnGroupSelectionChanged(IGroupableView groupableView);
}