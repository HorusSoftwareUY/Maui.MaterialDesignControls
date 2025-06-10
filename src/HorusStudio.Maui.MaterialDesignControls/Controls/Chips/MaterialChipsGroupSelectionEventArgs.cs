using System.Collections;

namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Event argument raised when when the selection changed on the MaterialChipsGroup control.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class MaterialChipsGroupSelectionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the selected item.
        /// </summary>
        public object? SelectedItem { get; }

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        public IList? SelectedItems { get; }

        public MaterialChipsGroupSelectionEventArgs(object? selectedItem)
        {
            SelectedItem = selectedItem;
        }

        public MaterialChipsGroupSelectionEventArgs(IList? selectedItems)
        {
            SelectedItems = selectedItems;
        }
    }
}