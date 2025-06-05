namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Event argument raised when when the selection of one of the segmented buttons changes.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class SegmentedButtonSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the currently collection of selected items when <see cref="AllowMultiSelect"/> is <see langword="True"/>.
        /// </summary>
        public IEnumerable<MaterialSegmentedButtonItem>? SelectedItems { get; private set; }

        /// <summary>
        /// Gets the currently selected item when <see cref="AllowMultiSelect"/> is <see langword="False"/>.
        /// </summary>
        public MaterialSegmentedButtonItem? SelectedItem { get; private set; }

        public SegmentedButtonSelectedEventArgs(IEnumerable<MaterialSegmentedButtonItem> selectedItems)
        {
            SelectedItems = selectedItems;
        }

        public SegmentedButtonSelectedEventArgs(MaterialSegmentedButtonItem selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}