using System.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// The IGroupableView interface serves as a base for groupable views.
/// </summary>
public interface IGroupableView
{
    /// <summary>
    /// Gets the selected value of the view.
    /// </summary>
    object? Value { get; }
    
    /// <summary>
    /// Gets or sets if the view is selected or not.
    /// </summary>
    bool IsSelected { get; set; }
    
    /// <summary>
    /// Event used to notify the group that one of the properties of the groupable view has changed its value.
    /// </summary>
    /// <remarks>This event should be raised when any <see cref="IGroupableView">IGroupableView</see> property changes.</remarks>
    event EventHandler<GroupableViewPropertyChangedEventArgs>? GroupableViewPropertyChanged;
}