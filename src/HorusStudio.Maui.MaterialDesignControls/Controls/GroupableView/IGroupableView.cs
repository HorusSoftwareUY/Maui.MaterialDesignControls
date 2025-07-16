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
    /// <remarks>This event should be raised when the <see cref="Value" />, and/or <see cref="IsSelected" /> properties change.</remarks>
    event EventHandler<GroupableViewPropertyChangedEventArgs>? GroupableViewPropertyChanged;
}