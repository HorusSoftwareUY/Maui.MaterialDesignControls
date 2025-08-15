namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Event argument raised when the selection of one of the segmented buttons changes.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class SegmentedButtonSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the selected items collection when <see cref="MaterialSegmentedButton.AllowMultiSelect">AllowMultiSelect</see>=<see langword="true">True</see>.
        /// </summary>
        public IEnumerable<MaterialSegmentedButtonItem>? SelectedItems { get; private set; }

        /// <summary>
        /// Gets the selected item when <see cref="MaterialSegmentedButton.AllowMultiSelect">AllowMultiSelect</see>=<see langword="false">False</see>.
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